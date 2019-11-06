using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunWave : MonoBehaviour
{
    private GameObject Target;
    private int damage;
    private Action<bool> callback;
    private float duration = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(GameObject target, int waveDamage, Action<bool> callback)
    {
        Target = target;
        damage = waveDamage;
        this.callback = callback;

        Rigidbody2D rbdy = gameObject.GetComponent<Rigidbody2D>();
        Vector3 velocity = new Vector3((target.transform.position.x - gameObject.transform.position.x) / duration, 0, 0);
        rbdy.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null && BattleManager.manager.CheckIfEntityIsDead(Target) && gameObject != null)
        {
            if(callback != null)
            {
                callback(false);
            }
            Destroy(gameObject);
            return;
        }
        duration -= Time.deltaTime;
        if(duration <= 0 && gameObject != null)
        {
            Target.GetComponent<TakeDamage>().DisplayDamage(damage, Target.transform.position);
            if(callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
        }
    }
}
