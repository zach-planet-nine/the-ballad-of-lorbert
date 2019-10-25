using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCylinder : MonoBehaviour
{

    private GameObject Target;
    private int damage;
    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTarget(GameObject target, int targetDamage)
    {
        Target = target;
        damage = targetDamage;
        destination = new Vector3(target.transform.position.x, target.transform.position.y - 1.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(BattleManager.manager.CheckIfEntityIsDead(Target) && gameObject != null)
        {
            Destroy(gameObject);
        }
        if(gameObject.transform.position.y < destination.y && gameObject != null)
        {
            Target.GetComponent<TakeDamage>().DisplayDamage(damage, gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
