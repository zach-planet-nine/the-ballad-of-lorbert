using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tacticsoft;

public class TestCounterCell : TableViewCell
{
    public Image NineumImage;
        public Text LevelText;
        public Text StatText;
    public Button RowListener;

    private Equipment storedEquipment;

    private string GetAdjective(int statModifier)
    {
        if(statModifier > 50)
        {
            return "Peerless";
        } else if(statModifier > 40)
        {
            return "Superb ";
        } else if(statModifier > 30)
        {
            return "Great ";
        } else if(statModifier > 20)
        {
            return "Major ";
        } else if(statModifier > 10)
        {
            return "Minor ";
        } else
        {
            return "Alrighty ";
        }
    }

        public void SetEquipment(Equipment equipment)
    {
        Sprite sprite = Resources.Load<Sprite>("Nineum/" + equipment.level.ToString("f") + "Bubble");
        NineumImage.sprite = sprite;
        
        LevelText.text = GetAdjective(equipment.statModifier) + equipment.stat.ToString("f");
        StatText.text = "+" + equipment.statModifier + " " + equipment.stat.ToString("f");

        RowListener.onClick.RemoveAllListeners();
        RowListener.onClick.AddListener(delegate { ListenToRow(RowListener.name); });

        storedEquipment = equipment;
    }

    private void ListenToRow(string buttonName)
    {
        Debug.Log("Handle row tap here with " + storedEquipment.nineaHexString);
        InventoryManager.manager.Equip(storedEquipment);
    }
}
