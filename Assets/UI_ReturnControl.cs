using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ReturnControl : MonoBehaviour
{
    public GameObject CurrentMenu;
    public GameObject PreviousMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PreviousMenu.SetActive(true);
            CurrentMenu.SetActive(false);
        }
    }
}
