using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FixLvlSize : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    public float widthPercentage;
    [SerializeField]
    //[Range(0, 1)]
    public float heightPercentage;
    float lWidthPercentage = 0;
    float lHeightPercentage = 0;
    Vector2 viewSize = Vector2.zero;

    void Start()
    {
        Fix();
    }

    void Update()
    {
#if UNITY_EDITOR
        //This is used to detect whether in editor view resolution has changed
        if (Application.isPlaying) return;
        if (GetMainGameViewSize() != viewSize || widthPercentage != lWidthPercentage || heightPercentage != lHeightPercentage)
        {
            Fix();
            viewSize = GetMainGameViewSize();
            lWidthPercentage = widthPercentage;
            lHeightPercentage = heightPercentage;
        }
#endif
    }

    public void Fix()
    {
        GridLayoutGroup grid = GetComponent<GridLayoutGroup>();
        var width = (float)GetMainGameViewSize().x;
        var valWidth = (int)Mathf.Round(width * widthPercentage);
        //var valHeight = (int)Mathf.Round(width * heightPercentage);
        var valHeight = heightPercentage;
        grid.cellSize = new Vector2(valWidth, valHeight);
        //Toggle enabled to update screen (is there a better way to do this?)
        grid.enabled = false;
        grid.enabled = true;
    }

    //Thanks to http://kirillmuzykov.com/unity-get-game-view-resolution/
    public static Vector2 GetMainGameViewSize()
    {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
        return (Vector2)Res;
    }
}