using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAttack : MonoBehaviour
{
    public float duration = 1.0f;
    private GameObject Entity;
    private Vector3 destination;
    private int targetDamage;
    private Action<bool> callback;

    public void SetTarget(GameObject entity, Vector3 destination, int damage)
    {
        Rigidbody2D rbdy = GetComponent<Rigidbody2D>();
        Vector2 velocity = new Vector2((destination.x - rbdy.position.x) / duration, (destination.y - rbdy.position.y) / duration);
        rbdy.velocity = velocity;
        Entity = entity;
        this.destination = destination;
        targetDamage = damage;
    }

    public void SetTargetWithCallback(GameObject entity, Vector3 destination, int damage, Action<bool> callback)
    {
        SetTarget(entity, destination, damage);
        this.callback = callback;
    }

    // Update is called once per frame
    void Update()
    {
        if ((BattleManager.manager.battleIsOver && gameObject != null) || (BattleManager.manager.CheckIfEntityIsDead(Entity) && gameObject != null))
        {
            Destroy(gameObject);
            return;
        }
        duration -= Time.deltaTime;
        if(duration < 0)
        {
            Debug.Log("Destroying game object");
            Entity.GetComponent<TakeDamage>().DisplayDamage(targetDamage, destination);
            if(callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
        }
    }
}
