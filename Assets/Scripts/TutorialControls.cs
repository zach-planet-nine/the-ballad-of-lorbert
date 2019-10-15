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

    public TextAsset boxAsset;

    private GameObject AlienWithPriority;
    private int stateIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnGUI()
    {
        GUIStyle boxStyle = new GUIStyle();
        Texture2D boxTexture = new Texture2D(100, 200);
        boxTexture.LoadImage(boxAsset.bytes);
        boxStyle.normal.background = boxTexture;
        GUI.Box(new Rect(50, 40, 180, 300), "Here is a box", boxStyle);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hitInfo)
            {
                switch(hitInfo.transform.gameObject.name)
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

                }
            }
        }
    }
}
