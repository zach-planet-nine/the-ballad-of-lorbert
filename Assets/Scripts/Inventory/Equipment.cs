using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//These are just rarities and should be replaced by the rarity once NineumModel is created
public enum EquipmentLevels
{
    Common,
    Nine,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythical
}

public class Equipment
{
    public EquipLocations location;
    public Stats stat;
    public int statModifier;
    public EquipmentLevels level;
    public string nineaHexString;
    public string displayString;

    private EquipLocations GetLocationForNineum(string ninea)
    {
        string substring = ninea.Substring(16, 2);
        switch(substring)
        {
            case "01": return EquipLocations.Weapon;
            case "02":
                return EquipLocations.Head;
            case "03":
                return EquipLocations.Body;
            case "04":
                return EquipLocations.Shield;
            case "05":
                return EquipLocations.Arms;
            case "06":
                return EquipLocations.Neck;
            case "07":
                return EquipLocations.Hands;
            case "08":
                return EquipLocations.Feet;
        }
        return EquipLocations.Weapon;
    }

    private Stats GetStatForNineum(string ninea)
    {
        string substring = ninea.Substring(18, 2);
        switch (substring)
        {
            case "01": return Stats.Strength;
            case "02": return Stats.Vitality;
            case "03": return Stats.Agility;
            case "04": return Stats.Dexterity;
            case "05": return Stats.Wisdom;
            case "06": return Stats.Aura;
            case "07": return Stats.Perception;
            case "08": return Stats.Luck;

        }
        return Stats.Strength;
    }

    private int GetStatModifierForNineum(string ninea)
    {
        int shapeModifier = 0;
        int rarityModifier = 0;
        string substring = ninea.Substring(20, 2);

        switch (substring)
        {
            case "01": shapeModifier = 2;
                break;
            case "02": shapeModifier = 4;
                break;
            case "03": shapeModifier = 6;
                break;
            case "04": shapeModifier = 8;
                break;
            case "05": shapeModifier = 10;
                break;
            case "06": shapeModifier = 12;
                break;
            case "07": shapeModifier = 14;
                break;
            case "08": shapeModifier = 16;
                break;
        }

        EquipmentLevels level = GetLevelForNineum(ninea);
        switch(level)
        {
            case EquipmentLevels.Common: rarityModifier = 0;
                break;
            case EquipmentLevels.Nine:
                rarityModifier = -20;
                break;
            case EquipmentLevels.Uncommon:
                rarityModifier = 10;
                break;
            case EquipmentLevels.Rare:
                rarityModifier = 20;
                break;
            case EquipmentLevels.Epic:
                rarityModifier = 30;
                break;
            case EquipmentLevels.Legendary:
                rarityModifier = 40;
                break;
            case EquipmentLevels.Mythical:
                rarityModifier = 50;
                break;
        }

        return shapeModifier + rarityModifier;
    }

    private EquipmentLevels GetLevelForNineum(string ninea)
    {
        string substring = ninea.Substring(14, 2);
        switch(substring)
        {
            case "01": return EquipmentLevels.Common;
            case "02": return EquipmentLevels.Nine;
            case "03": return EquipmentLevels.Uncommon;
            case "04": return EquipmentLevels.Rare;
            case "05": return EquipmentLevels.Epic;
            case "06": return EquipmentLevels.Legendary;
            case "07": return EquipmentLevels.Mythical;
        }
        return EquipmentLevels.Common;
    }

    private string GetAdjective(int statModifier)
    {
        if (statModifier > 50)
        {
            return "Peerless";
        }
        else if (statModifier > 40)
        {
            return "Superb ";
        }
        else if (statModifier > 30)
        {
            return "Great ";
        }
        else if (statModifier > 20)
        {
            return "Major ";
        }
        else if (statModifier > 10)
        {
            return "Minor ";
        }
        else
        {
            return "Alrighty ";
        }
    }

    public Equipment(string ninea)
    {
        location = GetLocationForNineum(ninea);
        stat = GetStatForNineum(ninea);
        statModifier = GetStatModifierForNineum(ninea);
        level = GetLevelForNineum(ninea);
        nineaHexString = ninea;
        displayString = GetAdjective(statModifier) + " " + stat.ToString("f");
    }
}
