using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageController
{
    static GameObject playerObj;
    static bool isShieldActive;
    static int maxShieldResist;
    public static bool ActiveInvulnerability { get ; set;}
    public static bool ActiveDeceitOfDeath { get; set; }
    public static bool ActiveReduceDamage { get; set; }

    public static GameObject PlayerObj
    {
        get
        {
            if (playerObj == null)
            {
                playerObj = GameObject.Find("Player");
            }
            return playerObj;
        }
        set => playerObj = value;
    }

    static Player playerComponent;

    public static Player PlayerComponent
    {
        get
        {
            if (playerComponent == null)
            {
                playerComponent = PlayerObj.GetComponent<Player>();
            }
            return playerComponent;
        }
        set => playerComponent = value;
    }

    public static void GetDamage(int damage)
    {
        if (ActiveInvulnerability)
            return;
        damage = ApplayReduceDamage(damage);
        damage = ShielfAbsorption(damage);
        if (damage != 0)
            PlayerComponent.Health += damage;
    }

    static int ApplayReduceDamage(int damage)
    {
        //if (!ActiveReduceDamage)
        //    return damage;
        if (ReduceDamage.NeedReduceDamage())
            return 1;
        return damage;
    }

    static int ShielfAbsorption(int damage)
    {
        if (!isShieldActive) return damage;
        maxShieldResist -= damage;
        damage = maxShieldResist;
        if (maxShieldResist <= 0)
            DeactivateShield();
        return damage >= 0
                ? 0
                : damage;
    }

    static void DeactivateShield()
    {
        isShieldActive = false;
    }

    public static void ActivateShield(int newShieldResist)
    {
        isShieldActive = true;
        maxShieldResist = newShieldResist;
    }
}
