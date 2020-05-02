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
        int core1ItemId;
        [SerializeField]
        List<ForgeQueue> core1;
        [SerializeField]
        int core2ItemId;
        [SerializeField]
        List<ForgeQueue> core2;
        [SerializeField]
        int core3ItemId;
        [SerializeField]
        List<ForgeQueue> core3;
        [SerializeField]
        int core4ItemId;
        [SerializeField]
        List<ForgeQueue> core4;
        [SerializeField]
        int core5ItemId;
        [SerializeField]
        List<ForgeQueue> core5;

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

        //public bool SetToQueue(int itemId)
        //{
        //    return true;
        //}

        public int SetToQueue(int itemId)
        {
            var currentCoreIndex = ItemCoreIndex(itemId);
            //Debug.Log("currentCoreIndex " + currentCoreIndex);
            if (currentCoreIndex != -1)
            {
                Debug.Log("SetToQueue IF 1");
                TryAddItemToQueue(itemId, currentCoreIndex);
            } else
            {
                Debug.Log("SetToQueue IF 2");
                var coreForeItem = GetFirstFreeCore(itemId);
                Debug.Log("GetFirstFreeCore " + coreForeItem);
                TryAddItemToQueue(itemId, coreForeItem);
            }
            return 0;
        }

        int GetFirstFreeCore(int itemId)
        {
            var res = -1;
            var i = 0;
            var freeCoreIndex = -1;
            while (i < 10 && freeCoreIndex != -1)
            {
                ++i;
                if (GetQueuOnCore(i).Count == 0)
                    freeCoreIndex = i;
            }
            if (freeCoreIndex <= int.Parse(GetLvlAttrValue(ItemAttrType.ForgeFreeCors)))
            {
                return res;
            }
            return -1;
        }

        public int ItemCoreIndex(int itemId)
        {
            var res = -1;
            if (core1ItemId == itemId)
                res = 0;
            if (core2ItemId == itemId)
                res = 1;
            if (core3ItemId == itemId)
                res = 2;
            if (core4ItemId == itemId)
                res = 3;
            if (core5ItemId == itemId)
                res = 4;
            return res;
        }

        public bool TryAddItemToQueue(int itemId, int coreIndex)
        {
            //var maxQueue= GetLvlAttrValue(ItemAttrType.ForgeMaxQueue);
            var queue = GetQueuOnCore(coreIndex);
            //Debug.Log("coreIndex " + coreIndex);
            //Debug.Log(" queue.Count " + queue.Count);
            //Debug.Log(" GetLvlAttrValue(ItemAttrType.ForgeMaxQueue) " + GetLvlAttrValue(ItemAttrType.ForgeMaxQueue));

            //Debug.Log("queue != null " + queue != null);
            Debug.Log("queue.Count " + queue.Count);
            Debug.Log("ForgeMaxQueue " + GetLvlAttrValue(ItemAttrType.ForgeMaxQueue));
            Debug.Log("queue.Count " + (queue.Count < int.Parse(GetLvlAttrValue(ItemAttrType.ForgeMaxQueue))).ToString());
            //Debug.Log("ForgeMaxQueue " + GetLvlAttrValue(ItemAttrType.ForgeMaxQueue));

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
            if (!CanUseCoreOnCurrentLvl(coreIndex))
                return null;
            List<ForgeQueue> res = null;
            switch (coreIndex)
            {
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
                case 5:
                    res = core5;
                    //Debug.Log("core5");
                    break;
            }
            return res;
        }

        bool CanUseCoreOnCurrentLvl(int coreIndex)
        {
            return coreIndex <= int.Parse(GetLvlAttrValue(ItemAttrType.ForgeFreeCors));
        }

        int GetFreeCoreForItem(int itemId)
        {

            return 0;

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