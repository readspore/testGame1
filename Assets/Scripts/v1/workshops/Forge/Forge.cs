﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Forge
{
    static List<IForgeItem> allItems;
    static GameObject uiControllerGO;
    static UIController uiController;
    static int maxQueue = 3;
    public static List<IForgeItem> AllItems { get {
            if (allItems == null)
            {
                allItems = new List<IForgeItem>
                {
                    new ForgeItem(0, "Test 1", 10, 200, 30, 3, false),
                    new ForgeItem(1, "Test 2", 15, 300, 60, 3, false),
                    new ForgeItem(1, "Skill 1", 15, 300, 60, 3, true),
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

    public static int MaxQueue { get => maxQueue; private set => maxQueue = value; }

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
            uiController.GetMenuPage(BtnClickActions.ForgeMain)
                .gameObject.transform.Find("ItemsPanel").transform, 
            false
        );
    }

    public static bool CanCreateItem(int id, int createCount)
    {
        if (createCount <= 0) 
            return false;
        var forgeItem = GetForgeItem(id);
        if (!Bank.AccountContain(Currency.Silver, forgeItem.CostSilver * createCount))
            return false;
        //if (forgeItem.GetFreeQueueLength()) {
        //    Debug.Log("IsOnCreationg");
        //    return false;
        //}
        return true;
    }

    public static bool CanBuyItem(int id)
    {
        return true;
    }

    public static void Buy(int id)
    {
        if (!CanBuyItem(id)) return;

    }

    public static void Create(int id, int createCount)
    {
        var forgeItem = GetForgeItem(id);
        var freeQueueLength = forgeItem.GetFreeQueueLength();
        createCount = freeQueueLength < createCount
            ? createCount
            : createCount;
        if (!CanCreateItem(id, createCount))
        {
            Debug.Log("Cant NOT create id " + id);
            return;
        }
        if (Bank.Pay(Currency.Silver, forgeItem.CostSilver * createCount))
        {
            forgeItem.StartCreation(createCount);
            Debug.Log("creating id " + id);
        }
    }
}
