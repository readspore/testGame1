using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed = 3;

    public float maxX = 0;
    public float maxY = 0;
    public float maxZ = 0;

    public float initialX = 0;
    public float initialY = 0;
    public float initialZ = 0;

    public bool playerStartMove = false;
    public bool moveIfPlayerOnPlatform = false;
    public bool needMove = true;
    public bool moveLeft = false;
    public bool moveDown = false;

    public float incrementation = 0;
    public float incrementationPlayer = 0;

    public bool playerOnPlatform = false;
    private void Start()
    {
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;

        if (playerStartMove || moveIfPlayerOnPlatform)
        {
            needMove = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        if (playerStartMove)
        {
            needMove = true;
        }
        playerOnPlatform = true;
        incrementationPlayer = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != Constants.PlayerLayer)
            return;
        if (moveIfPlayerOnPlatform)
        {
            needMove = false;
        }
        playerOnPlatform = false;
    }

    void Update()
    {
        if (needMove)
        {
            Move();
        }
    }

    private void Move()
    {
        incrementation += speed * Time.deltaTime;
        Vector3 offSet = new Vector3(
            (
                maxX == 0 ? transform.position.x
                    : moveLeft
                        ? initialX - Mathf.PingPong(incrementation, maxX)
                        : initialX + Mathf.PingPong(incrementation, maxX)
            ),
            (
                maxY == 0 ? transform.position.y
                    : initialY + Mathf.PingPong(incrementation, maxY)
            ),
            transform.position.z
        //(
        //    maxZ == 0 ? transform.position.z
        //        : Mathf.PingPong(incrementation, maxZ)
        //)
        );
        transform.position = offSet;
    }
}
