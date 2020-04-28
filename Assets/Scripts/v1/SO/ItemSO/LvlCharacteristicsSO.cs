using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LvlCharacteristicsSO : ScriptableObject
{
    [SerializeField]
    List<TheCharacteristicSO> characteristics;
}
