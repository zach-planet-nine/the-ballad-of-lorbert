using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBrickWall : MonoBehaviour
{
    private GameObject Target;
    private float timer;
    private float duration;
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    private Vector3 wallPosition;
    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
    }

    public void SetTarget(GameObject target, float wallDuration)
    {
        Target = target;
        duration = wallDuration;
        wallPosition = new Vector3(target.transform.position.x + 1.5f, target.transform.position.y - 1.2f, 0);
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
            int mod = i % 5;
            int xPosition = 32 * mod;
            int div = i / 5;
            int yPosition = 24 * div;
            float xDelta = (float)xPosition / 64;
            float yDelta = (float)yPosition / 64;
            particles[i].velocity = Vector3.zero;
            particles[i].position = new Vector3(xDelta, yDelta, 0);
        }
        ps.SetParticles(particles, numberOfParticles);
    }

    // Update is called once per frame
    void Update()
    {
        if(BattleManager.manager.CheckIfEntityIsDead(Target) && gameObject != null)
        {
            Destroy(gameObject);
            return;
        }
        timer += Time.deltaTime;
        if(timer >= duration && gameObject != null)
        {
            BattleManager.manager.GetStatsForEntity(Target).isProtectedByWall = false;
            Destroy(gameObject);
        }
    }
}
