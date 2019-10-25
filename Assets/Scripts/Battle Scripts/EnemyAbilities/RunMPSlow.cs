using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunMPSlow : MonoBehaviour
{
    public GameObject MPSlowEmitter;

    private GameObject Target;
    private float duration;
    private Action<bool> callback;
    private float emitterDuration = 0.75f;
    private bool hasStoppedEmitter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(GameObject target, float slowDuration, Action<bool> callback)
    {
        Target = target;
        duration = slowDuration;
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
        emitterDuration -= Time.deltaTime;
        if(emitterDuration < 0 && !hasStoppedEmitter)
        {
            hasStoppedEmitter = true;
            Destroy(MPSlowEmitter);
        }
        duration -= Time.deltaTime;
        if(duration <= 0 && gameObject != null)
        {
            BattleManager.manager.GetStatsForEntity(Target).isSlowedMP = false;
            if (callback != null)
            {
                callback(false);
            }
            Destroy(gameObject);
        }
    }
}
