using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeOnTouch : MonoBehaviour
{
    public bool needToBreak = false;
    public bool isBreakingProcess = false;
    public float timeBeforeBreak = 2;
    public float createAfter = 5;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        needToBreak = true;
        BrekPlatform();
    }

    void BrekPlatform()
    {
        needToBreak = false;
        if (isBreakingProcess)
            return;
        StartCoroutine(TheBrekPlatform());
    }

    IEnumerator TheBrekPlatform()
    {
        if (transform.GetComponent<TrafficLight>() == null)
        {
            transform.gameObject.AddComponent<TrafficLight>();
        }
        //Debug.Log("TheBrekPlatform " + transform.name);
        isBreakingProcess = true;
        transform.GetComponent<TrafficLight>().SetColor("y");
        yield return new WaitForSeconds(timeBeforeBreak);
        PlatformHelpers.HidAndTrigger(true, transform.gameObject);
        yield return new WaitForSeconds(createAfter);
        PlatformHelpers.HidAndTrigger(false, transform.gameObject);
        transform.GetComponent<TrafficLight>().SetColor("w");
        isBreakingProcess = false;
    }
}
