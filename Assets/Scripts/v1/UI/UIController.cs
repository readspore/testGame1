﻿using System;
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
    public GameObject gameUIAvailableAction;
    public GameObject availableActionButtonPrefab;

    //public enum AllMenuPagesEN
    //{
    //    HomeMenu,
    //    ChooseLvl,
    //    ForgeMain,
    //    PauseMenu,
    //    GameUi
    //}
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
    void OnEnable()
    {
        SetRadioListeners();
    }

    void OnDisable()
    {
        RemoveRadioListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRadioListeners()
    {
        Radio.Radio.onPlayerDeath += PlayerDeadHandler;
        Radio.Radio.OnUpdateDirectionHint += UpdateDirectionHintHandler;
        Radio.Radio.OnToggleAvailableAction += ToggleAvailableActionHandler;
    }

    void RemoveRadioListeners()
    {
        Radio.Radio.onPlayerDeath -= PlayerDeadHandler;
        Radio.Radio.OnUpdateDirectionHint -= UpdateDirectionHintHandler;
        Radio.Radio.OnToggleAvailableAction -= ToggleAvailableActionHandler;
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
            //case BtnClickActions.HomeMenu:
            //    ShowMenuPage(BtnClickActions.HomeMenu);
            //    break;
            //case BtnClickActions.SkillsHome:
            //    ShowMenuPage(BtnClickActions.SkillsHome);
            //    break;
            //case BtnClickActions.ForgeMain:
            //    ShowMenuPage(BtnClickActions.ForgeMain);
            //    break;
            //case BtnClickActions.ForgeUpgradePage:
            //    ShowMenuPage(BtnClickActions.ForgeUpgradePage);
            //    break;
            //case BtnClickActions.TestPage:
            //    ShowMenuPage(BtnClickActions.TestPage);
            //    break;
            //case BtnClickActions.Inventory:
            //    ShowMenuPage(BtnClickActions.Inventory);
            //    break;
            //case BtnClickActions.ShowAllLvl:
            //    ShowMenuPage(BtnClickActions.ChooseLvl);
            //    break;
            case BtnClickActions.ForgeUpgradeAction:
                ForgeUpgradeAction();
                break;
            case BtnClickActions.ChooseLvl:
                LoadLvl(int.Parse(info));
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
            case BtnClickActions.Restart:
                Restart();
                break;
            case BtnClickActions.RestartOnSpawn:
                RestartOnSpawn();
                break;
            case BtnClickActions.QuitGame:
                QuitGame();
                break;
            case BtnClickActions.HomeMenu:
                ShowHomeMenu();
                break;
            default:
                ShowMenuPage(action);
                break;
        }
    }

    private void ShowHomeMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayerDeadHandler()
    {
        Time.timeScale = 0;
        ShowMenuPage(BtnClickActions.DeadMenu);
        Debug.Log(" UIController PlayerDeadHandler");
    }

    public void UpdateDirectionHintHandler( string msg)
    {
        Debug.Log(" UIController UpdateDirectionHintHandler msg " + msg);
    }

    public void ToggleAvailableActionHandler(BtnAvailableAction action)
    {
        bool existBtn = gameUIAvailableAction.transform.Find(action.ToString()) == null
            ? false
            : true;
        if (!existBtn)
        {
            CreateAvailableActionBtn(action);
        }
        else
        {
            Destroy(
                gameUIAvailableAction.transform.Find(action.ToString()).gameObject
            );
        }
    }

    void CreateAvailableActionBtn(BtnAvailableAction action)
    {
        GameObject button = (GameObject)Instantiate(availableActionButtonPrefab);
        button.name = action.ToString();
        button.GetComponentInChildren<Text>().text = action.ToString();
        button.transform.position = gameUIAvailableAction.transform.position;
        button.GetComponent<RectTransform>().SetParent(gameUIAvailableAction.transform);
        button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        button.GetComponent<Button>().onClick.AddListener(Radio.Radio.ToggleBtnCameraView);
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

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RestartOnSpawn()
    {

    }

    void QuitGame()
    {
        Application.Quit();
    }
}
