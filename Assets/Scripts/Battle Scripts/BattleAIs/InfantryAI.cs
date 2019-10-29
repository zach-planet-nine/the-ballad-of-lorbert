using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryAI : BattleAI
{

    private ActionAndTarget ChooseProjectileMachineGunOrGrenade(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if(Randomness.CoinToss() && Randomness.OneOfFour())
        {
            actionAndTarget.action = EnemyActions.Grenade;
            actionAndTarget.target = characters[0];
        } else
        {
            if(Randomness.OneOfThree())
            {
                actionAndTarget.action = EnemyActions.MachineGunSingle;
                actionAndTarget.target = GetEntityWithLeastMP(characters);
            } else
            {
                actionAndTarget.action = EnemyActions.Projectile;
                actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
            }
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
                actionAndTarget = ChooseProjectileMachineGunOrGrenade(characters);
            }
        } else if(enemies.Count == 2)
        {
            if(Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileMachineGunOrGrenade(characters);
            }
        } else
        {
            if(Randomness.ThreeOfFour())
            {
                actionAndTarget = ChooseProjectileMachineGunOrGrenade(characters);
            }
        }

        return actionAndTarget;
    }

}
