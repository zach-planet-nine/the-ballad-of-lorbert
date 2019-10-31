using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    public GameObject Bolt;
    private GameObject Target;
    private Action<bool> callback;
	Vector3 destination;
    int damagePortion;
    public float duration = 8.0f;
    float durationThreshold;
    int damageTimes;
    float damageTimer;
	float boltTimer;

    public void SetTarget(GameObject entity, Vector3 destination, int damage) {
		this.destination = destination;
		Target = entity;
		damagePortion = damage / 4;
		durationThreshold = duration / 2.0f;
    }

    public void SetTargetWithCallback(GameObject entity, Vector3 destination, int damage, Action<bool> callback)
    {
        SetTarget(entity, destination, damage);
        this.callback = callback;
    }

    private void CheckAndCallCallback(bool result)
    {
        if(callback != null)
        {
            callback(result);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.manager.battleIsOver && gameObject != null)
        {
            CheckAndCallCallback(false);
            Destroy(gameObject);
            return;
        }
        if(BattleManager.manager.CheckIfEntityIsDead(Target) && gameObject != null)
        {
            CheckAndCallCallback(false);
            Destroy(gameObject);
            return;
        }
        damageTimer += Time.deltaTime;
        if(damageTimer >= durationThreshold)
		{
			damageTimer = 0;
			damageTimes += 1;
            if (damagePortion < 0)
            {
                Target.GetComponent<TakeHealing>().DisplayHealing(-damagePortion, destination);
            }
            else
            {
                Target.GetComponent<TakeDamage>().DisplayDamage(damagePortion, destination);
            }
			boltTimer = 0;
			Bolt.SetActive(true);
		}
		boltTimer += Time.deltaTime;
        if(boltTimer >= 0.15f)
		{
			Bolt.SetActive(false);
		}
        if(damageTimes >= 4 && boltTimer >= 0.15f)
        {
            CheckAndCallCallback(true);
            Destroy(gameObject);
        }
    }
}
