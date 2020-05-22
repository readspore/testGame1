using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Currency { Gold, Silver };

public static class Bank
{
    //public static void AddMoney(Currency currency, int value)
    //{
    //    int currencyCount = PlayerPrefs.GetInt(currency.ToString());
    //    PlayerPrefs.SetInt(currency.ToString(), currencyCount + value);
    //}

    public static void PickUpCoin(Currency currency, int value)
    {
        int currencyCount = PlayerPrefs.GetInt(currency.ToString());
        PlayerPrefs.SetInt(currency.ToString(), currencyCount + value);
        Radio.Radio.UpdateMoneyValue(currency, currencyCount);
    }

    public static bool Pay(Currency currency, int value)
    {
        if (AccountContain(currency, value))
        {
            PlayerPrefs.SetInt(
                currency.ToString(),
                PlayerPrefs.GetInt(currency.ToString()) - value
            );
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool AccountContain(Currency currency, int value)
    {
        return PlayerPrefs.GetInt(currency.ToString()) >= value;
    }

    public static int GetCurrency(Currency currency)
    {
        return PlayerPrefs.GetInt(currency.ToString());
    }
}
