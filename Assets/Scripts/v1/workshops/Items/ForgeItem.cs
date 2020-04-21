using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeItem : IForgeItem
{
    int id;
    int costGold;
    int costSilver;
    int timeCreation;
    string name;
    bool isOpened;
    int totalPartsForOpen;
    int hasPartsForOpen;
    public ForgeItem(int id, string name, int costGold, int costSilver, int timeCreation)
    {
        this.id = id;
        this.costGold = costGold;
        this.costSilver = costSilver;
        this.timeCreation = timeCreation;
        this.name = name;
    }
    public int CostGold { get => costGold; set => costGold = value; }
    public int CostSilver { get => costSilver; set => costSilver = value; }
    public int TimeCreation { get => timeCreation; set => timeCreation = value; }
    public string Name { get => name; set => name = value; }
    public int Id { get => id; set => id = value; }
    public bool IsOpened { 
        get => TotalPartsForOpen == HasPartsForOpen;
        set { } 
    }
    public int TotalPartsForOpen
    {
        get
        {
            if (totalPartsForOpen == 0)
            {
                totalPartsForOpen = PlayerPrefs.GetInt("ForgeItemTotalPartsForOpen" + id);
            }
            return totalPartsForOpen;
        }
        set { }
    }
    public int HasPartsForOpen
    {
        get
        {
            if (totalPartsForOpen == 0)
            {
                totalPartsForOpen = PlayerPrefs.GetInt("ForgeItemHasPartsForOpen" + id);
            }
            return totalPartsForOpen;
        }
        set { }
    }

    public void AddPartForItem()
    {
        PlayerPrefs.SetInt("ForgeItemHasPartsForOpen" + id, PlayerPrefs.GetInt("ForgeItemHasPartsForOpen" + id + 1));
    }
}
