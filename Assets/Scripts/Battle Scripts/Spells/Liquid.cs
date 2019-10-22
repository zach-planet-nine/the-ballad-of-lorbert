using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    public GameObject LiquidObject;
    public GameObject HealingObject;
    public GameObject AttackObject;

    private void OnEnable()
    {
        LiquidObject.SetActive(true);
    }

    private void OnDisable()
    {
        LiquidObject.SetActive(false);
    }

    public void HealEntity(GameObject entity, Vector3 destination, int healing)
    {
        var clone = (GameObject)Instantiate(HealingObject, new Vector3(destination.x, destination.y + 0.7f, destination.z), Quaternion.Euler(Vector3.zero));
        clone.GetComponent<HealingLiquid>().SetTarget(entity, destination, healing);
    }

    public void AttackEntity(GameObject entity, Vector3 destination, int damage)
    {
        var clone = (GameObject)Instantiate(AttackObject, new Vector3(destination.x, destination.y + 0.7f, 0), Quaternion.Euler(Vector3.zero));
        clone.GetComponent<Storm>().SetTarget(entity, destination, damage);
    }

    public void Update()
    {
        int currentMP = BattleManager.manager.GetStatsForEntity(gameObject).currentMP;
        if(currentMP < BattleManager.manager.liquidCost)
        {
            float mpPercent = (float)currentMP / (float)BattleManager.manager.liquidCost;
            var col = LiquidObject.GetComponent<SpriteRenderer>().material.color;
            col.a = mpPercent - 0.25f;
            LiquidObject.GetComponent<SpriteRenderer>().material.color = col;
            LiquidObject.GetComponent<Transform>().localScale = new Vector3(mpPercent, mpPercent, 1.0f);
            LiquidObject.GetComponent<CircleCollider2D>().enabled = false;
        } else
        {
            var col = LiquidObject.GetComponent<SpriteRenderer>().material.color;
            col.a = 1.0f;
            LiquidObject.GetComponent<SpriteRenderer>().material.color = col;
            LiquidObject.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            LiquidObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

}
