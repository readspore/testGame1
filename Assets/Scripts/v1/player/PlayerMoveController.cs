using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public BeterJoyStick.JoyStick joyStick;
    public float speed = 4;
    public float jsMinXVal = 0.1f; // js - JoyStick
    public float jsMinYVal = 0.5f; // js - JoyStick
    public float jumpForce = 300;
    public float moveDowmForce = 100;
    public SphereCollider sphereCollider;
    [SerializeField]
    float distanceToGround;
    Rigidbody rb;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        if (sphereCollider != null)
        {
            distanceToGround = sphereCollider.radius + 0.1f;
        }
        else
        {
            distanceToGround = transform.GetComponent<SphereCollider>().radius + 0.1f;
        }
    }

    void Update()
    {
        MoveControl();
    }

    void MoveControl()
    {
        if (joyStick.Result.x == 0 && joyStick.Result.y == 0)
            return;
        Vector3 newPos = transform.position;
        if (Math.Abs(joyStick.Result.x) > jsMinXVal)
        {
            newPos.x += joyStick.Result.x * speed * Time.deltaTime;
        }
        if (joyStick.Result.y >= jsMinYVal)
        {
            Jump();
        }
        else if (joyStick.Result.y >= jsMinYVal)
        {
            MoveDown();
        }
        transform.position = newPos;
    }

    private void Jump()
    {
        if (!isGrounded())
            return;
        rb.AddForce(new Vector3(0, jumpForce,0));
    }

    void MoveDown()
    {
        if (!isGrounded())
            return;
        rb.AddForce(new Vector3(0, moveDowmForce, 0));
    }

    bool isGrounded()
    {
        var isGrounded = true;
        RaycastHit hit;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distanceToGround))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        return isGrounded;
    }

}
