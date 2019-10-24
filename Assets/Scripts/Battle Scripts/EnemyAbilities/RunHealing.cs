using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunHealing : MonoBehaviour
{
    public float emitterDuration = 0.6f;

    private GameObject Target;
    private int healing;
    private Action<bool> callback;
    private float emitterTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(GameObject target, int healingAmount, Action<bool> callback)
    {
        Target = target;
        healing = healingAmount;
        this.callback = callback;
        Target.GetComponent<TakeHealing>().DisplayHealing(healingAmount, Target.transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(BattleManager.manager.CheckIfEntityIsDead(Target) && gameObject != null)
        {
            if (callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
            return;
        }
        emitterTimer += Time.deltaTime;
        if(emitterTimer > emitterDuration && gameObject != null)
        {
            if (callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
        }
    }
}
