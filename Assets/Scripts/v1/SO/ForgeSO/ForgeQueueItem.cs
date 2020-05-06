using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace v1.SO.SOForge
{
    [System.Serializable]
    public class ForgeQueueItem
    {
        public int id;
        public long timeStart;
        public long timeEnd;

        public ForgeQueueItem()
        {

        }

        public ForgeQueueItem(int id, long timeStart, long timeEnd)
        {
            this.id = id;
            this.timeStart = timeStart;
            this.timeEnd = timeEnd;
        }
    }
}