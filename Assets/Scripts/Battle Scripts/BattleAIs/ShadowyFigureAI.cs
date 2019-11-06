using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowyFigureAI : BattleAI
{
    private GameObject ChooseTargetForProjectile(List<GameObject> characters)
    {
        return GetEntityWithMostStamina(characters);
    }

    private ActionAndTarget ChooseSwordSlashHourglassOrAgileStar(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if (Randomness.CoinToss())
        {
            actionAndTarget.action = EnemyActions.SwordSlash;
            actionAndTarget.target = GetEntityWithMostHP(characters);
        }
        else
        {
            if (Randomness.CoinToss())
            {
                actionAndTarget.action = EnemyActions.StaminaSlowAttack;
                actionAndTarget.target = GetEntityWithLeastStamina(characters);
            }
            else
            {
                actionAndTarget.action = EnemyActions.AgileStar;
                actionAndTarget.target = GetEntityWithLeastHP(characters);
            }
        }

        return actionAndTarget;
    }

    private ActionAndTarget ChooseProjectileOrSkill(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if (Randomness.CoinToss())
        {
            actionAndTarget.action = EnemyActions.Projectile;
            actionAndTarget.target = ChooseTargetForProjectile(characters);
        }
        else
        {
            actionAndTarget = ChooseSwordSlashHourglassOrAgileStar(characters);
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
