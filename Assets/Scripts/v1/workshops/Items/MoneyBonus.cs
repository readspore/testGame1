using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoneyBonus
{
    static int maxLvl = 3;

    static int SilverCostLvl1 = 100;
    static int SilverCostLvl2 = 200;
    static int SilverCostLvl3 = 300;

    static int GoldCostLvl1 = 10;
    static int GoldCostLvl2 = 20;
    static int GoldCostLvl3 = 30;

    static float BonusLvl1 = 0.10f;
    static float BonusLvl2 = 0.15f;
    static float BonusLvl3 = 0.30f;



    public static int CurrentLvl
    {
        get
        {
            return PlayerPrefs.GetInt("MoneyBonus");
        }
    }

    public static float CurrentLvlBonus
    {
        get
        {
            var bonus = 0f;
            switch (CurrentLvl)
            {
                case 1:
                    bonus = BonusLvl1;
                    break;
                case 2:
                    bonus = BonusLvl2;
                    break;
                case 3:
                    bonus = BonusLvl3;
                    break;
            }
            return bonus;
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

    public static bool TryUpgradeMoneyBonus(Currency currency)
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
            var curLvl = PlayerPrefs.GetInt("MoneyBonus");
            if (curLvl == maxLvl)
                return false;
            PlayerPrefs.SetInt("MoneyBonus", curLvl + 1);
            return true;
        }
        else
        {

            return false;
        }
    }
}
