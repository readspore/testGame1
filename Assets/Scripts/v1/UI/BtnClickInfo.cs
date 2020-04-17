using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BtnClickActions {
    HomeMenu,
    ShowAllLvl,
    ChooseLvl,
    PrevoiusMenu,
    ForgeUpgrade,
    PauseMenu,
    GameUI,
    Byu,
    Create,
    ContinueGame,
    ForgeMain
};
public class BtnClickInfo : MonoBehaviour
{
    public string clickedInfo = "";
    public BtnClickActions clickAction;

    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(ThisClickedHandler);
    }

    void ThisClickedHandler()
    {
        GameObject.Find("MenuCanvas").GetComponent<UIController>().ClickedHendler(clickAction, clickedInfo);
    }
}
