using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigetExit : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Constants.PlayerLayer)
        {
            PlatformHelpers.WaitForPlatformExit = false;
            PlatformHelpers.IgnorePlayerPlatform(false, "OnTriggerExit");
            GameObject.Find("Player").GetComponent<Player>().SetParentPlatform();
        }
    }
}
