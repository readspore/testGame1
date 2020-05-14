using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RespawnCRUD
{
    FileSave fileSave;
    RespawnTable respawnTable;

    public FileSave FileSave {
        get {
            if (fileSave == null)
                fileSave = new FileSave(FileFormat.Xml);
            return fileSave;
        } 
        set => fileSave = value; 
    }

    public RespawnTable RespawnTable
    {
        get
        {
            if (respawnTable == null)
            {
                respawnTable = FileSave.ReadFromFile<RespawnTable>(Constants.pathToRespawnTable);
                respawnTable = respawnTable ?? new RespawnTable();
            }
            return respawnTable;
        }
        set => respawnTable = value;
    }
    void Start()
    {

    }

    public void AddNewRespawn(int scene, string name, Vector3 position)
    {
        var respawnItem = new RespawnItem();
        respawnItem.Id = RespawnTable.UniqId;
        respawnItem.Name = name;
        respawnItem.Position = position;
        RespawnTable.Respawns.Add(respawnItem);
        SaveChanges();
    }

    public void RemoveRespawn(int spawnId)
    {
        var itemToRemove = RespawnTable.Respawns.Single(r => r.Id == spawnId);
        RespawnTable.Respawns.Remove(itemToRemove);
        SaveChanges();
    }

    public List<RespawnItem> GetAllSpawns()
    {
        return RespawnTable.Respawns;
    }

    void SaveChanges()
    {
        FileSave.WriteToFile(Constants.pathToRespawnTable, RespawnTable);
    }
}
