using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoneyType { Currency1 , Currency2 , Currency3};
public class MoneyObj : MonoBehaviour
{
    public MoneyType moneyType;
    public int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.PlayerLayer)
        {
            int currencyCount = PlayerPrefs.GetInt(moneyType.ToString());
            PlayerPrefs.SetInt(moneyType.ToString(), currencyCount + value);
            Destroy(transform.gameObject);
            //Debug.Log(currencyCount + value);
        }
    }
}
