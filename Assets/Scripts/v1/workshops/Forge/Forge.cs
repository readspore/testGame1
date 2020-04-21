using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Forge
{
    static List<IForgeItem> allItems;
    static GameObject uiControllerGO;
    static UIController uiController;
    public static List<IForgeItem> AllItems { get {
            if (allItems == null)
            {
                allItems = new List<IForgeItem>
                {
                    new ForgeItem(0, "Test 1", 10, 200, 30),
                    new ForgeItem(1, "Test 2", 15, 300, 60)
                };
            }   
            return allItems;
        }
    }

    public static UIController UiController
    {
        get
        {
            if (uiController == null)
            {
                uiControllerGO = GameObject.Find("MenuCanvas");
                uiController = uiControllerGO.GetComponent<UIController>();
            }
            return uiController;
        }
        set => uiController = value;
    }

    public static IForgeItem GetForgeItem(int id)
    {
        return AllItems.Find(item => item.Id == id);
    }

    public static void FillForgeItemPrefab(int id)
    {
        FillForgeItemPrefab(GetForgeItem(id));
    }

    public static void FillForgeItemPrefab(IForgeItem item)
    {
        //Debug.Log(" uiControllerGO " + UiController);
        //Debug.Log(item.Name);
        var prefab = UiController.ForgItemPrefab;
        prefab.GetComponent<Transform>().position = new Vector3();
        Debug.Log(" item 2");
        // instance2 = (prefab2, transform.position, transform.rotation) as GameObject;
        prefab.transform.Find("ImageLeft/Text").GetComponent<Text>().text = item.IsOpened 
            ? "opened"
            : item.HasPartsForOpen+"/"+item.TotalPartsForOpen;
        prefab.transform.SetParent(
            uiController.GetMenuPage(UIController.AllMenuPagesEN.ForgeMain)
                .gameObject.transform.Find("ItemsPanel").transform, 
            false
        );
    }

}
