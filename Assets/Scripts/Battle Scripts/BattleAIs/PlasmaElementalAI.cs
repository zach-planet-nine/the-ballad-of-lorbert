using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaElementalAI : BattleAI
{
    private ActionAndTarget ChooseProjectileOrPlasmaParticleAttack(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if (Randomness.OneOfThree())
        {
            actionAndTarget.action = EnemyActions.PlasmaParticleAttack;
            if(Randomness.CoinToss())
            {
                actionAndTarget.target = GetEntityWithLeastHP(characters);
            } else
            {
                actionAndTarget.target = GetEntityWithMostMP(characters);
            }
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

        if (enemies.Count == 2)
        {
            if (Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileOrPlasmaParticleAttack(characters);
            }
        }
        else
        {
            actionAndTarget = ChooseProjectileOrPlasmaParticleAttack(characters);
        }

        return actionAndTarget;
    }
}
