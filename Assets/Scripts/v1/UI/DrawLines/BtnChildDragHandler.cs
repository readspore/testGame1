using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnChildDragHandler : MonoBehaviour
{
    public void IsHovered(bool hover)
    {
        if (hover)
        {
            transform.GetComponent<Image>().color = Color.red;
        }
        else
        {
            transform.GetComponent<Image>().color = Color.white;
        }
    }

    public void Choose()
    {
        Debug.Log("Choose " + transform.name);
        IsHovered(false);
        transform.GetComponent<BtnClickInfo>().EmulateClick();
    }
}
