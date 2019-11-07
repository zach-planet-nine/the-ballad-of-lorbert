using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasFlanAI : BattleAI
{

    private ActionAndTarget ChooseProjectileOrSkill(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        GameObject entity = GetEntityThatIsGassed(characters);
        if(entity == null)
        {
            actionAndTarget.action = EnemyActions.GasAttack;
            actionAndTarget.target = GetEntityWithMostHP(characters);
        } else
        {
            actionAndTarget.action = EnemyActions.Projectile;
            if(Randomness.CoinToss())
            {
                actionAndTarget.target = entity;
            } else
            {
                actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
            }
        }

        return actionAndTarget;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        return ChooseProjectileOrSkill(characters);
    }

}
