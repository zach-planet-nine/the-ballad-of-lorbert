using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBit : MonoBehaviour
{

    public GameObject Bit;

    private GameObject Attacker;
    private GameObject Target;
    private float bitTimer;
    private float bitThreshold = 2.0f;
    private int damageDealt;
    private int damageThreshold;
    private float opacity;
    private Action<bool> callback;

    public void SetTarget(GameObject target, GameObject attacker)
    {
        Target = target;
        Attacker = attacker;
        damageThreshold = BattleManager.manager.GetBitDamageThreshold(attacker, target);
    }

    public void SetTargetWithCallback(GameObject target, GameObject attacker, Action<bool> callback)
    {
        Debug.Log("Setting the target here");
        SetTarget(target, attacker);
        this.callback = callback;
    }

    // Update is called once per frame
    void Update()
    {
        if(BattleManager.manager.CheckIfEntityIsDead(Target) && gameObject != null)
        {
            if(callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
            return;
        }
        if(opacity < 1.0f)
        {
            opacity += Time.deltaTime;
            if (opacity > 1.0f)
            {
                opacity = 1.0f;
            }
            var col = Bit.GetComponent<SpriteRenderer>().material.color;
            col.a = opacity;
            Bit.GetComponent<SpriteRenderer>().material.color = col;
        } else
        {
            bitTimer += Time.deltaTime;
            if(bitTimer > bitThreshold)
            {
                bitTimer = 0;
                int damage = BattleManager.manager.BitAttacksEntity(Attacker, Target);
                Target.GetComponent<TakeDamage>().DisplayDamage(damage, Target.transform.position);
                damageDealt += damage;
                if(damageDealt > damageThreshold && gameObject != null)
                {
                    if(callback != null)
                    {
                        callback(true);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}
