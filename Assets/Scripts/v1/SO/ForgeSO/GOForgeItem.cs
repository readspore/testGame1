using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using v1.SO;
using v1.SO.SOItem;

public class GOForgeItem : MonoBehaviour
{
    public SOItemObjId itemType;
    public Button btnBuy;
    public Button btnCreate;

    // Start is called before the first frame update
    private void Start()
    {
        btnBuy?.onClick.AddListener(TryBuy);
        btnCreate?.onClick.AddListener(TryCreate);
        SetItemInfo();
    }

    public void SetItemInfo()
    {
        var item = AssetDatabase.LoadAssetAtPath<SOItem>(
            Constants.pathToSOImplementationItems + "/" + itemType.ToString() + ".asset"
        );
        Debug.Log("item " + item.Name);
    }

    public void TryCreate()
    {
        Debug.Log("TryCreate " + itemType.ToString());
    }

    public void TryBuy()
    {
        Debug.Log("TryBuy " + itemType.ToString());
    }

}
