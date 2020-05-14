using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                spawnWrap.name = "spawn"+spawn.Id;
                spawnWrap.transform.position = allSpawns.transform.position;
                spawnWrap.GetComponent<RectTransform>().SetParent(allSpawns.transform);

                var btnChoose = spawnWrap.transform.Find("Choose");
                var btnEdit = spawnWrap.transform.Find("Edit");
                var btnRemove = spawnWrap.transform.Find("Remove");
                var nameText = spawnWrap.transform.Find("nameText").GetComponent<Text>();
                nameText.text = spawn.Name;
                btnChoose.GetComponent<Button>().onClick.AddListener(
                    () => {
                        spawn.MoveToRespawn();
                        GameObject.FindObjectOfType<UIController>().ClickedHendler(BtnClickActions.GameUI, "");
                    }
                );
                btnEdit.GetComponent<Button>().onClick.AddListener(() => BtnEditHandler(spawn.Id));
                btnRemove.GetComponent<Button>().onClick.AddListener(() => BtnRemoveHandler(spawn.Id, spawnWrap));
            }
        }
    }

    void BtnChooseHandler(int spawnId)
    {
        Debug.Log("BtnChooseHandler");
    }

    void BtnEditHandler(int spawnId)
    {
        Debug.Log("BtnEditHandler");
    }

    void BtnRemoveHandler(int spawnId, GameObject spawnWrap)
    {
        var respCrud = new RespawnCRUD();
        respCrud.RemoveRespawn(spawnId);
        spawnWrap.SetActive(false);
        Debug.Log("BtnRemoveHandler");
    }
}
