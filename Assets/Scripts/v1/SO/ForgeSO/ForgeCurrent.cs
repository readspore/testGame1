using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace v1.SO.ForgeSO
{
    [CreateAssetMenu]
    public class ForgeCurrent : ScriptableObject
    {
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

        public string GetLvlAttrValue(ItemAttrType attrnName)
        {
            var listToSelect = lvl1;
            switch (lvl)
            {
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

        public bool SetToQueue(int itemId)
        {
            return true;
        }

        public int GetCoreForeItem(int itemId)
        {
            var currentCoreIndex = ItemCoreIndex(itemId);
            if (currentCoreIndex != -1)
            {
                TryAddItemToQueue(itemId, currentCoreIndex);
            } else
            {
                GetFreeCoreForItem(itemId);
            }
            //var maxCorsVal = GetLvlAttrValue(ItemAttrType.ForgeFreeCors);
            //var maxFreeQueue= GetLvlAttrValue(ItemAttrType.ForgeMaxQueue);
            //var tc = 0;
            //var freeCoreIndex = 0;
            //while (tc < 10 || freeCoreIndex != 0)
            //{
            //    ++tc;
            //    if (IdexCoreFree(tc))
            //    {

            //    }
            //}
            return 0;
        }

        public int ItemCoreIndex(int itemId)
        {
            var res = -1;
            if (core1ItemId == itemId)
                res = 1;
            if (core2ItemId == itemId)
                res = 2;
            if (core3ItemId == itemId)
                res = 3;
            if (core4ItemId == itemId)
                res = 4;
            if (core5ItemId == itemId)
                res = 5;
            return res;
        }

        public bool TryAddItemToQueue(int itemId, int coreIndex)
        {
            //var maxQueue= GetLvlAttrValue(ItemAttrType.ForgeMaxQueue);
            var queue = GetQueuOnCore(coreIndex);
            if (
                queue != null && 
                queue.Count < int.Parse(GetLvlAttrValue(ItemAttrType.ForgeMaxQueue))
            )
            {
                Debug.Log("TryAddItemToQueue Add");
                //ForgeQueue queueObj = ScriptableObject.CreateInstance(Constants.pathToSO + "/ForgeSo/ForgeQueue.asset");
                //AssetDatabase.LoadAssetAtPath<ItemSO>(Constants.pa + "/Arm.asset");
                //var asset = ScriptableObject.CreateInstance<ForgeQueue>();

                //queue.Add(

                //);
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

            switch (coreIndex)
            {
                case 1:
                        return core1;
                    break;
                case 2:
                    return core2;
                    break;
                case 3:
                    return core3;
                    break;
                case 4:
                    return core4;
                    break;
                case 5:
                    return core5;
                    break;
            }
            return null;
        }

        bool CanUseCoreOnCurrentLvl(int coreIndex)
        {
            return coreIndex >= int.Parse(GetLvlAttrValue(ItemAttrType.ForgeFreeCors));
        }

        int GetFreeCoreForItem(int itemId)
        {

            return 0;

        }
    }
}