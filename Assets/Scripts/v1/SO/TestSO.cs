using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using v1.SO.ForgeSO;
using v1.SO.ItemSO;

public class TestSO : MonoBehaviour
{
    public Button TEST_btn_so;
    // Start is called before the first frame update
    void Start()
    {
        var armAsset = AssetDatabase.LoadAssetAtPath<ItemSO>(Constants.pathToSOImplementationItems + "/" + Enum.GetName(typeof(SOItemObjId), 0) + ".asset");
        var forgeAsset = AssetDatabase.LoadAssetAtPath<ForgeCurrent>(Constants.pathToSOImplementationForge + "/ForgeCurrentData.asset");

        //GameObject.Find("TEST_SO ").GetComponent<Text>().text = "Forge id: " + forgeAsset.QueuId;
        transform.GetComponent<Text>().text = "Forge id: " + forgeAsset.QueuId;
        TEST_btn_so.onClick.AddListener(CreateQueue);
    }

    // Update is called once per frame
    void CreateQueue()
    {
        var forgeAsset = AssetDatabase.LoadAssetAtPath<ForgeCurrent>(Constants.pathToSOImplementationForge + "/ForgeCurrentData.asset");

        forgeAsset.SetToQueue(0, Currency.Silver);
        transform.GetComponent<Text>()
            .text
            = "U Forge id: " +
            forgeAsset.QueuId;

    }
}
