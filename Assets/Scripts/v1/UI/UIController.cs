using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    GameObject prevoiusMenu;
    GameObject activeMenu;
    public GameObject forgItemPrefab;

    public enum AllMenuPagesEN
    {
        HomeMenu,
        ChooseLvl,
        ForgeMain,
        PauseMenu,
        GameUi
    }
    public List<GameObject> AllMenuPages =  new List<GameObject>();

    public GameObject ForgItemPrefab {
        get {
            //Debug.Log("tst");
            //Debug.Log("forgItemPrefab.name " + forgItemPrefab.name);
            //return activeMenu;
            //return forgItemPrefab;
            return Instantiate(forgItemPrefab, transform.position, transform.rotation);
        } 
        set => forgItemPrefab = value; 
    }

    // Start is called before the first frame update
    void Start()
    {
        SetRadioListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRadioListeners()
    {
        Radio.Radio.onPlayerDeath += PlayerDeadHandler;
        Radio.Radio.OnUpdateDirectionHint += UpdateDirectionHintHandler;
    }

    public void ClickedHendler(BtnClickActions action, string info)
    {
        ClickedHendler(action, info, null);
    }
    public void ClickedHendler(BtnClickActions action, string info, GameObject clickedInfoGO)
    {
        //Debug.Log(action.ToString());
        switch (action)
        {
            case BtnClickActions.HomeMenu:
                ShowMenuPage(BtnClickActions.HomeMenu);
                break;
            case BtnClickActions.SkillsHome:
                ShowMenuPage(BtnClickActions.SkillsHome);
                break;
            case BtnClickActions.ForgeMain:
                ShowMenuPage(BtnClickActions.ForgeMain);
                break;
            case BtnClickActions.ForgeUpgradePage:
                ShowMenuPage(BtnClickActions.ForgeUpgradePage);
                break;
            case BtnClickActions.ForgeUpgradeAction:
                ForgeUpgradeAction();
                break;
            case BtnClickActions.TestPage:
                ShowMenuPage(BtnClickActions.TestPage);
                break;
            case BtnClickActions.Inventory:
                ShowMenuPage(BtnClickActions.Inventory);
                break;
            case BtnClickActions.ChooseLvl:
                LoadLvl(int.Parse(info));
                break;
            case BtnClickActions.ShowAllLvl:
                ShowMenuPage(BtnClickActions.ChooseLvl);
                break;
            case BtnClickActions.PauseMenu:
                GameOnPause(true);
                ShowMenuPage(BtnClickActions.PauseMenu);
                break;
            case BtnClickActions.GameUI:
            case BtnClickActions.ContinueGame:
                GameOnPause(false);
                ShowMenuPage(BtnClickActions.GameUi);
                break;
            case BtnClickActions.Buy:
                BuyForgeItemHandler(info);
                break;
            case BtnClickActions.Create:
                CreateForgeItemHandler(clickedInfoGO);
                break;

            // skils
            case BtnClickActions.DeathDeceitActivate:
                DeathDeceitActivate();
                break;
            case BtnClickActions.TimeScaleActivate:
                TimeScaleActivate();
                break;
            case BtnClickActions.InvulnerabilityActivate:
                InvulnerabilityActivate();
                break;
            case BtnClickActions.ArmActivate:
                ArmActivate();
                break;
        }
    }

    public void PlayerDeadHandler()
    {
        Debug.Log(" UIController PlayerDeadHandler");
    }

    public void UpdateDirectionHintHandler( string msg)
    {
        Debug.Log(" UIController UpdateDirectionHintHandler msg " + msg);
    }

    public void ShowMenuPage(BtnClickActions pageName)
    {
        activeMenu?.SetActive(false);
        activeMenu = AllMenuPages.Find(obj => obj.name.ToLower() == pageName.ToString().ToLower());
        activeMenu.SetActive(true);
    }
    public GameObject GetMenuPage(BtnClickActions pageName)
    {
        return AllMenuPages.Find(obj => obj.name.ToLower() == pageName.ToString().ToLower());
    }

    void LoadLvl(int lvlIndex)
    {
        SceneManager.LoadScene(lvlIndex);
    }

    void GameOnPause(bool flag)
    {
        if (flag)
        {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
    }

    void CreateForgeItemHandler(GameObject infoGO)
    {
        Debug.Log("CreateForgeItemHandler");
        //Debug.Log(infoGO.name);
        //var id = infoGO.GetComponent<ItemGO>().id;
        //var howManyCreate = int.Parse(
        //        infoGO.transform.Find("InputField/Text").GetComponent<Text>().text
        //    );
        //if (Forge.CanCreateItem(id, howManyCreate))
        //{
        //    Debug.Log("UI Can create");
        //    Forge.Create( id, howManyCreate );
        //}
        //else
        //{
        //    Debug.Log("Can not create");
        //}
    }

    void BuyForgeItemHandler(string info)
    {
        Debug.Log("BuyForgeItemHandler");
        //var id = int.Parse(info);
        //if (Forge.CanBuyItem(id))
        //{
        //    Debug.Log("Can buy");
        //    Forge.Buy(id);
        //}
        //else
        //{
        //    Debug.Log("Can not buy");
        //}
    }

    void DeathDeceitActivate()
    {
        DeathDeceit.Activate();
        StartCoroutine(DeathDeDeceitActivate());
    }

    IEnumerator DeathDeDeceitActivate()
    {
        yield return new WaitForSeconds(DeathDeceit.CurrentLvlActiveTime);
        DeathDeceit.DeActivate();
        yield return null;
    }
    void TimeScaleActivate()
    {
        Transform.FindObjectOfType<Player>().ActivateTimeScale();
        //TimeScale.Activate();
    }

    void InvulnerabilityActivate()
    {
        Invulnerability.Activate();
        StartCoroutine(InvulnerabilityDeActivate());
    }

    IEnumerator InvulnerabilityDeActivate()
    {
        yield return new WaitForSeconds(Invulnerability.CurrentLvlActiveTime);
        Invulnerability.DeActivate();
        yield return null;
    }

    void ArmActivate()
    {
        Arm.Activate();
    }

    void ForgeUpgradeAction()
    {
        Debug.Log("ForgeUpgradeAction");
    }
}
