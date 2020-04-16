using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool needIgnorePlayer;
    public bool t_setIgnorePlayer;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Constants.PlayerLayer)
        {
            collision.gameObject.GetComponent<Player>().SetParentPlatform(gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == Constants.PlayerLayer)
        {
            collision.gameObject.GetComponent<Player>().SetParentPlatform();
        }
    }

    private void OnTrigerExit(Collision collision)
    {
        if (collision.gameObject.layer == Constants.PlayerLayer)
        {
            collision.gameObject.GetComponent<Player>().SetParentPlatform();
        }
    }
}
