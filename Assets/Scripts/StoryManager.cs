using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager manager;
    public DialogBox dialogBox;
    private string currentDialog = "";
    private int storyIndex = 0;

    private void Awake()
    {
        if(manager == null)
        {
            Debug.Log("Should set manager here");
            DontDestroyOnLoad(gameObject);
            manager = this;
        } else if (this != manager)
        {
            Destroy(gameObject);
        } else
        {
            Debug.Log("Got to the else somehow");
        }
    }

    void Start()
    {
        currentDialog = Story.story[manager.storyIndex];
        DialogBox.dialog = currentDialog;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            storyIndex += 1;
            if(storyIndex < Story.story.Count)
            {
                currentDialog = Story.story[storyIndex];
                DialogBox.dialog = currentDialog;
            } else
            {
                Debug.Log("Out of story");
            }
            
        }
    }

    public string getCurrentDialog()
    {
        return Story.story[storyIndex];
    }
}
