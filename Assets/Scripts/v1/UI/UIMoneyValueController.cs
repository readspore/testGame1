using Radio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoneyValueController : MonoBehaviour
{
    [SerializeField]
    Text moneyTextSilver;
    [SerializeField]
    Text moneyTextGold;
    // Start is called before the first frame update
    void Start()
    {
        Radio.Radio.OnUpdateMoneyValueHandler += UpdateMoneyValueHandler;
    }

    void UpdateMoneyValueHandler(Currency currency, int newVal)
    {
        switch (currency)
        {
            case Currency.Gold:
                moneyTextGold.text = newVal.ToString();
                break;
            case Currency.Silver:
                moneyTextSilver.text = newVal.ToString();
                break;
            default:
                break;
        }
    }
}
