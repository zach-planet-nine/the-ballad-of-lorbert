using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunNeedles : MonoBehaviour
{
    private GameObject Target;
    private float duration = 0.5f;
    private float needleDuration = 0.33f;
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    private int damage;
    private Action<bool> callback;
    private Transform worldTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        worldTransform = GetComponent<Transform>();
    }

    public void SetTargetWithCallback(GameObject target, int needleDamage, Action<bool> callback)
    {
        Target = target;
        damage = needleDamage;
        this.callback = callback;
    }

    private void LateUpdate()
    {
        if (particles == null || particles.Length < ps.main.maxParticles)
        {
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
        }
        int numberOfParticles = ps.GetParticles(particles);
        for (int i = 0; i < numberOfParticles; i++)
        {
            Debug.Log(particles[i].velocity.z);
            if(particles[i].velocity.z - 0.1f > 0.2f)
            {
                Vector3 worldPosition = worldTransform.TransformPoint(particles[i].position);
                Vector3 velocity = new Vector3((Target.transform.position.x - 0.5f - worldPosition.x) / needleDuration,
                    (Target.transform.position.y + Randomness.GetValueBetween(-0.5f, 0.5f) - worldPosition.y) / needleDuration, 0.1f);
                particles[i].velocity = velocity;
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
        if(duration <= 0 && gameObject != null)
        {
            Target.GetComponent<TakeDamage>().DisplayDamage(damage, Target.transform.position);
            if(callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
        }
    }
}
