using UnityEngine;

public enum ControllType { EditTransfotm, AddForce };
public enum MoveDirection
{
    Left,
    Right,
    Up,
    Down,
    None,
    RightSlow,
    LeftSlow,
};
namespace oldVersion1
{
public class MoveController : MonoBehaviour
{
    public ControllType controllType = ControllType.EditTransfotm;
    //public float playerSpeed = 1;
    //public float jumpForce = 1;
    public float minForceUpDir = 0.5f;
    public float minForceDownDir = -0.5f;
    public float minForceLeftDir = -0.5f;
    public float minForceRightDir = 0.5f;
    public float kdApplayForce = 0.02f;
    //MoveDirection lastMoveDirection;
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
        if (controllType == ControllType.EditTransfotm)
            CheckMoveViaTransform();
        if (controllType == ControllType.AddForce)
            CheckMoveViaForce();
    }

    void CheckMoveViaTransform()
    {
        var moveDirection = GetMoveDirection();
        if (!IsAvailableTransformMove(moveDirection)) return;
        if (
             moveDirection == MoveDirection.Down
             ||
             moveDirection == MoveDirection.Up
             )
        {
            lastTimeAplayedForce = Time.time;
        }

        Debug.Log("moveDirection " + moveDirection);
        ShapeController.CurrentShapeControl.OnMoveJoystick();

    }

    bool IsAvailableTransformMove(MoveDirection moveDirection)
    {
        if (
           moveDirection == MoveDirection.Down
           ||
           moveDirection == MoveDirection.Up
           )
        {
            if ((lastTimeAplayedForce + kdApplayForce) > Time.time)
                return false;
        }
        if (moveDirection == MoveDirection.Up)
        {
            if (!transform.GetComponent<Player>().IsGrounded())
            {
                return false;
            }
            if (
                transform.GetComponent<Rigidbody>().velocity.y > 0.1
            )
            {
                //Debug.Log("velocity.y > 0.1 " + transform.GetComponent<Rigidbody>().velocity.y);
                return false;
            }
            if (
                transform.GetComponent<Rigidbody>().velocity.y < -0.01
            )
            {
                //Debug.Log("velocity.y < -0.01 " + transform.GetComponent<Rigidbody>().velocity.y);
                return false;
            }
        }
        return true;
    }


    void CheckMoveViaForce()
    {
        var moveDirection = GetMoveDirection();
        if (!IsAvailableForceMove(moveDirection)) return;
        lastTimeAplayedForce = Time.time;
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
            case MoveDirection.LeftSlow:
                ShapeController.CurrentShapeControl.OnMoveJoystick();
                lastTimeAplayedForce = 0;
                break;
            case MoveDirection.RightSlow:
                ShapeController.CurrentShapeControl.OnMoveJoystick();
                lastTimeAplayedForce = 0;
                break;
            case MoveDirection.None:
                break;
        }
    }

    MoveDirection GetMoveDirection()
    {
        if (MobileJoystick_UI.instance.moveDirection.y > minForceUpDir)
            return MoveDirection.Up;
        if (
            MobileJoystick_UI.instance.moveDirection.y < minForceDownDir
            &&
            MobileJoystick_UI.instance.moveDirection.x < minForceRightDir
            &&
            MobileJoystick_UI.instance.moveDirection.x > (minForceRightDir * -1)
        )
            return MoveDirection.Down;
        if (MobileJoystick_UI.instance.moveDirection.x > minForceRightDir)
            return MoveDirection.Right;
        if (MobileJoystick_UI.instance.moveDirection.x > 0)
            return MoveDirection.RightSlow;
        if (MobileJoystick_UI.instance.moveDirection.x < minForceLeftDir)
            return MoveDirection.Left;
        if (MobileJoystick_UI.instance.moveDirection.x < 0)
            return MoveDirection.LeftSlow;
        return MoveDirection.None;
    }

    bool IsAvailableForceMove(MoveDirection moveDirection)
    {
        if (moveDirection == MoveDirection.None)
            return false;
        if ((lastTimeAplayedForce + kdApplayForce) > Time.time)
            return false;
        if (
            moveDirection == MoveDirection.Up
        )
        {
            if (!transform.GetComponent<Player>().IsGrounded())
            {
                return false;
            }
            if (
                transform.GetComponent<Rigidbody>().velocity.y > 0.1
            )
            {
                //Debug.Log("velocity.y > 0.1 " + transform.GetComponent<Rigidbody>().velocity.y);
                return false;
            }
            if (
                transform.GetComponent<Rigidbody>().velocity.y < -0.01
            )
            {
                //Debug.Log("velocity.y < -0.01 " + transform.GetComponent<Rigidbody>().velocity.y);
                return false;
            }
        }
        //Debug.Log("Rigidbody " + transform.GetComponent<Rigidbody>().velocity.y);
        return true;
    }
}
}