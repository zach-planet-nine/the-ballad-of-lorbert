using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager manager;
    public GameObject InventoryHUD;
    public TestTableViewController TableViewController;
    public static bool shouldDisplayInventory;
    public Inventory inventory;

    private Characters currentCharacter = Characters.Lorbert;

    private Texture2D InventoryLorbert;
    private Texture2D InventoryArtro;
    private Texture2D InventoryIO;

    private Texture2D AddNineum;
    private Texture2D CommonNineum;
    private Texture2D NineNineum;
    private Texture2D UncommonNineum;
    private Texture2D RareNineum;
    private Texture2D EpicNineum;
    private Texture2D LegendaryNineum;
    private Texture2D MythicalNineum;

    private Texture2D GreenBox;
    private Rect greenBoxRect;

    private GUIStyle orbitronStyle;
    private GUIStyle primaryOrbitronStyle;
    private GUIStyle ubuntuStyle;
    private GUIStyle primaryUbuntuStyle;
    private GUIStyle powerStyle;
    private GUIStyle closeStyle;
    private GUIStyle addNineumStyle;
    private GUIStyle commonStyle;
    private GUIStyle nineStyle;
    private GUIStyle uncommonStyle;
    private GUIStyle rareStyle;
    private GUIStyle epicStyle;
    private GUIStyle legendaryStyle;
    private GUIStyle mythicalStyle;
    private GUIStyle greenBoxStyle;

    private bool selectingUser;

    private void Awake()
    {
        manager = this;
        inventory = new Inventory();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        InventoryLorbert = (Texture2D)Resources.Load("InventoryLorbert");
        InventoryArtro = (Texture2D)Resources.Load("InventoryArtro");
        InventoryIO = (Texture2D)Resources.Load("InventoryIO");
        AddNineum = (Texture2D)Resources.Load("Nineum/AddNineum");
        CommonNineum = (Texture2D)Resources.Load("Nineum/CommonBubble");
        NineNineum = (Texture2D)Resources.Load("Nineum/NineBubble");
        UncommonNineum = (Texture2D)Resources.Load("Nineum/UncommonBubble");
        RareNineum = (Texture2D)Resources.Load("Nineum/RareBubble");
        EpicNineum = (Texture2D)Resources.Load("Nineum/EpicBubble");
        LegendaryNineum = (Texture2D)Resources.Load("Nineum/LegendaryBubble");
        MythicalNineum = (Texture2D)Resources.Load("Nineum/MythicalBubble");
        GreenBox = (Texture2D)Resources.Load("greenbox");

        addNineumStyle = new GUIStyle();
        addNineumStyle.normal.background = AddNineum;
        commonStyle = new GUIStyle();
        commonStyle.normal.background = CommonNineum;
        nineStyle = new GUIStyle();
        nineStyle.normal.background = NineNineum;
        uncommonStyle = new GUIStyle();
        uncommonStyle.normal.background = UncommonNineum;
        rareStyle = new GUIStyle();
        rareStyle.normal.background = RareNineum;
        epicStyle = new GUIStyle();
        epicStyle.normal.background = EpicNineum;
        legendaryStyle = new GUIStyle();
        legendaryStyle.normal.background = LegendaryNineum;
        mythicalStyle = new GUIStyle();
        mythicalStyle.normal.background = MythicalNineum;

        greenBoxStyle = new GUIStyle();
        greenBoxStyle.normal.background = GreenBox;

        Font orbitron = (Font)Resources.Load("Orbitron-Bold");
        orbitronStyle = new GUIStyle();
        orbitronStyle.font = orbitron;
        orbitronStyle.fontSize = 32;
        orbitronStyle.normal.textColor = new Color(228.0f / 255.0f, 210.0f / 255.0f, 197.0f / 255.0f);

        primaryOrbitronStyle = new GUIStyle();
        primaryOrbitronStyle.font = orbitron;
        primaryOrbitronStyle.fontSize = 38;
        primaryOrbitronStyle.normal.textColor = new Color(93.0f / 255.0f, 193.0f / 255.0f, 185.0f / 255.0f);

        Font ubuntu = (Font)Resources.Load("Ubuntu-Medium");
        ubuntuStyle = new GUIStyle();
        ubuntuStyle.font = ubuntu;
        ubuntuStyle.fontSize = 24;
        ubuntuStyle.normal.textColor = Color.white;

        primaryUbuntuStyle = new GUIStyle();
        primaryUbuntuStyle.font = ubuntu;
        primaryUbuntuStyle.fontSize = 24;
        primaryUbuntuStyle.normal.textColor = new Color(93.0f / 255.0f, 193.0f / 255.0f, 185.0f / 255.0f);
        primaryUbuntuStyle.wordWrap = true;
        primaryUbuntuStyle.alignment = TextAnchor.MiddleCenter;

        powerStyle = new GUIStyle();
        powerStyle.font = orbitron;
        powerStyle.fontSize = 48;
        powerStyle.normal.textColor = new Color(93.0f / 255.0f, 193.0f / 255.0f, 185.0f / 255.0f);

        closeStyle = new GUIStyle();
        closeStyle.font = orbitron;
        closeStyle.fontSize = 64;
        closeStyle.normal.textColor = Color.white;
        Texture2D buttonTexture = (Texture2D)Resources.Load("roundedcorners");
        closeStyle.normal.background = buttonTexture;

        inventory.SetInventoryForNineum(NineumManager.manager.GetNineum());
    }

    public void Equip(Equipment equipment)
    {
        CharacterStats.characterStats.EquipOnCharacter(currentCharacter, equipment);
        inventory.SetInventoryForNineum(NineumManager.manager.GetNineum());
        TableViewController.ReloadAfterEquip();
    }

    private void DisplayStats()
    {
        CharacterData data = CharacterStats.characterStats.partyData.LorbertData;
        switch(currentCharacter)
        {
            case Characters.Lorbert:
                data = CharacterStats.characterStats.partyData.LorbertData;
                break;
            case Characters.Artro:
                data = CharacterStats.characterStats.partyData.ArtroData;
                break;
            case Characters.IO:
                data = CharacterStats.characterStats.partyData.IOData;
                break;
        }
        GUI.Label(new Rect(20, 120, 100, 20), "" + data.strength, orbitronStyle);
        GUI.Label(new Rect(20, 160, 100, 20), "Strength", ubuntuStyle);
        GUI.Label(new Rect(20, 120 + (90 * 1), 100, 20), "" + data.vitality, orbitronStyle);
        GUI.Label(new Rect(20, 160 + (90 * 1), 100, 20), "Vitality", ubuntuStyle);
        GUI.Label(new Rect(20, 120 + (90 * 2), 100, 20), "" + data.agility, orbitronStyle);
        GUI.Label(new Rect(20, 160 + (90 * 2), 100, 20), "Agility", ubuntuStyle);
        GUI.Label(new Rect(20, 120 + (90 * 3), 100, 20), "" + data.dexterity, orbitronStyle);
        GUI.Label(new Rect(20, 160 + (90 * 3), 100, 20), "Dexterity", ubuntuStyle);
        GUI.Label(new Rect(20, 120 + (90 * 4), 100, 20), "" + data.wisdom, orbitronStyle);
        GUI.Label(new Rect(20, 160 + (90 * 4), 100, 20), "Wisdom", ubuntuStyle);
        GUI.Label(new Rect(20, 120 + (90 * 5), 100, 20), "" + data.aura, orbitronStyle);
        GUI.Label(new Rect(20, 160 + (90 * 5), 100, 20), "Aura", ubuntuStyle);
        GUI.Label(new Rect(20, 120 + (90 * 6), 100, 20), "" + data.perception, orbitronStyle);
        GUI.Label(new Rect(20, 160 + (90 * 6), 100, 20), "Perception", ubuntuStyle);
        GUI.Label(new Rect(20, 120 + (90 * 7), 100, 20), "" + data.luck, orbitronStyle);
        GUI.Label(new Rect(20, 160 + (90 * 7), 100, 20), "Luck", ubuntuStyle);
    }

    private void DisplayUI()
    {
        if (GUI.Button(new Rect(20, 20, 48, 48), "X", closeStyle))
        {
            Debug.Log("Handle exit inventory here");
            StoryManager.manager.CloseInventory();
        }

        if(GUI.Button(new Rect(200, 20, 200, 60), GUIContent.none, closeStyle))
        {
            Debug.Log("Handle switch characters here");
            selectingUser = true;
        }
        Texture2D textureToDisplay = InventoryLorbert;
        string nameToDisplay = "Lorbert";

        switch(currentCharacter)
        {
            case Characters.Artro: textureToDisplay = InventoryArtro;
                nameToDisplay = "Artro";
                break;
            case Characters.IO: textureToDisplay = InventoryIO;
                nameToDisplay = "I-O";
                break;
        }

        GUI.Label(new Rect(210, 25, 50, 50), textureToDisplay);
        GUI.Label(new Rect(240, 20, 100, 50), nameToDisplay, primaryOrbitronStyle);
        GUI.Label(new Rect(380, 30, 20, 50), ">", primaryUbuntuStyle);

        GUI.Label(new Rect(480, 20, 200, 60), "1000 Power", powerStyle);

        GUI.Label(new Rect(240, 180, 80, 30), "Equip", orbitronStyle);
        GUI.Label(new Rect(240, 220, 300, 30), "Use your inventory to improve your stats and win the fight", ubuntuStyle);

        GUI.Label(new Rect(Screen.width - 300, 30, 280, 30), "Inventory", orbitronStyle);
        GUI.Label(new Rect(Screen.width - 300, 70, 280, 30), "Select from your inventory to equip", ubuntuStyle);

        
    }

    private GUIStyle GetStyleForEquipment(Equipment equipment)
    {
        switch(equipment.level)
        {
            case EquipmentLevels.Common: return commonStyle;
            case EquipmentLevels.Nine: return nineStyle;
            case EquipmentLevels.Uncommon: return uncommonStyle;
            case EquipmentLevels.Rare: return rareStyle;
            case EquipmentLevels.Epic: return epicStyle;
            case EquipmentLevels.Legendary: return legendaryStyle;
            case EquipmentLevels.Mythical: return mythicalStyle;
        }
        return commonStyle;
    }

    private void DisplayInventorySlotsInRect(Rect rect)
    {
        Inventory equippedInventory = CharacterStats.characterStats.partyData.LorbertData.inventory;
        switch(currentCharacter)
        {
            case Characters.Lorbert: equippedInventory = CharacterStats.characterStats.partyData.LorbertData.inventory;
                break;
            case Characters.Artro: equippedInventory = CharacterStats.characterStats.partyData.ArtroData.inventory;
                break;
            case Characters.IO: equippedInventory = CharacterStats.characterStats.partyData.IOData.inventory;
                break;
        }

        float width = rect.width / 4;
        float height = rect.height / 2;
        float imageHeight = height * 0.66f;
        float displayedImageHeight = imageHeight * 0.66f;
        //float displayedImageHeight = AddNineum.height;
        float imagePadding = (width - displayedImageHeight) / 2;
        float textHeight = height * 0.33f;

        if(greenBoxRect.x < rect.xMin)
        {
            greenBoxRect = new Rect(rect.xMin, rect.yMin, width, height);
        }

        GUI.Box(greenBoxRect, GUIContent.none, greenBoxStyle);

        for(var i = 0; i < 4; i++)
        {
            GUIStyle topStyle = addNineumStyle;
            GUIStyle bottomStyle = addNineumStyle;
            string topText = "Add From Inventory";
            string bottomText = "Add From Inventory";
            if(i == 0)
            {
                if(equippedInventory.weapons.Count > 0)
                {
                    topStyle = GetStyleForEquipment(equippedInventory.weapons[0]);
                    topText = equippedInventory.weapons[0].displayString;
                }
                if(equippedInventory.bracers.Count > 0)
                {
                    bottomStyle = GetStyleForEquipment(equippedInventory.bracers[0]);
                    bottomText = equippedInventory.bracers[0].displayString;
                }
            } else if (i == 1)
            {
                if (equippedInventory.helms.Count > 0)
                {
                    topStyle = GetStyleForEquipment(equippedInventory.helms[0]);
                    topText = equippedInventory.helms[0].displayString;
                }
                if (equippedInventory.necklaces.Count > 0)
                {
                    bottomStyle = GetStyleForEquipment(equippedInventory.necklaces[0]);
                    bottomText = equippedInventory.necklaces[0].displayString;
                }
            } else if (i == 2)
            {
                if (equippedInventory.armors.Count > 0)
                {
                    topStyle = GetStyleForEquipment(equippedInventory.armors[0]);
                    topText = equippedInventory.armors[0].displayString;
                }
                if (equippedInventory.gloves.Count > 0)
                {
                    bottomStyle = GetStyleForEquipment(equippedInventory.gloves[0]);
                    bottomText = equippedInventory.gloves[0].displayString;
                }
            } else
            {
                if (equippedInventory.shields.Count > 0)
                {
                    topStyle = GetStyleForEquipment(equippedInventory.shields[0]);
                    topText = equippedInventory.shields[0].displayString;
                }
                if (equippedInventory.boots.Count > 0)
                {
                    bottomStyle = GetStyleForEquipment(equippedInventory.boots[0]);
                    bottomText = equippedInventory.boots[0].displayString;
                }
            }
            Rect topButtonRect = new Rect(rect.xMin + (width * i), rect.yMin, width, height);
            Rect bottomButtonRect = new Rect(rect.xMin + (width * i), rect.yMin + height, width, height);
            if (GUI.Button(topButtonRect, GUIContent.none, new GUIStyle())) {
                Debug.Log("Should handle tap here");
                greenBoxRect = topButtonRect;
                TableViewController.SetEquipmentSlotTo(i);
            }
            if(GUI.Button(bottomButtonRect, GUIContent.none, new GUIStyle()))
            {
                Debug.Log("Should handle tap below here");
                greenBoxRect = bottomButtonRect;
                TableViewController.SetEquipmentSlotTo(i + 4);
            }
            GUI.Label(new Rect(rect.xMin + (width * i) + imagePadding, rect.yMin + imagePadding, displayedImageHeight, displayedImageHeight), GUIContent.none, topStyle);
            GUI.Label(new Rect(rect.xMin + (width * i), rect.yMin + imageHeight + (imagePadding / 2), width, textHeight), "Add From Inventory", primaryUbuntuStyle);
            GUI.Label(new Rect(rect.xMin + (width * i) + imagePadding, rect.yMin + height + imagePadding, displayedImageHeight, displayedImageHeight), GUIContent.none, bottomStyle);
            GUI.Label(new Rect(rect.xMin + (width * i), rect.yMin + height + imageHeight + (imagePadding / 2), width, textHeight), "Add From Inventory", primaryUbuntuStyle);
        }
    }

    private void DisplayInventorySlots()
    {
        if (Screen.width > 2700)
        {
            DisplayInventorySlotsInRect(new Rect(200, 300, 1600, 800));
        }
        else if (Screen.width <= 2700 && Screen.width > 2480)
        {
            DisplayInventorySlotsInRect(new Rect(200, 200, 1500, 700));
        }
        else if (Screen.width <= 2480 && Screen.width > 2000)
        {
            DisplayInventorySlotsInRect(new Rect(200, 300, 1000, 600));
        }
        else if (Screen.width <= 2000 && Screen.width > 1680)
        {
            DisplayInventorySlotsInRect(new Rect(200, 250, 1000, 500));
        }
        else
        {
            DisplayInventorySlotsInRect(new Rect(200, 200, 800, 400));
        }
    }

    private void DisplaySelectingUser()
    {
        if (GUI.Button(new Rect(300, 300, 400, 100), GUIContent.none))
        {
            currentCharacter = Characters.Lorbert;
            selectingUser = false;
        }
        if (GUI.Button(new Rect(300, 400, 400, 100), GUIContent.none))
        {
            currentCharacter = Characters.Artro;
            selectingUser = false;
        }
        if (GUI.Button(new Rect(300, 500, 400, 100), GUIContent.none))
        {
            currentCharacter = Characters.IO;
            selectingUser = false;
        }
        GUI.Label(new Rect(310, 310, 80, 80), InventoryLorbert);
        GUI.Label(new Rect(420, 310, 180, 80), "Lorbert", primaryOrbitronStyle);
        GUI.Label(new Rect(310, 410, 80, 80), InventoryArtro);
        GUI.Label(new Rect(420, 410, 180, 80), "Artro", primaryOrbitronStyle);
        GUI.Label(new Rect(310, 510, 80, 80), InventoryIO);
        GUI.Label(new Rect(420, 510, 180, 80), "I-O", primaryOrbitronStyle);
    }

    private void OnGUI()
    {
        if(shouldDisplayInventory)
        {
            if(InventoryHUD.activeInHierarchy == false)
            {
                Debug.Log("Should become active");
                InventoryHUD.SetActive(true);
            }

            DisplayStats();
            DisplayUI();
            DisplayInventorySlots();
            if (selectingUser)
            {
                DisplaySelectingUser();
            }
        } else
        {
            if(InventoryHUD.activeInHierarchy)
            {
                InventoryHUD.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
