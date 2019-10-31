using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidFlanAI : BattleAI
{

    private ActionAndTarget ChooseProjectileOrLiquidAttack(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.OneOfThree())
        {
            actionAndTarget.action = EnemyActions.LiquidAttack;
            actionAndTarget.target = GetEntityWithLeastHP(characters);
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

        if(enemies.Count == 2)
        {
            if(Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileOrLiquidAttack(characters);
            }
        } else
        {
            actionAndTarget = ChooseProjectileOrLiquidAttack(characters);
        }

        return actionAndTarget;
    }

}
