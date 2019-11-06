using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAgileStar : MonoBehaviour
{

    public GameObject NinjaStar;

    private GameObject Target;
    private int damage;
    private int baseStarDamage;
    private Action<bool> callback;
    private List<GameObject> stars = new List<GameObject>();
    private int numberOfStars = 6;
    private float starDuration = 0.3f;
    private float randomizerValue = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetTargetWithCallback(GameObject target, int agileStarDamage, Action<bool> callback)
    {
        Target = target;
        damage = agileStarDamage;
        baseStarDamage = damage / numberOfStars;
        this.callback = callback;

        for(int i = 0; i < numberOfStars; i++)
        {
            Vector3 startPosition = new Vector3(gameObject.transform.position.x + Randomness.GetValueBetween(-randomizerValue, randomizerValue),
                gameObject.transform.position.y + Randomness.GetValueBetween(-randomizerValue, randomizerValue), 0);
            Vector3 endPosition = new Vector3(Target.transform.position.x + Randomness.GetValueBetween(-randomizerValue, randomizerValue),
                startPosition.y, 0);
            Vector3 velocity = new Vector3((endPosition.x - startPosition.x) / starDuration, 0, 0);
            var clone = (GameObject)Instantiate(NinjaStar, startPosition, Quaternion.Euler(Vector3.zero));
            Rigidbody2D rbdy = clone.GetComponent<Rigidbody2D>();
            rbdy.velocity = velocity;
            rbdy.angularVelocity = 1080.0f;
            stars.Add(clone);
        }
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
        starDuration -= Time.deltaTime;
        if(starDuration <= 0 && gameObject != null)
        {
            stars.ForEach(star =>
            {
                int starDamage = baseStarDamage + Randomness.GetIntBetween(0, numberOfStars);
                Target.GetComponent<TakeDamage>().DisplayDamage(starDamage, star.transform.position);
                Destroy(star);
            });
            Destroy(gameObject);
        }
    }
}
