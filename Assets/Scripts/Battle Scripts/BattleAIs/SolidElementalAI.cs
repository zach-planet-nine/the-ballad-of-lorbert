using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidElementalAI : BattleAI
{
    private ActionAndTarget ChooseProjectileOrStalactite(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if (Randomness.OneOfThree())
        {
            actionAndTarget.action = EnemyActions.Stalactite;
            actionAndTarget.target = GetEntityWithLeastStamina(characters);
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
                actionAndTarget = ChooseProjectileOrStalactite(characters);
            }
        }
        else
        {
            actionAndTarget = ChooseProjectileOrStalactite(characters);
        }

        return actionAndTarget;
    }
}
