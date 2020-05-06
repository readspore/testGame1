using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace v1.SO.SOForge
{
    [System.Serializable]
    public class ForgeQueueItem
    {
         //int id;
         long timeStart;
         long timeEnd;

        //public int Id { get => id; set => id = value; }
        public long TimeStart { get => timeStart; set => timeStart = value; }
        public long TimeEnd { get => timeEnd; set => timeEnd = value; }

        public ForgeQueueItem()
        {

        }

        public ForgeQueueItem(long timeStart, long timeEnd)
        {
            //this.Id = id;
            this.TimeStart = timeStart;
            this.TimeEnd = timeEnd;
        }
    }
}