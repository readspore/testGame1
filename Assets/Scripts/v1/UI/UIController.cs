using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    GameObject prevoiusMenu;
    GameObject activeMenu;
    public enum AllMenuPagesEN
    {
        HomeMenu,
        ChooseLvl,
        ForgeMain,
        PauseMenu,
        GameUi
    }
    public List<GameObject> AllMenuPages =  new List<GameObject>();
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
}
