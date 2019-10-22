using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : MonoBehaviour
{
    public GameObject PlasmaObject;

    private void OnEnable()
    {
        if(BattleManager.manager.plasmaLearned)
        {
            PlasmaObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        PlasmaObject.SetActive(false);
    }

    private void Update()
    {
        int currentMP = BattleManager.manager.GetStatsForEntity(gameObject).currentMP;
        if (currentMP < BattleManager.manager.plasmaCost)
        {
            float mpPercent = (float)currentMP / (float)BattleManager.manager.plasmaCost;
            var col = PlasmaObject.GetComponent<SpriteRenderer>().material.color;
            col.a = mpPercent - 0.25f;
            PlasmaObject.GetComponent<SpriteRenderer>().material.color = col;
            PlasmaObject.GetComponent<Transform>().localScale = new Vector3(mpPercent, mpPercent, 1.0f);
            PlasmaObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        else
        {
            PlasmaObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
