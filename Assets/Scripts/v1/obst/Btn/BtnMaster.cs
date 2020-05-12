using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BtnAvailableAction
{
    ShowCamera
}

public class BtnMaster : MonoBehaviour
{
    public int clickedCounter;
    public GameObject btnSlave;
    //public GameObject btnSlave2;
    //public GameObject btnSlave3;
    public Camera _camera;
    bool playerInside = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        //Debug.Log("collision " + collision.gameObject.name);
        //playerInside = true;
        Radio.Radio.ToggleAvailableAction(BtnAvailableAction.ShowCamera);
        Radio.Radio.onSwipeDown += OnSwipeDown;
        //OnCollisionExit
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        //Radio.Radio.ToggleAvailableAction(BtnAvailableAction.ShowCamera);
        //playerInside = false;
    }

    void OnSwipeDown()
    {
        btnSlave.GetComponent<IBTNSlave>().ExecAction();
    }

    void btnSlave1Action()
    {
        Debug.Log("btnSlave1Action");
    }
    void btnSlave2Action()
    {
        Debug.Log("btnSlave2Action");
    }
    void btnSlave3Action()
    {
        Debug.Log("btnSlave3Action");
    }
}
