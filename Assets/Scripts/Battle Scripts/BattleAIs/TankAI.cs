using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : BattleAI
{
    private bool hasBigBulletRecently;

    private EnemyActions ChooseSmallOrBigBullet()
    {
        EnemyActions action = EnemyActions.MachineGun;
        int roll = Randomness.GetIntBetween(0, 3);
        if(roll == 0)
        {
            if(hasBigBulletRecently)
            {
                hasBigBulletRecently = false;
            } else
            {
                action = EnemyActions.BigBullet;
            }
        }
        return action;
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        Debug.Log("Calling the tank ai");
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        int coinToss = Randomness.GetIntBetween(0, 2);
        if(coinToss == 0)
        {
            return actionAndTarget;
        }
        actionAndTarget.action = ChooseSmallOrBigBullet();
        actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
        return actionAndTarget;
    }
}
