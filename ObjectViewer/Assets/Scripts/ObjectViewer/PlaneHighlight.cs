using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneHighlight : MonoBehaviour
{
    public GameObject childObject; // Reference to the child object to highlight
    private Color originalColor; // Store the original color of the child
    public Color highlightColor; // Set your desired highlight color

    void Start()
    {
        originalColor = childObject.GetComponent<Renderer>().material.color;
    }

    void OnMouseEnter()
    {
        childObject.GetComponent<Renderer>().material.color = highlightColor;
    }

    void OnMouseExit()
    {
        childObject.GetComponent<Renderer>().material.color = originalColor;
    }
}
