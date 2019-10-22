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
    DischargeStoredEnergy,
    Bit
}

public class EnemyAI : MonoBehaviour
{
    public BattleAI ai;
    public GameObject activeSelf;

    public void Start()
    {
        /*int zeros = 0;
        int ones = 0;
        int twos = 0;
        int threes = 0;
        int fours = 0;
        for(int i = 0; i < 1000; i++)
        {
            switch(Randomness.GetIntBetween(0, 5))
            {
                case 0: zeros++;
                    break;
                case 1: ones++;
                    break;
                case 2: twos++;
                    break;
                case 3: threes++;
                    break;
                case 4: fours++;
                    break;
            }
        }
        Debug.Log("zeros: " + zeros);
        Debug.Log("ones: " + ones);
        Debug.Log("twos: " + twos);
        Debug.Log("threes: " + threes);
        Debug.Log("fours: " + fours);*/
        //I feel that there's a better way to do this, but I can't figure out what it is.
        if (gameObject.name.Contains("SludgeMonster"))
        {
            ai = new SludgeMonsterAI();
        }
        else if (gameObject.name.Contains("MPStealer"))
        {
            ai = new MPStealerAI();
        } else if(gameObject.name.Contains("AttackBot"))
        {
            ai = new AttackBotAI();
        }
        else
        {
            ai = new SludgeMonsterAI();
        }
        
    }

    public void Attacked(GameObject attacker)
    {
        ai.Attacked(attacker);
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
                activeSelf.SetActive(true);
                int dischargeDamage = BattleManager.manager.EntityDischargesMPAtEntity(gameObject, actionAndTarget.target);
                activeSelf.GetComponent<EnemyAbilities>().DischargeMPWithCallback(actionAndTarget.target, actionAndTarget.target.transform.position, dischargeDamage, callback);
                break;
            case EnemyActions.Bit:
                Debug.Log("Should deploy the bit here");
                activeSelf.SetActive(true);
                activeSelf.GetComponent<EnemyAbilities>().AttackWithBit(actionAndTarget.target, activeSelf, callback);
                break;
        }
    }
}
