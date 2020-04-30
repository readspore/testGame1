using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemAttrType
{
    Kd,
    TimeApplied,
    SilverCost,
    GoldCost,
    Arm,
    TimeScale,
    DeathDeceitHP,
    ForgeCraftSpeed,
    ForgeMaxQueue,
    ForgeChanceSecondItem,
    ForgeFreeCors
};

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
    List<InputOutputData> lvl1 = new List<InputOutputData>();
    [SerializeField]
    List<InputOutputData> lvl2 = new List<InputOutputData>();
    [SerializeField]
    List<InputOutputData> lvl3 = new List<InputOutputData>();
    [SerializeField]
    List<InputOutputData> lvl4 = new List<InputOutputData>();
    [SerializeField]
    List<InputOutputData> lvl5;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public int MaxLvl { get => maxLvl; set => maxLvl = value; }
    public int CurLvl { get => curLvl; set => curLvl = value; }

    public string GetAttrValue(ItemAttrType attrnName)
    {
        if (CurLvl == 0)
            return "notOpened";
        var listToSelect = lvl1;
        switch (CurLvl)
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
        return listToSelect.Find(obj => obj.name == attrnName)?.value ?? "null";
    }
}
