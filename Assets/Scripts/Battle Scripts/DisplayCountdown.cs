using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCountdown : MonoBehaviour
{
    public Text CountDownText;
    private int countdown = 10;
    private float countDuration = 1.0f;
    private float counter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DisplayCountdownOfValue(int countdownValue)
    {
        countdown = countdownValue;
    }

    public bool CountDownIsDone()
    {
        return countdown <= 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter >= countDuration)
        {
            countdown -= 1;
            counter = 0;
        }
        CountDownText.text = "" + countdown;
    }
}
