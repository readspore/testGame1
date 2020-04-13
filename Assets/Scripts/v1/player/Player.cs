using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ShapeController.player = transform.gameObject;
        ShapeController.CurrentShapeType = CurrentShapeType.Sphere;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
