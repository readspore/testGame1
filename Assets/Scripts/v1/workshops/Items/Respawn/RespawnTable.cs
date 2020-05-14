using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class RespawnTable
{
    int uniqId;
    List<RespawnItem> respawns = new List<RespawnItem>();

    public int UniqId {
        get {
            int maxUniqId = PlayerPrefs.GetInt("RespawnTableMaxUniqId");
            maxUniqId += 1;
            PlayerPrefs.SetInt("RespawnTableMaxUniqId", maxUniqId);
            return maxUniqId;
        }
        set => uniqId = value; 
    }
    public List<RespawnItem> Respawns { get => respawns; set => respawns = value; }

    public RespawnTable()
    {

    }

    public RespawnTable(List<RespawnItem> respawns)
    {
        Respawns = respawns;
    }

}
