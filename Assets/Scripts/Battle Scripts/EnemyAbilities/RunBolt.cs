using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBolt : MonoBehaviour
{

    public GameObject Bolter;

    private GameObject Target;
    private int damage;
    private Action<bool> callback;
    private float duration = 0.3f;
    private bool hasDamaged;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(GameObject target, int boltDamage, Action<bool> callback)
    {
        Target = target;
        damage = boltDamage;
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
        if(duration < 0 && !hasDamaged)
        {
            hasDamaged = true;
            Target.GetComponent<TakeDamage>().DisplayDamage(damage, Target.transform.position);
            if(callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
        }
    }
}
