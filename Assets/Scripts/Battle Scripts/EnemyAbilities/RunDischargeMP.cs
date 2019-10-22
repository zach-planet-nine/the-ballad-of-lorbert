using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunDischargeMP : MonoBehaviour
{

    public float duration = 2.0f;
    private float pauseThreshold = 1.2f;
    private float attackThreshold = 0.7f;
    private float attackDuration = 0.5f;

    private GameObject Entity;
    private Vector3 destination;
    private Action<bool> callback;
    private int targetDamage;
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    private Transform worldTransform;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        worldTransform = GetComponent<Transform>();
    }

    public void SetTarget(GameObject entity, Vector3 startPosition, Vector3 destination, int damage)
    {
        Entity = entity;
        gameObject.transform.position = startPosition;
        this.destination = destination;
        targetDamage = damage;
    }

    public void SetTargetWithCallback(GameObject entity, Vector3 startPosition, Vector3 destination, int damage, Action<bool> callback)
    {
        SetTarget(entity, startPosition, destination, damage);
        this.callback = callback;
    }

    private void LateUpdate()
    {
        if (particles == null || particles.Length < ps.main.maxParticles)
        {
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
        }
        int numberOfParticles = ps.GetParticles(particles);
        for (int i = 0, n = particles.Length; i < n; i++)
        {

            if(duration < pauseThreshold && duration >= attackThreshold)
            {
                particles[i].velocity = Vector3.zero;
            }

            if (duration < attackThreshold && attackDuration > 0)
            {
                Vector3 worldPosition = worldTransform.TransformPoint(particles[i].position);
                float worldDeltaX = destination.x - worldPosition.x;
                float worldDeltaY = destination.y - worldPosition.y;
                /*if(destination.y < particles[i].position.y)
                {
                    deltaY = -deltaY;
                }*/
                //particles[i].velocity = new Vector3((destination.x - particles[i].position.x) / particles[i].remainingLifetime, deltaY / particles[i].remainingLifetime, 0);
                //particles[i].velocity = new Vector3(worldDeltaX / particles[i].remainingLifetime, worldDeltaY / particles[i].remainingLifetime, 0);
                particles[i].remainingLifetime = attackDuration;
                particles[i].velocity = new Vector3(worldDeltaX / attackDuration, worldDeltaY / attackDuration, 0);
            }
        }
        ps.SetParticles(particles, numberOfParticles);
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.manager.CheckIfEntityIsDead(Entity) && gameObject != null)
        {
            Destroy(gameObject);
            return;
        }
        duration -= Time.deltaTime;
        if(duration < attackThreshold)
        {
            attackDuration -= Time.deltaTime;
        }
        if (duration < 0)
        {
            Entity.GetComponent<TakeDamage>().DisplayDamage(targetDamage, destination);
            if (callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
        }
    }
}
