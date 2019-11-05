using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidFlanAI : BattleAI
{


    private ActionAndTarget ChooseProjectileOrSolidAttack(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if (Randomness.OneOfThree())
        {
            actionAndTarget.action = EnemyActions.SolidAttack;
            actionAndTarget.target = GetEntityWithMostHP(characters);
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

        if(enemies.Count == 3)
        {
            if(Randomness.CoinToss())
            {
                actionAndTarget = ChooseProjectileOrSolidAttack(characters);
            }
        }
        else if (enemies.Count == 2)
        {
            if (Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileOrSolidAttack(characters);
            }
        }
        else
        {
            actionAndTarget = ChooseProjectileOrSolidAttack(characters);
        }

        return actionAndTarget;
    }
}
