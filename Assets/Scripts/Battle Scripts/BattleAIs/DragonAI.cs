using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAI : BattleAI
{

    private ActionAndTarget ChooseFlamethrowerOrHomingMissile(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        GameObject entity = GetEntityWithMPBelowThreshold(characters, 0.4f);

        if(entity != null)
        {
            if(Randomness.TwoOfThree())
            {
                actionAndTarget.action = EnemyActions.Flamethrower;
                actionAndTarget.target = characters[0];
            } else
            {
                actionAndTarget.action = EnemyActions.HomingMissile;
                actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
            }
        } else
        {
            if(Randomness.CoinToss())
            {
                actionAndTarget.action = EnemyActions.Flamethrower;
                actionAndTarget.target = characters[0];
            } else
            {
                actionAndTarget.action = EnemyActions.HomingMissile;
                actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
            }
        }

        return actionAndTarget;
    }

    private ActionAndTarget ChooseProjectileOrSkill(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.OneOfThree())
        {
            actionAndTarget = ChooseFlamethrowerOrHomingMissile(characters);
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

        if(Randomness.ThreeOfFour())
        {
            actionAndTarget = ChooseProjectileOrSkill(characters);
        }

        return actionAndTarget;
    }

}
