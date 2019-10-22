using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    Animator anim;
    bool isFading = false;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    public IEnumerator FadeToClear()
    {
        Debug.Log("Fading to clear");
        isFading = true;
        StoryManager.manager.isFading = true;
        anim.SetBool("ShouldFade", true);
        anim.SetTrigger("FadeIn");

        while (isFading)
            yield return null;
    }

    public IEnumerator FadeToBlack()
    {
        Debug.Log("Fading to black");
        isFading = true;
        if(StoryManager.manager != null)
        {
            StoryManager.manager.isFading = true;
        }
        anim.SetBool("ShouldFade", true);
        anim.SetTrigger("FadeOut");

        while (isFading)
            yield return null;
    }

    void AnimationComplete()
    {
        isFading = false;
        if(StoryManager.manager != null)
        {
            StoryManager.manager.isFading = false;
        }
    }
}
