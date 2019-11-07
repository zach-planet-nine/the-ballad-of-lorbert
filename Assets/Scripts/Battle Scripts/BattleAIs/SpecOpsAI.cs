using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecOpsAI : BattleAI
{

    private ActionAndTarget ChooseProjectileOrMissile(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.CoinToss())
        {
            actionAndTarget.action = EnemyActions.HomingMissile;
            actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
        } else
        {
            actionAndTarget.action = EnemyActions.Projectile;
            actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
        }

        return actionAndTarget;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(enemies.Count == 3)
        {
            if(Randomness.CoinToss())
            {
                actionAndTarget = ChooseProjectileOrMissile(characters);
            }
        } else if(enemies.Count == 2)
        {
            if(Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileOrMissile(characters);
            }
        } else
        {
            if(Randomness.ThreeOfFour())
            {
                actionAndTarget = ChooseProjectileOrMissile(characters);
            }
        }

        return actionAndTarget;
    }
}
