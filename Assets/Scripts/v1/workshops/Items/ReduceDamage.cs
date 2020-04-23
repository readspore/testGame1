using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ReduceDamage
{
    static int SilverCostLvl1 = 100;
    static int SilverCostLvl2 = 200;
    static int SilverCostLvl3 = 300;

    static int GoldCostLvl1 = 10;
    static int GoldCostLvl2 = 20;
    static int GoldCostLvl3 = 30;

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
            var curLvl = CurrentLvl;
            int cost = 0;
            switch (curLvl)
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

    public static bool TryUpgradeReduceDamage(Currency currency)
    {
        if (
            currency == Currency.Gold
            &&
            Bank.AccountContain(currency, CurrentLvlGoldCost)
        )
        {

        }
        else if (
            currency == Currency.Silver
            &&
            Bank.AccountContain(currency, CurrentLvlSilverCost)
        )
        {

        }

        return true;
    }


}
