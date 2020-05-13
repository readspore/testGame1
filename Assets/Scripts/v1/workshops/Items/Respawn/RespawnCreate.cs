using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnCreate : MonoBehaviour
{
    public Button createNewRespawn;
    public InputField newNameField;

    void Start()
    {
        createNewRespawn?.onClick.AddListener(CreateNewRespawn);
    }

    void CreateNewRespawn()
    {
        Debug.Log("newName " + newNameField.text);
        var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        Debug.Log("player P  " + playerPosition);
        var respCRUD = new RespawnCRUD();
        respCRUD.AddNewRespawn(0, newNameField.text, playerPosition);
        RespawHelpers.CreateNewRespawn(playerPosition, newNameField.text);
        newNameField.text = "";
    }
}
