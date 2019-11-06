using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPlasma : MonoBehaviour
{

    private GameObject Target;
    private int damage;
    private float duration = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTarget(GameObject target, int plasmaDamage)
    {
        Target = target;
        damage = plasmaDamage;
        Vector3 velocity = new Vector3((target.transform.position.x - gameObject.transform.position.x) / duration,
            (target.transform.position.y - gameObject.transform.position.y) / duration, 0);
        gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if(BattleManager.manager.CheckIfEntityIsDead(Target) && gameObject != null)
        {
            Destroy(gameObject);
        }
        duration -= Time.deltaTime;
        if(duration <= 0 && gameObject != null)
        {
            Target.GetComponent<TakeDamage>().DisplayDamage(damage, Target.transform.position);
            Destroy(gameObject);
        }
    }
}
