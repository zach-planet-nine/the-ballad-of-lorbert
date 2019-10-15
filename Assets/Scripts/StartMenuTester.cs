using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuTester : MonoBehaviour
{

    IEnumerator FadeToScene(string scene)
    {
        Debug.Log("Should fade to scene " + scene);
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

        yield return StartCoroutine(sf.FadeToBlack());

        SceneManager.LoadScene(scene);
    }

    private void OnGUI()
    {

        GUI.depth = -1;        

        if(GUI.Button(new Rect(50, 100, 150, 60), "Start New"))
        {
            Debug.Log("Calling the button");
            StartCoroutine(FadeToScene("IntroductionScene"));
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
