using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowIOAI : BattleAI
{
    private ActionAndTarget ChooseSpell(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        int roll = Randomness.GetIntBetween(0, 4);

        switch (roll)
        {
            case 0:
                actionAndTarget.action = EnemyActions.LiquidAttack;
                actionAndTarget.target = GetEntityWithLeastMP(characters);
                break;
            case 1:
                actionAndTarget.action = EnemyActions.SolidAttack;
                actionAndTarget.target = GetEntityWithMostMP(characters);
                break;
            case 2:
                actionAndTarget.action = EnemyActions.Healing;
                actionAndTarget.target = GetEntityWithLeastHP(enemies);
                break;
            case 3:
                actionAndTarget.action = EnemyActions.PlasmaAttack;
                actionAndTarget.target = GetEntityWithMostStamina(characters);
                break;
        }

        return actionAndTarget;
    }

    private ActionAndTarget ChooseProjectileOrSpell(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if (Randomness.TwoOfThree())
        {
            actionAndTarget = ChooseSpell(characters, enemies);
        }
        else
        {
            actionAndTarget.action = EnemyActions.Projectile;
            if (Randomness.CoinToss())
            {
                actionAndTarget.target = GetEntityWithLeastMP(characters);
            }
            else
            {
                actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
            }
        }

        return actionAndTarget;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if (enemies.Count == 3)
        {
            if (Randomness.CoinToss())
            {
                actionAndTarget = ChooseProjectileOrSpell(characters, enemies);
            }
        }
        else if (enemies.Count == 2)
        {
            if (Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileOrSpell(characters, enemies);
            }
        }
        else
        {
            if (Randomness.ThreeOfFour())
            {
                actionAndTarget = ChooseProjectileOrSpell(characters, enemies);
            }
        }

        return actionAndTarget;
    }
}
