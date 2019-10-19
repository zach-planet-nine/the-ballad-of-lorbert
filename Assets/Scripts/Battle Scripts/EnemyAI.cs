using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyActions
{
    None,
    Projectile,
    Sludge
}

public class EnemyAI : MonoBehaviour
{
    public BattleAI ai;
    public GameObject activeSelf;

    public void Start()
    {
        //I feel that there's a better way to do this, but I can't figure out what it is.
        if(gameObject.name.Contains("SludgeMonster"))
        {
            ai = new SludgeMonsterAI();
        }
        
    }

    public void DecideWhatToDo(List<GameObject> characters, List<GameObject> enemies, Action<bool> callback)
    {
        
        EnemyActions action = ai.ChooseAction();
        switch(action)
        {
            case EnemyActions.Projectile:
                activeSelf.SetActive(true);
                GameObject target = ai.ChooseTarget(characters);
                int damage = BattleManager.manager.EntityAttacksEntity(gameObject, target);
                activeSelf.GetComponent<Attack>().AttackEntityWithCallback(target, target.transform.position, damage, callback);
                break;
        }
    }
}
