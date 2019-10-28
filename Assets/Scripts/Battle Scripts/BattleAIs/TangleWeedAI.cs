using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangleWeedAI : BattleAI
{

    private ActionAndTarget ChooseProjectileOrNeedles(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        int roll = Randomness.GetIntBetween(0, 3);
        if(roll == 0)
        {
            actionAndTarget.action = EnemyActions.Needle;
            actionAndTarget.target = GetEntityWithMostHP(characters);
        } else
        {
            actionAndTarget.action = EnemyActions.Projectile;
            actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
        }
        return actionAndTarget;
    }

    private ActionAndTarget ChooseProjectileTangleOrNeedles(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        GameObject entityMeetsThreshold = GetEntityWithHPBelowThreshold(enemies, 0.6f);
        if(entityMeetsThreshold != null)
        {
            int roll = Randomness.GetIntBetween(0, 4);
            if(roll == 0)
            {
                actionAndTarget.action = EnemyActions.Tangle;
                actionAndTarget.target = GetEntityWithMostMP(characters);
                if(BattleManager.manager.GetStatsForEntity(actionAndTarget.target).isStopped)
                {
                    actionAndTarget = ChooseProjectileOrNeedles(characters);
                }
            } else
            {
                actionAndTarget = ChooseProjectileOrNeedles(characters);
            }
        } else
        {
            actionAndTarget = ChooseProjectileOrNeedles(characters);
        }

        return actionAndTarget;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(enemies.Count == 2)
        {
            int oneOfThree = Randomness.GetIntBetween(0, 3);
            if(oneOfThree != 0)
            {
                actionAndTarget = ChooseProjectileTangleOrNeedles(characters, enemies);
            }
        } else
        {
            GameObject entityMeetsThreshold = GetEntityWithHPBelowThreshold(enemies, 0.5f);
            if(entityMeetsThreshold != null)
            {
                actionAndTarget = ChooseProjectileTangleOrNeedles(characters, enemies);
            } else
            {
                int oneOfFour = Randomness.GetIntBetween(0, 4);
                if(oneOfFour != 0)
                {
                    actionAndTarget = ChooseProjectileTangleOrNeedles(characters, enemies);
                }
            }
        }

        return actionAndTarget;
    }
}
