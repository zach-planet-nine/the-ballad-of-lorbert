using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunScythe : MonoBehaviour
{
    public GameObject CountDown;
    private GameObject Target;
    private GameObject Count;
    private Action<bool> callback;
    private float scytheDuration = 0.6f;
    private float pauseDuration = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(GameObject target, Action<bool> callback)
    {
        Target = target;
        this.callback = callback;
        Rigidbody2D rbdy = gameObject.GetComponent<Rigidbody2D>();
        Vector3 velocity = new Vector3((target.transform.position.x - gameObject.transform.position.x) / scytheDuration, (target.transform.position.y - gameObject.transform.position.y) / scytheDuration, 0) ;
        rbdy.velocity = velocity;
        rbdy.angularVelocity = 360.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(BattleManager.manager.CheckIfEntityIsDead(Target) && gameObject != null)
        {
            if(Count != null)
            {
                Destroy(Count);
            }
            Destroy(gameObject);
            return;
        }
        if (scytheDuration > 0)
        {
            scytheDuration -= Time.deltaTime;
            if (scytheDuration <= 0)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                int coinToss = Randomness.GetIntBetween(0, 2);
                if(coinToss == 0)
                {
                    BattleManager.manager.GetStatsForEntity(Target).isInCountdown = true;
                    Vector3 countdownPosition = new Vector3(Target.transform.position.x - 0.4f, Target.transform.position.y + 0.5f, 0);
                    Count = (GameObject)Instantiate(CountDown, countdownPosition, Quaternion.Euler(Vector3.zero));
                    Count.GetComponent<DisplayCountdown>().DisplayCountdownOfValue(20);
                } else
                {
                    Target.GetComponent<TakeDamage>().DisplayDamage(0, Target.transform.position);
                }
            }
        }
        else if (gameObject != null)
        {
            pauseDuration -= Time.deltaTime;
            if(pauseDuration <= 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            if(Count.GetComponent<DisplayCountdown>().CountDownIsDone())
            {
                int damage = BattleManager.manager.GetStatsForEntity(Target).maxHP + 1;
                Target.GetComponent<TakeDamage>().DisplayDamage(damage, Target.transform.position);
                Destroy(Count);
                Destroy(gameObject);
            }
        }
        
    }
}
