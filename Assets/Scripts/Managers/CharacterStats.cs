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
        Debug.Log("File saved");
    }

    public void Load(string fileName)
    {
        if(File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
            partyData = (PartyData)bf.Deserialize(file);
            file.Close();
            Debug.Log("File loaded");
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
        stats.attackStaminaCost = 80;

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
        stats.attackStaminaCost = 70;

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
        stats.attackStaminaCost = 50;

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

    private string name = "";

    public CharacterData(string name)
    {
        this.name = name;
        switch(name)
        {
            case "Lorbert": health = 600;
                stamina = 350;
                mp = 120;
                strength = 64;
                vitality = 55;
                agility = 45;
                dexterity = 40;
                wisdom = 40;
                aura = 35;
                perception = 50;
                luck = 50;
                break;
            case "Artro": health = 650;
                stamina = 280;
                mp = 240;
                strength = 45;
                vitality = 43;
                agility = 55;
                dexterity = 50;
                wisdom = 65;
                aura = 55;
                perception = 50;
                luck = 50;
                break;
            case "I-O":
                health = 550;
                stamina = 320;
                mp = 210;
                strength = 53;
                vitality = 45;
                agility = 59;
                dexterity = 55;
                wisdom = 55;
                aura = 60;
                perception = 50;
                luck = 50;
                break;
        }
    }
}
