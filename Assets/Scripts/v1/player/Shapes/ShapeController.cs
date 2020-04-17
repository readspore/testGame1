using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentShapeType {Sphere, Cube};

public static class ShapeController
{
    public static GameObject player;
    static GameObject currentShape;
    static IShapeController currentShapeControl;
    static CurrentShapeType currentShapeType;

    public static GameObject CurrentShape { get => currentShape; private set => currentShape = value; }
    public static IShapeController CurrentShapeControl { get => currentShapeControl; private set => currentShapeControl = value; }
    public static CurrentShapeType CurrentShapeType {
        get => currentShapeType;
        set {
            BeforeSetNewShape(value);
            currentShapeType = value;
            AfterSetNewShape();
        }
    }

    static void BeforeSetNewShape(CurrentShapeType value)
    {
        try
        {
            currentShape?.GetComponent<IShapeController>().OnDisable();
        }
        catch (System.Exception)
        {
        }
    }

    static void AfterSetNewShape()
    {
        currentShape = player.transform.Find(currentShapeType.ToString()).gameObject;
        currentShapeControl = currentShape.GetComponent<IShapeController>();
        currentShapeControl.OnEnableControl(player);
    }
}
