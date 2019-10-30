using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTrash : MonoBehaviour
{

    private GameObject Target;
    private int damage;
    private Action<bool> callback;
    private float duration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void SetTargetWithCallback(GameObject target, int trashDamage, Action<bool> callback)
    {
        Target = target;
        damage = trashDamage;
        this.callback = callback;

        float angle = Vector3.Angle(gameObject.transform.position, target.transform.position);
        gameObject.transform.RotateAround(gameObject.transform.position, new Vector3(0, 0, 1), angle);
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
            if(callback != null)
            {
                callback(true);
            }
            Target.GetComponent<TakeDamage>().DisplayDamage(damage, Target.transform.position);
            Destroy(gameObject);
        }
    }
}
