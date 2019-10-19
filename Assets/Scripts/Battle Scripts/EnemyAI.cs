using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyActions
{
    Projectile,
    Sludge
}

public class EnemyAI : MonoBehaviour
{
    public BattleAI ai;

    public void Start()
    {
        switch(gameObject.name)
        {
            case "SludgeMonster": ai = new SludgeMonsterAI();
                break;
        }
    }

    public void DecideWhatToDo(List<GameObject> characters, List<GameObject> enemies, Action<bool> callback)
    {
        EnemyActions action = ai.ChooseAction();
        switch(action)
        {
            case EnemyActions.Projectile:
                GameObject target = ai.ChooseTarget(characters);
                int damage = BattleManager.manager.EntityAttacksEntity(gameObject, target);
                gameObject.GetComponent<Attack>().AttackEntityWithCallback(target, target.transform.position, damage, callback);
                break;
        }
    }
}
