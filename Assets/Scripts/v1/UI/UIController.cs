using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    GameObject prevoiusMenu;
    GameObject activeMenu;
    enum AllMenuPagesEN
    {
        HomeMenu,
        ChooseLvl,
        PauseMenu
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
        switch (action)
        {
            case BtnClickActions.ChooseLvl:
                break;
            case BtnClickActions.ShowAllLvl:
                ShowMenuPage(AllMenuPagesEN.ChooseLvl);
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

    void ShowMenuPage(AllMenuPagesEN pageName)
    {
        activeMenu?.SetActive(false);
        activeMenu = AllMenuPages.Find(obj => obj.name == pageName.ToString());
        activeMenu.SetActive(true);
    }
}
