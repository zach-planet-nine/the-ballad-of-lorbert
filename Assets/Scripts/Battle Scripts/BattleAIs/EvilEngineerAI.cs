using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilEngineerAI : BattleAI
{

    private ActionAndTarget ChooseHomingMissilesPoisonPillsOrNeedles(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.OneOfThree())
        {
            actionAndTarget.action = EnemyActions.HomingMissiles;
            actionAndTarget.target = characters[0];
        } else
        {
            GameObject entity = GetEntityWithMPBelowThreshold(characters, 0.5f);
            if(entity != null)
            {
                actionAndTarget.action = EnemyActions.Needle;
                actionAndTarget.target = entity;
            } else
            {
                actionAndTarget.action = EnemyActions.PoisonPills;
                actionAndTarget.target = GetEntityWithMostMP(characters);
            }
        }

        return actionAndTarget;
    }

    private ActionAndTarget ChooseProjectileOrSkill(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.OneOfThree())
        {
            actionAndTarget = ChooseHomingMissilesPoisonPillsOrNeedles(characters);
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
