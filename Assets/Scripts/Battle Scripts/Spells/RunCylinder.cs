using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCylinder : MonoBehaviour
{

    private GameObject Target;
    private int damage;
    private Vector3 destination;
    private Action<bool> callback;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTarget(GameObject target, int targetDamage)
    {
        Target = target;
        damage = targetDamage;
        destination = new Vector3(target.transform.position.x, target.transform.position.y - 1.0f, 0);
    }

    public void SetTargetWithCallback(GameObject target, int targetDamage, Action<bool> callback)
    {
        SetTarget(target, targetDamage);
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
        }
        if(gameObject.transform.position.y < destination.y && gameObject != null)
        {
            if(damage < 0)
            {
                Target.GetComponent<TakeHealing>().DisplayHealing(-damage, gameObject.transform.position);
            } else
            {
                Target.GetComponent<TakeDamage>().DisplayDamage(damage, gameObject.transform.position);
            }
            if(callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
        }
    }
}
