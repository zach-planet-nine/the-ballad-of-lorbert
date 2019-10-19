using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SludgeMonsterAI : BattleAI
{
    public override EnemyActions ChooseAction()
    {
        int coinToss = Randomness.GetIntBetween(0, 2);
        if (coinToss == 1)
        {
            return EnemyActions.None;
        }
        return EnemyActions.Projectile;
    }

    public override GameObject ChooseTarget(List<GameObject> potentialTargets)
    {
        return potentialTargets[0];
    }
}
