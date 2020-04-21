using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyObj : MonoBehaviour
{
    public Currency currency; 
    public int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.PlayerLayer)
        {
            Bank.AddMoney(currency, value);
            Destroy(transform.gameObject);
            //Debug.Log(currencyCount + value);
        }
    }
}
