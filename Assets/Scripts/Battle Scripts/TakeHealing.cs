using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHealing : MonoBehaviour
{
    public GameObject HealingText;

    private int healing;
    private Vector2 startPosition;

    public void DisplayHealing(int healing, Vector3 position)
    {
        Vector3 rightPosition = new Vector3(position.x, position.y, 0);
        var clone = (GameObject)Instantiate(HealingText, rightPosition, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<DisplayDamage>().damageNumber = healing;
        BattleManager.manager.ApplyHealing(gameObject, healing);
    }
}
