using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBit : MonoBehaviour
{

    public GameObject Bit;
    public GameObject Beam;
    private SpriteRenderer spriteRenderer;

    private GameObject Attacker;
    private GameObject Target;
    private float bitTimer;
    private float bitThreshold = 2.0f;
    private float laserTimer;
    private float laserDuration = 0.6f;
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
        Debug.Log(target);
        Debug.Log(attacker);
        SetTarget(target, attacker);
        this.callback = callback;
    }

    private void Start()
    {
        spriteRenderer = Bit.GetComponent<SpriteRenderer>();
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
            var col = spriteRenderer.material.color;
            col.a = opacity;
            spriteRenderer.material.color = col;
        } else
        {
            bitTimer += Time.deltaTime;
            if(bitTimer > bitThreshold && damageDealt < damageThreshold)
            {
                bitTimer = 0;
                laserTimer = 0;
                int damage = BattleManager.manager.BitAttacksEntity(Attacker, Target);
                //Target.GetComponent<TakeDamage>().DisplayDamage(damage, Target.transform.position);
                Beam.GetComponent<RunLaser>().FireLaserAt(Target, damage);
                damageDealt += damage;
                
            }
            if (damageDealt >= damageThreshold && gameObject != null && laserTimer >= laserDuration)
            {
                if (callback != null)
                {
                    callback(true);
                }
                Destroy(gameObject);
            } else
            {
                laserTimer += Time.deltaTime;
            }
        }
    }
}
