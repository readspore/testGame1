using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ReduceDamage
{
    static int maxLvl = 3;

    static int SilverCostLvl1 = 100;
    static int SilverCostLvl2 = 200;
    static int SilverCostLvl3 = 300;

    static int GoldCostLvl1 = 10;
    static int GoldCostLvl2 = 20;
    static int GoldCostLvl3 = 30;

    static int chanceReduceLvl1 = 10;
    static int chanceReduceLvl2 = 15;
    static int chanceReduceLvl3 = 20;
    static int chanceReduceLvl4 = 35;

    public static int CurrentLvl {
        get
        {
            return PlayerPrefs.GetInt("ReduceDamage");
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

    public static bool NeedReduceDamage()
    {
        var reduce = false;
        var rand = new System.Random().Next(0, 100);
        switch (CurrentLvl)
        {
            case 0:
                break;
            case 1:
                reduce = rand <= chanceReduceLvl1;
                break;
            case 2:
                reduce = rand <= chanceReduceLvl2;
                break;
            case 3:
                reduce = rand <= chanceReduceLvl3;
                break;
        }
        return reduce;
    }

    public static bool TryUpgradeReduceDamage(Currency currency)
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
            var curLvl = PlayerPrefs.GetInt("ReduceDamage");
            if (curLvl == maxLvl)
                return false;
            PlayerPrefs.SetInt("ReduceDamage", curLvl + 1);
            return true;
        }
        else
        {

            return false;
        }
    }


}
