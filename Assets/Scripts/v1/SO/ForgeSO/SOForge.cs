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
        [SerializeField]
        int lvl;
        [SerializeField]
        int queuId;
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

        [SerializeField]
        int core0ItemId = -1;
        [SerializeField]
        List<ForgeQueue> core0;
        [SerializeField]
        int core1ItemId = -1;
        [SerializeField]
        List<ForgeQueue> core1;
        [SerializeField]
        int core2ItemId = -1;
        [SerializeField]
        List<ForgeQueue> core2;
        [SerializeField]
        int core3ItemId = -1;
        [SerializeField]
        List<ForgeQueue> core3;
        [SerializeField]
        int core4ItemId = -1;
        [SerializeField]
        List<ForgeQueue> core4;


        public int Lvl { get => lvl; set => lvl = value; }
        public int QueuId
        {
            get
            {
                ++queuId;
                return queuId;
            }
        }

        public int Core0ItemId { get => PlayerPrefs.GetInt("Core0ItemId"); }
        public int Core1ItemId { get => PlayerPrefs.GetInt("Core1ItemId"); }
        public int Core2ItemId { get => PlayerPrefs.GetInt("Core2ItemId"); }
        public int Core3ItemId { get => PlayerPrefs.GetInt("Core3ItemId"); }
        public int Core4ItemId { get => PlayerPrefs.GetInt("Core4ItemId"); }
        //public List<ForgeQueue> Core4
        //{
        //    get(){
        //        FileSave fileSave = new FileSave(FileFormat.Xml);
        //        return ForgeQueue queue = fileSave.ReadFromFile<ForgeQueue>(Application.persistentDataPath + "/ForgeQueue/Core4.xml");
        //    }
        //    set => core4 = value; 
        //}

        public string GetLvlAttrValue(ItemAttrType attrnName)
        {
            List<InputOutputData> listToSelect = null;
            switch (Lvl)
            {
                case 1:
                    listToSelect = lvl1;
                    //Debug.Log("list lvl1");
                    break;
                case 2:
                    listToSelect = lvl2;
                    //Debug.Log("list lvl2");
                    break;
                case 3:
                    listToSelect = lvl3;
                    //Debug.Log("list lvl3");
                    break;
                case 4:
                    listToSelect = lvl4;
                    //Debug.Log("list lvl4");
                    break;
                case 5:
                    listToSelect = lvl5;
                    //Debug.Log("list lvl5");
                    break;
            }
            //foreach (var item in listToSelect)
            //{
            //Debug.Log("attrnName " + attrnName.ToString());
            //}
            //Debug.Log("listToSelect " + listToSelect.Find(obj => obj.name == attrnName)?.value);
            //var res = listToSelect.Find(obj => obj.name == attrnName);
            //var res3 = listToSelect.Find(obj => obj.name == attrnName)?.value ?? "";
            //var res2 = "";
            //if (res != null) {
            //    Debug.Log("list 1");
            //    res2 = res.value;
            //} else
            //{
            //    Debug.Log("list 2");
            //    res2 = "";
            //}
            //return res2;
            return listToSelect.Find(obj => obj.name == attrnName)?.value ?? "";
        }

        public int SetToQueue(int itemId, Currency currency)
        {
            var item = AssetDatabase.LoadAssetAtPath<SOItem.SOItem>(
                Constants.pathToSOImplementationItems + "/" + Enum.GetName(typeof(SOItemObjId), itemId) + ".asset"
            );
            if (!BankAllow(item, currency))
            {
                Debug.Log("BankAllow not allow");
                return 0;
            }
            var currentCoreIndex = ItemCoreIndex(itemId);
            //Debug.Log("currentCoreIndex " + currentCoreIndex);
            if (currentCoreIndex != -1)
            {
                //Debug.Log("queue exist coreindex = " + currentCoreIndex);
                AddToExistingQueue(itemId, currentCoreIndex);
            }
            else
            {
                //Debug.Log("create new queue");
                AddToFreeCore(itemId);

            }
            return 1;
        }

        int FirstFreeCore(int itemId)
        {
            var i = 0;
            var freeCoreIndex = -1;
            var maxCoreIndex = int.Parse(GetLvlAttrValue(ItemAttrType.ForgeFreeCors));
            //Debug.Log("maxCoreIndex " + maxCoreIndex);
            while (i < maxCoreIndex && freeCoreIndex == -1)
            {
                //Debug.Log("GetQueuOnCore(i).Count " + GetQueuOnCore(i).Count + " i " + i);
                if (GetQueuOnCore(i).Count == 0)
                    freeCoreIndex = i;
                ++i;
            }
            return freeCoreIndex;
        }

        public int ItemCoreIndex(int itemId)
        {
            var res = -1;
            if (Core0ItemId == itemId)
                res = 0;
            if (Core1ItemId == itemId)
                res = 1;
            if (Core2ItemId == itemId)
                res = 2;
            if (Core3ItemId == itemId)
                res = 3;
            if (Core4ItemId == itemId)
                res = 4;

            return res;
        }

        void AddToExistingQueue(int itemId, int coreIndex)
        {
            TryAddItemToQueue(itemId, coreIndex);
        }

        void AddToFreeCore(int itemId)
        {
            var coreForeItem = FirstFreeCore(itemId);
            TryAddItemToQueue(itemId, coreForeItem);
        }

        bool TryAddItemToQueue(int itemId, int coreIndex)
        {
            if (!CanUseCore(coreIndex, itemId))
                return false;
            var queue = GetQueuOnCore(coreIndex);

            if (queue == null)
            {
                Debug.Log("TryAddItemToQueue NOT Add queue is null");
                return false;
            }
            else if (queue.Count >= int.Parse(GetLvlAttrValue(ItemAttrType.ForgeMaxQueue)))
            {
                Debug.Log("TryAddItemToQueue NOT Add queue is full");
                return false;
            }

            var asset = CreateNewQueue(itemId);
            queue.Add(
                asset
            );
            SetItemIdOnCoreLable(coreIndex, itemId);
            Debug.Log("ADDED TO QUEUE itemId " + itemId + " coreIndex " + coreIndex + " queueId " + asset.Id);
            return true;
        }

        List<ForgeQueue> GetQueuOnCore(int coreIndex)
        {
            return GetCore(coreIndex).queue;
            //List<ForgeQueue> res = null;
            //switch (coreIndex)
            //{
            //    case 0:
            //        res = core0;
            //        break;
            //    case 1:
            //        res = core1;
            //        break;
            //    case 2:
            //        res = core2;
            //        break;
            //    case 3:
            //        res = core3;
            //        break;
            //    case 4:
            //        res = core4;
            //        break;
            //}
            //return res;
        }

        void SetQueueOnCore(int coreIndex)
        {
            SetQueueOnCore(coreIndex, new List<ForgeQueue>());
        }

        void SetQueueOnCore(int coreIndex, List<ForgeQueue> queue)
        {
            //names = names.Where(x => x != "Dima").ToList()
            switch (coreIndex)
            {
                case 0:
                    core0 = queue;
                    break;
                case 1:
                    core1 = queue;
                    break;
                case 2:
                    core2 = queue;
                    break;
                case 3:
                    core3 = queue;
                    break;
                case 4:
                    core4 = queue;
                    break;
            }
        }

        bool CanUseCore(int coreIndex, int itemId)
        {
            int itemIdOnCore = GetItemIdOnCore(coreIndex);
            if (coreIndex > int.Parse(GetLvlAttrValue(ItemAttrType.ForgeFreeCors)))
            {
                Debug.Log("CanUseCore FALSE core index out of range " + coreIndex);
                return false;
            }
            if (itemIdOnCore != itemId && itemIdOnCore != -1)
            {
                Debug.Log("CanUseCore FALSE using by other item " + itemIdOnCore + " || " + itemId);
                return false;
            }
            return true;
        }

        int GetItemIdOnCore(int coreIndex)
        {
            if (coreIndex == 0)
                return Core0ItemId;
            if (coreIndex == 1)
                return Core1ItemId;
            if (coreIndex == 2)
                return Core2ItemId;
            if (coreIndex == 3)
                return Core3ItemId;
            if (coreIndex == 4)
                return Core4ItemId;

            return -1;
        }

        void SetItemIdOnCoreLable(int coreIndex, int itemId)
        {
            PlayerPrefs.SetInt("Core" + coreIndex + "ItemId", itemId);
            //if (coreIndex == 0)
            //    Core0ItemId = itemId;
            //if (coreIndex == 1)
            //    Core1ItemId = itemId;  
            //if (coreIndex == 2)
            //    Core2ItemId = itemId;
            //if (coreIndex == 3)
            //    Core3ItemId = itemId;
            //if (coreIndex == 4)
            //    Core4ItemId = itemId;
        }

        ForgeQueue CreateNewQueue(int itemId)
        {
            var item = AssetDatabase.LoadAssetAtPath<SOItem.SOItem>(
                    Constants.pathToSOImplementationItems + "/" + Enum.GetName(typeof(SOItemObjId), itemId) + ".asset"
                );

            ForgeQueue queue = ScriptableObject.CreateInstance<ForgeQueue>();
            var timeNow = new DateTimeOffset(DateTime.UtcNow);

            queue.Id = QueuId;
            queue.TimeStart = timeNow.ToUnixTimeSeconds();
            queue.TimeEnd =
                timeNow.ToUnixTimeSeconds()
                + Convert.ToInt64(
                    item.GetAttrValue(ItemAttrType.TimeCraftInForge)
                  );
            queue.name = Constants.forgeQueueAssetPrefix + queue.Id;
            UnityEditor.AssetDatabase.CreateAsset(queue, Constants.pathToSOImplementationForge + "/Queue/" + queue.name + ".asset");
            return queue;
        }

        public void T_ClearCores()
        {
            ResetCore(0);
            ResetCore(1);
            ResetCore(2);
            ResetCore(3);
            ResetCore(4);
            Debug.Log("T_ClearCores");
        }

        void ResetCore(int coreIndex)
        {
            SetItemIdOnCoreLable(coreIndex, -1);
            var queue = GetQueuOnCore(coreIndex);
            foreach (var item in queue)
            {
                if (item != null)
                {
                    AssetDatabase.DeleteAsset(Constants.pathToSOImplementationForge + "/Queue/" + item.name + ".asset");
                }
            }
            SetQueueOnCore(coreIndex);
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

        Core GetCore(int coreIndex)
        {
            var corePath = Application.persistentDataPath + "/Core" + coreIndex + ".xml";
            if (!File.Exists(corePath))
            {
                CreateCore(coreIndex);
            }

            FileSave fileSave = new FileSave(FileFormat.Xml);
            return fileSave.ReadFromFile<Core>(corePath);
        }

        void CreateCore(int coreIndex)
        {
            FileSave fileSave = new FileSave(FileFormat.Xml);
            var qw = new Core();
            qw.queue = new List<ForgeQueueItem>() {
            new ForgeQueueItem(0, 100, 100),
            new ForgeQueueItem(1, 100, 100),
            new ForgeQueueItem(2, 100, 100),
            new ForgeQueueItem(99, 100, 100)
        };
            fileSave.WriteToFile(
                Application.persistentDataPath + "/Core-" + coreIndex + ".xml",
                qw
            );
        }
    }
}