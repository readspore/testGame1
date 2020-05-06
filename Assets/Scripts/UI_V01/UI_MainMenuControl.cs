using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_MainMenuControl : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject GameExitPanel;
    public GameObject Forge;
    public GameObject Levels;

    void Start()
    {
        MainMenu.SetActive(true);
        GameExitPanel.SetActive(false);
        Forge.SetActive(false);
        Levels.SetActive(false);
    }
}
