using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ActionAndTarget
{
    public EnemyActions action;
    public GameObject target;
    public bool allCharacters;
    public bool allEnemies;
}

public class BattleAI
{
    public virtual void Attacked(GameObject attacker)
    {

    }

    public virtual ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        return new ActionAndTarget();
    }

    public virtual EnemyActions ChooseAction(List<GameObject> characters, List<GameObject> enemies)
    {
        Debug.Log("This gets called");
        return EnemyActions.Projectile;
    }

    public virtual GameObject ChooseTarget(List<GameObject> potentialTargets)
    {
        return potentialTargets[0];
    }
}
