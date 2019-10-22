using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    public GameObject GasObject;

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
            GasObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
