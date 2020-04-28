using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemCharacteristic
{
    Arm,
    UsedTime,
    Сooldown,
    CostSilver,
    CostGold,
};

[CreateAssetMenu]
public class TheCharacteristicSO : ScriptableObject
{
    [SerializeField]
    ItemCharacteristic type;
    [SerializeField]
    string value;
}
