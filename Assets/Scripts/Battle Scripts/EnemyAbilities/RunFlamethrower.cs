using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunFlamethrower : MonoBehaviour
{
    private List<GameObject> Targets;
    private List<int> damages;
    private Action<bool> callback;
    private float flamethrowerDuration = 1.0f;
    private bool hasDamaged;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTargetWithCallback(List<GameObject> targets, List<int> damages, Action<bool> callback)
    {
        Targets = targets;
        this.damages = damages;
        this.callback = callback;

        gameObject.GetComponent<Rigidbody2D>().angularVelocity = -90.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject != null && BattleManager.manager.gameOver)
        {
            Destroy(gameObject);
        }
        flamethrowerDuration -= Time.deltaTime;
        if(!hasDamaged && flamethrowerDuration <= 0.2f)
        {
            hasDamaged = true;
            int i = 0;
            Targets.ForEach(target =>
            {
                if(!BattleManager.manager.CheckIfEntityIsDead(target))
                {
                    target.GetComponent<TakeDamage>().DisplayDamage(damages[i], target.transform.position);
                }
                i += 1;
            });
        }
        if(gameObject != null && flamethrowerDuration <= 0)
        {
            if(callback != null)
            {
                callback(true);
            }
            Destroy(gameObject);
        }
    }
}
