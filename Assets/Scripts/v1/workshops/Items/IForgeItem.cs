using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IForgeItem
{
    int Id { get; set; }
    int CostGold { get; set; }
    int CostSilver { get; set; }
    int TimeCreation { get; set; }
    string Name { get; set; }
    bool IsOpened { get; set; }
    int TotalPartsForOpen { get; set; }
    int HasPartsForOpen { get; set; }

    int TotalInBag { get; set; }
    int GetFreeQueueLength();
    void StartCreation(int count);
    //int СreatePerTime();
    bool IsSkill();
}
