using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilities : MonoBehaviour
{
    public GameObject Ability1;
    public GameObject Ability2;
    public GameObject Ability3;
    public GameObject Ability4;

    private float projectileDuration = 0.5f;
    private float sludgeDuration = 1.0f;
    private float sludgeTimer;
    private float projectileTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SludgeEntityWithCallback(GameObject target, Vector3 destination, int staminaDamage, Action<bool> callback)
    {
        var clone = (GameObject)Instantiate(Ability1, gameObject.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunSludgeAttack>().SetTargetWithCallback(target, destination, staminaDamage, callback);
    }

    public void StealMPWithCallback(GameObject target, Vector3 destination, int mpDamage, Action<bool> callback)
    {
        Debug.Log("Creating clone of " + Ability1.name);
        var clone = (GameObject)Instantiate(Ability1, destination, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunStealMP>().SetTargetWithCallback(target, gameObject.transform.position, mpDamage, callback);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
