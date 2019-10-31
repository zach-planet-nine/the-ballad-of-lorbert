using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidElementalAI : BattleAI
{


    private ActionAndTarget ChooseProjectileOrWave(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if (Randomness.OneOfThree())
        {
            actionAndTarget.action = EnemyActions.Wave;
            actionAndTarget.target = GetEntityWithLeastHP(characters);
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
                actionAndTarget = ChooseProjectileOrWave(characters);
            }
        }
        else
        {
            actionAndTarget = ChooseProjectileOrWave(characters);
        }

        return actionAndTarget;
    }
}
