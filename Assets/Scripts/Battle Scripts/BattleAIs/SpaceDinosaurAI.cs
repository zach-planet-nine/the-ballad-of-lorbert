using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDinosaurAI : BattleAI
{

    private ActionAndTarget ChooseClawOrLaser(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.CoinToss())
        {
            actionAndTarget.action = EnemyActions.Claw;
            if(Randomness.CoinToss())
            {
                actionAndTarget.target = GetEntityWithLeastHP(characters);
            } else
            {
                actionAndTarget.target = GetEntityWithMostHP(characters);
            }
        } else
        {
            actionAndTarget.action = EnemyActions.Laser;
            actionAndTarget.target = GetEntityWithMostMP(characters);
        }

        return actionAndTarget;
    }

    private ActionAndTarget ChooseProjectileOrSkill(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.CoinToss())
        {
            actionAndTarget = ChooseClawOrLaser(characters);
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

        actionAndTarget = ChooseProjectileOrSkill(characters);

        return actionAndTarget;
    }

}
