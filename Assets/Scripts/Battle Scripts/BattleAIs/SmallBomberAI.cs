using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBomberAI : BattleAI
{

    private ActionAndTarget ChooseProjectileOrSmallBomb(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        int roll = Randomness.GetIntBetween(0, 4);
        if(roll == 0)
        {
            actionAndTarget.action = EnemyActions.SmallBomb;
            actionAndTarget.target = GetEntityWithMostHP(characters);
        } else
        {
            actionAndTarget.action = EnemyActions.Projectile;
            int targetRoll = Randomness.GetIntBetween(0, 3);
            if(targetRoll == 0)
            {
                actionAndTarget.target = GetEntityWithLeastHP(characters);
            } else
            {
                actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
            }
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
                actionAndTarget = ChooseProjectileOrSmallBomb(characters);
            }
        } else
        {
            int oneOfFive = Randomness.GetIntBetween(0, 5);
            if(oneOfFive != 0)
            {
                actionAndTarget = ChooseProjectileOrSmallBomb(characters);
                if(actionAndTarget.action != EnemyActions.SmallBomb)
                {
                    actionAndTarget = ChooseProjectileOrSmallBomb(characters);
                }
            }
        }

        return actionAndTarget;
    }

}
