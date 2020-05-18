using UnityEngine;

public enum ControllType {EditTransfotm, AddForce };
public enum MoveDirection { Left, Right, Up, Down, None };
public class MoveController : MonoBehaviour
{
    public ControllType controllType = ControllType.EditTransfotm;
    public float playerSpeed = 1;
    public float jumpForce = 1;
    public float minForceUpDir = 0.5f;
    public float minForceDownDir = -0.5f;
    public float minForceLeftDir = -0.5f;
    public float minForceRightDir = 0.5f;
    public float kdApplayForce = 0.02f;
    MoveDirection lastMoveDirection;
    float lastTimeAplayedForce = 0;
    // Update is called once per frame
    void Update()
    {
        CheckMove();
    }

    void CheckMove()
    {
        if (MobileJoystick_UI.instance.moveDirection.y == 0 && MobileJoystick_UI.instance.moveDirection.x == 0)
            return;
        if (controllType == ControllType.EditTransfotm
            && (
                MobileJoystick_UI.instance.moveDirection.y != 0
                || MobileJoystick_UI.instance.moveDirection.x != 0
            )
        )
            CheckMoveViaTransform();
        if (controllType == ControllType.AddForce)
            CheckMoveViaForce();
    }

    void CheckMoveViaTransform()
    {
        var moveDirection = GetMoveForceDirection();
        if (!IsAvailableTransformMove()) return;
        ShapeController.CurrentShapeControl.OnMoveJoystick();
    }

    bool IsAvailableTransformMove()
    {
        return MobileJoystick_UI.instance.moveDirection.y != 0
                || MobileJoystick_UI.instance.moveDirection.x != 0;
    }


    void CheckMoveViaForce()
    {
        var moveDirection = GetMoveForceDirection();
        if (!IsAvailableForceMove(moveDirection)) return;
        switch (moveDirection)
        {
            case MoveDirection.Left:
                ShapeController.CurrentShapeControl.OnMoveLeft();
                break;
            case MoveDirection.Right:
                ShapeController.CurrentShapeControl.OnMoveRight();
                break;
            case MoveDirection.Up:
                ShapeController.CurrentShapeControl.OnMoveUp();
                break;
            case MoveDirection.Down:
                ShapeController.CurrentShapeControl.OnMoveDown();
                Radio.Radio.SwipeDown();
                break;
            case MoveDirection.None:
                break;
        }
        lastMoveDirection = moveDirection;
        lastTimeAplayedForce = Time.time;
    }

    MoveDirection GetMoveForceDirection()
    {
        if (MobileJoystick_UI.instance.moveDirection.y > minForceUpDir)
            return MoveDirection.Up;
        if (MobileJoystick_UI.instance.moveDirection.y < minForceDownDir)
            return MoveDirection.Down;
        if (MobileJoystick_UI.instance.moveDirection.x > minForceRightDir)
            return MoveDirection.Right;
        if (MobileJoystick_UI.instance.moveDirection.x < minForceLeftDir)
            return MoveDirection.Left;
        return MoveDirection.None;
    }

    bool IsAvailableForceMove(MoveDirection moveDirection)
    {
        if (moveDirection == MoveDirection.None)
            return false;
        if ((lastTimeAplayedForce + kdApplayForce) > Time.time)
            return false;
        if (moveDirection == MoveDirection.Up)
        {
            if (!transform.GetComponent<Player>().IsGrounded())
                return false;
        }
        return true;
    }
}