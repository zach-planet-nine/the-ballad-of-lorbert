using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager manager;
    public bool isFading = false;
    public DialogBox dialogBox;
    private string currentDialog = "";
    private int storyIndex;
    private bool shouldWrite = true;
    private int dialogIndex;
    private float writeDelay;
    private float writeDelayMax = 0.07f;

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

    // Update is called once per frame
    void Update()
    {
        if(shouldWrite)
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
            dialogIndex = currentDialog.Length;
        }
        if(isFading)
        {
            DialogBox.dialog = null;
        } else
        {
            DialogBox.dialog = currentDialog.Substring(0, dialogIndex);
        }
        if(Input.GetMouseButtonUp(0))
        {
            storyIndex += 1;
            if(storyIndex < Story.story.Count)
            {
                Debug.Log(storyIndex);
                WorldManager.manager.storyIndex = storyIndex;
                currentDialog = Story.story[storyIndex];
                dialogIndex = 0;
                DialogBox.dialog = shouldWrite ? "" : currentDialog;
            } else
            {
                Debug.Log("Out of story");
            }
            
        }
    }

   
}
