using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBotAI : BattleAI
{

    private ActionAndTarget ChooseProjectileSlowOrLaser(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.OneOfThree())
        {
            actionAndTarget.action = EnemyActions.Hourglass;
            actionAndTarget.target = GetEntityWithMostStamina(characters);
        } else
        {
            if(Randomness.CoinToss())
            {
                actionAndTarget.action = EnemyActions.Projectile;
                actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
            } else
            {
                actionAndTarget.action = EnemyActions.Laser;
                if(Randomness.CoinToss())
                {
                    actionAndTarget.target = GetEntityWithLeastHP(characters);
                } else
                {
                    actionAndTarget.target = GetEntityWithLeastMP(characters);
                }
            }
        }

        return actionAndTarget;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(enemies.Count == 4)
        {
            if(Randomness.OneOfThree())
            {
                actionAndTarget = ChooseProjectileSlowOrLaser(characters);
            }
        } else if(enemies.Count == 3)
        {
            if(Randomness.CoinToss())
            {
                actionAndTarget = ChooseProjectileSlowOrLaser(characters);
            }
        } else if(enemies.Count == 2)
        {
            if(Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileSlowOrLaser(characters);
            }

        } else
        {
            if(Randomness.ThreeOfFour())
            {
                actionAndTarget = ChooseProjectileSlowOrLaser(characters);
            }
        }

        return actionAndTarget;
    }

}
