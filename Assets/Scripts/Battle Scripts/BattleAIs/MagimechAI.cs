using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagimechAI : BattleAI
{

    private ActionAndTarget ChooseFireIceOrBolt(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        int roll = Randomness.GetIntBetween(0, 3);
        switch(roll)
        {
            case 0: actionAndTarget.action = EnemyActions.Fire;
                break;
            case 1: actionAndTarget.action = EnemyActions.Ice;
                break;
            case 2: actionAndTarget.action = EnemyActions.Bolt;
                break;
        }
        actionAndTarget.target = characters[0];
        return actionAndTarget;
    }

    private ActionAndTarget ChooseProjectileOrMagic(List<GameObject> characters)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        int oneOfThree = Randomness.GetIntBetween(0, 3);
        if(oneOfThree == 0)
        {
            actionAndTarget = ChooseFireIceOrBolt(characters);
        } else
        {
            actionAndTarget.action = EnemyActions.Projectile;
            actionAndTarget.target = GetEntityWithLeastMP(characters);
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
                actionAndTarget = ChooseProjectileOrMagic(characters);
            }
        } else if(enemies.Count == 2)
        {
            if(Randomness.TwoOfThree())
            {
                actionAndTarget = ChooseProjectileOrMagic(characters);
            }
        } else
        {
            if(Randomness.ThreeOfFour())
            {
                actionAndTarget = ChooseProjectileOrMagic(characters);
            }
        }

        return actionAndTarget;
    }

}
