using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBunnyAI : BattleAI
{

    private ActionAndTarget ChoosePollenOrHealing(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        GameObject entity = GetEntityWithHPBelowThreshold(enemies, 0.5f);
        if(entity != null)
        {
            if(Randomness.OneOfThree())
            {
                actionAndTarget.action = EnemyActions.Healing;
                actionAndTarget.target = entity;
            } else
            {
                actionAndTarget.action = EnemyActions.Pollen;
                actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
            }
        } else
        {
            if(Randomness.CoinToss())
            {
                actionAndTarget.action = EnemyActions.Healing;
                actionAndTarget.target = GetEntityWithMostHP(enemies);
            } else
            {
                actionAndTarget.action = EnemyActions.Pollen;
                actionAndTarget.target = GetEntityWithMostStamina(characters);
            }
        }

        return actionAndTarget;
    }

    private ActionAndTarget ChooseProjectileOrSkill(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.CoinToss())
        {
            actionAndTarget = ChoosePollenOrHealing(characters, enemies);
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

        if(enemies.Count == 3)
        {
            if(Randomness.CoinToss())
            {
                actionAndTarget = ChooseProjectileOrSkill(characters, enemies);
            }
        } else if(enemies.Count == 2)
        {
            if(Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileOrSkill(characters, enemies);
            }
        } else
        {
            if(Randomness.ThreeOfFour())
            {
                actionAndTarget = ChooseProjectileOrSkill(characters, enemies);
            }
        }

        return actionAndTarget;
    }
}
