using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    public static string dialog;

    private void Start()
    {
        
    }

    private void OnGUI()
    {
        if(dialog == null)
        {
            return;
        }
        GUIStyle dialogStyle = new GUIStyle();
        dialogStyle.font = (Font) Resources.Load("Orbitron-Bold");
        dialogStyle.fontSize = 32;
        Texture2D blueTexture = new Texture2D(1, 1);
        blueTexture.SetPixel(0, 0, Color.blue);
        blueTexture.Apply();
        dialogStyle.normal.background = blueTexture;
        dialogStyle.normal.textColor = Color.white;
        dialogStyle.wordWrap = true;

        GUI.Box(new Rect(10, Screen.height - 300, Screen.width - 20, 300), GUIContent.none, dialogStyle);
        GUI.Label(new Rect(40, Screen.height - 280, Screen.width - 80, 260), dialog, dialogStyle);
    }
}
