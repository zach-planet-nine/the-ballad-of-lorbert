using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunSmallBomb : MonoBehaviour
{

    public GameObject ExplosionEmitter;

    private GameObject Target;
    private int damage;
    private Action<bool> callback;
    private bool hasExploded;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(GameObject target, int smallBombDamage, Action<bool> callback)
    {
        Target = target;
        damage = smallBombDamage;
        this.callback = callback;
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
            return;
        }
        if(gameObject.transform.position.y <= Target.transform.position.y - 0.2f)
        {
            if(!hasExploded)
            {
                hasExploded = true;
                Target.GetComponent<TakeDamage>().DisplayDamage(damage, gameObject.transform.position);
                ExplosionEmitter.GetComponent<ParticleSystem>().Play();
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                Rigidbody2D rbdy = gameObject.GetComponent<Rigidbody2D>();
                rbdy.gravityScale = 0;
                rbdy.velocity = Vector3.zero;
            } else
            {
                if(!ExplosionEmitter.GetComponent<ParticleSystem>().isPlaying && gameObject != null)
                {
                    if(callback != null)
                    {
                        callback(true);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}
