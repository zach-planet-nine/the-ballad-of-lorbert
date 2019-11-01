using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkastBallPlayerAI : BattleAI
{


    private ActionAndTarget ChooseProjectileOrOrkastBall(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if (Randomness.OneOfFour())
        {
            actionAndTarget.action = EnemyActions.OrkastBall;
            actionAndTarget.target = GetEntityWithLeastMP(characters);
        }
        else
        {
            actionAndTarget.action = EnemyActions.Projectile;
            actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
        }

        return actionAndTarget;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if (enemies.Count == 4)
        {
            if (Randomness.CoinToss())
            {
                actionAndTarget = ChooseProjectileOrOrkastBall(characters);
            }
        }
        else if (enemies.Count == 3)
        {
            if (Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileOrOrkastBall(characters);
            }
        }
        else if (enemies.Count == 2)
        {
            if (Randomness.ThreeOfFour())
            {
                actionAndTarget = ChooseProjectileOrOrkastBall(characters);
            }

        }
        else
        {
            actionAndTarget = ChooseProjectileOrOrkastBall(characters);
        }

        return actionAndTarget;
    }
}
