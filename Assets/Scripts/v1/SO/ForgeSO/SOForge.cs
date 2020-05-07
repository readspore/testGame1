using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using v1.SO.SOItem;
using SaveSystem;

namespace v1.SO.SOForge
{
    [CreateAssetMenu]
    public class SOForge : ScriptableObject
    {
        bool isDebug = false;

        [SerializeField]
        int lvl;
        [SerializeField]
        List<InputOutputData> lvl1;
        [SerializeField]
        List<InputOutputData> lvl2;
        [SerializeField]
        List<InputOutputData> lvl3;
        [SerializeField]
        List<InputOutputData> lvl4;
        [SerializeField]
        List<InputOutputData> lvl5;

        public int Lvl { get => lvl; set => lvl = value; }

        public int GetCoreItemId(int coreIndex)
        {
            return PlayerPrefs.GetInt("Core" + coreIndex + "ItemId");
        }

        public string GetLvlAttrValue(ItemAttrType attrnName)
        {
            List<InputOutputData> listToSelect = null;
            switch (Lvl)
            {
                case 1:
                    listToSelect = lvl1;
                    break;
                case 2:
                    listToSelect = lvl2;
                    break;
                case 3:
                    listToSelect = lvl3;
                    break;
                case 4:
                    listToSelect = lvl4;
                    break;
                case 5:
                    listToSelect = lvl5;
                    break;
            }
            return listToSelect.Find(obj => obj.name == attrnName)?.value ?? "";
        }

        public ForgeStatuses BuyAndSetToQueue(int itemId, Currency currency)
        {
            var item = AssetDatabase.LoadAssetAtPath<SOItem.SOItem>(
                Constants.pathToSOImplementationItems + "/" + Enum.GetName(typeof(SOItemObjId), itemId) + ".asset"
            );
            if (!BankAllow(item, currency))
            {
                if (isDebug)
                    Debug.Log("BankAllow not allow");
                return ForgeStatuses.BankNotAllow;
            }
            var currentCoreIndex = ItemCoreIndex(itemId);
            if (isDebug)
                Debug.Log("currentCoreIndex " + currentCoreIndex);
            if (currentCoreIndex != -1)
            {
                if (isDebug)
                    Debug.Log("queue exist coreindex = " + currentCoreIndex);
                return AddToExistingQueue(itemId, currentCoreIndex);
            }
            else
            {
                if (isDebug)
                    Debug.Log("create new queue");
                return AddToFreeCore(itemId);
            }
        }

        int FirstFreeCore(int itemId)
        {
            var i = 0;
            var freeCoreIndex = -1;
            var maxCoreIndex = int.Parse(GetLvlAttrValue(ItemAttrType.ForgeFreeCors));
            while (i < maxCoreIndex && freeCoreIndex == -1)
            {
                if (GetQueuOnCore(i).Count == 0)
                    freeCoreIndex = i;
                ++i;
            }
            return freeCoreIndex;
        }

        public int ItemCoreIndex(int itemId)
        {
            var res = -1;
            if (GetCoreItemId(0) == itemId)
                res = 0;
            if (GetCoreItemId(1) == itemId)
                res = 1;
            if (GetCoreItemId(2) == itemId)
                res = 2;
            if (GetCoreItemId(3) == itemId)
                res = 3;
            if (GetCoreItemId(4) == itemId)
                res = 4;

            return res;
        }

        ForgeStatuses AddToExistingQueue(int itemId, int coreIndex)
        {
            return TryAddItemToQueue(itemId, coreIndex);
        }

        ForgeStatuses AddToFreeCore(int itemId)
        {
            var coreForeItem = FirstFreeCore(itemId);
            return TryAddItemToQueue(itemId, coreForeItem);
        }

        ForgeStatuses TryAddItemToQueue(int itemId, int coreIndex)
        {
            var canUseCore = CanUseCore(coreIndex, itemId);
            if (canUseCore != ForgeStatuses.Ok)
                return canUseCore;
            var queue = GetQueuOnCore(coreIndex);

            if (queue == null)
            {
                if (isDebug)
                    Debug.Log("TryAddItemToQueue NOT Add queue is null");
                return ForgeStatuses.QueueIsNull;
            }
            else if (queue.Count >= int.Parse(GetLvlAttrValue(ItemAttrType.ForgeMaxQueue)))
            {
                if (isDebug)
                    Debug.Log("TryAddItemToQueue NOT Add queue is full");
                return ForgeStatuses.QueueFull;
            }

            var asset = CreateQueueItem(itemId);
            queue.Add(
                asset
            );
            SetNewQueue(coreIndex, queue);
            SetItemIdOnCoreLable(coreIndex, itemId);
            if (isDebug)
                Debug.Log("ADDED TO QUEUE itemId " + itemId + " coreIndex " + coreIndex);
            return ForgeStatuses.Ok;
        }

        List<ForgeQueueItem> GetQueuOnCore(int coreIndex)
        {
            List<ForgeQueueItem> res = GetCore(coreIndex).queue ?? new List<ForgeQueueItem>();
            return res;
        }

        ForgeStatuses CanUseCore(int coreIndex, int itemId)
        {
            int itemIdOnCore = GetCoreItemId(coreIndex);
            if (coreIndex > int.Parse(GetLvlAttrValue(ItemAttrType.ForgeFreeCors)))
            {
                if (isDebug)
                    Debug.Log("CanUseCore FALSE core index out of range " + coreIndex);
                return ForgeStatuses.NoFreeCore;
            }
            if (itemIdOnCore != itemId && itemIdOnCore != -1)
            {
                if (isDebug)
                    Debug.Log("CanUseCore FALSE using by other item " + itemIdOnCore + " || " + itemId);
                return ForgeStatuses.NoFreeCore;
            }
            return ForgeStatuses.Ok;
        }

        void SetItemIdOnCoreLable(int coreIndex, int itemId)
        {
            PlayerPrefs.SetInt("Core" + coreIndex + "ItemId", itemId);
        }

        ForgeQueueItem CreateQueueItem(int itemId)
        {
            var item = AssetDatabase.LoadAssetAtPath<SOItem.SOItem>(
                    Constants.pathToSOImplementationItems + "/" + Enum.GetName(typeof(SOItemObjId), itemId) + ".asset"
                );

            ForgeQueueItem queue = new ForgeQueueItem();
            var timeNow = new DateTimeOffset(DateTime.UtcNow);

            queue.TimeStart = timeNow.ToUnixTimeSeconds();
            queue.TimeEnd =
                timeNow.ToUnixTimeSeconds()
                + Convert.ToInt64(
                    item.GetAttrValue(ItemAttrType.TimeCraftInForge)
                  );
            return queue;
        }

        public void T_ClearCores()
        {
            ResetCore(0);
            ResetCore(1);
            ResetCore(2);
            ResetCore(3);
            ResetCore(4);
            if (isDebug)
                Debug.Log("T_ClearCores");
        }

        void ResetCore(int coreIndex)
        {
            SetItemIdOnCoreLable(coreIndex, -1);
            SetNewQueue(coreIndex, new Core());
        }

        bool BankAllow(SOItem.SOItem item, Currency currency)
        {
            switch (currency)
            {
                case Currency.Gold:
                    return Bank.AccountContain(Currency.Silver, int.Parse(item.GetAttrValue(ItemAttrType.GoldCost)));
                case Currency.Silver:
                    return Bank.AccountContain(Currency.Silver, int.Parse(item.GetAttrValue(ItemAttrType.SilverCost)));
            }
            return false;
        }

        public Core GetCore(int coreIndex)
        {
            var corePath = Application.persistentDataPath + "/Core" + coreIndex + ".xml";
            if (!File.Exists(corePath))
            {
                if (isDebug)
                    Debug.Log("Create new core file");
                CreateCore(corePath);
            }

            FileSave fileSave = new FileSave(FileFormat.Xml);
            return fileSave.ReadFromFile<Core>(corePath);
        }

        void CreateCore(string corePath)
        {
            FileSave fileSave = new FileSave(FileFormat.Xml);
            var core = new Core();
            core.queue = new List<ForgeQueueItem>();
            fileSave.WriteToFile(corePath, core);
            if (isDebug)
                Debug.Log("created new file => " + corePath);
        }

        void SetNewQueue(int coreIndex, List<ForgeQueueItem> queue)
        {
            Core core = new Core();
            core.queue = queue;
            SetNewQueue(coreIndex, core);
        }

        void SetNewQueue(int coreIndex, Core core)
        {
            FileSave fileSave = new FileSave(FileFormat.Xml);
            fileSave.WriteToFile(
                Application.persistentDataPath + "/Core" + coreIndex + ".xml",
                core
            );
        }

    }
}