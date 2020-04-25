using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class TimeScale
{
    public static bool isActive = false;
    static int maxLvl = 3;

    static int SilverCostLvl1 = 100;
    static int SilverCostLvl2 = 200;
    static int SilverCostLvl3 = 300;

    static int GoldCostLvl1 = 10;
    static int GoldCostLvl2 = 20;
    static int GoldCostLvl3 = 30;

    static int timeApplayedLvl1 = 3;
    static int timeApplayedLvl2 = 4;
    static int timeApplayedLvl3 = 5;

    static float scaleOnLvl1 = 0.50f;
    static float scaleOnLvl2 = 0.50f;
    static float scaleOnLvl3 = 0.20f;

    public static int CurrentLvl
    {
        get
        {
            return PlayerPrefs.GetInt("TimeScale");
        }
    }

    public static int CurrentLvlActiveTime
    {
        get
        {
            float time = 0;
            switch (CurrentLvl)
            {
                case 1:
                    time = timeApplayedLvl1 * scaleOnLvl1;
                    break;
                case 2:
                    time = timeApplayedLvl2 * scaleOnLvl2;
                    break;
                case 3:
                    time = timeApplayedLvl3 * scaleOnLvl3;
                    Debug.Log("switch " + timeApplayedLvl3 + " " + scaleOnLvl3);
                    Debug.Log("time " + time);
                    break;
            }
            return (int) time;
        }
    }

    public static float ScaleOnCurentLvl
    {
        get
        {
            var scale = 0f;
            switch (CurrentLvl)
            {
                case 1:
                    scale = scaleOnLvl1;
                    break;
                case 2:
                    scale = scaleOnLvl2;
                    break;
                case 3:
                    scale = scaleOnLvl3;
                    break;
            }
            return scale;
        }
    }

    public static int CurrentLvlSilverCost
    {
        get
        {
            int cost = 0;
            switch (CurrentLvl)
            {
                case 0:
                    cost = SilverCostLvl1;
                    break;
                case 1:
                    cost = SilverCostLvl2;
                    break;
                case 2:
                    cost = SilverCostLvl3;
                    break;
            }
            return cost;
        }
    }

    public static int CurrentLvlGoldCost
    {
        get
        {
            var curLvl = CurrentLvl;
            int cost = 0;
            switch (curLvl)
            {
                case 0:
                    cost = GoldCostLvl1;
                    break;
                case 1:
                    cost = GoldCostLvl2;
                    break;
                case 2:
                    cost = GoldCostLvl3;
                    break;
            }
            return cost;
        }
    }

    public static bool Activate()
    {
        //if (isActive)
        //    return false;
        //isActive = true;
        //Time.timeScale = ScaleOnCurentLvl;
        ////Radio.Radio.onTimeScaleEnd += DeActivate;
        ////Task.Delay(CurrentLvlActiveTime).ContinueWith(t => {
        ////    Radio.Radio.TimeScaleEnd();
        ////    //DeActivate();
        ////}
        ////);
        ////DeActivate(true);
        //Debug.Log("Activate ");
        //Debug.Log("Time " + CurrentLvlActiveTime);
        //Debug.Log("Scale " + ScaleOnCurentLvl);
        return true;
    }

    public static void DeActivate()
    {

        //Transform.FindObjectOfType<Player>().DeActivateTimeScale();

        //Debug.Log("DeActivate 1");
        //Time.timeScale = 1;
        //isActive = false;
        //Debug.Log("DeActivate 2");
    }

    //public static float DeathBlow()
    //{
    //    if (!isActive)
    //        return 0;
    //    DeActivate();
    //    Debug.Log("DeathBlow " + ScaleOnCurentLvl);
    //    return ScaleOnCurentLvl;
    //}

    public static bool TryUpgradeTimeScale(Currency currency)
    {
        if (
            (
                currency == Currency.Gold
                &&
                Bank.AccountContain(currency, CurrentLvlGoldCost)
            )
            ||
            (
                currency == Currency.Silver
                &&
                Bank.AccountContain(currency, CurrentLvlSilverCost)
            )
        )
        {
            var curLvl = PlayerPrefs.GetInt("TimeScale");
            if (curLvl == maxLvl)
                return false;
            PlayerPrefs.SetInt("TimeScale", curLvl + 1);
            Debug.Log("TimeScale upgraded");
            return true;
        }
        else
        {
            Debug.Log("TimeScale NOT upgraded");
            return false;
        }
    }
}
