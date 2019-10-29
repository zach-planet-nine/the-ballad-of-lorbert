using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunMachineGun : MonoBehaviour
{
    private List<GameObject> targets;
    private GameObject attacker;
    private Action<bool> callback;

    private float targetTimer;
    private float machineGunDuration = 1.2f;
    private float targetDuration = 0.4f;
    private int targetsHit;
    private float damageTimer;
    private float damageThreshold = 0.045f;
    private Vector3 currentTargetPosition;

    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    private Transform worldTransform;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        worldTransform = GetComponent<Transform>();
    }

    public void SetTargetWithCallback(List<GameObject> characters, GameObject attacker, Action<bool> callback)
    {
        targets = characters;
        this.attacker = attacker;
        this.callback = callback;
        machineGunDuration = targetDuration * characters.Count;
        currentTargetPosition = characters[0].transform.position;
        Debug.Log("Targets count is: " + targets.Count);
    }

    private void LateUpdate()
    {
        if(particles == null || particles.Length < ps.main.maxParticles)
        {
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
        }
        int numberOfParticles = ps.GetParticles(particles);
        for(int i = 0, n = particles.Length; i < n; i++)
        {
            if(particles[i].remainingLifetime > 0 && Math.Abs(particles[i].velocity.z - 0.01f) > 0.005f)
            {
                Vector3 worldPosition = worldTransform.TransformPoint(particles[i].position);
                Vector3 destination = new Vector3(currentTargetPosition.x + Randomness.GetValueBetween(-0.6f, 0.6f), currentTargetPosition.y + Randomness.GetValueBetween(-0.6f, 0.6f), 0);
                float deltaX = destination.x - worldPosition.x;
                float deltaY = destination.y - worldPosition.y;
                particles[i].velocity = new Vector3(deltaX / particles[i].remainingLifetime, deltaY / particles[i].remainingLifetime, 0.01f);
            }
        }
        ps.SetParticles(particles, numberOfParticles);
    }

    // Update is called once per frame
    void Update()
    {
        if((BattleManager.manager.battleIsOver || BattleManager.manager.gameOver) && gameObject != null)
        {
            Destroy(gameObject);
            return;
        }
        targetTimer += Time.deltaTime;
        if(targetTimer > targetDuration)
        {
            targetTimer = 0;
            targetsHit += 1;
            if(targetsHit >= targets.Count)
            {
                Destroy(gameObject);
                return;
            }
            currentTargetPosition = targets[targetsHit].transform.position;
        }
        damageTimer += Time.deltaTime;
        if(damageTimer >= damageThreshold)
        {
            damageTimer = 0;
            int damage = BattleManager.manager.EntityAttacksEntityWithMachineGun(attacker, targets[targetsHit]);
            targets[targetsHit].GetComponent<TakeDamage>().DisplayDamage(damage, new Vector3(currentTargetPosition.x + Randomness.GetValueBetween(-0.2f, 0.2f),
                currentTargetPosition.y + Randomness.GetValueBetween(-0.2f, 0.2f), 0));
        }
    }
}
