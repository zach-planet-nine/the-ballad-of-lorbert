using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SludgeMonsterAI : BattleAI
{
    new public EnemyActions ChooseAction()
    {
        return EnemyActions.Projectile;
    }

    new public GameObject ChooseTarget(List<GameObject> potentialTargets)
    {
        return potentialTargets[0];
    }
}
