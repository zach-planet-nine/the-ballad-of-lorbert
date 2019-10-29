using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGrenade : MonoBehaviour
{
    public GameObject ExplosionEmitter;

    private List<GameObject> Targets;
    private Action<bool> callback;
    private float duration = 1.2f;
    private bool hasExploded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetsWithCallback(List<GameObject> characters, Action<bool> callback)
    {
        Targets = characters;
        this.callback = callback;
        Vector3 destination = new Vector3(-5.6f, -1.0f, 0);
        float vx = (destination.x - gameObject.transform.position.x) / duration;
        float vy = ((destination.y - gameObject.transform.position.y) / duration) - (-9.8f * duration / 2);
        Vector3 velocity = new Vector3(vx, vy, 0);
        gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if(BattleManager.manager.gameOver && gameObject != null)
        {
            Destroy(gameObject);
        }
        duration -= Time.deltaTime;
        if(duration < 0 && !hasExploded)
        {
            hasExploded = true;
            Targets.ForEach(target =>
            {
                int damage = BattleManager.manager.GrenadeHitsEntity(target);
                target.GetComponent<TakeDamage>().DisplayDamage(damage, target.transform.position);
            });
            Rigidbody2D rbdy = gameObject.GetComponent<Rigidbody2D>();
            rbdy.gravityScale = 0;
            rbdy.velocity = Vector3.zero;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            ExplosionEmitter.GetComponent<ParticleSystem>().Play();
        }
    }
}
