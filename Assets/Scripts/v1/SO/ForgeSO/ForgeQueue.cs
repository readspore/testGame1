using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace v1.SO.ForgeSO
{
    //[CreateAssetMenu]
    public class ForgeQueue : ScriptableObject
    {
        [SerializeField]
        long timeStart;
        [SerializeField]
        long timeEnd;
        [SerializeField]
        public int id;
    }

}