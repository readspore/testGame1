using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Radio
{
    public delegate void DirectionHintHandler(string message);
    public delegate void ToggleAvailableAction(BtnAvailableAction action);
    public static class Radio
    {
        public static event Action onPlayerDeath;
        public static void PlayerDeath()
        {
            //Debug.Log("Radio PlayerDeath");
            if (onPlayerDeath != null)
                onPlayerDeath();
        }

        public static event Action onTimeScaleEnd;
        public static void TimeScaleEnd()
        {
            //Debug.Log("Radio TimeScaleEnd");
            if (onTimeScaleEnd != null)
                onTimeScaleEnd();
        }

        public static event DirectionHintHandler OnUpdateDirectionHint;
        public static void UpdateDirectionHint(string str)
        {
            if (OnUpdateDirectionHint != null)
                OnUpdateDirectionHint(str);
        }

        public static event ToggleAvailableAction OnToggleAvailableAction;
        public static void ToggleAvailableAction(BtnAvailableAction action)
        {
            //Debug.Log("ToggleAvailableAction");
            if (OnToggleAvailableAction != null)
                OnToggleAvailableAction(action);
        }

        public static event Action onSwipeDown;
        public static void SwipeDown()
        {
            if (onSwipeDown != null)
                onSwipeDown();
        }

        public static event Action onToggleBtnCameraView;
        public static void ToggleBtnCameraView()
        {
            if (onToggleBtnCameraView != null)
                onToggleBtnCameraView();
        }
    }
}
