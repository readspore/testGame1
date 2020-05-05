using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_MainMenuControl : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Forge;
    public GameObject Levels;

    void Start()
    {
        MainMenu.SetActive(true);
        Forge.SetActive(false);
        Levels.SetActive(false);
    }

    //public GameObject Play_Button;
    //public GameObject Forge_Button;
    //public GameObject Levels_Button;

    public void Button_Control()
    {

    }
}
