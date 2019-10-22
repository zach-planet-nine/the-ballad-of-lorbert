﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerAmounts
{
    public GameObject attacker;
    public int attackAmount;
}

public class AttackBotAI : BattleAI
{

    private List<AttackerAmounts> amounts = new List<AttackerAmounts>();

    private bool AttackerAmountsContainsAttacker(GameObject attacker)
    {
        bool contains = false;
        amounts.ForEach(attackerAmount =>
        {
            if (attackerAmount.attacker == attacker)
            {
                contains = true;
            }
        });
        return contains;
    }

    private AttackerAmounts GetAttackerAmountsForAttacker(GameObject attacker)
    {
        AttackerAmounts amount = null;
        amounts.ForEach(attackerAmount =>
        {
            if (attackerAmount.attacker == attacker)
            {
                amount = attackerAmount;
            }
        });
        return amount;
    }

    private bool CheckIfShouldBit()
    {
        Debug.Log("Checking if should bit");
        bool shouldBit = false;
        amounts.ForEach(amount =>
        {
            Debug.Log("attackAmount is " + amount.attackAmount);
            if (amount.attackAmount >= 5)
            {
                shouldBit = true;
            }
        });
        return shouldBit;
    }

    private EnemyActions ChooseProjectileOrBit(List<GameObject> characters)
    {
        
        int bitRoll = Randomness.GetIntBetween(0, 3);
        if (bitRoll == 0 && CheckIfShouldBit())
        {
            Debug.Log("Should return bit");
            return EnemyActions.Bit;
        } else
        {
            return EnemyActions.Projectile;
        }
    }

    private GameObject ChooseTargetForBit(List<GameObject> characters)
    {
        int maxAmount = 0;
        GameObject target = characters[0];
        amounts.ForEach(amount =>
        {
            characters.ForEach(character =>
            {
                if (character == amount.attacker && amount.attackAmount > maxAmount)
                {
                    maxAmount = amount.attackAmount;
                    target = character;
                }
            });
        });
        Debug.Log("Should target: " + target.name);
        return target;
    }

    public override void Attacked(GameObject attacker)
    {
        Debug.Log("Attacked!");
        AttackerAmounts amount = GetAttackerAmountsForAttacker(attacker);
        if(amount == null)
        {
            amount = new AttackerAmounts();
            amount.attacker = attacker;
            amount.attackAmount = 1;
            amounts.Add(amount);
        } else
        {
            amount.attackAmount += 1;
        }
        Debug.Log("Now you've been attacked " + amount.attackAmount + " times");
        Debug.Log("And there are " + amounts.Count + " amounts");
    }

    public override ActionAndTarget ChooseActionAndTarget(List<GameObject> characters, List<GameObject> enemies)
    {
        ActionAndTarget actionAndTarget = new ActionAndTarget();
        if(enemies.Count == 4)
        {
            int oneOfThree = Randomness.GetIntBetween(0, 3);
            if(oneOfThree == 0)
            {
                actionAndTarget.action = ChooseProjectileOrBit(characters);
            }
        } else if(enemies.Count == 3)
        {
            int coinToss = Randomness.GetIntBetween(0, 2);
            if(coinToss == 0)
            {
                actionAndTarget.action = ChooseProjectileOrBit(characters);
            } 
        } else if(enemies.Count == 2)
        {
            int twoOfThree = Randomness.GetIntBetween(0, 3);
            if(twoOfThree > 0)
            {
                actionAndTarget.action = ChooseProjectileOrBit(characters);
            }
        }

        if(actionAndTarget.action == EnemyActions.Projectile)
        {
            actionAndTarget.target = characters[Randomness.GetIntBetween(0, characters.Count)];
        } else if(actionAndTarget.action == EnemyActions.Bit)
        {
            ChooseTargetForBit(characters);
        }

        return actionAndTarget;
    }
}
