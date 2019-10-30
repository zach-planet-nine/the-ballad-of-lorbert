using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTrash : MonoBehaviour
{

    public GameObject TrashEmitter;

    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;

    // Start is called before the first frame update
    void Start()
    {
        ps = TrashEmitter.GetComponent<ParticleSystem>();
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
            particles[i].SetMeshIndex(2);
            particles[i].velocity = Vector3.zero;
        }
        ps.SetParticles(particles, numberOfParticles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
