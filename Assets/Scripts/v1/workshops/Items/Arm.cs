using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Arm
{
    static bool isActive = false;
    static int maxLvl = 3;
    static int maxShieldResist;

    static int SilverCostLvl1 = 100;
    static int SilverCostLvl2 = 200;
    static int SilverCostLvl3 = 300;

    static int GoldCostLvl1 = 10;
    static int GoldCostLvl2 = 20;
    static int GoldCostLvl3 = 30;

    static int ArmValueLvl1 = 80;
    static int ArmValueLvl2 = 50;
    static int ArmValueLvl3 = 100;

    public static int CurrentLvl
    {
        get
        {
            return PlayerPrefs.GetInt("Arm");
        }
    }

    public static int CurrentLvlArmValue
    {
        get
        {
            var arm = 0;
            switch (CurrentLvl)
            {
                case 1:
                    arm = ArmValueLvl1;
                    break;
                case 2:
                    arm = ArmValueLvl2;
                    break;
                case 3:
                    arm = ArmValueLvl3;
                    break;
            }
            return arm;
        }
    }

    public static int CurrentLvlSilverCost
    {
        get
        {
            int cost = 0;
            switch (CurrentLvl)
            {
                case 0:
                    cost = SilverCostLvl1;
                    break;
                case 1:
                    cost = SilverCostLvl2;
                    break;
                case 2:
                    cost = SilverCostLvl3;
                    break;
            }
            return cost;
        }
    }

    public static int CurrentLvlGoldCost
    {
        get
        {
            var curLvl = CurrentLvl;
            int cost = 0;
            switch (curLvl)
            {
                case 0:
                    cost = GoldCostLvl1;
                    break;
                case 1:
                    cost = GoldCostLvl2;
                    break;
                case 2:
                    cost = GoldCostLvl3;
                    break;
            }
            return cost;
        }
    }

    public static bool IsActive { get => isActive; private set => isActive = value; }

    public static bool Activate()
    {
        //if (IsActive)
        //    return false;
        Debug.Log("Activate Arm");
        //IsActive = true;
        Debug.Log("CurrentLvlArmValue " + CurrentLvlArmValue);
        maxShieldResist = CurrentLvlArmValue;
        //Task.Delay(CurrentLvlArmValue).ContinueWith(t => Arm.DeActivate());
        return true;
    }

    public static void DeActivate()
    {
        //yield return new WaitForSeconds(CurrentLvlArmValue);
        //IsActive = false;
        Debug.Log("DeActivate");
    }

    public static int TakeDamage(int damage)
    {
        if (maxShieldResist <= 0)
            return damage;
            Debug.Log("Take damage " + damage);
        maxShieldResist -= damage;
        if (maxShieldResist <= 0)
            DeActivate();
        damage = maxShieldResist;
        Debug.Log("maxShieldResist " + maxShieldResist);
        return damage >= 0
                ? 0
                : damage * -1;
    }

    public static bool TryUpgradeArm(Currency currency)
    {
        if (
            (
                currency == Currency.Gold
                &&
                Bank.AccountContain(currency, CurrentLvlGoldCost)
            )
            ||
            (
                currency == Currency.Silver
                &&
                Bank.AccountContain(currency, CurrentLvlSilverCost)
            )
        )
        {
            var curLvl = PlayerPrefs.GetInt("Arm");
            if (curLvl == maxLvl)
                return false;
            PlayerPrefs.SetInt("Arm", curLvl + 1);
            return true;
        }
        else
        {

            return false;
        }
    }
}
