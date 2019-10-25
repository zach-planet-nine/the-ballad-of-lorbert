﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solid : MonoBehaviour
{
    public GameObject SolidObject;
    public GameObject WallObject;
    public GameObject AttackObject;

    private void OnEnable()
    {
        if(BattleManager.manager.solidLearned)
        {
            SolidObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        SolidObject.SetActive(false);
    }

    public void UseWall(GameObject target, float duration)
    {
        BattleManager.manager.GetStatsForEntity(target).isProtectedByWall = true;
        var clone = (GameObject)Instantiate(WallObject, target.transform.position, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<RunBrickWall>().SetTarget(target, duration);
    }

    private void Update()
    {
        int currentMP = BattleManager.manager.GetStatsForEntity(gameObject).currentMP;
        if (currentMP < BattleManager.manager.solidCost)
        {
            float mpPercent = (float)currentMP / (float)BattleManager.manager.solidCost;
            var col = SolidObject.GetComponent<SpriteRenderer>().material.color;
            col.a = mpPercent - 0.25f;
            SolidObject.GetComponent<SpriteRenderer>().material.color = col;
            SolidObject.GetComponent<Transform>().localScale = new Vector3(mpPercent, mpPercent, 1.0f);
            SolidObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        else
        {
            SolidObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
