using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyActions
{
    None,
    Projectile,
    Sludge,
    StealMP,
    DischargeStoredEnergy
}

public class EnemyAI : MonoBehaviour
{
    public BattleAI ai;
    public GameObject activeSelf;

    public void Start()
    {
        //I feel that there's a better way to do this, but I can't figure out what it is.
        if (gameObject.name.Contains("SludgeMonster"))
        {
            ai = new SludgeMonsterAI();
        }
        else if (gameObject.name.Contains("MPStealer"))
        {
            ai = new MPStealerAI();
        }
        else
        {
            ai = new SludgeMonsterAI();
        }
        
    }

    public void DecideWhatToDo(List<GameObject> characters, List<GameObject> enemies, Action<bool> callback)
    {
        ActionAndTarget actionAndTarget = ai.ChooseActionAndTarget(characters, enemies);
        switch(actionAndTarget.action)
        {
            case EnemyActions.Projectile:
                activeSelf.SetActive(true);
                int damage = BattleManager.manager.EntityAttacksEntity(gameObject, actionAndTarget.target);
                activeSelf.GetComponent<Attack>().AttackEntityWithCallback(actionAndTarget.target, actionAndTarget.target.transform.position, damage, callback);
                break;
            case EnemyActions.Sludge:
                activeSelf.SetActive(true);
                int staminaDamage = BattleManager.manager.EntityStaminaAttacksEntity(gameObject, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().SludgeEntityWithCallback(actionAndTarget.target, actionAndTarget.target.transform.position, staminaDamage, callback);
                break;
            case EnemyActions.StealMP:
                Debug.Log("Steal MP here");
                activeSelf.SetActive(true);
                int mpDamage = BattleManager.manager.EntityMPAttacksEntity(gameObject, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().StealMPWithCallback(actionAndTarget.target, actionAndTarget.target.transform.position, mpDamage, callback);
                break;
            case EnemyActions.DischargeStoredEnergy:
                Debug.Log("Discharge energy here");
                break;
        }
    }
}
