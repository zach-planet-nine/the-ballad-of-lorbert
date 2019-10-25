using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizenTreeAI : BattleAI
{

    private bool shouldAttack;

    private ActionAndTarget ChooseProjectileBranchOrMPSlow(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        GameObject characterWithLeastMP = GetEntityWithLeastMP(characters);
        if(!BattleManager.manager.GetStatsForEntity(characterWithLeastMP).isSlowedMP)
        {
            actionAndTarget.action = EnemyActions.MPSlow;
            actionAndTarget.target = characterWithLeastMP;
        } else
        {
            int roll = Randomness.GetIntBetween(0, 3);
            if(roll == 0)
            {
                actionAndTarget.action = EnemyActions.Branch;
                actionAndTarget.target = GetEntityWithMostMP(characters);
            } else
            {
                actionAndTarget.action = EnemyActions.Projectile;
                actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
            }
        }
        return actionAndTarget;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        if(enemies.Count == 3)
        {
            int oneOfThree = Randomness.GetIntBetween(0, 3);
            if(oneOfThree == 0)
            {
                actionAndTarget = ChooseProjectileBranchOrMPSlow(characters);
            }
        } else if(enemies.Count == 2)
        {
            int coinToss = Randomness.GetIntBetween(0, 2);
            if(coinToss == 0)
            {
                actionAndTarget = ChooseProjectileBranchOrMPSlow(characters);
            }
        } else
        {
            int roll = Randomness.GetIntBetween(0, 3);
            if(roll != 0)
            {
                actionAndTarget = ChooseProjectileBranchOrMPSlow(characters);
            }
        }

        if(actionAndTarget.action == EnemyActions.None && shouldAttack)
        {
            actionAndTarget = ChooseProjectileBranchOrMPSlow(characters);
        } else if(actionAndTarget.action == EnemyActions.None && !shouldAttack)
        {
            shouldAttack = true;
        }

        return actionAndTarget;
    }

}
