using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnShowAll : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject allSpawns;
    public GameObject spawnPrefab;

    private void Start()
    {
        ShowAllSpawns();
    }

    void ShowAllSpawns()
    {
        var respCRUD = new RespawnCRUD();
        var spawns = respCRUD.GetAllSpawns();
        if (spawns != null && spawns.Count != 0)
        {
            foreach (var spawn in spawns)
            {
                GameObject spawnWrap = (GameObject)Instantiate(spawnPrefab);
                spawnPrefab.name = "spawn"+spawn.Id;
                spawnPrefab.transform.position = allSpawns.transform.position;
                spawnPrefab.GetComponent<RectTransform>().SetParent(allSpawns     .transform);

                //button.GetComponentInChildren<Text>().text = action.ToString();
                //button.transform.position = gameUIAvailableAction.transform.position;
                //button.GetComponent<RectTransform>().SetParent(gameUIAvailableAction.transform);
                //button.GetComponent<Button>().onClick.AddListener(Radio.Radio.ToggleBtnCameraView);
            }

        }
    }
}
