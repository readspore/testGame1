using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Radio
{
    public delegate void DirectionHintHandler(string message);
    public static class Radio
    {
        public static event Action onPlayerDeath;
        public static void PlayerDeath()
        {
            Debug.Log("Radio PlayerDeath");   
            if (onPlayerDeath != null)
                onPlayerDeath();
        }

        public static event DirectionHintHandler OnUpdateDirectionHint;
        public static void UpdateDirectionHint(string str)
        {
            if (OnUpdateDirectionHint != null)
                OnUpdateDirectionHint(str);
        }
    }

}
