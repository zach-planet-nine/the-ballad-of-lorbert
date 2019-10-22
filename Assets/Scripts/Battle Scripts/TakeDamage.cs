using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public GameObject DamageText;

    private int damage;
    private Vector2 startPosition;

    public void DisplayDamage(int damage, Vector3 position)
    {
        Vector3 rightPosition = new Vector3(position.x, position.y, 0);
        var clone = (GameObject)Instantiate(DamageText, rightPosition, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<DisplayDamage>().damageNumber = damage;
        //BattleManager.manager.ApplyDamage(gameObject, damage);
    }

    public void TakeStaminaDamage(int damage)
    {
        BattleManager.manager.ApplyStaminaDamage(gameObject, damage);
    }

    public void TakeMPDamage(int damage)
    {
        BattleManager.manager.ApplyMPDamage(gameObject, damage);
    }
}
