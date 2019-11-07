using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGasParticleEmitter : MonoBehaviour
{
    private GameObject Target;
    private float duration;
    private int damage;
    private Action<bool> callback;
    private float floatDuration = 1.2f;
    private float timer;
    private bool hasStopped;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetTargetWithCallback(GameObject target, float gasDuration, int gasDamage, Action<bool> callback)
    {
        Target = target;
        duration = gasDuration;
        damage = gasDamage;
        this.callback = callback;

        Rigidbody2D rbdy = gameObject.GetComponent<Rigidbody2D>();
        Vector3 velocity = new Vector3((target.transform.position.x - 0.4f - gameObject.transform.position.x) / floatDuration, (target.transform.position.y + 0.4f - gameObject.transform.position.y) / floatDuration, 0);
        rbdy.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.manager.CheckIfEntityIsDead(Target) && gameObject != null)
        {
            if (callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
            return;
        }
        floatDuration -= Time.deltaTime;
        if (floatDuration < 0)
        {
            if (!hasStopped)
            {
                hasStopped = true;
                BattleManager.manager.GetStatsForEntity(Target).isBlinded = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                Target.GetComponent<TakeDamage>().DisplayDamage(damage, Target.transform.position);
            }

            duration -= Time.deltaTime;
            if (duration < 0 && gameObject != null)
            {
                BattleManager.manager.GetStatsForEntity(Target).isBlinded = false;
                if (callback != null)
                {
                    callback(true);
                }
                Destroy(gameObject);
            }
        }
    }
}
