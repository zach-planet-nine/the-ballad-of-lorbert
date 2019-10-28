using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTangle : MonoBehaviour
{

    private GameObject Target;
    private GameObject TangleEmitter;
    private float timer;
    private float duration;
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    private Vector3 tanglePosition;
    private Action<bool> callback;

    // Start is called before the first frame update
    void Start()
    {

        //ps = gameObject.GetComponentInChildren<ParticleSystem>();
        ps = gameObject.GetComponent<ParticleSystem>();
    }

    public void SetTargetWithCallback(GameObject target, float tangleDuration, Action<bool> callback)
    {
        Debug.Log("Setting target with duration: " + tangleDuration);
        Target = target;
        duration = tangleDuration;
        tanglePosition = new Vector3(target.transform.position.x - 0.5f, target.transform.position.y - 0.5f, 0);
        gameObject.transform.position = tanglePosition;
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
            //particles[i].velocity = Vector3.zero;
            int index = i / 1;
            float yDelta = (float)index * 0.5f;
            particles[i].position = new Vector3(0, yDelta, 0);
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
        timer += Time.deltaTime;
        if(timer >= duration && gameObject != null)
        {
            BattleManager.manager.GetStatsForEntity(Target).isStopped = false;
            if (callback != null)
            {
                callback(false);
            }
            Destroy(gameObject);
        }
    }
}
