using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PiecesText : MonoBehaviour
{
    public Text InventoryQuantity;
    public Text Pieces;
    int ConvertedQuantity;
    string Piece1 = "piece";
    string Piece2 = "pieces";


    void FixedUpdate()
    {
        ConvertedQuantity = Convert.ToInt32(InventoryQuantity.text);
        if (ConvertedQuantity <= 1)
        {
            Pieces.text = Piece1;
        }
        else
        {
            Pieces.text = Piece2;
        }
    }
}
