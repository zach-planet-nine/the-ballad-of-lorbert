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

    public GameObject GetEntityWithLeastHP(List<GameObject> entities)
    {
        int minHP = 0;
        GameObject target = entities[0];
        entities.ForEach(entity =>
        {
            BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
            if (stats.currentHP < minHP)
            {
                target = entity;
                minHP = stats.currentHP;
            }
        });
        return target;
    }

    public GameObject GetEntityWithHPBelowThreshold(List<GameObject> entities, float threshold)
    {
        GameObject entityWithLowestHP = GetEntityWithLeastHP(entities);
        BattleStats stats = BattleManager.manager.GetStatsForEntity(entityWithLowestHP);
        if(stats.currentHP / stats.maxHP < threshold)
        {
            return entityWithLowestHP;
        } else
        {
            return null;
        }
    }

    public GameObject GetEntityWithMostStamina(List<GameObject> entities)
    {
        int maxStamina = 0;
        GameObject target = entities[0];
        entities.ForEach(entity =>
        {
            BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
            if (stats.currentStamina > maxStamina)
            {
                maxStamina = stats.currentStamina;
                target = entity;
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

    public GameObject GetEntityWithLeastMP(List<GameObject> entities)
    {
        int minMP = 10000;
        GameObject target = entities[0];
        entities.ForEach(entity =>
        {
            BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
            if (stats.currentMP < minMP)
            {
                target = entity;
                minMP = stats.currentMP;
            }
        });
        return target;
    }

    public GameObject GetEntityWithMPBelowThreshold(List<GameObject> entities, float threshold)
    {
        GameObject entityWithLowestMP = GetEntityWithLeastMP(entities);
        BattleStats stats = BattleManager.manager.GetStatsForEntity(entityWithLowestMP);
        if (stats.currentMP / stats.maxMP < threshold)
        {
            return entityWithLowestMP;
        }
        else
        {
            return null;
        }
    }

    public GameObject GetEntityWithMost(List<GameObject> entities, Stats stat)
    {
        int maxStat = 0;
        GameObject target = entities[0];
        switch (stat)
        {
            case Stats.Strength:
                entities.ForEach(entity =>
                {
                    BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
                    if (stats.strength > maxStat)
                    {
                        maxStat = stats.strength;
                        target = entity;
                    }
                });
                break;
            case Stats.Vitality:
                entities.ForEach(entity =>
                {
                    BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
                    if (stats.vitality > maxStat)
                    {
                        maxStat = stats.vitality;
                        target = entity;
                    }
                });
                break;
            case Stats.Agility:
                entities.ForEach(entity =>
                {
                    BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
                    if (stats.agility > maxStat)
                    {
                        maxStat = stats.strength;
                        target = entity;
                    }
                });
                break;
            case Stats.Dexterity:
                entities.ForEach(entity =>
                {
                    BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
                    if (stats.dexterity > maxStat)
                    {
                        maxStat = stats.dexterity;
                        target = entity;
                    }
                });
                break;
            case Stats.Wisdom:
                entities.ForEach(entity =>
                {
                    BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
                    if (stats.wisdom > maxStat)
                    {
                        maxStat = stats.wisdom;
                        target = entity;
                    }
                });
                break;
            case Stats.Aura:
                entities.ForEach(entity =>
                {
                    BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
                    if (stats.aura > maxStat)
                    {
                        maxStat = stats.aura;
                        target = entity;
                    }
                });
                break;
            case Stats.Perception:
                entities.ForEach(entity =>
                {
                    BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
                    if (stats.perception > maxStat)
                    {
                        maxStat = stats.perception;
                        target = entity;
                    }
                });
                break;
            case Stats.Luck:
                entities.ForEach(entity =>
                {
                    BattleStats stats = BattleManager.manager.GetStatsForEntity(entity);
                    if (stats.luck > maxStat)
                    {
                        maxStat = stats.luck;
                        target = entity;
                    }
                });
                break;
        }
        return target;
    }

    public bool CheckIfEntityInCountdown(List<GameObject> entities)
    {
        bool inCountdown = false;
        entities.ForEach(entity =>
        {
            if (BattleManager.manager.GetStatsForEntity(entity).isInCountdown)
            {
                inCountdown = true;
            }
        });
        return inCountdown;
    }
}
