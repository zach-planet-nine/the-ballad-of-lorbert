using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlasmaParticleAttack : MonoBehaviour
{

    private GameObject Target;
    private int damage;
    private Action<bool> callback;
    private float damageDurationThreshold = 0.3f;
    private float damageDuration = 0.3f;
    private float totalDuration = 1.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(GameObject target, int plasmaDamage, Action<bool> callback)
    {
        Target = target;
        damage = plasmaDamage;
        this.callback = callback;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject != null && BattleManager.manager.CheckIfEntityIsDead(Target))
        {
            if(callback != null)
            {
                callback(false);
            }
            Destroy(gameObject);
        }
        damageDuration -= Time.deltaTime;
        if(damageDuration <= 0)
        {
            damageDuration = damageDurationThreshold;
            int portionDamage = damage / 4 + Randomness.GetIntBetween(0, 8);
            Target.GetComponent<TakeDamage>().DisplayDamage(portionDamage, Target.transform.position);
        }
        totalDuration -= Time.deltaTime;
        if(gameObject != null && totalDuration <= 0)
        {
            if(callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
        }
    }
}
