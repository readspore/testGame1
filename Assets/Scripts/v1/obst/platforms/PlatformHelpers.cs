using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlatformHelpers 
{
    private static bool waitForPlatformExit = false;

    public static bool WaitForPlatformExit { get => waitForPlatformExit; set => waitForPlatformExit = value; }

    public static void IgnorePlayerPlatform(bool flag, string course)
    {
        if (!WaitForPlatformExit)
        {
            //Debug.Log("PL Ignore " + flag + " course " + course);
            Physics.IgnoreLayerCollision(Constants.PlayerLayer, Constants.PlatformsLayer, flag);
        }
    }

    public static void IgnorePlayerPlatform(bool flag, string course, bool waitForExit)
    {
        IgnorePlayerPlatform(flag, course);
        WaitForPlatformExit = waitForExit;
    }

    public static void DoTrigger(bool isTrigger, GameObject platform)
    {
        platform.GetComponent<MeshCollider>().isTrigger = isTrigger;
    }

    public static void HidAndTrigger(bool hid, GameObject platform)
    {
        if (hid)
        {
            platform.GetComponent<Renderer>().enabled = false;
            //Debug.Log("platform " + platform.name);
            platform.GetComponent<MeshCollider>().isTrigger = true;
        }
        else
        {
            platform.GetComponent<Renderer>().enabled = true;
            platform.GetComponent<MeshCollider>().isTrigger = false;
        }
    }
}
