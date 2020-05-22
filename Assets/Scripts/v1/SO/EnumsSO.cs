using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace v1.SO
{
    //[System.Serializable]
    [Serializable]

    public enum ItemAttrType
    {
        Kd,
        TimeApplied,
        SilverCost,
        GoldCost,
        Arm,
        TimeScale,
        DeathDeceitHP,
        ForgeCraftSpeed,
        ForgeMaxQueue,
        ForgeChanceSecondItem,
        ForgeFreeCors,
        TimeCraftInForge
    };

    [Serializable]
    public enum SOItemObjId
    {
        Arm = 0,
        Invulnerability = 1,
        TimeScale = 2,
        DeathDeceit = 3,
        Respawn = 4
    }

    [Serializable]
    public enum ForgeStatuses
    {
        Ok,
        NoFreeCore,
        QueueFull,
        BankNotAllow,
        QueueIsNull,
    };
}