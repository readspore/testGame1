using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountInpValidate : MonoBehaviour
{
    void Start()
    {
        InputField inputField = gameObject.GetComponent<InputField>();
        inputField.characterValidation = InputField.CharacterValidation.Integer;

        inputField.onValidateInput += ValidateInput;

    }

    public char ValidateInput(string text, int charIndex, char addedChar)
    {
        var output = System.Text.RegularExpressions.Regex.Replace(addedChar + "", @"[^\d]", "\0"); //"output" is a String
        return output.ToCharArray()[0];
    }

}
