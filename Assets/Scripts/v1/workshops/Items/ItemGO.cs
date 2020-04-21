using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGO : MonoBehaviour
{
    public int id;

    private void Start()
    {
        transform.Find("ButtonBuy").GetComponent<BtnClickInfo>().clickedInfo = id.ToString();
    }
}
