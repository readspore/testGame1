﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixContentHeight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var content = transform.Find("Content");
        var contentGridLay = content.GetComponent<GridLayoutGroup>();
        content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            (contentGridLay.spacing.x + contentGridLay.cellSize.y) * content.transform.childCount
        );
    }
}
