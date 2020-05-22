using Radio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoneyValueController : MonoBehaviour
{
    [SerializeField]
    Currency textForCurrency;
    // Start is called before the first frame update
    void Awake()
    {
        Radio.Radio.OnUpdateMoneyValueHandler += UpdateMoneyValueHandler;
    }

    private void OnDestroy()
    {
        Radio.Radio.OnUpdateMoneyValueHandler -= UpdateMoneyValueHandler;
    }

    void UpdateMoneyValueHandler(Currency currency, int newVal)
    {
        if (currency == textForCurrency)
        {
            transform.GetComponent<Text>().text = newVal.ToString();
        }
    }
}
