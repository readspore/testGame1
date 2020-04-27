using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class DeathDeceit
{
    static bool isActive = false;
    static int maxLvl = 3;

    static int SilverCostLvl1 = 100;
    static int SilverCostLvl2 = 200;
    static int SilverCostLvl3 = 300;

    static int GoldCostLvl1 = 10;
    static int GoldCostLvl2 = 20;
    static int GoldCostLvl3 = 30;

    static int timeApplayedLvl1 = 3;
    static int timeApplayedLvl2 = 6;
    static int timeApplayedLvl3 = 10;

    static float HPOnDeathLvl1 = 0.10f;
    static float HPOnDeathLvl2 = 0.15f;
    static float HPOnDeathLvl3 = 0.30f;



    public static int CurrentLvl
    {
        get
        {
            return PlayerPrefs.GetInt("DeathDeceit");
        }
    }

    public static int CurrentLvlActiveTime
    {
        get
        {
            var time = 0;
            switch (CurrentLvl)
            {
                case 1:
                    time = timeApplayedLvl1;
                    break;
                case 2:
                    time = timeApplayedLvl2;
                    break;
                case 3:
                    time = timeApplayedLvl3;
                    break;
            }
            return time;
        }
    }

    public static float HPOnDeathCurentLvl
    {
        get
        {
            var hp = 0f;
            switch (CurrentLvl)
            {
                case 1:
                    hp = HPOnDeathLvl1;
                    break;
                case 2:
                    hp = HPOnDeathLvl2;
                    break;
                case 3:
                    hp = HPOnDeathLvl3;
                    break;
            }
            return hp;
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
        if (isActive)
            return false;
        isActive = true;
        Transform.FindObjectOfType<Player>().deathDeceit.SetActive(true);
        //Task.Delay(CurrentLvlActiveTime).ContinueWith(t => DeActivate());

        //DeActivate(true);
        //Debug.Log("Activate DeathDeceit CurrentLvlActiveTime " + CurrentLvlActiveTime);
        return true;
    }

    public static void DeActivate()
    {
        //Debug.Log(" 1 DeActivate DeathDeceit");
        //if (needWait)
            //yield return new WaitForSeconds(CurrentLvlActiveTime);
        isActive = false;
        Transform.FindObjectOfType<Player>().deathDeceit.SetActive(false);
        //yield return new WaitForSeconds(0);
        //Debug.Log("DeActivate DeathDeceit");
    }

    public static float DeathBlow()
    {
        if (!isActive)
            return 0;
        DeActivate();
        Debug.Log("DeathBlow " + HPOnDeathCurentLvl);
        return HPOnDeathCurentLvl;
    }

    public static bool TryUpgradeDeathDeceit(Currency currency)
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
            var curLvl = PlayerPrefs.GetInt("DeathDeceit");
            if (curLvl == maxLvl)
                return false;
            PlayerPrefs.SetInt("DeathDeceit", curLvl + 1);
            Debug.Log("Death Deceit upgraded");
            return true;
        }
        else
        {
            Debug.Log("Death Deceit NOT upgraded");
            return false;
        }
    }
}
