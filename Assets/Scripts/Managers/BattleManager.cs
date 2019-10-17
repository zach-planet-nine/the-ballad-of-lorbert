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

    public bool battleIsOver;

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
        if(damage <= 0)
        {
            return;
        }
        BattleStats stats = GetStatsForEntity(entity);
        stats.currentHP -= damage;
        if(stats.currentHP <= 0)
        {
            Debug.Log("Handle dying here");
            if(entity == Enemy1)
            {
                Enemy1.GetComponent<EnemyDeath>().Die();
            }
        }
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

    public int EntityUsesWaterToHealEntity(GameObject healer, GameObject healed)
    {
        BattleStats healerStats = GetStatsForEntity(healer);

        int baseHealingPower = healerStats.aura * 3;
        int healing = Randomness.GetIntBetween(baseHealingPower * 3 / 4, baseHealingPower + (baseHealingPower * 1 / 3));

        healerStats.currentMP -= 70;

        return healing;
    }

    public int EntityUsesWaterToAttackEntity(GameObject attacker, GameObject target)
    {
        BattleStats attackerStats = GetStatsForEntity(attacker);

        attackerStats.currentMP -= 120;

        return 240;
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
