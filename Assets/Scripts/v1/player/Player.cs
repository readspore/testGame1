using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    Rigidbody rb;
    public float distToGround = 0.51f;
    private int health = 100;

    public int Health 
    { 
        get => health; 
        set {
            health = value;
            if (health <= 0)
                PlayerIsDead();
        }
    }

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        ShapeController.player = transform.gameObject;
        ShapeController.CurrentShapeType = CurrentShapeType.Sphere;

        //DamageController.ActivateShield(15);
        //DamageController.GetDamage(11);

        //PlayerPrefs.SetString("respanUsednOnLvl" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex, "");

        //PlayerPrefs.SetInt("FreeRespawns", 20);
        //RespawHelpers.CreateNewRespawn(new Vector3(1, 0), "first");
        //RespawHelpers.CreateNewRespawn(new Vector3(1, 2), "Second");
        //RespawHelpers.CreateNewRespawn(new Vector3(1222, 24), "the last");
        //var getAllRespawnIdsOnLvl = RespawHelpers.GetAllRespawnIdsOnLvl();
        Respawn resp8 = new Respawn(10);
    }

    void Update()
    {
        if (rb.velocity.y < 0)
            PlatformHelpers.IgnorePlayerPlatform(false, "rb.velocity.y < 0");
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up,  distToGround );
    }

    public void PlayerIsDead()
    {
        Radio.Radio.PlayerDeath();
        //Radio.Radio.UpdateDirectionHint("my MSG");
    }
}
