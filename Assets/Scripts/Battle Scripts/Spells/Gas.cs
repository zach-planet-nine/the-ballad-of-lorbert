using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    public GameObject GasObject;
    public GameObject CharacterObject;
    public GameObject AttackObject;

    private void OnEnable()
    {
        if(BattleManager.manager.gasLearned)
        {
            GasObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        GasObject.SetActive(false);
    }

    public void AttackEntity(GameObject target)
    {
        Debug.Log("Running attack in Gas");
        var clone = (GameObject)Instantiate(AttackObject, target.transform.position, Quaternion.Euler(Vector3.zero));
        BattleManager.manager.GetStatsForEntity(target).GasEmitter = clone;
        clone.GetComponent<RunGasAttack>().SetTarget(gameObject, target);
    }

    private void Update()
    {
        int currentMP = BattleManager.manager.GetStatsForEntity(gameObject).currentMP;
        if (currentMP < BattleManager.manager.gasCost)
        {
            float mpPercent = (float)currentMP / (float)BattleManager.manager.gasCost;
            var col = GasObject.GetComponent<SpriteRenderer>().material.color;
            col.a = mpPercent - 0.25f;
            GasObject.GetComponent<SpriteRenderer>().material.color = col;
            GasObject.GetComponent<Transform>().localScale = new Vector3(mpPercent, mpPercent, 1.0f);
            GasObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        else
        {
            var col = GasObject.GetComponent<SpriteRenderer>().material.color;
            col.a = 1.0f;
            GasObject.GetComponent<SpriteRenderer>().material.color = col;
            GasObject.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            GasObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
