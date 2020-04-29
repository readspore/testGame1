using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    [SerializeField]
    int id;
    [SerializeField]
    string name;
    [SerializeField]
    int maxLvl;
    [SerializeField]
    int curLvl;
    [SerializeField]
    List<StringStringDictionary> attrs = null;

}
