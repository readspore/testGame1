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
        Debug.Log(action.ToString());
        switch (action)
        {
            case BtnClickActions.HomeMenu:
                ShowMenuPage(AllMenuPagesEN.HomeMenu);
                break;
            case BtnClickActions.ChooseLvl:
                LoadLvl(int.Parse(info));
                break;
            case BtnClickActions.ShowAllLvl:
                ShowMenuPage(AllMenuPagesEN.ChooseLvl);
                break;
            case BtnClickActions.ForgeMain:
                ShowMenuPage(AllMenuPagesEN.ForgeMain);
                break;
            case BtnClickActions.PauseMenu:
                GameOnPause(true);
                ShowMenuPage(AllMenuPagesEN.PauseMenu);
                break;
            case BtnClickActions.GameUI:
            case BtnClickActions.ContinueGame:
                GameOnPause(false);
                ShowMenuPage(AllMenuPagesEN.GameUi);
                break;
            case BtnClickActions.Buy:
                BuyForgeItemHandler(info);
                break;
            case BtnClickActions.Create:
                CreateForgeItemHandler(info);
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

    public void ShowMenuPage(AllMenuPagesEN pageName)
    {
        activeMenu?.SetActive(false);
        activeMenu = AllMenuPages.Find(obj => obj.name.ToLower() == pageName.ToString().ToLower());
        activeMenu.SetActive(true);
    }
    public GameObject GetMenuPage(AllMenuPagesEN pageName)
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

    void CreateForgeItemHandler(string info)
    {
        var id = int.Parse(info);
        if (Forge.CanCreateItem(id))
        {
            Debug.Log("Can buy");
            Forge.Create(id);
        }
        else
        {
            Debug.Log("Can not buy");
        }
    }

    void BuyForgeItemHandler(string info)
    {
        var id = int.Parse(info);
        if (Forge.CanBuyItem(id))
        {
            Debug.Log("Can buy");
            Forge.Buy(id);
        }
        else
        {
            Debug.Log("Can not buy");
        }
    }

}
