using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject AttackObject;
    public Vector3 startPosition;

    public void AttackEntity(GameObject entity, Vector3 destination, int damage)
    {
        var clone = (GameObject)Instantiate(AttackObject, startPosition, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunAttack>().SetTarget(entity, destination, damage);
    }

    public void AttackEntityWithCallback(GameObject entity, Vector3 destination, int damage, Action<bool> callback)
    {
        var clone = (GameObject)Instantiate(AttackObject, startPosition, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunAttack>().SetTargetWithCallback(entity, destination, damage, callback);
    }
}
