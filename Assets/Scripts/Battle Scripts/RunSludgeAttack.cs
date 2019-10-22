using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSludgeAttack : MonoBehaviour
{
    public GameObject Sludge;
    public float duration = 1.0f;
    private GameObject Entity;
    private Vector3 destination;
    private bool targetHasTakenDamage;
    private int targetDamage;
    private Action<bool> callback;
    private float sludgeTimer;
    private float sludgeDuration = 0.5f;

    public void SetTarget(GameObject entity, Vector3 destination, int damage)
    {
        Rigidbody2D rbdy = GetComponent<Rigidbody2D>();
        float vx = (destination.x - rbdy.position.x) / duration;
        float vy = ((destination.y - rbdy.position.y) / duration) - (-9.8f * duration / 2);
        Vector2 velocity = new Vector2( vx, vy);
        rbdy.velocity = velocity;
        Entity = entity;
        this.destination = destination;
        targetDamage = damage;
    }

    public void SetTargetWithCallback(GameObject entity, Vector3 destination, int staminaDamage, Action<bool> callback)
    {
        SetTarget(entity, destination, staminaDamage);
        this.callback = callback;
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.manager.battleIsOver && gameObject != null)
        {
            Destroy(gameObject);
            return;
        }
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Sludge.SetActive(true);
            if(!targetHasTakenDamage)
            {
                Entity.GetComponent<TakeDamage>().TakeStaminaDamage(targetDamage);
                targetHasTakenDamage = true;
            }
            if (callback != null)
            {
                callback(true);
            }
            if(sludgeTimer >= sludgeDuration) {
                Destroy(gameObject);
            } else
            {
                sludgeTimer += Time.deltaTime;
            }
        }
    }
}
