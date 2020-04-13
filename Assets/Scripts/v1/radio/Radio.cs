using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Radio
{
    public static class Radio
    {
        public static void Test()
        {
            Test("");
        }
        public static void Test( string text)
        {
            Debug.Log("Test " + text);
        }
    }
}
