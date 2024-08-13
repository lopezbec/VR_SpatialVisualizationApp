using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class InclineChangeText : MonoBehaviour
{
    
    private TextMeshProUGUI textComponent;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // Check if the reference is set; otherwise, get the component attached to the same GameObject
        if (textComponent == null)
        {
            textComponent = GetComponent<TextMeshProUGUI>();
        }

        // Change the text
        if (textComponent != null && RotateInclines.isRotating)
        {
            textComponent.text = "Incline Controls";
        }
        else if (textComponent != null && !RotateInclines.isRotating)
        {
            textComponent.text = "Controls";
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found!");
        }
    }
}
