using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace v1.SO.SOForge
{
    [System.Serializable]
    public class ForgeQueue : ScriptableObject
    {
        [SerializeField]
        long timeStart;
        [SerializeField]
        long timeEnd;
        [SerializeField]
        int id;

        public long TimeStart { get => timeStart; set => timeStart = value; }
        public long TimeEnd { get => timeEnd; set => timeEnd = value; }
        public int Id { get => id; set => id = value; }
    }

}