using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShapeController
{
    void OnMoveUp();
    void OnMoveDown();
    void OnMoveLeft();
    void OnMoveRight();
    void OnMoveUp(Vector3 dir);
    void OnMoveDown(Vector3 dir);
    void OnMoveLeft(Vector3 dir);
    void OnMoveRight(Vector3 dir);
    //void OnMoveReal(Vector3 dir);
    void OnMoveJoystick();

    void OnDisable();
    void OnEnableControl(GameObject player);
    //void UpdateOnSwitch(Rigidbody rb);
}
