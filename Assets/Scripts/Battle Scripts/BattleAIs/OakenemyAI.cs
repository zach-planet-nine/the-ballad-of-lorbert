using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OakenemyAI : BattleAI
{

    private ActionAndTarget ChooseProjectileOrFire(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        int roll = Randomness.GetIntBetween(0, 3);
        if(roll == 0)
        {
            actionAndTarget.action = EnemyActions.Fire;
            actionAndTarget.target = characters[0];
        } else
        {
            actionAndTarget.action = EnemyActions.Projectile;
            actionAndTarget.target = GetEntityWithMostHP(characters);
        }
        return actionAndTarget;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        if(enemies.Count == 3)
        {
            int coinToss = Randomness.GetIntBetween(0, 2);
            if(coinToss == 0)
            {
                actionAndTarget = ChooseProjectileOrFire(characters);
            }
        }
        else if(enemies.Count == 2)
        {
            int oneOfThree = Randomness.GetIntBetween(0, 3);
            if(oneOfThree != 0)
            {
                actionAndTarget = ChooseProjectileOrFire(characters);
            }
        } else
        {
            actionAndTarget = ChooseProjectileOrFire(characters);
        }

        return actionAndTarget;
    }
}
