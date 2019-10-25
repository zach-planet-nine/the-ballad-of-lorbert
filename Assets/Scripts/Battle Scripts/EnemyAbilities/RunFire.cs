using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunFire : MonoBehaviour
{
    private GameObject Target;
    private int damage;
    private Action<bool> callback;
    private float duration = 2.0f;
    private float damageThreshold = 1.7f;
    private bool hasDamaged;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(GameObject target, int targetDamage, Action<bool> callback)
    {
        Target = target;
        damage = targetDamage;
        this.callback = callback;
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
        if(duration < damageThreshold && !hasDamaged)
        {
            hasDamaged = true;
            Target.GetComponent<TakeDamage>().DisplayDamage(damage, gameObject.transform.position);
        }
        if(duration <= 0 && gameObject != null)
        {
            if(callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
        }
    }
}
