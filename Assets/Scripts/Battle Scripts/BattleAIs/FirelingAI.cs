using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirelingAI :  BattleAI
{
    private GameObject ChooseTargetForPollen(List<GameObject> characters)
    {
        if (characters.Count == 0)
        {
            return null;
        }
        GameObject target = GetEntityWithMost(characters, Stats.Perception);
        if (BattleManager.manager.GetStatsForEntity(target).isBlinded)
        {
            characters.Remove(target);
            return ChooseTargetForPollen(characters);
        }
        else
        {
            return target;
        }
    }

    private ActionAndTarget ChooseProjectileOrPollen(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        int roll = Randomness.GetIntBetween(0, 5);
        if (roll == 0)
        {
            actionAndTarget.action = EnemyActions.Pollen;
            actionAndTarget.target = ChooseTargetForPollen(characters);
        }
        else
        {
            actionAndTarget.action = EnemyActions.Projectile;
            actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
        }

        return actionAndTarget;

    }

    private ActionAndTarget ChooseProjectileFireOrPollen(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        int roll = Randomness.GetIntBetween(0, 3);
        if(roll == 0)
        {
            actionAndTarget.action = EnemyActions.Fire;
            actionAndTarget.target = characters[0];
        } else
        {
            actionAndTarget = ChooseProjectileOrPollen(characters);
        }
        return actionAndTarget;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        if (enemies.Count == 4)
        {
            int coinToss = Randomness.GetIntBetween(0, 2);
            if (coinToss == 0)
            {
                actionAndTarget = ChooseProjectileFireOrPollen(characters);
            }

        }
        else if (enemies.Count == 3)
        {
            int oneOfThree = Randomness.GetIntBetween(0, 3);
            if (oneOfThree != 0)
            {
                actionAndTarget = ChooseProjectileFireOrPollen(characters);
            }
        }
        else if (enemies.Count == 2)
        {
            int oneOfFour = Randomness.GetIntBetween(0, 4);
            if (oneOfFour != 0)
            {
                actionAndTarget = ChooseProjectileFireOrPollen(characters);
            }
        }
        else
        {
            int coinToss = Randomness.GetIntBetween(0, 2);
            if (coinToss == 0)
            {
                actionAndTarget.action = EnemyActions.Pollen;
                actionAndTarget.target = ChooseTargetForPollen(characters);
            }
            else
            {
                actionAndTarget.action = EnemyActions.Projectile;
                actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
            }
        }

        if (actionAndTarget.action == EnemyActions.Pollen && actionAndTarget.target == null)
        {
            actionAndTarget.action = EnemyActions.Projectile;
            actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
        }

        return actionAndTarget;
    }
}
