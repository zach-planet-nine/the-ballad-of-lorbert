using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryHUD;
    public static bool shouldDisplayInventory;

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

    private GUIStyle orbitronStyle;
    private GUIStyle primaryOrbitronStyle;
    private GUIStyle ubuntuStyle;
    private GUIStyle primaryUbuntuStyle;
    private GUIStyle powerStyle;
    private GUIStyle closeStyle;
    private GUIStyle addNineumStyle;

    // Start is called before the first frame update
    void Start()
    {
        InventoryLorbert = (Texture2D)Resources.Load("InventoryLorbert");
        InventoryArtro = (Texture2D)Resources.Load("InventoryArtro");
        InventoryIO = (Texture2D)Resources.Load("InventoryIO");
        AddNineum = (Texture2D)Resources.Load("Nineum/AddNineum");

        addNineumStyle = new GUIStyle();
        addNineumStyle.normal.background = AddNineum;

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
        }

        if(GUI.Button(new Rect(200, 20, 200, 60), GUIContent.none, closeStyle))
        {
            Debug.Log("Handle switch characters here");
        }
        GUI.Label(new Rect(210, 25, 50, 50), InventoryLorbert);
        GUI.Label(new Rect(240, 20, 100, 50), "Lorbert", primaryOrbitronStyle);
        GUI.Label(new Rect(380, 30, 20, 50), ">", primaryUbuntuStyle);

        GUI.Label(new Rect(480, 20, 200, 60), "1000 Power", powerStyle);

        GUI.Label(new Rect(240, 180, 80, 30), "Equip", orbitronStyle);
        GUI.Label(new Rect(240, 220, 300, 30), "Use your inventory to improve your stats and win the fight", ubuntuStyle);

        GUI.Label(new Rect(Screen.width - 300, 30, 280, 30), "Inventory", orbitronStyle);
        GUI.Label(new Rect(Screen.width - 300, 70, 280, 30), "Select from your inventory to equip", ubuntuStyle);
    }

    private void DisplayInventorySlotsInRect(Rect rect)
    {
        float width = rect.width / 4;
        float height = rect.height / 2;
        float imageHeight = height * 0.66f;
        float displayedImageHeight = imageHeight * 0.66f;
        //float displayedImageHeight = AddNineum.height;
        float imagePadding = (width - displayedImageHeight) / 2;
        float textHeight = height * 0.33f;

        for(var i = 0; i < 4; i++)
        {
            GUI.Label(new Rect(rect.xMin + (width * i) + imagePadding, rect.yMin + imagePadding, displayedImageHeight, displayedImageHeight), GUIContent.none, addNineumStyle);
            GUI.Label(new Rect(rect.xMin + (width * i), rect.yMin + imageHeight + (imagePadding / 2), width, textHeight), "Add From Inventory", primaryUbuntuStyle);
            GUI.Label(new Rect(rect.xMin + (width * i) + imagePadding, rect.yMin + height + imagePadding, displayedImageHeight, displayedImageHeight), GUIContent.none, addNineumStyle);
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

    private void DisplayEquipped()
    {

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
            GUI.Label(new Rect(100, 100, 400, 300), "HEYOOO DISPLAY INVE?NTOR?Y HERE\n");

            DisplayStats();
            DisplayUI();
            DisplayInventorySlots();
            DisplayEquipped();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
