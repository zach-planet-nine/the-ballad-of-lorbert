using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    public GameObject Bolt;
    private GameObject Target;
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

    // Update is called once per frame
    void Update()
    {
		damageTimer += Time.deltaTime;
        if(damageTimer >= durationThreshold)
		{
			damageTimer = 0;
			damageTimes += 1;
			Target.GetComponent<TakeDamage>().DisplayDamage(damagePortion, destination);
			boltTimer = 0;
			Bolt.SetActive(true);
		}
		boltTimer += Time.deltaTime;
        if(boltTimer >= 0.15f)
		{
			Bolt.SetActive(false);
		}
    }
}
