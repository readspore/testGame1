using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PlatformHelpers 
{
    private static bool waitForPlatformExit = false;

    public static int T_c = 0;

    public static bool WaitForPlatformExit { get => waitForPlatformExit; set => waitForPlatformExit = value; }

    public static void IgnorePlayerPlatform(bool flag, string course)
    {
        if (!WaitForPlatformExit)
        {
            //Debug.Log(flag + " " + course);
            //Debug.Log("PL Ignore " + flag + " course " + course);
            Physics.IgnoreLayerCollision(Constants.PlayerLayer, Constants.PlatformsLayer, flag);
        }
    }

    public static void IgnorePlayerPlatform(bool flag, string course, bool waitForExit)
    {
        // waitForExit used for prevent "course" - "rb.velocity.y < 0"
        SetHelperText(flag, course);
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

    static void SetHelperText(bool flag, string course)
    {
        return;
        //switch (T_c % 3)
        //{
        //    case 0:
        //        GameObject.Find("Test_Text1").GetComponent<Text>().text = flag.ToString() + " C " + course + " " + T_c;
        //        break;
        //    case 1:
        //        GameObject.Find("Test_Text2").GetComponent<Text>().text = flag.ToString() + " C " + course + " " + T_c;
        //        break;
        //    case 2:
        //        GameObject.Find("Test_Text3").GetComponent<Text>().text = flag.ToString() + " C " + course + " " + T_c;
        //        break;
        //    default:
        //        break;
        //}
        //++T_c;
    }
}
