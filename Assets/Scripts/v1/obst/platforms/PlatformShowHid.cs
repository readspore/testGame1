using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShowHid : MonoBehaviour
{
    public bool needToBreak = false;
    public float timeBeforeBreak = 2;
    public float createAfter = 5;
    public float timeBeforeStart = 1;

    private void Start()
    {
        TrafficLight trafficLight;
        if (!transform.gameObject.TryGetComponent<TrafficLight>(out trafficLight))
        {
            transform.gameObject.AddComponent<TrafficLight>();
        }
        StartCoroutine(TheStart());
    }

    IEnumerator TheStart()
    {
        yield return new WaitForSeconds(timeBeforeStart);
        while (true)
        {
            transform.GetComponent<TrafficLight>().SetColor("y");
            yield return new WaitForSeconds(timeBeforeBreak);
            PlatformHelpers.HidAndTrigger(true, transform.gameObject);
            yield return new WaitForSeconds(createAfter);
            PlatformHelpers.HidAndTrigger(false, transform.gameObject);
            transform.GetComponent<TrafficLight>().SetColor("w");
        }
    }
}
