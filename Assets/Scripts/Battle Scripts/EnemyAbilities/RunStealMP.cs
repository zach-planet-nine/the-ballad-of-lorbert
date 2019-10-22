using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStealMP : MonoBehaviour
{
    public float duration = 1.0f;
    private GameObject Entity;
    private Vector3 destination;
    private Action<bool> callback;
    private int targetDamage;
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    private Transform worldTransform;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        worldTransform = GetComponent<Transform>();
    }

    public void SetTarget(GameObject entity, Vector3 destination, int damage)
    {
        Entity = entity;
        gameObject.transform.position = entity.transform.position;
        this.destination = destination;
        targetDamage = damage;
    }

    public void SetTargetWithCallback(GameObject entity, Vector3 destination, int mpDamage, Action<bool> callback)
    {
        SetTarget(entity, destination, mpDamage);
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
            //ParticleSystem.Particle particle = particles[i];
            if (particles[i].remainingLifetime > 0)
            {
                Vector3 worldPosition = worldTransform.TransformPoint(particles[i].position);
                float worldDeltaX = destination.x - worldPosition.x;
                float worldDeltaY = destination.y - worldPosition.y;
                /*if(destination.y < particles[i].position.y)
                {
                    deltaY = -deltaY;
                }*/
                //particles[i].velocity = new Vector3((destination.x - particles[i].position.x) / particles[i].remainingLifetime, deltaY / particles[i].remainingLifetime, 0);
                particles[i].velocity = new Vector3(worldDeltaX / particles[i].remainingLifetime, worldDeltaY / particles[i].remainingLifetime, 0);
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
        if (duration < 0)
        {
            Entity.GetComponent<TakeDamage>().TakeMPDamage(targetDamage);
            if (callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
        }
    }
}
