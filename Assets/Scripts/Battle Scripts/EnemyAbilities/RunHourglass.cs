using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunHourglass : MonoBehaviour
{

    private GameObject Target;
    private Action<bool> callback;
    private float hourglassTimer;
    private float hourglassDuration = 0.6f;
    private float durationTimer;
    private float duration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(GameObject target, float duration, Action<bool> callback)
    {
        Target = target;
        this.callback = callback;
        this.duration = hourglassDuration = duration;
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
        hourglassTimer += Time.deltaTime;
        if(hourglassTimer >= hourglassDuration)
        {
            if(callback != null)
            {
                callback(true);
            }
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        durationTimer += Time.deltaTime;
        if(durationTimer >= duration && gameObject != null) 
        {
            BattleManager.manager.GetStatsForEntity(Target).isSlowedStamina = false;
            Destroy(gameObject);
        }
    }
}
