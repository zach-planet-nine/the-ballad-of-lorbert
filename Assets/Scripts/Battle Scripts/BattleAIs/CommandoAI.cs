using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandoAI : BattleAI
{

    private ActionAndTarget ChooseProjectileMachineGunOrGrenade(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        if (Randomness.OneOfFour())
        {
            actionAndTarget.action = EnemyActions.Grenade;
            actionAndTarget.target = characters[0];
        } else if(Randomness.OneOfThree())
        {
            actionAndTarget.action = EnemyActions.MachineGunSingle;
            actionAndTarget.target = GetEntityWithMostMP(characters);
        } else if(Randomness.OneOfThree())
        {
            actionAndTarget.action = EnemyActions.MachineGun;
            actionAndTarget.target = characters[0];
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

        if(enemies.Count == 2)
        {
            if(Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileMachineGunOrGrenade(characters);
            }
        } else
        {
            actionAndTarget = ChooseProjectileMachineGunOrGrenade(characters);
        }

        return actionAndTarget;
    }
}
