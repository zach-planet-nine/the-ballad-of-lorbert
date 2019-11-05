using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleMonsterAI : BattleAI
{

    private GameObject ChooseTargetForProjectile(List<GameObject> characters)
    {
        return GetEntityWithMostStamina(characters);
    }

    private ActionAndTarget ChooseTrashSludgeOrWave(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.CoinToss())
        {
            actionAndTarget.action = EnemyActions.Sludge;
            actionAndTarget.target = GetEntityWithMostStamina(characters);
        } else
        {
            if(Randomness.CoinToss())
            {
                actionAndTarget.action = EnemyActions.Trash;
                actionAndTarget.target = GetEntityWithLeastStamina(characters);
            } else
            {
                actionAndTarget.action = EnemyActions.Wave;
                actionAndTarget.target = GetEntityWithLeastStamina(characters);
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
            actionAndTarget = ChooseTrashSludgeOrWave(characters); 
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
