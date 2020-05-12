using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    static Vector3 oldGravity;
    static ControllType oldControllType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Constants.PlayerLayer)
            return;
        oldGravity = Physics.gravity;
        Physics.gravity = Vector3.zero;
        var playerMoveControl = GameObject.FindObjectOfType<MoveController>();
        playerMoveControl.GetComponent<Rigidbody>().velocity = Vector3.zero;
        oldControllType = playerMoveControl.controllType;
        playerMoveControl.controllType = ControllType.EditTransfotm;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != Constants.PlayerLayer)
            return;
        Physics.gravity = oldGravity;
        GameObject.FindObjectOfType<MoveController>().controllType = oldControllType;
    }
}
