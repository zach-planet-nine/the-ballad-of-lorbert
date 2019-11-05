using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineAI : BattleAI
{

    private ActionAndTarget ChooseGrenadeOrBaton(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        GameObject target = GetEntityWithMPBelowThreshold(characters, 0.5f);
        if(Randomness.OneOfThree())
        {
            actionAndTarget.action = EnemyActions.Grenade;
            actionAndTarget.target = characters[0];
        } else
        {
            if (target != null)
            {
                actionAndTarget.action = EnemyActions.Baton;
                actionAndTarget.target = target;
            } else
            {
                actionAndTarget.action = EnemyActions.Grenade;
                actionAndTarget.target = characters[0];
            }
        }
        

        return actionAndTarget;
    }

    private ActionAndTarget ChooseProjectileOrSkill(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.CoinToss())
        {
            actionAndTarget.action = EnemyActions.Projectile;
            actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
        } else
        {
            actionAndTarget = ChooseGrenadeOrBaton(characters);
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
                actionAndTarget = ChooseProjectileOrSkill(characters);
            }
        } else if(enemies.Count == 2)
        {
            if(Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileOrSkill(characters);
            }
        } else
        {
            if(Randomness.ThreeOfFour())
            {
                actionAndTarget = ChooseProjectileOrSkill(characters);
            }
        }

        return actionAndTarget;
    }
}
