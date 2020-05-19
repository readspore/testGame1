using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildBtnHover : MonoBehaviour
{
    //public GameObject childrenBtnPanel;
    public void IsHovered(bool hover)
    {
        if (hover)
        {
            transform.GetComponent<Image>().color = Color.red;
            //if (childrenBtnPanel != null)
            //{
            //    childrenBtnPanel.SetActive(true);
            //    //lineRenderer.secondDragPosition = transform.position;
            //}
        }
        else
        {
            transform.GetComponent<Image>().color = Color.white;
            //if (childrenBtnPanel != null)
            //{
            //    childrenBtnPanel.SetActive(false);
            //}
        }
    }
}
