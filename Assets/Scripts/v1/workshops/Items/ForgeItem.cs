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
    bool isSkill;
    int totalPartsForOpen;
    int hasPartsForOpen;
    int totalInBag;
    int createPerTime;
    public ForgeItem(int id, string name, int costGold, int costSilver, int timeCreation, int createPerTime, bool isSkill)
    {
        this.id = id;
        this.costGold = costGold;
        this.costSilver = costSilver;
        this.timeCreation = timeCreation;
        this.name = name;
        this.createPerTime = createPerTime;
        this.isSkill = isSkill;

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

    public bool IsSkill()
    {
        return isSkill;
    }

    public void StartCreation(int count)
    {
        //Debug.Log("Start Cretaion " + id);
        for (int i = 0; i < count; i++)
        {
            //Debug.Log("for i " + i);
            var timeNow = new DateTimeOffset(DateTime.UtcNow);
            AddToQueue(GetFreeQueuePosition(timeNow), timeNow.ToUnixTimeSeconds() + timeCreation);
        }
    }

    //public int СreatePerTime()
    //{
    //    return createPerTime;
    //}

    public int CountOfReadyItems()
    {
        return 1;
    }

    public int CountOfQueueItems()
    {
        return 1;
    }

    void AddToQueue(int queuePosition, long timeCreationFinish)
    {
        AddToQueue(queuePosition, timeCreationFinish.ToString());
    }
    void AddToQueue(int queuePosition, string timeCreationFinish)
    {
        if (queuePosition == -1)
        {
            Debug.Log("queue is full");
            return;
        }
        Debug.Log("AddToQueue on position " + queuePosition);
        PlayerPrefs.SetString("creationTimeEnd" + id + "" + queuePosition, timeCreationFinish);
        PlayerPrefs.SetInt("totalInQueue" + id, PlayerPrefs.GetInt("totalInQueue") + 1);
    }

    void RemoveFromQueue(int queuePosition)
    {
        //PlayerPrefs.SetString("creationTimeEnd" + id + "" + queuePosition, 0);
        PlayerPrefs.SetInt("totalInQueue" + id, PlayerPrefs.GetInt("totalInQueue") - 1);
    }

    int GetFreeQueuePosition()
    {
        return GetFreeQueuePosition(new DateTimeOffset(DateTime.UtcNow));
    }
    int GetFreeQueuePosition(DateTimeOffset timeNow)
    {
        var i = 1;
        var freePosition = -1;
        while (i <= Forge.MaxQueue && freePosition == -1)
        {
            var creationTimeEndSTR = PlayerPrefs.GetString("creationTimeEnd" + id + "" + i);
            creationTimeEndSTR  = creationTimeEndSTR  == "" ? "0" : creationTimeEndSTR;
            var creationTimeEnd = Convert.ToInt64(creationTimeEndSTR);
            //Debug.Log("i == " + i + "creationTimeEnd " + creationTimeEnd + " timeNow " + timeNow.ToUnixTimeSeconds());
            if (creationTimeEnd < timeNow.ToUnixTimeSeconds())
            {
                //Debug.Log("SET NEW freePosition " + i);
                freePosition = i;
            }
            ++i;
        }
        //Debug.Log("freePosition " + freePosition);
        return freePosition;
    }

    public int GetFreeQueueLength()
    {
        return GetFreeQueueLength(new DateTimeOffset(DateTime.UtcNow));
    }
    public int GetFreeQueueLength(DateTimeOffset timeNow)
    {
        var i = 1;
        var freePosition = 0;
        while (i <= Forge.MaxQueue )
        {
            var creationTimeEndSTR = PlayerPrefs.GetString("creationTimeEnd" + id + "" + i);
            creationTimeEndSTR  = creationTimeEndSTR  == "" ? "0" : creationTimeEndSTR;
            var creationTimeEnd = Convert.ToInt64(creationTimeEndSTR);
            if (creationTimeEnd < timeNow.ToUnixTimeSeconds())
            {
                ++freePosition;
            }
            ++i;
        }
        //Debug.Log("freePosition LEN " + freePosition);
        return freePosition;
    }
}
