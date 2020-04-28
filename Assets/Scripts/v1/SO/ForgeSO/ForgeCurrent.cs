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
        ForgeQueue queu1;
        [SerializeField]
        ForgeQueue queu2;
        [SerializeField]
        ForgeQueue queu3;
        [SerializeField]
        ForgeQueue queu4;
    }

}