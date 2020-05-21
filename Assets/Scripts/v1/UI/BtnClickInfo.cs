using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BtnClickActions
{
    // ДОБАВЛЯТЬ ТОЛЬКО В КОНЕЦ!!!!
    HomeMenu,
    ShowAllLvl,
    ChooseLvl,
    PrevoiusMenu,
    ForgeUpgradePage,
    ForgeUpgradeAction,
    PauseMenu,
    GameUI,
    Buy,
    Create,
    ContinueGame,
    GameUi,
    ForgeMain,
    Inventory,
    TestPage,
    DeathDeceitActivate,
    TimeScaleActivate,
    InvulnerabilityActivate,
    ArmActivate,
    SkillsHome,
    TheSkill,
    ShowRespawnPanel,
    ShowRespawnCreate,
    ShowRespawnViewAll,
    RespawnCreate,
    Restart,
    RestartOnSpawn,
    DeadMenu,
    QuitGame
    // ДОБАВЛЯТЬ ТОЛЬКО В КОНЕЦ!!!!
};
public class BtnClickInfo : MonoBehaviour
{
    public string clickedInfo = "";
    public GameObject clickedInfoGO;
    public BtnClickActions clickAction;

    void Start()
    {
        transform.GetComponent<Button>()?.onClick.AddListener(ThisClickedHandler);
        clickedInfoGO = clickedInfoGO ?? transform.gameObject;
    }

    void ThisClickedHandler()
    {
        GameObject.Find("MenuCanvas").GetComponent<UIController>().ClickedHendler(clickAction, clickedInfo, clickedInfoGO);
    }

    public void EmulateClick()
    {
        ThisClickedHandler();
    }
}
