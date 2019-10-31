using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementalEffects
{
    None,
    Weak,
    Strong,
    Absorb
}

public class EnemyStats : MonoBehaviour
{
    public int hp;
    public int stamina;
    public int mp;
    public int strength;
    public int vitality;
    public int agility;
    public int dexterity;
    public int wisdom;
    public int aura;
    public int perception;
    public int luck;

    public ElementalEffects liquidEffect;
    public ElementalEffects solidEffect;
    public ElementalEffects gasEffect;
    public ElementalEffects plasmaEffect;

    public BattleStats GetBattleStats()
    {
        BattleStats stats = new BattleStats();
        stats.currentHP = stats.maxHP = hp;
        stats.currentStamina = stats.maxStamina = stamina;
        stats.currentMP = stats.maxMP = mp;
        stats.strength = strength;
        stats.vitality = vitality;
        stats.agility = agility;
        stats.dexterity = dexterity;
        stats.wisdom = wisdom;
        stats.aura = aura;
        stats.perception = perception;
        stats.luck = luck;

        stats.liquidEffect = liquidEffect;
        stats.solidEffect = solidEffect;
        stats.gasEffect = gasEffect;
        stats.plasmaEffect = plasmaEffect;

        return stats;
    }
}
