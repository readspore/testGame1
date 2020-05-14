using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RespawnItem
{
    int id;
    string name;
    Vector3 position;
    private int sceneId;

    public int Id { get => id; set => id = value; }
    public string Name { get => name; set => name = value; }
    public Vector3 Position { get => position; set => position = value; }
    public int SceneId { get => sceneId; set => sceneId = value; }

    public RespawnItem()
    {

    }

    public RespawnItem(int id, string name, Vector3 position, int sceneId)
    {
        Id = id;
        Name = name;
        Position = position;
        SceneId = sceneId;
    }

    public void MoveToRespawn()
    {
        GameObject.Find("Player").transform.position = Position;
    }

}
