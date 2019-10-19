using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public static StoryManager manager;
    public bool isFading;
    public DialogBox dialogBox;
    public bool engaged = true;
    public bool shouldWrite = true;
    public bool debounce = false;

    private string currentDialog = "";
    private int storyIndex = 24;
    private int dialogIndex;
    private float writeDelay;
    private float writeDelayMax = 0.07f;
    private float gotNineumTimer;
    private float gotNineumDuration = 1.0f;
    private int gotNineumIndex = 10;
    private bool inInventory;


    private void Awake()
    {
        if(manager == null)
        {
            Debug.Log("Should set manager here");
            DontDestroyOnLoad(gameObject);
            manager = this;
        } else if (this != manager)
        {
            Debug.Log("Destroying gameObject");
            WorldManager.manager.storyIndex = manager.storyIndex;
            Destroy(gameObject);
        } else
        {
            Debug.Log("Got to the else somehow");
            
        }

        
    }

    void Start()
    {
        currentDialog = Story.story[storyIndex];
        if(WorldManager.manager != null)
        {
            WorldManager.manager.storyIndex = storyIndex;
        }
    }

    private void DisplaySelectionScreen()
    {
        if(GUI.Button(new Rect(200, 300, 400, 200), "Science Lab"))
        {
            JumpToStoryIndex(30);
        }
        if(GUI.Button(new Rect(200, 600, 400, 200), "Hangar"))
        {
            JumpToStoryIndex(40);
        }
        if(GUI.Button(new Rect(200, 900, 400, 200), "Observation Room"))
        {
            JumpToStoryIndex(50);
        }
        if(GUI.Button(new Rect(800, 300, 400, 200), "Hydroponics"))
        {
            JumpToStoryIndex(60);
        }
        if(GUI.Button(new Rect(800, 600, 400, 200), "Recycling Center"))
        {
            JumpToStoryIndex(70);
        }
        if(GUI.Button(new Rect(800, 900, 400, 200), "Engine Room"))
        {
            JumpToStoryIndex(80);
        }
    }

    private void OnGUI()
    {
        if(currentDialog == "SelectionScreen")
        {
            DialogBox.dialog = "";
            DisplaySelectionScreen();
        }
        if(storyIndex == 5)
        {
            if(GUI.Button(new Rect(600, 50, 400, 100), "Yes Please"))
            {
                Debug.Log("Do Tutorial");
                AdvanceStory();
                shouldWrite = false;
                SceneManager.LoadScene("TutorialScene");
            }
            if(GUI.Button(new Rect(600, 180, 400, 100), "No thanks"))
            {
                Debug.Log("Skip Tutorial");
                debounce = true;
                AdvanceStory();
            }
        }
        if(storyIndex == gotNineumIndex)
        {
            if(gotNineumTimer > gotNineumDuration)
            {
                if(inInventory)
                {
                    DialogBox.dialog = "";
                    return;
                }
                GUIStyle inventoryStyle = new GUIStyle();
                inventoryStyle.font = (Font) Resources.Load("Orbitron-Bold");
                inventoryStyle.fontSize = 80;
                inventoryStyle.normal.textColor = Color.white;
                if(GUI.Button(new Rect(Screen.width - 500, 50, 400, 120), "Inventory", inventoryStyle))
                {
                    Debug.Log("Handle Inventory here");
                    OpenInventory();
                    
                }
                DialogBox.dialog = "Go ahead and tap your inventory button now to equip some Nineum.";
            } else
            {
                DialogBox.dialog = "You received 3 common Nineum!";
            }
        }
        if(storyIndex > gotNineumIndex && !inInventory)
        {
            GUIStyle inventoryStyle = new GUIStyle();
            inventoryStyle.font = (Font)Resources.Load("Orbitron-Bold");
            inventoryStyle.fontSize = 80;
            inventoryStyle.normal.textColor = Color.white;
            if (GUI.Button(new Rect(Screen.width - 500, 50, 400, 120), "Inventory", inventoryStyle))
            {
                Debug.Log("Handle Inventory here");
                OpenInventory();

            }
        }
    }

    public void OpenInventory()
    {
        CameraOnLorbert.shouldFollowLorbert = false;
        InventoryManager.shouldDisplayInventory = true;
        inInventory = true;
        DialogBox.dialog = "";
        Camera.main.transform.position = new Vector3(-27.35f, 18.04f, Camera.main.transform.position.z);
    }

    public void CloseInventory()
    {
        inInventory = false;
        dialogIndex = 1;
        CameraOnLorbert.shouldFollowLorbert = true;
        InventoryManager.shouldDisplayInventory = false;
        if (storyIndex == gotNineumIndex)
        { 
            debounce = true;
            AdvanceStory();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if(WorldManager.manager != null)
        {
            WorldManager.manager.storyIndex = storyIndex;
        }
       
    }

    void JumpToStoryIndex(int index)
    {
        storyIndex = index;
        WorldManager.manager.storyIndex = storyIndex;
        currentDialog = Story.story[storyIndex];
        dialogIndex = 1;
        if (dialogIndex < currentDialog.Length)
        {
            DialogBox.dialog = currentDialog.Substring(0, dialogIndex);
        }
    }

    void AdvanceStory()
    {
        if(!engaged)
        {
            return;
        }
        storyIndex += 1;
        if (storyIndex < Story.story.Count)
        {
            Debug.Log(storyIndex);
            WorldManager.manager.storyIndex = storyIndex;
            currentDialog = Story.story[storyIndex];
            Debug.Log(currentDialog);
            dialogIndex = 1;
            if(dialogIndex < currentDialog.Length)
            {
                DialogBox.dialog = currentDialog.Substring(0, dialogIndex);
            }
        } else
        {
            Debug.Log("Out of story");
        }
        if(storyIndex == gotNineumIndex)
        {
            /*NineumManager.manager.AddNineum("01000000010201010102030100000001");
            NineumManager.manager.AddNineum("01000000010201010102060100000001");
            NineumManager.manager.AddNineum("01000000010203010104080100000001");*/
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inInventory || currentDialog == "SelectionScreen")
        {
            return;
        }
        if(storyIndex == gotNineumIndex)
        {
            gotNineumTimer += Time.deltaTime;
            return;
        }

        if(shouldWrite && !isFading)
        {
            writeDelay += Time.deltaTime;
            if(writeDelay > writeDelayMax)
            {
                dialogIndex += 1;
                if (dialogIndex > currentDialog.Length)
                {
                    dialogIndex = currentDialog.Length;
                }
                writeDelay = 0;
            }
        } else
        {
            //dialogIndex = currentDialog.Length;
        }
        if(isFading)
        {
            DialogBox.dialog = null;
        } else
        {
            if(dialogIndex > currentDialog.Length)
            {
                
            } else
            {
                DialogBox.dialog = currentDialog.Substring(0, dialogIndex);
            }
        }
        if(shouldWrite && Input.GetMouseButtonUp(0) && storyIndex != 5)
        {
            if(dialogIndex < currentDialog.Length)
            {
                if(debounce)
                {
                    debounce = false;
                } else
                {
                    Debug.Log("Here is where this gets called");
                    dialogIndex = currentDialog.Length;
                }
                
            } else
            {
                if(debounce)
                {
                    debounce = false;
                } else
                {
                    AdvanceStory();
                }
                
            }
        }
    }

   
}
