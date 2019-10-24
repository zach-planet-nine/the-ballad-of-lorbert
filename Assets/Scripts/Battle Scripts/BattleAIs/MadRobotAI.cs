using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadRobotAI : BattleAI
{
    private int attacked;

    private ActionAndTarget ChooseBitOrMachineGun(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        if(attacked > 8)
        {
            actionAndTarget.action = EnemyActions.Bit;
            actionAndTarget.target = GetEntityWithMostHP(characters);
            attacked = 0;
        } else
        {
            int coinToss = Randomness.GetIntBetween(0, 2);
            if(coinToss == 1)
            {
                actionAndTarget.action = EnemyActions.MachineGun;
                actionAndTarget.target = characters[0];
            }
        }
        return actionAndTarget;
    }

    public override void Attacked(GameObject attacker)
    {
        attacked += 1;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        int roll = Randomness.GetIntBetween(0, 3);
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        switch(roll)
        {
            case 0: return actionAndTarget;
            case 1: actionAndTarget.action = EnemyActions.Projectile;
                actionAndTarget.target = GetEntityWithMostMP(characters);
                break;
            case 2: actionAndTarget.action = EnemyActions.MachineGun;
                break;
        }
        actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];

        return actionAndTarget;
    }
}
