using System;
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
    int totalInBag;
    int createPerTime;
    public ForgeItem(int id, string name, int costGold, int costSilver, int timeCreation, int createPerTime)
    {
        this.id = id;
        this.costGold = costGold;
        this.costSilver = costSilver;
        this.timeCreation = timeCreation;
        this.name = name;
        this.createPerTime = createPerTime;

        totalInBag = PlayerPrefs.GetInt("totalInBag" + id);
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

    public int TotalInBag { get => totalInBag; set => totalInBag = value; }

    public void AddPartForItem()
    {
        PlayerPrefs.SetInt("ForgeItemHasPartsForOpen" + id, PlayerPrefs.GetInt("ForgeItemHasPartsForOpen" + id + 1));
    }

    public bool IsOnCreationg()
    {
        return
            new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()
            <
            Convert.ToInt64(
                PlayerPrefs.GetString("endCreation" + id)
            );
    }

    public void StartCreation(int count)
    {
        var TimestampStart= new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        PlayerPrefs.SetString("startCreation" + id, TimestampStart.ToString());
        var TimestampEnd = new DateTimeOffset(DateTime.UtcNow).AddSeconds(300).ToUnixTimeSeconds();
        PlayerPrefs.SetString("endCreation" + id, TimestampEnd.ToString());
        Debug.Log("Start Cretaion " + id);
        Debug.Log("TimestampStart " + TimestampStart.ToString() + " TimestampEnd " + TimestampEnd.ToString());
    }

    public int TakeFromForge()
    {
        return createPerTime;
    }
}
