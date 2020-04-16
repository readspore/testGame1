using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class RespawHelpers
{
    public static void CreateNewRespawn(Vector3 position, string name)
    {
        int freeRespawns = PlayerPrefs.GetInt("FreeRespawns");
        if (freeRespawns <= 0) return;
        int newMaxRespawnId = GetNewId();
        AddIdRespawnsOnLvl(newMaxRespawnId);
        SaveNewRespawnInfo(newMaxRespawnId, position, name);
        UpdateFreeRespawns();
        Debug.Log("created " + newMaxRespawnId);
    }

    public static int GetNewId()
    {
        int newMaxRespawnId = PlayerPrefs.GetInt("maxRespawnId") + 1;
        PlayerPrefs.SetInt("maxRespawnId", newMaxRespawnId);
        return newMaxRespawnId;
    }

    public static void AddIdRespawnsOnLvl(int newMaxRespawnId)
    {
        string respanUsednOnLvl = PlayerPrefs.GetString("respanUsednOnLvl" + SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetString("respanUsednOnLvl" + SceneManager.GetActiveScene().buildIndex, respanUsednOnLvl + "|" + newMaxRespawnId);
    }

    public static void SaveNewRespawnInfo(int newMaxRespawnId, Vector3 position, string name)
    {
        string respawnInfo = newMaxRespawnId + "|" + position.x + "|" + position.y + "|" + name;
        PlayerPrefs.SetString("respawn" + newMaxRespawnId, respawnInfo);
    }

    public static void UpdateFreeRespawns(int freeRespawns)
    {
        freeRespawns -= 1;
        PlayerPrefs.SetInt("freeRespawns", freeRespawns);
    }

    public static void UpdateFreeRespawns()
    {
        int freeRespawns = PlayerPrefs.GetInt("freeRespawns");
        freeRespawns -= 1;
        PlayerPrefs.SetInt("freeRespawns", freeRespawns);
    }

    public static List<int> GetAllRespawnIdsOnLvl()
    {
        string idsSting = PlayerPrefs.GetString("respanUsednOnLvl" + SceneManager.GetActiveScene().buildIndex);
        string[] splitedIds = idsSting.Split('|');
        List<int> respawnsIndOnLvl = new List<int>();
        foreach (var item in splitedIds)
        {
            if (String.IsNullOrEmpty(item))
                continue;
            respawnsIndOnLvl.Add(Int32.Parse(item));
        }
        return respawnsIndOnLvl;
    }

}
