using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BtnClickActions
{
    HomeMenu,
    ShowAllLvl,
    ChooseLvl,
    PrevoiusMenu,
    ForgeUpgrade,
    PauseMenu,
    GameUI,
    Buy,
    Create,
    ContinueGame,
    GameUi,
    ForgeMain,
    Inventory
};
public class BtnClickInfo : MonoBehaviour
{
    public string clickedInfo = "";
    public GameObject clickedInfoGO;
    public BtnClickActions clickAction;

    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(ThisClickedHandler);
        clickedInfoGO = clickedInfoGO ?? transform.gameObject;
    }

    void ThisClickedHandler()
    {
        GameObject.Find("MenuCanvas").GetComponent<UIController>().ClickedHendler(clickAction, clickedInfo, clickedInfoGO);
    }
}
