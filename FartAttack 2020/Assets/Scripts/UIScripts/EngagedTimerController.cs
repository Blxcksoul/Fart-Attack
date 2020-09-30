using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngagedTimerController : MonoBehaviour
{
    public bool engagedBegin = false;

    public float truetime;

    float counterTime = 0f;
    int seconds = 0;
    int minutes = 0;

    Text timetext;
    // Start is called before the first frame update
    void Start()
    {
        timetext = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (engagedBegin)
        {
            truetime += Time.deltaTime;

            counterTime += Time.deltaTime;
            string minuteStr = "mTest"; // for this to work we need a string
            string secondStr = "sTest";

            if (counterTime > 1f)
            {
                seconds++;
                if (seconds % 60 == 0) // checks if the number of seconds is divisible by 60, and if so, makes it 0 and adds a minute
                {
                    minutes++;
                    seconds = 0;
                }

                minuteStr = minutes.ToString();
                secondStr = seconds.ToString();
                if (minutes < 10)
                {
                    minuteStr = "0" + minutes.ToString(); // adding a "0" in front of the minutes (and seconds) to make it look better
                }
                if (seconds < 10)
                {
                    secondStr = "0" + seconds.ToString();
                }

                timetext.text = minuteStr + ":" + secondStr;
                counterTime--;
            }
        }
    }

    void OnEnable()
    {
        //gameObject.GetComponent<Animator>().Play("TimerText");
    }
}
