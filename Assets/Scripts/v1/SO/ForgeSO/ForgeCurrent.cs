using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using v1.SO.ItemSO;

public enum ForgeErrors
{
    NoFreeCore,
    QueueFull,

};

namespace v1.SO.ForgeSO
{
    [CreateAssetMenu]
    public class ForgeCurrent : ScriptableObject
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

        public int SetToQueue(int itemId)
        {
            var currentCoreIndex = ItemCoreIndex(itemId);
            Debug.Log("currentCoreIndex " + currentCoreIndex);
            if (currentCoreIndex != -1)
            {
                Debug.Log("queue exist coreindex = " + currentCoreIndex);
                AddToExistingQueue(itemId, currentCoreIndex);
            }
            else
            {
                Debug.Log("create new queue");
                AddToFreeCore(itemId);

            }
            return 0;
        }

        int FirstFreeCore(int itemId)
        {
            var i = 0;
            var freeCoreIndex = -1;
            var maxCoreIndex = int.Parse(GetLvlAttrValue(ItemAttrType.ForgeFreeCors));
            //Debug.Log("maxCoreIndex " + maxCoreIndex);
            while (i < maxCoreIndex && freeCoreIndex == -1)
            {
                Debug.Log("GetQueuOnCore(i).Count " + GetQueuOnCore(i).Count + " i " + i);
                if (GetQueuOnCore(i).Count == 0)
                    freeCoreIndex = i;
                ++i;
            }
            //if (freeCoreIndex <= int.Parse(GetLvlAttrValue(ItemAttrType.ForgeFreeCors)))
            //{
            return freeCoreIndex;
            //}
            //return -1;
        }

        public int ItemCoreIndex(int itemId)
        {
            var res = -1;
            if (core0ItemId == itemId)
                res = 0;
            if (core1ItemId == itemId)
                res = 1;
            if (core2ItemId == itemId)
                res = 2;
            if (core3ItemId == itemId)
                res = 3;
            if (core4ItemId == itemId)
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
            //Debug.Log("CanUseCore " + CanUseCore(coreIndex, itemId).ToString());
            if (!CanUseCore(coreIndex, itemId))
                return false;
            var queue = GetQueuOnCore(coreIndex);
            // queue can be null
            //Debug.Log("queue.Count " + queue.Count);

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

            Debug.Log("ADDED TO QUEUE itemId " + itemId + " coreIndex " + coreIndex);
            var asset = CreateNewQueue();
            queue.Add(
                asset
            );
            SetItemIdOnCoreLable(coreIndex, itemId);
            return true;

        }

        List<ForgeQueue> GetQueuOnCore(int coreIndex)
        {
            List<ForgeQueue> res = null;
            switch (coreIndex)
            {
                case 0:
                    res = core0;
                    break;
                case 1:
                    res = core1;
                    break;
                case 2:
                    res = core2;
                    break;
                case 3:
                    res = core3;
                    break;
                case 4:
                    res = core4;
                    break;
            }
            return res;
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
                return core0ItemId;
            if (coreIndex == 1)
                return core1ItemId;
            if (coreIndex == 2)
                return core2ItemId;
            if (coreIndex == 3)
                return core3ItemId;
            if (coreIndex == 4)
                return core4ItemId;

            return -1;
        }

        void SetItemIdOnCoreLable(int coreIndex, int itemId)
        {
            if (coreIndex == 0)
                core0ItemId = itemId;
            if (coreIndex == 1)
                core1ItemId = itemId;
            if (coreIndex == 2)
                core2ItemId = itemId;
            if (coreIndex == 3)
                core3ItemId = itemId;
            if (coreIndex == 4)
                core4ItemId = itemId;
        }

        ForgeQueue CreateNewQueue()
        {
            ForgeQueue asset = ScriptableObject.CreateInstance<ForgeQueue>();
            asset.Id = QueuId;
            asset.TimeStart = 124;
            asset.TimeEnd = 333334;
            asset.name = Constants.forgeQueueAssetPrefix + asset.Id;
            UnityEditor.AssetDatabase.CreateAsset(asset, Constants.pathToSOImplementationForge + "/Queue/" + asset.name + ".asset");
            Debug.Log("NEW queue " + asset.name);
            return asset;
        }

        public void T_ClearCores()
        {
            ResetCore(0);
            ResetCore(1);
            ResetCore(2);
            ResetCore(3);
            ResetCore(4);
            return;
        }

        void ResetCore(int coreIndex)
        {
            SetItemIdOnCoreLable(coreIndex, -1);
            var queue = GetQueuOnCore(coreIndex);
            foreach (var item in queue)
            {
                if (item != null)
                {
                    UnityEditor.AssetDatabase.DeleteAsset(Constants.pathToSOImplementationForge + "/Queue/" + Constants.forgeQueueAssetPrefix + item.Id + ".asset");
                }
            }
            SetQueueOnCore(coreIndex);
        }
    }
}