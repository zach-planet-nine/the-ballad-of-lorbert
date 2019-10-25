using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public enum Characters
{
    Lorbert = 0,
    Artro = 1,
    IO = 2
}

public enum Stats
{
    Strength,
    Vitality,
    Agility,
    Dexterity,
    Wisdom,
    Aura,
    Perception,
    Luck
}

public enum EquipLocations
{
    Weapon = 0,
    Head = 1,
    Body = 2,
    Shield = 3,
    Arms = 4,
    Neck = 5,
    Hands = 6,
    Feet = 7
}

public class CharacterStats : MonoBehaviour
{

    public static CharacterStats characterStats;
    public PartyData partyData = new PartyData();
    public string continueFile = "continue.dat";
    public List<string> Nineum
    {
        set
        {
            partyData.Nineum = value;
        }
    }

    private void Awake()
    {
        if (characterStats == null)
        {
            characterStats = this;
            DontDestroyOnLoad(gameObject);
        } else if(this != characterStats)
        {
            Destroy(gameObject);
        }
    }

    public void Save(string fileName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + fileName);

        bf.Serialize(file, partyData);
        file.Close();
        Debug.Log("File saved with storyIndex " + partyData.storyIndex);
    }

    public void Load(string fileName)
    {
        
        if(File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
            partyData = (PartyData)bf.Deserialize(file);
            file.Close();
            Debug.Log("File loaded with Story index: " + partyData.storyIndex);
        } else
        {
            Debug.Log("File could not be found.");
        }
    }

    private void EquipOnLocation(CharacterData character, EquipLocations location, string ninea)
    {
        switch(location)
        {
            case EquipLocations.Weapon:
                character.weapon = ninea;
                break;
            case EquipLocations.Head:
                character.head = ninea;
                break;
            case EquipLocations.Body:
                character.body = ninea;
                break;
            case EquipLocations.Shield:
                character.shield = ninea;
                break;
            case EquipLocations.Arms:
                character.arms = ninea;
                break;
            case EquipLocations.Neck:
                character.neck = ninea;
                break;
            case EquipLocations.Hands:
                character.hands = ninea;
                break;
            case EquipLocations.Feet:
                character.feet = ninea;
                break;
        }
    }

    public void Equip(CharacterData data, Equipment equipment)
    {
        switch(equipment.location)
        {
            case EquipLocations.Weapon: data.inventory.weapons = new List<Equipment>();
                data.inventory.weapons.Add(equipment);
                break;
            case EquipLocations.Head:
                data.inventory.helms = new List<Equipment>();
                data.inventory.helms.Add(equipment);
                break;
            case EquipLocations.Body:
                data.inventory.armors = new List<Equipment>();
                data.inventory.armors.Add(equipment);
                break;
            case EquipLocations.Shield:
                data.inventory.shields = new List<Equipment>();
                data.inventory.shields.Add(equipment);
                break;
            case EquipLocations.Arms:
                data.inventory.bracers = new List<Equipment>();
                data.inventory.bracers.Add(equipment);
                break;
            case EquipLocations.Neck:
                data.inventory.necklaces = new List<Equipment>();
                data.inventory.necklaces.Add(equipment);
                break;
            case EquipLocations.Hands:
                data.inventory.gloves = new List<Equipment>();
                data.inventory.gloves.Add(equipment);
                break;
            case EquipLocations.Feet:
                data.inventory.boots = new List<Equipment>();
                data.inventory.boots.Add(equipment);
                break;
        }

        data.RecalculateStats();
    }

    public void EquipOnCharacter(Characters character, Equipment equipment)
    {
        switch(character)
        {
            case Characters.Lorbert: Equip(partyData.LorbertData, equipment);
                break;
            case Characters.Artro: Equip(partyData.ArtroData, equipment);
                break;
            case Characters.IO: Equip(partyData.IOData, equipment);
                break;
        }
    }

    public void EquipOnCharacterOnLocation(Characters character, EquipLocations location, string ninea)
    {
        switch(character)
        {
            case Characters.Lorbert: EquipOnLocation(partyData.LorbertData, location, ninea);
                break;
            case Characters.Artro: EquipOnLocation(partyData.ArtroData, location, ninea);
                break;
            case Characters.IO: EquipOnLocation(partyData.IOData, location, ninea);
                break;
        }
    }

    public List<string> GetEquippedNineumForCharacter(CharacterData data)
    {
        List<string> nineum = new List<string>();
        if(data.inventory.weapons.Count > 0)
        {
            nineum.Add(data.inventory.weapons[0].nineaHexString);
        }
        if (data.inventory.helms.Count > 0)
        {
            nineum.Add(data.inventory.helms[0].nineaHexString);
        }
        if (data.inventory.armors.Count > 0)
        {
            nineum.Add(data.inventory.armors[0].nineaHexString);
        }
        if (data.inventory.shields.Count > 0)
        {
            nineum.Add(data.inventory.shields[0].nineaHexString);
        }
        if (data.inventory.bracers.Count > 0)
        {
            nineum.Add(data.inventory.bracers[0].nineaHexString);
        }
        if (data.inventory.necklaces.Count > 0)
        {
            nineum.Add(data.inventory.necklaces[0].nineaHexString);
        }
        if (data.inventory.gloves.Count > 0)
        {
            nineum.Add(data.inventory.gloves[0].nineaHexString);
        }
        if (data.inventory.boots.Count > 0)
        {
            nineum.Add(data.inventory.boots[0].nineaHexString);
        }

        return nineum;
    }

    public List<string> GetEquippedNineum()
    {
        List<string> nineum = new List<string>();
        nineum.AddRange(GetEquippedNineumForCharacter(partyData.LorbertData));
        nineum.AddRange(GetEquippedNineumForCharacter(partyData.ArtroData));
        nineum.AddRange(GetEquippedNineumForCharacter(partyData.IOData));

        return nineum;
    }

    public void LogPartyData()
    {
        Debug.Log(partyData);
        Debug.Log(partyData.ArtroData);
        Debug.Log(partyData.ArtroData.arms);
    }

    public BattleStats GetLorbertBattleStats()
    {
        BattleStats stats = new BattleStats();
        stats.currentHP = partyData.LorbertData.health;
        stats.maxHP = partyData.LorbertData.health;
        stats.currentStamina = stats.maxStamina = partyData.LorbertData.stamina;
        stats.currentMP = stats.maxMP = partyData.LorbertData.mp;
        stats.strength = partyData.LorbertData.strength;
        stats.vitality = partyData.LorbertData.vitality;
        stats.agility = partyData.LorbertData.agility;
        stats.dexterity = partyData.LorbertData.dexterity;
        stats.wisdom = partyData.LorbertData.wisdom;
        stats.aura = partyData.LorbertData.aura;
        stats.perception = partyData.LorbertData.perception;
        stats.luck = partyData.LorbertData.luck;
        stats.attackStaminaCost = 120;

        return stats;
    }

    public BattleStats GetArtroBattleStats()
    {
        BattleStats stats = new BattleStats();
        stats.currentHP = partyData.ArtroData.health;
        stats.maxHP = partyData.ArtroData.health;
        stats.currentStamina = stats.maxStamina = partyData.ArtroData.stamina;
        stats.currentMP = stats.maxMP = partyData.ArtroData.mp;
        stats.strength = partyData.ArtroData.strength;
        stats.vitality = partyData.ArtroData.vitality;
        stats.agility = partyData.ArtroData.agility;
        stats.dexterity = partyData.ArtroData.dexterity;
        stats.wisdom = partyData.ArtroData.wisdom;
        stats.aura = partyData.ArtroData.aura;
        stats.perception = partyData.ArtroData.perception;
        stats.luck = partyData.ArtroData.luck;
        stats.attackStaminaCost = 100;

        return stats;
    }

    public BattleStats GetIOBattleStats()
    {
        BattleStats stats = new BattleStats();
        stats.currentHP = partyData.IOData.health;
        stats.maxHP = partyData.IOData.health;
        stats.currentStamina = stats.maxStamina = partyData.IOData.stamina;
        stats.currentMP = stats.maxMP = partyData.IOData.mp;
        stats.strength = partyData.IOData.strength;
        stats.vitality = partyData.IOData.vitality;
        stats.agility = partyData.IOData.agility;
        stats.dexterity = partyData.IOData.dexterity;
        stats.wisdom = partyData.IOData.wisdom;
        stats.aura = partyData.IOData.aura;
        stats.perception = partyData.IOData.perception;
        stats.luck = partyData.IOData.luck;
        stats.attackStaminaCost = 85;

        return stats;
    }
}

[Serializable]
public class PartyData
{
    public CharacterData LorbertData = new CharacterData("Lorbert");
    public CharacterData ArtroData = new CharacterData("Artro");
    public CharacterData IOData = new CharacterData("I-O");

    public List<string> Nineum = new List<string>();

    public int storyIndex;

    public bool haveLearnedSolid;
    public bool haveLeanedGas;
    public bool haveLearnedPlasma;
}

[Serializable]
public class CharacterData
{
    public int health = 1000;
    public int stamina = 300;
    public int mp = 200;
    public int strength = 50;
    public int vitality = 50;
    public int agility = 50;
    public int dexterity = 50;
    public int wisdom = 50;
    public int aura = 50;
    public int perception = 50;
    public int luck = 50;
    public string weapon = "";
    public string head = "";
    public string body = "";
    public string neck = "";
    public string hands = "";
    public string arms = "";
    public string shield = "";
    public string feet = "";

    private int baseHealth;
    private int baseStamina;
    private int baseMP;
    private int baseStrength;
    private int baseVitality;
    private int baseAgility;
    private int baseDexterity;
    private int baseWisdom;
    private int baseAura;
    private int basePerception;
    private int baseLuck;

    public Inventory inventory = new Inventory();

    private string name = "";

    public CharacterData(string name)
    {
        this.name = name;
        switch(name)
        {
            case "Lorbert": baseHealth = health = 600;
                baseStamina = stamina = 300;
                baseMP = mp = 150;
                baseStrength = strength = 64;
                baseVitality = vitality = 55;
                baseAgility = agility = 45;
                baseDexterity = dexterity = 40;
                baseWisdom = wisdom = 40;
                baseAura = aura = 35;
                basePerception = perception = 50;
                baseLuck = luck = 50;
                break;
            case "Artro": baseHealth = health = 650;
                baseStamina = stamina = 280;
                baseMP = mp = 220;
                baseStrength = strength = 45;
                baseVitality = vitality = 43;
                baseAgility = agility = 55;
                baseDexterity = dexterity = 50;
                baseWisdom = wisdom = 65;
                baseAura = aura = 55;
                basePerception = perception = 50;
                baseLuck = luck = 50;
                break;
            case "I-O":
                baseHealth = health = 550;
                baseStamina = stamina = 320;
                baseMP = mp = 185;
                baseStrength = strength = 53;
                baseVitality = vitality = 45;
                baseAgility = agility = 59;
                baseDexterity = dexterity = 55;
                baseWisdom = wisdom = 55;
                baseAura = aura = 60;
                basePerception = perception = 50;
                baseLuck = luck = 50;
                break;
        }
    }

    public void RecalculateStats()
    {
        int strengthModifier = 0;
        int vitalityModifier = 0;
        int agilityModifier = 0;
        int dexterityModifier = 0;
        int wisdomModifier = 0;
        int auraModifier = 0;
        int perceptionModifier = 0;
        int luckModifier = 0;

        List<Equipment> equipment = inventory.GetEquipment();
        equipment.ForEach(e =>
        {
            switch (e.stat)
            {
                case Stats.Strength: strengthModifier += e.statModifier;
                    break;
                case Stats.Vitality: vitalityModifier += e.statModifier;
                    break;
                case Stats.Agility:
                    agilityModifier += e.statModifier;
                    break;
                case Stats.Dexterity:
                    dexterityModifier += e.statModifier;
                    break;
                case Stats.Wisdom:
                    wisdomModifier += e.statModifier;
                    break;
                case Stats.Aura:
                    auraModifier += e.statModifier;
                    break;
                case Stats.Perception:
                    perceptionModifier += e.statModifier;
                    break;
                case Stats.Luck:
                    luckModifier += e.statModifier;
                    break;
            }
        });

        strength = baseStrength + strengthModifier;
        vitality = baseVitality + vitalityModifier;
        agility = baseAgility + agilityModifier;
        dexterity = baseDexterity + dexterityModifier;
        wisdom = baseWisdom + wisdomModifier;
        aura = baseAura + auraModifier;
        perception = basePerception + perceptionModifier;
        luck = baseLuck + luckModifier;

        health = baseHealth + vitality;
        stamina = baseStamina + (dexterity / 2);
        mp = baseMP + (wisdom / 2);
    }
}
