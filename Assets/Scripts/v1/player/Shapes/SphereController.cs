using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour, IShapeController
{
    private Rigidbody rb;
    private GameObject player;
    public float applayedForce = 10;
    public float speed = 10;
    public float upMoveKoef = 11f;
    private void Start()
    {
        //UpdateOnSwitch();
    }

    public void OnMoveUp()
    {
        OnMoveUp(new Vector3(0, applayedForce, 0));
    }
    public void OnMoveUp(Vector3 dir)
    {
        //Debug.Log("OnMove Up");   
        PlatformHelpers.IgnorePlayerPlatform(true, "OnMoveUp");
        rb.AddForce(dir * speed * upMoveKoef);
    }

    public void OnMoveDown()
    {
        OnMoveDown(new Vector3(0, -applayedForce));
    }

    public void OnMoveDown(Vector3 dir)
    {
        rb.AddForce(dir * speed);
        PlatformHelpers.IgnorePlayerPlatform(true, "OnMoveDown", false);
    }

    public void OnMoveLeft()
    {
        OnMoveLeft(new Vector3(-applayedForce, 0));
    }

    public void OnMoveLeft(Vector3 dir)
    {
        //Debug.Log("OnMove Left");
        rb.AddForce(dir * speed);
    }

    public void OnMoveRight()
    {
        OnMoveRight(new Vector3(applayedForce, 0));
    }
    public void OnMoveRight(Vector3 dir)
    {
        //Debug.Log("rb is " + rb);
        //Debug.Log("OnMove Right");
        rb.AddForce(dir * speed);
    }

    public void OnMoveReal(Vector3 dir)
    {
        rb.AddForce(dir * speed);
    }

    public void OnMoveJoystick()
    {
        Vector3 vectorNormal = Vector3.zero;
        if (MobileJoystick_UI.instance.moveDirection.x != 0)
        {
            vectorNormal.x += Time.deltaTime * 2.45f * MobileJoystick_UI.instance.moveDirection.x;
        }
        if (MobileJoystick_UI.instance.moveDirection.y != 0)
        {
            if (
                MobileJoystick_UI.instance.moveDirection.y > 0
                //&&
                //MobileJoystick_UI.instance.moveDirection.y < player.GetComponent<MoveController>().minForceUpDir
                )
            {
                OnMoveUp();
                //vectorNormal.y += Time.deltaTime * 2.45f * MobileJoystick_UI.instance.moveDirection.y;
                return;
            }
            else if (MobileJoystick_UI.instance.moveDirection.y < 0)
            {
                OnMoveDown();
                return;
            }
        }
        player.transform.Translate(vectorNormal, Space.World);
    }

    public void OnDisable()
    {
        transform.gameObject.SetActive(false);
    }

    public void OnEnableControl(GameObject player)
    {
        this.player = player;
        rb = player.GetComponent<Rigidbody>();
        transform.gameObject.SetActive(true);
    }
}
