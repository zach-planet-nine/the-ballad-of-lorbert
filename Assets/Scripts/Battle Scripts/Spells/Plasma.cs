using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : MonoBehaviour
{
    public GameObject PlasmaObject;
    public GameObject CharacterObject;
    public GameObject AttackObject;

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
            float scalePercent = 3.03f * mpPercent;
            PlasmaObject.GetComponent<SpriteRenderer>().material.color = col;
            PlasmaObject.GetComponent<Transform>().localScale = new Vector3(scalePercent, scalePercent, 1.0f);
            PlasmaObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        else
        {
            var col = PlasmaObject.GetComponent<SpriteRenderer>().material.color;
            col.a = 1.0f;
            PlasmaObject.GetComponent<SpriteRenderer>().material.color = col;
            PlasmaObject.GetComponent<Transform>().localScale = new Vector3(3.03f, 3.03f, 3.03f);
            PlasmaObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
