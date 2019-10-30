using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperAI : BattleAI
{

    private GameObject GetTargetForProjectile(List<GameObject> characters)
    {
        return characters[Randomness.GetIntBetween(0, characters.Count)];
    }

    private bool characterIsInCountdown(List<GameObject> characters)
    {
        bool isInCountdown = false;
        characters.ForEach(character =>
        {
            if(BattleManager.manager.GetStatsForEntity(character).isInCountdown)
            {
                isInCountdown = true;
            }
        });
        return isInCountdown;
    }

    private GameObject GetTargetForScythe(List<GameObject> characters)
    {
        int maxHP = 0;
        GameObject target = null;
        if(characterIsInCountdown(characters))
        {
            return target;
        }
        characters.ForEach(character =>
        {
            BattleStats stats = BattleManager.manager.GetStatsForEntity(character);
            
            if (!stats.isInCountdown && stats.currentHP > maxHP)
            {
                maxHP = stats.currentHP;
                target = character;
            }
        });
        return target;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        Debug.Log("There are " + characters.Count + " characters");
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        int roll = Randomness.GetIntBetween(0, 10);
        if(roll == 0)
        {
            actionAndTarget.action = EnemyActions.Scythe;
            actionAndTarget.target = GetTargetForScythe(characters);
            if (actionAndTarget.target == null)
            {
                actionAndTarget.action = EnemyActions.Projectile;
                actionAndTarget.target = GetTargetForProjectile(characters);
            }
        } else
        {
            actionAndTarget.action = EnemyActions.Projectile;
            actionAndTarget.target = GetTargetForProjectile(characters);
        }
        return actionAndTarget;
    }
}
