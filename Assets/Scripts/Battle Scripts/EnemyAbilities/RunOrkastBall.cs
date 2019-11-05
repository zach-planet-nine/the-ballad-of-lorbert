using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOrkastBall : MonoBehaviour
{
    public GameObject Ball;

    private GameObject Target;
    private int damage;
    private Action<bool> callback;
    private float spinDuration = 0.6f;
    private float shootDuration = 0.4f;
    private bool hasShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(GameObject target, int orkastDamage, Action<bool> callback)
    {
        Target = target;
        damage = orkastDamage;
        this.callback = callback;
        Ball.GetComponent<Rigidbody2D>().angularVelocity = 1080.0f;
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

        spinDuration -= Time.deltaTime;
        if(spinDuration <= 0)
        {
            if(hasShot)
            {
                shootDuration -= Time.deltaTime;
                if(shootDuration <= 0 && gameObject != null)
                {
                    Target.GetComponent<TakeDamage>().DisplayDamage(damage, Target.transform.position);
                    if(callback != null)
                    {
                        callback(true);
                    }
                    Destroy(gameObject);
                }
            } else
            {
                hasShot = true;
                Vector3 velocity = new Vector3((Target.transform.position.x - gameObject.transform.position.x) / shootDuration,
                    (Target.transform.position.y - gameObject.transform.position.y) / shootDuration, 0);
                gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
                Ball.GetComponent<Rigidbody2D>().velocity = velocity;
            }
            
        }
    }
}
