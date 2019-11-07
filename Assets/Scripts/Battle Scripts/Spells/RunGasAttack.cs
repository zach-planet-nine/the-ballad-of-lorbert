using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAndDamage
{
    public Vector3 position;
    public int damage;
}

public class RunGasAttack : MonoBehaviour
{

    public GameObject ExplosionEmitter;

    private GameObject Target;
    private GameObject Caster;
    private Action<bool> callback;
    private List<GameObject> ExplosionEmitters = new List<GameObject>();
    private int exploderIndex;
    private int exploders = 10;
    private float callbackDuration = 0.2f;
    private bool calledBack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTarget(GameObject caster, GameObject target)
    {
        Debug.Log("Setting target for gas attack");
        Caster = caster;
        Target = target;

        for(int i = 0; i < exploders; i++)
        {
            Vector3 position = new Vector3(target.transform.position.x + Randomness.GetValueBetween(-0.5f, 0.5f),
                target.transform.position.y + Randomness.GetValueBetween(-0.5f, 0.5f), 0);
            var clone = (GameObject)Instantiate(ExplosionEmitter, position, Quaternion.Euler(Vector3.zero));
            ExplosionEmitters.Add(clone);
        }
    }

    public void SetTargetWithCallback(GameObject caster, GameObject target, Action<bool> callback)
    {
        exploders = 5;
        SetTarget(caster, target);
        this.callback = callback;
        callback(true);
    }

    public PositionAndDamage ExplodeOnce()
    {
        PositionAndDamage pad = new PositionAndDamage();
        pad.position = gameObject.transform.position;
        pad.damage = BattleManager.manager.GasEmitterExplodesOnEntity(Caster, Target);
        //Debug.Log(ExplosionEmitters.Count);
        ExplosionEmitters[exploderIndex].GetComponent<ParticleSystem>().Play();
        exploderIndex += 1;
        return pad;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject != null && (BattleManager.manager.CheckIfEntityIsDead(Target) || BattleManager.manager.CheckIfEntityIsDead(Caster) || exploderIndex >= exploders))
        {
            BattleManager.manager.GetStatsForEntity(Target).GasEmitter = null;
            ExplosionEmitters.ForEach(emitter =>
            {
                Destroy(emitter);
            });
            Destroy(gameObject);
        }
        callbackDuration -= Time.deltaTime;
        if(!calledBack && callbackDuration <= 0)
        {
            calledBack = true;
            if(callback != null)
            {
                callback(true);
            }
        }
    }
}
