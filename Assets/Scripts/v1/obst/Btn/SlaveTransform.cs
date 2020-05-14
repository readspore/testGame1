using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BtnSlaveScripts { RandomColor, Transform, None }

public class SlaveTransform : MonoBehaviour, IBTNSlave
{
    public BtnSlaveScripts btnSlaveScripts;
    public Vector3 moveOffset;
    public Vector3 rotationOffset;
    private Vector3 initialPosition;
    private Quaternion initialQuaternion;
    int clickedCounter;
    public float execActionKD = 2;
    float lastExecAction = 0;
    //BtnMaster btnMasterScript;
    // Start is called before the first frame update
    private void Start()
    {
        initialPosition = transform.position;
        initialQuaternion = transform.rotation;
    }

    public void ExecAction()
    {
        //Debug.Log("ExecAction 1");
        if ((lastExecAction + execActionKD) > Time.time)
            return;
        Debug.Log("ExecAction 2");

        lastExecAction = Time.time;
        switch (btnSlaveScripts)
        {
            case BtnSlaveScripts.RandomColor:
                RandomColor();
                break;
            case BtnSlaveScripts.Transform:
                Transform();
                break;
            default:
                break;
        }
        ++clickedCounter;
    }

    private void RandomColor()
    {
        transform.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
    }

    private void Transform()
    {
        //clickedCounter start from 1
        if (clickedCounter % 2 == 0)
        {
            transform.position += moveOffset;
            transform.rotation = Quaternion.Euler(rotationOffset);
        }
        else
        {
            transform.position = initialPosition;
            transform.rotation = initialQuaternion;
        }
    }

}
