using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPoisonPills : MonoBehaviour
{

    public GameObject PoisonPill;

    private GameObject Target;
    private int damage;
    private int basePillDamage;
    private Action<bool> callback;
    private List<GameObject> poisonPills = new List<GameObject>();
    private int numberOfPills = 10;
    private float pillDuration = 1.2f;
    private float randomizerValue = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(GameObject target, int poisonPillDamage, Action<bool> callback)
    {
        Target = target;
        damage = poisonPillDamage;
        basePillDamage = damage / 10;
        this.callback = callback;

        for(int i = 0; i < numberOfPills; i++)
        {
            Vector3 startPosition = gameObject.transform.position;
            Vector3 endPosition = new Vector3(Target.transform.position.x + Randomness.GetValueBetween(-randomizerValue, randomizerValue),
                Target.transform.position.y + Randomness.GetValueBetween(-randomizerValue, randomizerValue), 0);
            float vx = (endPosition.x - startPosition.x) / pillDuration;
            float vy = ((endPosition.y - startPosition.y) / pillDuration) - (-9.8f * pillDuration / 2);
            Vector3 velocity = new Vector3(vx, vy, 0);
            var clone = (GameObject)Instantiate(PoisonPill, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<Rigidbody2D>().velocity = velocity;
            poisonPills.Add(clone);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject != null && BattleManager.manager.CheckIfEntityIsDead(Target))
        {
            if(callback != null)
            {
                callback(false);
            }
            Destroy(gameObject);
        }
        pillDuration -= Time.deltaTime;
        if(gameObject != null && pillDuration <= 0)
        {
            poisonPills.ForEach(pill =>
            {
                int pillDamage = basePillDamage + Randomness.GetIntBetween(0, numberOfPills);
                Target.GetComponent<TakeDamage>().DisplayDamage(pillDamage, pill.transform.position);
                Destroy(pill);
            });
            if(callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
        }
    }
}
