using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CloseGame : MonoBehaviour
{
    public void CloseApp()
    {
         Application.Quit();
         Debug.Log("Game Exit");
    }
}
