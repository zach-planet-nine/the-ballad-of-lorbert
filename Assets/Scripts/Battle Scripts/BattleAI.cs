using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAI
{
    public EnemyActions ChooseAction()
    {
        return EnemyActions.Projectile;
    }

    public GameObject ChooseTarget(List<GameObject> potentialTargets)
    {
        return potentialTargets[0];
    }
}
