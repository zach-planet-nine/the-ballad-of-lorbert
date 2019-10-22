using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    public List<Equipment> weapons = new List<Equipment>();
    public List<Equipment> helms = new List<Equipment>();
    public List<Equipment> armors = new List<Equipment>();
    public List<Equipment> shields = new List<Equipment>();
    public List<Equipment> bracers = new List<Equipment>();
    public List<Equipment> necklaces = new List<Equipment>();
    public List<Equipment> gloves = new List<Equipment>();
    public List<Equipment> boots = new List<Equipment>();

    private void ClearInventory()
    {
        weapons = new List<Equipment>();
        helms = new List<Equipment>();
        armors = new List<Equipment>();
        shields = new List<Equipment>();
        bracers = new List<Equipment>();
        necklaces = new List<Equipment>();
        gloves = new List<Equipment>();
        boots = new List<Equipment>();
    }

    public void SetInventoryForNineum(List<string> nineum)
    {

        Debug.Log("There are " + nineum.Count + " Nineum");

        ClearInventory();

        Debug.Log("And there are " + weapons.Count + " weapons");

        List<string> equippedNineum = CharacterStats.characterStats.GetEquippedNineum();

        if(equippedNineum.Count > 0)
        {
            Debug.Log("Equipped Nineum");
            Debug.Log(equippedNineum[0]);
        }
        

        nineum.ForEach(delegate (string ninea)
        {
            if (!equippedNineum.Contains(ninea))
            {
                Equipment equipment = new Equipment(ninea);
                switch (equipment.location)
                {
                    case EquipLocations.Weapon:
                        weapons.Add(equipment);
                        break;
                    case EquipLocations.Head:
                        helms.Add(equipment);
                        break;
                    case EquipLocations.Body:
                        armors.Add(equipment);
                        break;
                    case EquipLocations.Shield:
                        shields.Add(equipment);
                        break;
                    case EquipLocations.Arms:
                        bracers.Add(equipment);
                        break;
                    case EquipLocations.Neck:
                        necklaces.Add(equipment);
                        break;
                    case EquipLocations.Hands:
                        gloves.Add(equipment);
                        break;
                    case EquipLocations.Feet:
                        boots.Add(equipment);
                        break;
                }
            } else
            {
                Debug.Log("It contains for some reason");
                Debug.Log(ninea);
            }
        });
        weapons.Sort((a, b) => b.statModifier.CompareTo(a.statModifier));
        helms.Sort((a, b) => b.statModifier.CompareTo(a.statModifier));
        armors.Sort((a, b) => b.statModifier.CompareTo(a.statModifier));
        shields.Sort((a, b) => b.statModifier.CompareTo(a.statModifier));
        bracers.Sort((a, b) => b.statModifier.CompareTo(a.statModifier));
        necklaces.Sort((a, b) => b.statModifier.CompareTo(a.statModifier));
        gloves.Sort((a, b) => b.statModifier.CompareTo(a.statModifier));
        boots.Sort((a, b) => b.statModifier.CompareTo(a.statModifier));

    }

    public List<Equipment> GetEquipment()
    {
        List<Equipment> equipment = new List<Equipment>();
        weapons.ForEach(a => equipment.Add(a));
        helms.ForEach(a => equipment.Add(a));
        armors.ForEach(a => equipment.Add(a));
        shields.ForEach(a => equipment.Add(a));
        bracers.ForEach(a => equipment.Add(a));
        necklaces.ForEach(a => equipment.Add(a));
        gloves.ForEach(a => equipment.Add(a));
        boots.ForEach(a => equipment.Add(a));
        return equipment;
    }

}
