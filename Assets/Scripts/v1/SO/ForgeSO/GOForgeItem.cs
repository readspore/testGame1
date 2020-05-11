using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using v1.SO;
using v1.SO.SOItem;
using v1.SO.SOForge;
using System.Linq;
using SaveSystem;

public class GOForgeItem : MonoBehaviour
{
    public SOForge soForge;
    public SOItemObjId itemType;
    public Button btnBuy;
    public Button btnCreate;
    public Button takeReadyItems;
    public Text amountToCreate;
    public GameObject progressBar;
    public Text countDown;
    public Text amountInCreation;
    public Text amountReady;

    ForgeQueueItem nearestReady = null;
    private float progressMaxWidth;

    public float ProgressMaxWidth { get => progressMaxWidth; 
        set
        {
            if (progressMaxWidth != 0)
            {
                return;
            }
            Debug.Log("SET ProgressMaxWidth = " + value);
            progressMaxWidth = value;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        btnBuy?.onClick.AddListener(TryBuy);
        btnCreate?.onClick.AddListener(TryCreate);
        takeReadyItems?.onClick.AddListener(TakeReadyItems);
        SetItemInfo();
    }

    public void SetItemInfo()
    {
        var item = AssetDatabase.LoadAssetAtPath<SOItem>(
            Constants.pathToSOImplementationItems + "/" + itemType.ToString() + ".asset"
        );
        //Debug.Log("item " + item.Name);
        TryShowProgress();
    }

    public void TryCreate()
    {
        soForge.T_ClearCores();
        if (String.IsNullOrEmpty(amountToCreate.text))
            return;
        var createAmount = int.Parse(amountToCreate.text);
        for (int i = 0; i < createAmount; i++)
        {
            var status = soForge.BuyAndSetToQueue((int)itemType, Currency.Silver);
            //Debug.Log("TryCreate " + status.ToString());
        }
        TryShowProgress();
    }

    public void TakeReadyItems()
    {
        //TryShowProgress();
        soForge.T_ClearCores();
        //Debug.Log("TakeReadyItems clicked");
        //UpdateAmountCreationReady();
    }

    public void TryBuy()
    {
        var createAmount = int.Parse(amountToCreate.text);
        for (int i = 0; i < createAmount; i++)
        {
            var status = soForge.BuyAndSetToQueue((int)itemType, Currency.Gold);
            //Debug.Log("TryBuy " + status.ToString());
        }
    }

    public void TryShowProgress()
    {
        ResetProgressBar();
        ShowTakeReady(false);
        var core = soForge.GetCoreForItem((int)itemType);
        if (core == null)
        {
            //Debug.Log("core is null");
            return;
        }
        if (
            core.queue == null
            ||
            core.queue.Count == 0
        )
        {
            //Debug.Log("queue is empty");
            return;
        }

        //var timeNow = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        try
        {
        nearestReady = core.queue
            .FindAll(i => !i.IsReady)
            .Aggregate((i1, i2) => i1.TimeEnd < i2.TimeEnd ? i1 : i2);
        }
        catch (InvalidOperationException e)
        {
            //Debug.Log("TryShowProgress InvalidOperationException");
            return;
        }
        RectTransform rt = (RectTransform)progressBar.transform;
        ProgressMaxWidth = rt.rect.width;
        if (nearestReady != null)
        {
            //Debug.Log("START InvokeRepeating");
            InvokeRepeating("RenderProgress", 1, 3);
        }
    }

    void RenderProgress()
    {
        var timeNow = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        var timeToEnd = nearestReady.TimeEnd - timeNow;
        if (timeToEnd <= 0)
        {
            CancelInvoke("RenderProgress");
            ItemIsReady();
            CancelInvoke();
            ResetProgressBar();
            TryShowProgress();
            return;
        }
        countDown.text = GetTimeSpanFromSeconds(timeToEnd);

        var totalTimeToCraft = nearestReady.TimeEnd - nearestReady.TimeStart;
        decimal timeInCraft = timeNow - nearestReady.TimeStart;
        var readyTimePersents = (int)((timeInCraft / totalTimeToCraft) * 100);
        //Debug.Log(
        //    " totalTimeToCraft " + totalTimeToCraft +
        //    " timeInCraft " + timeInCraft +
        //    " readyTimePersents " + readyTimePersents +
        //    ""
        //    );

        RectTransform rt = (RectTransform)progressBar.transform;
        var readyWidth = ProgressMaxWidth - ((ProgressMaxWidth / 100f) * readyTimePersents);
        //var readyWidth = progressMaxWidth - ((progressMaxWidth / 100) * readyTimePersents);
        readyWidth = Math.Abs(readyWidth);
        Debug.Log("readyWidth " + readyWidth);
        Debug.Log(
            " progressMaxWidth " + ProgressMaxWidth +
            " readyTimePersents " + readyTimePersents +
            ""
            );
        rt.offsetMax = new Vector2(-readyWidth, rt.offsetMax.y);
        //}
        //Debug.Log("progress for " + nearestReady.Id);
        //Debug.Log("% " + readyTimePersents + " readyWidth " + readyWidth + " progressMaxWidth " + progressMaxWidth);
    }

    string GetTimeSpanFromSeconds(long seconds)
    {
        var time = TimeSpan.FromSeconds(seconds);
        return time.ToString(@"mm\:ss");
    }

    void ItemIsReady()
    {
        //Debug.Log("Item is ready " + nearestReady.Id);
        //CancelInvoke();
        nearestReady = null;
        var core = soForge.GetCoreForItem((int)itemType);
        //if (core == null)
        //{
        ////    Debug.Log("core is null");
        //    UpdateAmountCreationReady();
        //    return;
        //}
        //if (
        //    core.queue == null
        //    ||
        //    core.queue.Count == 0
        //)
        //{
        ////    Debug.Log("queue is empty");
        //    UpdateAmountCreationReady();
        //    return;
        //}
        var timeNow = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        //var createdItems = core.queue.Select(i => i.IsReady).ToList();
        //core.queue.ToList().ForEach(
        //    ( item ) => 
        //    {
        ////        Debug.Log(" ToList().ForEach( " + (item.TimeEnd <= timeNow ? true : false) + "  " + item.TimeEnd + "  " +   timeNow);
        //        item.IsReady = item.TimeEnd <= timeNow ? true : false;
        //    }
        // );
        var r = core.queue.Select(item => item.IsReady = item.TimeEnd <= timeNow ? true : false);
        List<ForgeQueueItem> readyItems = new List<ForgeQueueItem>();
        for (int i = 0; i < core.queue.Count; i++)
        {
            if (core.queue[i].TimeEnd <= timeNow)
            {
                core.queue[i].IsReady = true;
            }
        }
        //foreach (var item in core.queue)
        //{
        //    item.IsReady = item.TimeEnd <= timeNow ? true : false;
        //}
        foreach (var item in core.queue)
        {
            if (item.IsReady)
            {
                //Debug.Log("ItemIsReady " + item.Id + " " + (item.IsReady ? " ready " : " process "));
            }
        }
        FileSave fileSave = new FileSave(FileFormat.Xml);
        var coreIndex = soForge.ItemCoreIndex((int)itemType);
        //Debug.Log("Data Path " + Application.persistentDataPath);
        fileSave.WriteToFile(
            Application.persistentDataPath + "/Core" + coreIndex + ".xml",
            core
        );
        UpdateAmountCreationReady();
    }

    void UpdateAmountCreationReady()
    {
        var core = soForge.GetCoreForItem((int)itemType);
        int createdItems = -1;
        int queueReady = -1;

        if (core == null)
        {
            //Debug.Log("core is null");
            queueReady = 0;
            createdItems = 0;
            //return;
        }
        if (
            core.queue == null
            ||
            core.queue.Count == 0
        )
        {
            //Debug.Log("queue is empty");
            queueReady = 0;
            createdItems = 0;
        }
        createdItems = createdItems == -1 
            ? core.queue.Select(i => i.IsReady).ToList().Count
            : createdItems;
        queueReady = queueReady == -1
            ? core.queue.Count - createdItems
            : queueReady;

        amountInCreation.text = createdItems.ToString();
        amountReady.text = queueReady.ToString();
        if (queueReady != 0)
        {
            ShowTakeReady(true);
        } else
        {
            ShowTakeReady(false);
        }
    }

    void ShowTakeReady(bool flag)
    {
        takeReadyItems?.gameObject.SetActive(flag);
        //if (!flag)
        //{
        //    TryShowProgress();
        //}
    }

    void ResetProgressBar() {
        RectTransform rt = (RectTransform)progressBar.transform;
        rt.offsetMax = new Vector2(-ProgressMaxWidth, rt.offsetMax.y);
        //Debug.Log("ResetProgressBar " + (-progressMaxWidth));
    }
}
