using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurretAI : BattleAI
{
    private int shouldAttackCounter = 2;
    private int shouldAttackThreshold = 4;

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();

        shouldAttackCounter += 1;

        if(shouldAttackCounter >= shouldAttackThreshold)
        {
            actionAndTarget.action = EnemyActions.MachineGun;
            actionAndTarget.target = characters[0];

            shouldAttackCounter = 0;
        }

        return actionAndTarget;
    }
}
