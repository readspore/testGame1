using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Countdown : MonoBehaviour
{

    float CurrentTime = 0f;
    float BaseTime = 180f;
    float CurrentMinutes;
    float CurrentSeconds;
    public Text Queue;
    [SerializeField] Text CountdownText;

    private void Start()
    {
        CurrentTime = BaseTime;
    }

//CurrentTime -= 1 * Time.deltaTime;

    private void Update()
    {
        CurrentTime = Convert.ToInt32(Queue.text) * BaseTime;
        if (CurrentTime >= 60)
        {
            CurrentMinutes = CurrentTime / 60;
            CurrentMinutes = Convert.ToInt32(CurrentMinutes);
            CurrentSeconds = CurrentTime - CurrentMinutes * 60;

            if (CurrentMinutes < 10 && CurrentSeconds < 10)
            {
                CountdownText.text = ("0" + CurrentMinutes.ToString("0") + ":0" + CurrentSeconds.ToString("0"));
            }
            else if (CurrentMinutes >= 10 && CurrentSeconds < 10)
            {
                CountdownText.text = (CurrentMinutes.ToString("0") + ":0" + CurrentSeconds.ToString("0"));
            }
            else if (CurrentMinutes >= 10 && CurrentSeconds >= 10)
            {
                CountdownText.text = (CurrentMinutes.ToString("0") + ":" + CurrentSeconds.ToString("0"));
            }
            else if (CurrentMinutes < 10 && CurrentSeconds >= 10)
            {
                CountdownText.text = ("0" + CurrentMinutes.ToString("0") + ":" + CurrentSeconds.ToString("0"));
            }
        }
        else if (CurrentTime < 60 && CurrentTime >= 10)
        {
            CountdownText.text = ("00:" + CurrentTime.ToString("0"));
        }
        else if (CurrentTime <= 9)
        {
            CountdownText.text = ("00:0" + CurrentTime.ToString("0"));
        }
        if (CurrentTime < 0)
        {
            CurrentTime = 0;
        }
    }

}
