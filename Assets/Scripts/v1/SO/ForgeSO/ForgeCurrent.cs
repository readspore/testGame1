using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using v1.SO.ItemSO;

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
        public int QueuId {
            get {
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
                Debug.Log("IF 1");

                TryAddItemToQueue(itemId, currentCoreIndex);
            } else
            {
                Debug.Log("IF 2");
                var coreForeItem = FirstFreeCore(itemId);
                TryAddItemToQueue(itemId, coreForeItem);
            }
            return 0;
        }

        int FirstFreeCore(int itemId)
        {
            var i = 0;
            var freeCoreIndex = -1;
            while (i < 10 && freeCoreIndex == -1)
            {
                ++i;
                Debug.Log("GetQueuOnCore(i).Count " + GetQueuOnCore(i).Count + " i " + i);
                if (GetQueuOnCore(i).Count == 0)
                    freeCoreIndex = i;
            }
            if (freeCoreIndex <= int.Parse(GetLvlAttrValue(ItemAttrType.ForgeFreeCors)))
            {
                return freeCoreIndex;
            }
            return -1;
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

        bool TryAddItemToQueue(int itemId, int coreIndex)
        {
                Debug.Log("CanUseCore " + CanUseCore(coreIndex, itemId).ToString());
            if (!CanUseCore(coreIndex, itemId))
                return false;
            var queue = GetQueuOnCore(coreIndex);
            // queue can be null
            Debug.Log("queue.Count " + queue.Count);

            if (
                queue != null &&
                queue.Count < int.Parse(GetLvlAttrValue(ItemAttrType.ForgeMaxQueue))
            )
            {
                Debug.Log("TryAddItemToQueue itemId " + itemId + " coreIndex " + coreIndex);
                var asset = CreateNewQueue(99);
                queue.Add(
                    asset  
                );
                SetItemIdOnCore(coreIndex, itemId);
                return true;
            }
            else
            {
                Debug.Log("TryAddItemToQueue NOT Add");
                return false;
            }
        }

        List<ForgeQueue> GetQueuOnCore(int coreIndex)
        {
            List<ForgeQueue> res = null;
            switch (coreIndex)
            {
                case 0:
                    res = core0;
                //Debug.Log("core0");
                    break;
                case 1:
                    res = core1;
                    //Debug.Log("core1");
                    break;
                case 2:
                    res = core2;
                    //Debug.Log("core2");
                    break;
                case 3:
                    res = core3;
                    //Debug.Log("core3");
                    break;
                case 4:
                    res = core4;
                    //Debug.Log("core4");
                    break;
            }
            return res;
        }

        bool CanUseCore(int coreIndex, int itemId)
        {
            int itemIdOnCore = GetItemIdOnCore(coreIndex);
            Debug.Log("CanUseCore  coreIndex " + coreIndex);
            Debug.Log("CanUseCore  itemId " + itemId);
            Debug.Log("CanUseCore  C1 " + (coreIndex <= int.Parse(GetLvlAttrValue(ItemAttrType.ForgeFreeCors))).ToString());
            Debug.Log("CanUseCore  C2 " + (itemIdOnCore == -1).ToString());
            Debug.Log("CanUseCore  C3 " + (itemIdOnCore == itemId).ToString());

            if (
                (coreIndex <= int.Parse(GetLvlAttrValue(ItemAttrType.ForgeFreeCors)))
                &&
                (itemIdOnCore == -1) || (itemIdOnCore == itemId)
            )
                return true;
            return false;
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

            return -2;
        }

        void SetItemIdOnCore(int coreIndex, int itemId)
        {
            if (coreIndex == 0)
                core1ItemId = itemId;
            if (coreIndex == 1)
                core2ItemId = itemId;
            if (coreIndex == 2)
                core3ItemId = itemId;
            if (coreIndex == 3)
                core4ItemId = itemId;
            if (coreIndex == 4)
                core4ItemId = itemId;
        }

        ForgeQueue CreateNewQueue(int someId)
        {
            ForgeQueue asset = ScriptableObject.CreateInstance<ForgeQueue>();
            asset.Id = QueuId;
            asset.TimeStart = 124;
            asset.TimeEnd = 333334;
            asset.name = "ForgeQueue" + asset.Id;
            //UnityEditor.AssetDatabase.CreateAsset(asset, Constants.pathToSOImplementationForge + "/queue");
            UnityEditor.AssetDatabase.CreateAsset(asset, Constants.pathToSOImplementationForge + "/Queue/"+ asset.name + ".asset");

            return asset;
            //GetQueuOnCore(coreIndex)[1].TimeStart = 234;
            //ForgeQueue queueObj = ScriptableObject.CreateInstance(Constants.pathToSO + "/ForgeSo/ForgeQueue.asset");
            //AssetDatabase.LoadAssetAtPath<ItemSO>(Constants.pa + "/Arm.asset");
            //var asset = ScriptableObject.CreateInstance<ForgeQueue>();

            //DirectoryInfo dir = new DirectoryInfo(Constants.pathToQueueFolter);
            //var len = dir.GetFiles("*.asset").Length;
            //Debug.Log("len " + len);
            //AssetDatabase.CreateAsset(fq, Constants.pathToQueueFolter + "/queue" + (len) + ".asset");


        }
    }
}