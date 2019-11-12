using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuTester : MonoBehaviour
{
    public URLOpener opener;

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

        GUIStyle buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 72;
        //buttonStyle.normal.textColor = Color.black;
        //Texture2D buttonTexture = (Texture2D)Resources.Load("roundedcorners");
        //buttonStyle.normal.background = buttonTexture;        

        if (GUI.Button(new Rect(50, 100, 400, 180), "Start New", buttonStyle))
        {
            Debug.Log("Calling the button");
            StartCoroutine(FadeToScene("IntroductionScene"));
        }
        if(GUI.Button(new Rect(50, 350, 400, 180), "Continue", buttonStyle))
        {
            CharacterStats.characterStats.Load(CharacterStats.characterStats.continueFile);
            StartCoroutine(FadeToScene("IntroductionScene"));
        }
        if(GUI.Button(new Rect(50, 600, 400, 180), "Load", buttonStyle))
        {
            CharacterStats.characterStats.LogPartyData();
            CharacterStats.characterStats.Load("testfile.dat");
            CharacterStats.characterStats.LogPartyData();
        }
        if(GUI.Button(new Rect(50, 850, 400, 180), "Settings", buttonStyle))
        {
            Debug.Log("Settings");
        }
        if(GUI.Button(new Rect(500, 110, 150, 60), "Test Save"))
        {
            CharacterStats.characterStats.EquipOnCharacterOnLocation(Characters.Artro, EquipLocations.Arms, "010100111101010");
            CharacterStats.characterStats.Save("testfile.dat");
        }
        if(GUI.Button(new Rect(700, 110, 400, 180), "Connect PN Account", buttonStyle))
        {
            string urlEncodedName = "TheBalladOfLorbertTest";

            GatewayModel gateway = new GatewayModel(urlEncodedName, CryptoManager.publicKey);
            Debug.Log(JsonUtility.ToJson(gateway));
            GatewayModelWithSignature gatewayWithSignature = new GatewayModelWithSignature(gateway, CryptoManager.signMessage(JsonUtility.ToJson(gateway)));
            string urlString = "planetnine://ongoing/details?gatewayname=" + urlEncodedName +
                "&publicKey=" + CryptoManager.publicKey + "&gatewayURL=pntbltest://ongoing&signature=" +
                gatewayWithSignature.signature + "&timestamp=" + gatewayWithSignature.timestamp;
            Debug.Log(urlString);
            //Application.OpenURL(urlString);
            string successURL = "pntbltest://ongoing/details?success=true&userId=41";
            opener.URLOpened(successURL);
        }
    }
}
