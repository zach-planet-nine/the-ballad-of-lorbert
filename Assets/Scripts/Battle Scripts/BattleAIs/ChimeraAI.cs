using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraAI : BattleAI
{
    private int attackedNumber;
    private int attackedThreshold;

    private ActionAndTarget ChooseIceOrScythe(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(attackedNumber >= attackedThreshold)
        {
            if(!CheckIfEntityInCountdown(characters) && Randomness.CoinToss())
            {
                actionAndTarget.action = EnemyActions.Scythe;
                actionAndTarget.target = GetEntityWithMostMP(characters);
            } else
            {
                actionAndTarget.action = EnemyActions.Ice;
                actionAndTarget.target = characters[0];
            }
        } else
        {
            actionAndTarget.action = EnemyActions.Ice;
            actionAndTarget.target = characters[0];
        }

        return actionAndTarget;
    }

    private ActionAndTarget ChooseProjectileOrSkill(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.OneOfThree())
        {
            actionAndTarget = ChooseIceOrScythe(characters);
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

    public override void Attacked(GameObject attacker)
    {
        attackedNumber += 1;
    }

}
