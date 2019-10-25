using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBash : MonoBehaviour
{
    private GameObject Target;
    private int damage;
    private Action<bool> callback;
    private float rotationDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(GameObject target, int bashDamage, Action<bool> callback) {
        Target = target;
        damage = bashDamage;
        this.callback = callback;

        Rigidbody2D rbdy = gameObject.GetComponent<Rigidbody2D>();
        rbdy.angularVelocity = 135;
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
        }
        rotationDuration -= Time.deltaTime;
        if(rotationDuration <= 0 && gameObject != null)
        {
            Target.GetComponent<TakeDamage>().DisplayDamage(damage, Target.transform.position);
            if (callback != null)
            {
                callback(false);
            }
            Destroy(gameObject);
        }
    }
}
