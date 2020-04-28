using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    Rigidbody rb;
    public float distToGround = 0.51f;
    private readonly int maxHealth = 100;
    private int health = 100;
    Camera mainCamera;
    GameObject curretnPlatform;

    public GameObject invalnerable;
    public GameObject timeScale;
    public GameObject deathDeceit;
    public GameObject armGO;
    

    public int Health 
    { 
        get => health; 
        set {
            Debug.Log("Health set " + value);
            health = value;
            if (health <= 0)
            {
                var addHP = DeathDeceit.DeathBlow();
                if (addHP != 0)
                {
                    Health = (int)(maxHealth * addHP);
                }
                else
                {
                    PlayerIsDead();
                }
            }
        }
    }

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        mainCamera.gameObject.AddComponent<CameraControll>().TheStart(this.gameObject);
        ShapeController.player = transform.gameObject;
        ShapeController.CurrentShapeType = CurrentShapeType.Sphere;

        //var fq = Forge.T_AddToQueue(99);
        //var fq2 = Forge.T_AddToQueue(100);
        //var fq3 = Forge.T_AddToQueue(101);


        //DamageController.ActivateShield(15);
        //DamageController.GetDamage(35);


        //Debug.Log("ReduceDamage lvl " + PlayerPrefs.GetInt("ReduceDamage"));
        //StartCoroutine("TDL");

        Bank.PickUpCoin(Currency.Silver, 1000);

        //Invulnerability.TryUpgradeInvulnerability(Currency.Silver);
        //Arm.TryUpgradeArm(Currency.Silver);
        //Debug.Log("CurrentLvlArmValue " + Arm.CurrentLvlArmValue);
        //DeathDeceit.TryUpgradeDeathDeceit(Currency.Silver);
        //TimeScale.TryUpgradeTimeScale(Currency.Silver);

        //InvokeRepeating("T_TakeDamage", 0 , 2);

        //Debug.Log("ss "  + (100 * 0.1));
        //Debug.Log("ss "  + (100 * 0.15));
        //Debug.Log("ss "  + (100 * 0.30));

        //Radio.Radio.onTimeScaleEnd += DeActivateTimeScale;

        //var ss = Forge.GetForgeItem(1);
        //Debug.Log(ss.TotalPartsForOpen);

        //Forge.FillForgeItemPrefab(0);
        //Forge.FillForgeItemPrefab(1);
        //Forge.FillForgeItemPrefab(1);
        //Forge.FillForgeItemPrefab(1);
        //Forge.FillForgeItemPrefab(1);

        //комментарий

        //PlayerPrefs.SetString("respanUsednOnLvl" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex, "");

        //PlayerPrefs.SetInt("FreeRespawns", 20);
        //RespawHelpers.CreateNewRespawn(new Vector3(10, 10), "New point");
        //RespawHelpers.CreateNewRespawn(new Vector3(1, 2), "Second");
        //RespawHelpers.CreateNewRespawn(new Vector3(1222, 24), "the last");
        //var getAllRespawnIdsOnLvl = RespawHelpers.GetAllRespawnIdsOnLvl();
        //Respawn resp = new Respawn(11);
        //resp.MoveToRespawn();
    }

    //IEnumerator TDL()
    //{
    //    yield return new WaitForSeconds(3);
    //    Debug.Log("TDL");
    //}

    void T_TakeDamage()
    {
        //Debug.Log("T_TakeDamage");
        DamageController.GetDamage(35);
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

    void PlayerIsDead()
    {
        Radio.Radio.PlayerDeath();
        //Radio.Radio.UpdateDirectionHint("my MSG");
    }


    public void SetParentPlatform()
    {
        transform.parent = null;
        curretnPlatform = null;
    }
    public void SetParentPlatform(GameObject platform)
    {
        curretnPlatform = platform;
        transform.parent = curretnPlatform.transform;
    }

    public void ActivateTimeScale()
    {
        Debug.Log("ActivateTimeScale");
        //if (TimeScale.isActive)
        //    return ;
        //TimeScale.isActive = true;
        timeScale.SetActive(true);
        Time.timeScale = TimeScale.ScaleOnCurentLvl;
        StartCoroutine(DeActivateTimeScale());
    }

    public IEnumerator DeActivateTimeScale()
    {
        Debug.Log("DeActivate 1 IEnumerator");
        //Debug.Log(TimeScale.CurrentLvlActiveTime);
        yield return new WaitForSeconds(TimeScale.CurrentLvlActiveTime);
        //yield return new WaitForSeconds(1.5f);
        Time.timeScale = 1;
        timeScale.SetActive(false);
        //TimeScale.isActive = false;
        Debug.Log("DeActivate 2");
        //yield return new WaitForSeconds(1);
    }

}
