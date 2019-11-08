using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaFlanAI : BattleAI
{

    private ActionAndTarget ChooseProjectileOrPlasmaAttack(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.OneOfThree())
        {
            actionAndTarget.action = EnemyActions.PlasmaAttack;
            actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
        } else
        {
            actionAndTarget.action = EnemyActions.Projectile;
            if(Randomness.CoinToss())
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

        if(enemies.Count == 4)
        {
            if(Randomness.OneOfThree())
            {
                actionAndTarget = ChooseProjectileOrPlasmaAttack(characters);
            }
        } else if(enemies.Count == 3)
        {
            if(Randomness.CoinToss())
            {
                actionAndTarget = ChooseProjectileOrPlasmaAttack(characters);
            }
        } else if(enemies.Count == 2)
        {
            if(Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileOrPlasmaAttack(characters);
            }
        } else
        {
            if(Randomness.ThreeOfFour())
            {
                actionAndTarget = ChooseProjectileOrPlasmaAttack(characters);
            }
        }

        return actionAndTarget;
    }
}
