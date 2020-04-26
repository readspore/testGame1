using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageController
{
    static GameObject playerObj;
    //static bool isShieldActive;
    //static int maxShieldResist;
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
        if (damage == 0) return;
        damage = damage > 0 ? damage : damage * -1;
        damage = ApplayInvulnerability(damage);
        damage = ApplyArm(damage);
        damage = ApplayReduceDamage(damage);
        if (damage != 0)
            PlayerComponent.Health -= damage;
    }

    static int ApplayInvulnerability(int damage)
    {
        return Invulnerability.IsActive ? 0 : damage;
    }

    static int ApplayReduceDamage(int damage)
    {
        if (ReduceDamage.NeedReduceDamage())
            return 1;
        return damage;
    }


    static int ApplyArm(int damage)
    {
        return Arm.TakeDamage(damage);
    }
    //static int ShielfAbsorption(int damage)
    //{
    //    if (!Arm.isShieldActive) return damage;
    //    Arm.maxShieldResist -= damage;
    //    damage = Arm.maxShieldResist;
    //    if (Arm.maxShieldResist <= 0)
    //        DeactivateShield();
    //    return damage >= 0
    //            ? 0
    //            : damage;
    //}

    //static void DeactivateShield()
    //{
    //    isShieldActive = false;
    //}

    //public static void ActivateShield(int newShieldResist)
    //{
    //    isShieldActive = true;
    //    maxShieldResist = newShieldResist;
    //}
}
