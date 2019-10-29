using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunIce : MonoBehaviour
{

    public GameObject SnowEmitter;

    private GameObject Target;
    private int damage;
    private Action<bool> callback;
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    private Transform worldTransform;
    private float damageDuration = 1.2f;
    private bool hasDamaged;
    private float duration = 1.8f;

    // Start is called before the first frame update
    void Start()
    {
        ps = SnowEmitter.GetComponent<ParticleSystem>();
        worldTransform = GetComponent<Transform>();
    }

    public void SetTargetWithCallback(GameObject target, int iceDamage, Action<bool> callback)
    {
        Target = target;
        damage = iceDamage;
        this.callback = callback;
    }

    private void LateUpdate()
    {
        if(particles == null || particles.Length < ps.main.maxParticles)
        {
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
        }
        int numberOfParticles = ps.GetParticles(particles);
        for(int i = 0; i < numberOfParticles; i++)
        {
            if(worldTransform.TransformPoint(particles[i].position).y < Target.transform.position.y - 2.4f)
            {
                particles[i].velocity = Vector3.zero;
            }
        }
        ps.SetParticles(particles, numberOfParticles);
    }

    // Update is called once per frame
    void Update()
    {
        if(BattleManager.manager.CheckIfEntityIsDead(Target) && gameObject != null)
        {
            if(callback != null)
            {
                callback(false);
            }
            Destroy(gameObject);
            return;
        }
        duration -= Time.deltaTime;
        damageDuration -= Time.deltaTime;
        if(damageDuration <= 0 && !hasDamaged)
        {
            hasDamaged = true;
            Target.GetComponent<TakeDamage>().DisplayDamage(damage, Target.transform.position);
        }
        if(duration <= 0 && gameObject != null)
        {
            if(callback != null)
            {
                callback(false);
            }
            Destroy(gameObject);
        }
    }
}
