using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ActionAndTarget
{
    public EnemyActions action;
    public GameObject target;
    public bool allCharacters;
    public bool allEnemies;
}

public class BattleAI
{
    public virtual void Attacked(GameObject attacker)
    {

    }

    public virtual ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        return new ActionAndTarget();
    }

    public virtual EnemyActions ChooseAction(List<GameObject> characters, List<GameObject> enemies)
    {
        Debug.Log("This gets called");
        return EnemyActions.Projectile;
    }

    public virtual GameObject ChooseTarget(List<GameObject> potentialTargets)
    {
        return potentialTargets[0];
    }

    public GameObject GetEntityWithMostHP(List<GameObject> entities)
    {
        int maxHP = 0;
        GameObject target = entities[0];
        entities.ForEach(entity =>
        {
            BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
            if (stats.currentHP > maxHP)
            {
                target = entity;
                maxHP = stats.currentHP;
            }
        });
        return target;
    }

    public GameObject GetEntityWithLeastStamina(List<GameObject> entities)
    {
        int minStamina = 1000;
        GameObject target = entities[0];
        entities.ForEach(entity =>
        {
            BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
            if (stats.currentStamina < minStamina)
            {
                minStamina = stats.currentStamina;
                target = entity;
            }
        });
        return target;
    }

    public GameObject GetEntityWithMostMP(List<GameObject> entities)
    {
        int maxMP = 0;
        GameObject target = entities[0];
        entities.ForEach(entity =>
        {
            BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
            if (stats.currentMP > maxMP)
            {
                target = entity;
                maxMP = stats.currentMP;
            }
        });
        return target;
    }
}
