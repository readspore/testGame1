using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public void SetColor(string shortName)
    {
        shortName = shortName.ToLower();
        var color = Color.green;
        switch (shortName)
        {
            case "y":
                color = Color.yellow;
                break;
            case "g":
                color = Color.green;
                break;
            case "r":
                color = Color.red;
                break;
            case "b":
                color = Color.blue;
                break;
            case "w":
                color = Color.white;
                break;
            default:
                break;
        }
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    public void SetColor(Color color)
    {
        gameObject.GetComponent<Renderer>().material.color = color;
    }
}
