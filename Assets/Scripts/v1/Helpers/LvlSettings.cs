using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSettings : MonoBehaviour
{
    public BtnClickActions uiEmulateClick;
    UIController uIController;

    public UIController UIController { 
        get {
            return GameObject.FindObjectOfType<UIController>();
        } 
        set => uIController = value; 
    }

    void Start()
    {
        UnityEditor.AssetDatabase.Refresh();

        if (uiEmulateClick != null)
        {
            UIController.ClickedHendler(uiEmulateClick, "");
            //UIController.ClickedHendler(BtnClickActions.ForgeMain, "");
        }

        RenderAllRespawns();
    }

    void RenderAllRespawns()
    {
        var respCrud = new RespawnCRUD();
        foreach (var item in respCrud.GetAllSpawnsOnSceve())
        {
            item.RenderOnLvl();
        }
    }

}
