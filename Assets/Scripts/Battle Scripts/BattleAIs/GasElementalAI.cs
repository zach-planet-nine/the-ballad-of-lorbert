using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasElementalAI : BattleAI
{

    private ActionAndTarget ChooseProjectileOrSkill(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.CoinToss())
        {
            actionAndTarget.action = EnemyActions.GasParticleAttack;
            actionAndTarget.target = GetEntityWithMostStamina(characters);
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

        if(enemies.Count == 4)
        {
            if(Randomness.OneOfThree())
            {
                actionAndTarget = ChooseProjectileOrSkill(characters);
            }
        } else if(enemies.Count == 3)
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
