using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    float CurrentTime = 0f;
    float StartingTime = 15f;

    [SerializeField] Text CountdownText;

    private void Start()
    {
        CurrentTime = StartingTime;
    }

    void Update()
    {
        CurrentTime -= 1 * Time.deltaTime;

        if(CurrentTime <= 60 && CurrentTime >= 10)
        {
            CountdownText.text = ("00:" + CurrentTime.ToString("0"));
        }
        else if(CurrentTime <= 9)
        {
            CountdownText.text = ("00:0" + CurrentTime.ToString("0"));
        }

        if (CurrentTime < 0)
        {
            CurrentTime = 0;
        }
    }
  
}
