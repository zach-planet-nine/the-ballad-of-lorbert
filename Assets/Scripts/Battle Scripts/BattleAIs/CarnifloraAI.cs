using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnifloraAI : BattleAI
{
    private bool shouldAttack;
    private int reviveCountdown = 16;

    private GameObject GetTargetForTangle(List<GameObject> characters)
    {
        return characters[0];
    }

    private ActionAndTarget ChooseProjectileBranchOrTangle(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        int tangleRoll = Randomness.GetIntBetween(0, 8);
        if(tangleRoll == 0)
        {
            GameObject target = GetTargetForTangle(characters);
            if(target == null)
            {
                actionAndTarget = ChooseProjectileBranchOrTangle(characters);
            } else
            {
                actionAndTarget.action = EnemyActions.Tangle;
                actionAndTarget.target = target;
            }
        } else
        {
            int branchRoll = Randomness.GetIntBetween(0, 3);
            if(branchRoll == 0)
            {
                actionAndTarget.action = EnemyActions.Branch;
                actionAndTarget.target = GetEntityWithMostStamina(characters);
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
        if (enemies.Count == 3)
        {
            int oneOfThree = Randomness.GetIntBetween(0, 3);
            if (oneOfThree == 0)
            {
                actionAndTarget = ChooseProjectileBranchOrTangle(characters);
            }
        }
        else if (enemies.Count == 2)
        {
            int coinToss = Randomness.GetIntBetween(0, 2);
            if (coinToss == 0)
            {
                actionAndTarget = ChooseProjectileBranchOrTangle(characters);
            }
        }
        else
        {
            reviveCountdown -= 1;
            if(reviveCountdown <= 0)
            {
                reviveCountdown = 16;
                actionAndTarget.action = EnemyActions.CarnifloraRevive;
                actionAndTarget.target = enemies[0];
            } else
            {
                int roll = Randomness.GetIntBetween(0, 3);
                if (roll != 0)
                {
                    actionAndTarget = ChooseProjectileBranchOrTangle(characters);
                }
            }
        }

        if (actionAndTarget.action == EnemyActions.None && shouldAttack)
        {
            actionAndTarget = ChooseProjectileBranchOrTangle(characters);
        }
        else if (actionAndTarget.action == EnemyActions.None && !shouldAttack)
        {
            shouldAttack = true;
        }

        return actionAndTarget;
    }
}
