using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using v1.SO;
using v1.SO.SOItem;
using v1.SO.SOForge;

public class GOForgeItem : MonoBehaviour
{
    public SOForge soForge;
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
        var status = soForge.BuyAndSetToQueue((int) itemType, Currency.Silver);
        Debug.Log("TryCreate " + status.ToString());
    }

    public void TryBuy()
    {
        var status = soForge.BuyAndSetToQueue((int)itemType, Currency.Gold);
        Debug.Log("TryBuy " + status.ToString());
    }

}
