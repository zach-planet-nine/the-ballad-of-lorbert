using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager manager;

    public GameObject Lorbert;
    public GameObject Artro;
    public GameObject IO;

    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;

    private BattleStats LorbertStats;
    private BattleStats ArtroStats;
    private BattleStats IOStats;
    private BattleStats Enemy1Stats;
    private BattleStats Enemy2Stats;
    private BattleStats Enemy3Stats;
    private BattleStats Enemy4Stats;

    private void Awake()
    {
        manager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        LorbertStats = CharacterStats.characterStats.GetLorbertBattleStats();
        ArtroStats = CharacterStats.characterStats.GetArtroBattleStats();
        IOStats = CharacterStats.characterStats.GetIOBattleStats();

        Enemy1Stats = Enemy1.GetComponent<EnemyStats>().GetBattleStats();

        StoryManager.manager.engaged = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    BattleStats GetStatsForEntity(GameObject entity)
    {
        if(entity.name == "LorbertRest" || entity.name == "LorbertActive")
        {
            return LorbertStats;
        }
        else if(entity.name == "ArtroRest" || entity.name == "ArtroActive")
        {
            return ArtroStats;
        }
        else if(entity.name == "IORest" || entity.name == "IOActive")
        {
            return IOStats;
        }
        else if(entity.name == Enemy1.name)
        {
            return Enemy1Stats;
        }
        else if (entity.name == Enemy2.name)
        {
            return Enemy2Stats;
        }
        else if (entity.name == Enemy3.name)
        {
            return Enemy3Stats;
        }
        else 
        {
            return Enemy4Stats;
        }
    }

    public void ApplyDamage(GameObject entity, int damage)
    {
        BattleStats stats = GetStatsForEntity(entity);
        stats.currentHP -= damage;
        if(stats.currentHP <= 0)
        {
            Debug.Log("Handle dying here");
        }
    }

    public int EntityAttacksEntity(GameObject attacker, GameObject defender)
    {
        BattleStats attackerStats = GetStatsForEntity(attacker);
        BattleStats defenderStats = GetStatsForEntity(defender);

        int baseAttackPower = attackerStats.strength + (attackerStats.perception / 2);
        int attackAttempts = attackerStats.agility / 20;

        int defensePower = (defenderStats.vitality + ((defenderStats.agility + defenderStats.dexterity) / 2)) / 2;

        int damage = 0;

        for (var i = 0; i < attackAttempts; i++)
        {
            int attemptDamage = baseAttackPower - defensePower + Randomness.GetIntBetween(0, (attackerStats.luck / 5));
            damage += attemptDamage;
        }

        attackerStats.Attack();

        return damage;
    }
}

public class BattleStats
{
    public int currentHP;
    public int maxHP;
    public int currentStamina;
    public int maxStamina;
    public int currentMP;
    public int maxMP;
    public int strength;
    public int vitality;
    public int agility;
    public int dexterity;
    public int wisdom;
    public int aura;
    public int perception;
    public int luck;

    public int attackStaminaCost;

    public void Attack()
    {
        currentStamina -= attackStaminaCost;
    }
}
