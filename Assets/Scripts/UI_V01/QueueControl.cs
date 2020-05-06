using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueControl : MonoBehaviour
{
    public GameObject IncreaseButton;
    public GameObject DecreaseButton;
    public Text Counter;

    int StartingQueue = 1;
    int CurrentQueue;

    void Start()
    {
        CurrentQueue = StartingQueue;
    }

    public void Increase()
    {
        if (CurrentQueue < 4)
        {
            CurrentQueue += 1;
            Counter.text = (CurrentQueue.ToString("0"));
        }
    }

    public void Decrease()
    {
        if (CurrentQueue > 1)
        {
            CurrentQueue -= 1;
            Counter.text = (CurrentQueue.ToString("0"));
        }
    }
}
