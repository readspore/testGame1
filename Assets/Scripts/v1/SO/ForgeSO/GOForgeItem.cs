using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using v1.SO;
using v1.SO.SOItem;
using v1.SO.SOForge;

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

    ForgeQueueItem nearestReady = null;
    float progressMaxWidth;
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
            Debug.Log("TryCreate " + status.ToString());
        }
        TryShowProgress();
    }

    public void TakeReadyItems()
    {

    }

    public void TryBuy()
    {
        var createAmount = int.Parse(amountToCreate.text);
        for (int i = 0; i < createAmount; i++)
        {
            var status = soForge.BuyAndSetToQueue((int)itemType, Currency.Gold);
            Debug.Log("TryBuy " + status.ToString());
        }
    }

    public void TryShowProgress()
    {
        var core = soForge.GetCoreForItem((int)itemType);
        if (core == null)
        {
            Debug.Log("core is null");
            return;
        }
        if (
            core.queue == null
            ||
            core.queue.Count == 0
        )
        {
            Debug.Log("queue is empty");
            return;
        }

        var timeNow = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        //var showProcesStarted = false;
        foreach (var qItem in core.queue)
        {
            //if (showProcesStarted)
            //    continue;
            if (qItem.TimeEnd > timeNow)
            {
                //if (nearestReady != null)
                //{

                //}
                //else
                //{
                nearestReady = qItem;
                RectTransform rt = (RectTransform)progressBar.transform;
                progressMaxWidth = rt.rect.width;

                //}

            }
            //var isReady = item.TimeEnd < timeNow ? "is Ready" : "creation process";
            //Debug.Log("qu " + item.TimeEnd + " " + isReady );
        }
        if (nearestReady != null)
        {
            InvokeRepeating("RenderProgress", 1, 3);
        }
        //int x = 500;
        //int y = 50;
        //Debug.Log(System.Math.Round((double)(x * y / 100)));
    }

    void RenderProgress()
    {
        var timeNow = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        var timeToEnd = nearestReady.TimeEnd - timeNow;
        Debug.Log(" timeToEnd " + timeToEnd);   
        var totalTimeToCraft = nearestReady.TimeEnd - nearestReady.TimeStart;
        //if (totalTimeToCraft >= 100)
        //{
            //Debug.Log(" totalTimeToCraft " + totalTimeToCraft);
        decimal timeInCraft = timeNow - nearestReady.TimeStart;
        var timeDevideCoef = (int)timeInCraft >= 100 ? 100 : 101 / (int)timeInCraft;
        //Debug.Log(" timeInCraft " + timeInCraft);
        //decimal pK = totalTimeToCraft / 100;
        //Debug.Log("pk " + totalTimeToCraft / 100);
        //decimal readyTimePersents = timeInCraft / pK;
        //Debug.Log("readyTimePersents " + readyTimePersents);
        var readyTimePersents = (timeInCraft / totalTimeToCraft) * 100;
        //var readyTimePersents = System.Math.Round((double)(totalTimeToCraft * timeInCraft / timeDevideCoef));
            countDown.text = GenTimeSpanFromSeconds(timeToEnd);
        Debug.Log(
            " timeDevideCoef " + timeDevideCoef + 
            " totalTimeToCraft " + totalTimeToCraft +
            " timeInCraft " + timeInCraft +
            " readyTimePersents " + readyTimePersents +
            ""
            );

        RectTransform rt = (RectTransform)progressBar.transform;
            var readyWidth = progressMaxWidth - ((progressMaxWidth / 100) * (float)readyTimePersents);
            readyWidth = Math.Abs(readyWidth);
            rt.offsetMax = new Vector2(-readyWidth, rt.offsetMax.y);
        //}

        //Debug.Log("% " + readyTimePersents + " readyWidth " + readyWidth + " progressMaxWidth " + progressMaxWidth);
    }

    string GenTimeSpanFromSeconds(long seconds)
    {
        var time = TimeSpan.FromSeconds(seconds);
        return time.ToString(@"mm\:ss");
    }

}
