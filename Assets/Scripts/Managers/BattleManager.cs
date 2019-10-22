using System;
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

    public bool solidLearned;
    public bool gasLearned;
    public bool plasmaLearned;

    public bool battleIsOver;
    public bool gameOver;

    public int liquidCost = 70;
    public int solidCost = 100;
    public int gasCost = 130;
    public int plasmaCost = 160;

    private BattleStats LorbertStats;
    private BattleStats ArtroStats;
    private BattleStats IOStats;
    private BattleStats Enemy1Stats;
    private BattleStats Enemy2Stats;
    private BattleStats Enemy3Stats;
    private BattleStats Enemy4Stats;

    private Texture2D healthBar;
    private Texture2D staminaBar;
    private Texture2D magicBar;
    private GUIStyle healthBarStyle;
    private GUIStyle staminaBarStyle;
    private GUIStyle magicBarStyle;
    private int barWidth = 200;
    private int barHeight = 20;
    private float lorbertStaminaTimer;
    private float artroStaminaTimer;
    private float ioStaminaTimer;
    private float lorbertMagicTimer;
    private float artroMagicTimer;
    private float ioMagicTimer;

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
        if(Enemy2 != null)
        {
            Enemy2Stats = Enemy2.GetComponent<EnemyStats>().GetBattleStats();
        }
        if(Enemy3 != null)
        {
            Enemy3Stats = Enemy3.GetComponent<EnemyStats>().GetBattleStats();
        }
        if(Enemy4 != null)
        {
            Enemy4Stats = Enemy4.GetComponent<EnemyStats>().GetBattleStats();
        }

        StoryManager.manager.engaged = false;

        healthBar = new Texture2D(1, 1);
        healthBar.SetPixel(1, 1, Color.green);
        healthBar.wrapMode = TextureWrapMode.Repeat;
        healthBar.Apply();

        staminaBar = new Texture2D(1, 1);
        staminaBar.SetPixel(1, 1, Color.magenta);
        staminaBar.wrapMode = TextureWrapMode.Repeat;
        staminaBar.Apply();

        magicBar = new Texture2D(1, 1);
        magicBar.SetPixel(1, 1, Color.blue);
        magicBar.wrapMode = TextureWrapMode.Repeat;
        magicBar.Apply();

        healthBarStyle = new GUIStyle();
        healthBarStyle.normal.background = healthBar;

        staminaBarStyle = new GUIStyle();
        staminaBarStyle.normal.background = staminaBar;

        magicBarStyle = new GUIStyle();
        magicBarStyle.normal.background = magicBar;
    }

    private void UpdateStaminaForCharacter(BattleStats stats, ref float characterTimer)
    {
        if(stats.currentStamina < stats.maxStamina)
        {
            if(characterTimer > 0.1)
            {
                characterTimer = 0;
                stats.currentStamina += 2;
            }
            if(stats.currentStamina > stats.maxStamina)
            {
                stats.currentStamina = stats.maxStamina;
            }
        }
    }

    private void UpdateMagicForCharacter(BattleStats stats, ref float characterTimer)
    {
        if(stats.currentMP < stats.maxMP)
        {
            if(characterTimer > 0.1)
            {
                characterTimer = 0;
                stats.currentMP += 1;
            }
            if(stats.currentMP > stats.maxMP)
            {
                stats.currentMP = stats.maxMP;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(battleIsOver)
        {
            return;
        }

        lorbertStaminaTimer += Time.deltaTime;
        artroStaminaTimer += Time.deltaTime;
        ioStaminaTimer += Time.deltaTime;
        UpdateStaminaForCharacter(LorbertStats, ref lorbertStaminaTimer);
        UpdateStaminaForCharacter(ArtroStats, ref artroStaminaTimer);
        UpdateStaminaForCharacter(IOStats, ref ioStaminaTimer);

        lorbertMagicTimer += Time.deltaTime;
        artroMagicTimer += Time.deltaTime;
        ioMagicTimer += Time.deltaTime;
        UpdateMagicForCharacter(LorbertStats, ref lorbertMagicTimer);
        UpdateMagicForCharacter(ArtroStats, ref artroMagicTimer);
        UpdateMagicForCharacter(IOStats, ref ioMagicTimer);
        
    }

    private void OnGUI()
    {
        DrawBarsForCharacterAt(LorbertStats, 300, 200);
        DrawBarsForCharacterAt(ArtroStats, 500, 450);
        DrawBarsForCharacterAt(IOStats, 700, 600);
    }

    private int getBarWidth(int currentValue, int maxValue)
    {
        if (currentValue < 0)
        {
            return 0;
        }
        return Convert.ToInt32(Mathf.Floor(((float)currentValue / (float)maxValue) * (float)barWidth));
    }

    private void DrawBarsForCharacterAt(BattleStats character, int x, int y)
    {
        int healthWidth = getBarWidth(character.currentHP, character.maxHP);
        int staminaWidth = getBarWidth(character.currentStamina, character.maxStamina);
        int magicWidth = getBarWidth(character.currentMP, character.maxMP);

        GUI.Box(new Rect(x, y, healthWidth, barHeight), GUIContent.none, healthBarStyle);
        GUI.Box(new Rect(x, y + barHeight, staminaWidth, barHeight), GUIContent.none, staminaBarStyle);
        GUI.Box(new Rect(x, y + 2 * barHeight, magicWidth, barHeight), GUIContent.none, magicBarStyle);
    }

    public BattleStats GetStatsForEntity(GameObject entity)
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
        else if(entity.name.Contains(Enemy1.name))
        {
            return Enemy1Stats;
        }
        else if (entity.name.Contains(Enemy2.name))
        {
            return Enemy2Stats;
        }
        else if (entity.name.Contains(Enemy3.name))
        {
            return Enemy3Stats;
        }
        else 
        {
            return Enemy4Stats;
        }
    }

    private bool IsCharacter(GameObject entity)
    {
        return entity.name == "LorbertRest" || entity.name == "LorbertActive" ||
               entity.name == "ArtroRest" || entity.name == "ArtroActive" ||
               entity.name == "IORest" || entity.name == "IOActive";
    }

    private GameObject GetCharacterForEntity(GameObject entity)
    {
        if(entity.name == "LorbertRest" || entity.name == "LorbertActive")
        {
            return Lorbert;
        } else if(entity.name == "ArtroRest" || entity.name == "ArtroActive")
        {
            return Artro;
        } else
        {
            return IO;
        }
    }

    public bool CheckIfEntityIsDead(GameObject entity)
    {
        BattleStats stats = GetStatsForEntity(entity);
        if(stats.currentHP <= 0)
        {
            return true;
        }
        return false;
    }

    private bool CheckIfGameOver()
    {
        return Lorbert.GetComponent<CharacterDeath>().isDead &&
            Artro.GetComponent<CharacterDeath>().isDead &&
            IO.GetComponent<CharacterDeath>().isDead;
        
    }

    public void ApplyDamage(GameObject entity, int damage)
    {
        if(damage <= 0)
        {
            return;
        }
        BattleStats stats = GetStatsForEntity(entity);
        stats.currentHP -= damage;
        if(stats.currentHP <= 0)
        {
            Debug.Log("Handle dying here");
            if(IsCharacter(entity))
            {
                GameObject character = GetCharacterForEntity(entity);
                character.GetComponent<CharacterDeath>().Die();
                if(CheckIfGameOver())
                {
                    Debug.Log("Handle GameOver here");
                    gameOver = true;
                }
            }
            if(entity == Enemy1)
            {
                Enemy1.GetComponent<EnemyDeath>().Die();
            }
            if(entity == Enemy2)
            {
                Enemy2.GetComponent<EnemyDeath>().Die();
            }
            if(entity == Enemy3)
            {
                Enemy3.GetComponent<EnemyDeath>().Die();
            }
            if(entity == Enemy4)
            {
                Enemy4.GetComponent<EnemyDeath>().Die();
            }
        }
    }

    public void ApplyStaminaDamage(GameObject entity, int damage)
    {
        BattleStats stats = GetStatsForEntity(entity);
        stats.currentStamina -= damage;
        Debug.Log("Stamina is now: " + stats.currentStamina);
    }

    public void ApplyMPDamage(GameObject entity, int damage)
    {
        BattleStats stats = GetStatsForEntity(entity);
        stats.currentMP -= damage;
    }

    public void ApplyHealing(GameObject entity, int healing)
    {
        if(healing <= 0)
        {
            return;
        }
        BattleStats stats = GetStatsForEntity(entity);
        stats.currentHP += healing;
        if (stats.currentHP > stats.maxHP)
        {
            stats.currentHP = stats.maxHP;
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

        damage = Randomness.GetIntBetween((damage * 3 / 4), (damage + (damage / 4)));

        damage = ApplyStamina(attackerStats, damage);

        attackerStats.Attack();

        return damage;
    }

    public int EntityStaminaAttacksEntity(GameObject attacker, GameObject defender)
    {
        BattleStats attackerStats = GetStatsForEntity(attacker);
        BattleStats defenderStats = GetStatsForEntity(defender);

        int staminaDamage = attackerStats.vitality + Randomness.GetIntBetween(0, attackerStats.luck) - (defenderStats.agility / 2);

        return staminaDamage;
    }

    public int EntityMPAttacksEntity(GameObject attacker, GameObject defender)
    {
        BattleStats attackerStats = GetStatsForEntity(attacker);
        BattleStats defenderStats = GetStatsForEntity(defender);

        int mpDamage = attackerStats.wisdom * 4 + Randomness.GetIntBetween(0, attackerStats.luck) - (defenderStats.aura + Randomness.GetIntBetween(0, defenderStats.luck));
        return mpDamage;
    }

    public int EntityDischargesMPAtEntity(GameObject attacker, GameObject defender)
    {
        BattleStats attackerStats = GetStatsForEntity(attacker);
        BattleStats defenderStats = GetStatsForEntity(defender);

        int damage = 106 + (attackerStats.wisdom * 3) + ((defenderStats.wisdom - defenderStats.aura) * 3) - (defenderStats.aura + Randomness.GetIntBetween(0, defenderStats.luck));
        return damage;
    }

    public int GetBitDamageThreshold(GameObject attacker, GameObject defender)
    {
        return BitAttacksEntity(attacker, defender) * 4;
    }

    public int BitAttacksEntity(GameObject attacker, GameObject defender)
    {
        BattleStats attackerStats = GetStatsForEntity(attacker);
        BattleStats defenderStats = GetStatsForEntity(defender);

        int damage = attackerStats.strength + attackerStats.wisdom + (Randomness.GetIntBetween(0, attackerStats.luck + 20)) - (defenderStats.aura + Randomness.GetIntBetween(0, defenderStats.luck));
        return damage;
    }

    public int EntityUsesWaterToHealEntity(GameObject healer, GameObject healed)
    {
        BattleStats healerStats = GetStatsForEntity(healer);

        int baseHealingPower = healerStats.aura * 5;
        int healing = Randomness.GetIntBetween(baseHealingPower * 3 / 4, baseHealingPower + (baseHealingPower * 1 / 3));

        healerStats.currentMP -= liquidCost;

        return healing;
    }

    public int EntityUsesWaterToAttackEntity(GameObject attacker, GameObject target)
    {
        BattleStats attackerStats = GetStatsForEntity(attacker);
        BattleStats defenderStats = GetStatsForEntity(target);

        int damage = 100 + attackerStats.wisdom + Randomness.GetIntBetween(0, attackerStats.wisdom) + Randomness.GetIntBetween(0, attackerStats.luck);
        damage -= defenderStats.aura + Randomness.GetIntBetween(0, defenderStats.luck);

        attackerStats.currentMP -= liquidCost;

        return damage;
    }

    public int ApplyStamina(BattleStats stats, int value)
    {
        if(stats.currentStamina <= 0)
        {
            return 0;
        }
        float staminaPercent = (float)stats.currentStamina / (float)stats.maxStamina;
        float modifier = 0.75f * staminaPercent;
        float modifiedValue = (float)value * (0.25f + modifier);
        int modifiedIntValue = (int)Mathf.Floor(modifiedValue);
        return modifiedIntValue;
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
