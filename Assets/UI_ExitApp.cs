using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ExitApp : MonoBehaviour
{
    public GameObject ExitPanel;
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            ExitPanel.SetActive(true);
        }
    }
}
