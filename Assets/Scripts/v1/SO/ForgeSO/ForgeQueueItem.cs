using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace v1.SO.SOForge
{
    [System.Serializable]
    public class ForgeQueueItem
    {
        int id;
        long timeStart;
        long timeEnd;
        bool isReady = false;

        public int Id { get => id; set => id = value; }
        public long TimeStart { get => timeStart; set => timeStart = value; }
        public long TimeEnd { get => timeEnd; set => timeEnd = value; }
        public bool IsReady { get => isReady; set => isReady = value; }

        public ForgeQueueItem()
        {

        }

        public ForgeQueueItem(int id, long timeStart, long timeEnd, bool status = false)
        {
            this.Id = id;
            this.TimeStart = timeStart;
            this.TimeEnd = timeEnd;
            this.IsReady = isReady;
        }
    }
}