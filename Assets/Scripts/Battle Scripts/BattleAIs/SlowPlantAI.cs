using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlantAI : BattleAI
{

    private GameObject ChooseTargetForHourglass(List<GameObject> characters)
    {
        GameObject potentialTarget = GetEntityWithLeastStamina(characters);
        if(BattleManager.manager.GetStatsForEntity(potentialTarget).isSlowedStamina)
        {
            return null;
        }
        return potentialTarget;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        int roll = Randomness.GetIntBetween(0, 4);
        if(roll != 0)
        {
            actionAndTarget.action = EnemyActions.Projectile;
            actionAndTarget.target = GetEntityWithMostHP(characters);
        } else
        {
            actionAndTarget.action = EnemyActions.Hourglass;
            actionAndTarget.target = ChooseTargetForHourglass(characters);
            if(actionAndTarget.target == null)
            {
                actionAndTarget.action = EnemyActions.Projectile;
                actionAndTarget.target = GetEntityWithMostHP(characters);
            }
        }
        return actionAndTarget;
    }
}
