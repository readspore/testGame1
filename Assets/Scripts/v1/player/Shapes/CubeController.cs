using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour/*, ISwipeController*/
{
    private Rigidbody rb;
    public float speed = 10;
    private void Start()
    {
        UpdateOnSwitch();
    }

    public void UpdateOnSwitch()
    {
        rb = transform.parent.GetComponent<Rigidbody>();
    }

    public void OnSwipeDown(Vector3 dir)
    {
        Debug.Log("CUBE OnSwipeDown");
        Debug.Log("NEED ATTENTION");
        //transform.parent.GetComponent<PlayerControl>().ExecBtnSlaveScript();
    }

    public void OnSwipeLeft(Vector3 dir)
    {
        Debug.Log("CubeControll OnSwipeLeft");
        //throw new System.NotImplementedException();
    }

    public void OnSwipeReal(Vector3 dir)
    {
        Debug.Log("CubeControll OnSwipeReal");
        //throw new System.NotImplementedException();
    }

    public void OnSwipeRight(Vector3 dir)
    {
        Debug.Log("CubeControll OnSwipeRight");
        //throw new System.NotImplementedException();
    }

    public void OnSwipeUp(Vector3 dir)
    {
        Debug.Log("CubeControll OnSwipeUp");
    }

    public void UpdateOnSwitch(Rigidbody rb)
    {
        //throw new System.NotImplementedException();
    }
}
