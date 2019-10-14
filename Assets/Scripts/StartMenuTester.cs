﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(50, 100, 150, 60), "Start New"))
        {

        }
        if(GUI.Button(new Rect(50, 160, 150, 60), "Continue"))
        {

        }
        if(GUI.Button(new Rect(50, 220, 150, 60), "Load"))
        {
            CharacterStats.characterStats.LogPartyData();
            CharacterStats.characterStats.Load("testfile.dat");
            CharacterStats.characterStats.LogPartyData();
        }
        if(GUI.Button(new Rect(50, 280, 150, 60), "Settings"))
        {
            Debug.Log("Settings");
        }
        if(GUI.Button(new Rect(500, 110, 150, 60), "Test Save"))
        {
            CharacterStats.characterStats.EquipOnCharacterOnLocation(Characters.Artro, EquipLocations.Arms, "010100111101010");
            CharacterStats.characterStats.Save("testfile.dat");
        }
    }
}
