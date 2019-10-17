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
    private string currentDialog = "";
    private int storyIndex;
    public bool shouldWrite = true;
    private int dialogIndex;
    private float writeDelay;
    private float writeDelayMax = 0.07f;
    public bool debounce = false;

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
        WorldManager.manager.storyIndex = storyIndex;
    }

    private void OnGUI()
    {
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
        WorldManager.manager.storyIndex = storyIndex;
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
            DialogBox.dialog = currentDialog.Substring(0, dialogIndex);
        } else
        {
            Debug.Log("Out of story");
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            DialogBox.dialog = currentDialog.Substring(0, dialogIndex);
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
