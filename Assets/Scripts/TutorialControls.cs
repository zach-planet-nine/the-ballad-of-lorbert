using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControls : MonoBehaviour
{

    public GameObject LorbertRest;
    public GameObject ArtroRest;
    public GameObject IORest;
    public GameObject LorbertActive;
    public GameObject ArtroActive;
    public GameObject IOActive;
    public GameObject Enemy;

    public GameObject battleManager;

    public TextAsset boxAsset;

    private GameObject AlienWithPriority;
    private int tutorialIndex;

    private List<string> tutorial = new List<string>
    {
        "Welcome to battle in The Ballad of Lorbert!",
        "To start go ahead and tap on Lorbert.",
        "When you tap on a character they will become the active character.",
        "Once a character has become active you can tap on an enemy to attack it. Go ahead and try that now.",
        "Good! When you attack, the attack will use some of your character's stamina. The lower the stamina, the weaker the attack.",
        "Stamina and mp both recharge over time."
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void RenderDialog(string dialog)
    {
        GUIStyle dialogStyle = new GUIStyle();
        dialogStyle.font = (Font)Resources.Load("Orbitron-Bold");
        dialogStyle.fontSize = 32;
        Texture2D blueTexture = new Texture2D(1, 1);
        blueTexture.SetPixel(0, 0, Color.blue);
        blueTexture.Apply();
        dialogStyle.normal.background = blueTexture;
        dialogStyle.normal.textColor = Color.white;
        dialogStyle.wordWrap = true;

        GUI.Box(new Rect(150, 10, Screen.width - 300, 150), GUIContent.none, dialogStyle);
        GUI.Label(new Rect(160, 20, Screen.width - 320, 130), dialog, dialogStyle);
    }

    private void OnGUI()
    {
        if(tutorialIndex > tutorial.Count)
        {
            return;
        }
        string dialog = tutorial[tutorialIndex];
        if(dialog.Length > 0)
        {
            RenderDialog(dialog);
        }
        if(tutorialIndex == 1)
        {
            GUIStyle boxStyle = new GUIStyle();
            Texture2D boxTexture = new Texture2D(100, 200);
            boxTexture.LoadImage(boxAsset.bytes);
            boxStyle.normal.background = boxTexture;
            GUI.Box(new Rect(50, 40, 180, 300), "Here is a box", boxStyle);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (tutorialIndex == 1)
            {
                if(hitInfo && hitInfo.transform.gameObject.name == "LorbertRest")
                {
                    LorbertRest.SetActive(false);
                    LorbertActive.SetActive(true);
                    ArtroRest.SetActive(true);
                    ArtroActive.SetActive(false);
                    IORest.SetActive(true);
                    IOActive.SetActive(false);
                    AlienWithPriority = LorbertActive;
                    tutorialIndex += 1;
                }
            }
            else if (tutorialIndex == 3)
            {
                if (AlienWithPriority != null && hitInfo && hitInfo.transform.gameObject.name == "SludgeMonster")
                {
                    int damage = battleManager.GetComponent<BattleManager>().EntityAttacksEntity(AlienWithPriority, Enemy);
                    Debug.Log("Deal " + damage + " damage.");
                    tutorialIndex += 1;
                }
            }
            else if (tutorialIndex < 7)
            {
                tutorialIndex += 1;
            }
            else if (hitInfo)
            {
                switch (hitInfo.transform.gameObject.name)
                {
                    case "LorbertRest":
                        LorbertRest.SetActive(false);
                        LorbertActive.SetActive(true);
                        ArtroRest.SetActive(true);
                        ArtroActive.SetActive(false);
                        IORest.SetActive(true);
                        IOActive.SetActive(false);
                        AlienWithPriority = LorbertActive;
                        break;
                    case "ArtroRest":
                        LorbertRest.SetActive(true);
                        LorbertActive.SetActive(false);
                        ArtroRest.SetActive(false);
                        ArtroActive.SetActive(true);
                        IORest.SetActive(true);
                        IOActive.SetActive(false);
                        AlienWithPriority = LorbertActive;
                        break;
                    case "IORest":
                        LorbertRest.SetActive(true);
                        LorbertActive.SetActive(false);
                        ArtroRest.SetActive(true);
                        ArtroActive.SetActive(false);
                        IORest.SetActive(false);
                        IOActive.SetActive(true);
                        AlienWithPriority = LorbertActive;
                        break;
                    case "SludgeMonster":
                        if(AlienWithPriority != null)
                        {
                            int damage = battleManager.GetComponent<BattleManager>().EntityAttacksEntity(AlienWithPriority, Enemy);
                            Debug.Log("Deal " + damage + " damage.");
                            tutorialIndex += 1;
                        }
                        break;
                }
            }
        }
    }
}
