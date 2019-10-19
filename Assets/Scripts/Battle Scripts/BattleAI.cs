using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAI
{
    public virtual EnemyActions ChooseAction()
    {
        Debug.Log("This gets called");
        return EnemyActions.Projectile;
    }

    public virtual GameObject ChooseTarget(List<GameObject> potentialTargets)
    {
        return potentialTargets[0];
    }
}
