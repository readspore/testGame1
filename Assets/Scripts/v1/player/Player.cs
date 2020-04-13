using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    Rigidbody rb;
    public float distToGround = 0.51f;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        ShapeController.player = transform.gameObject;
        ShapeController.CurrentShapeType = CurrentShapeType.Sphere;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0)
            PlatformHelpers.IgnorePlayerPlatform(false, "rb.velocity.y < 0");
    }

    IEnumerator IgnorePlatformAfterTime(float time, bool flag)
    {
        Debug.Log("IgnorePlatformAfterTime");
        yield return new WaitForSeconds(time);
        PlatformHelpers.IgnorePlayerPlatform(flag, "IgnorePlatformAfterTime");
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up,  distToGround );
    }
}
