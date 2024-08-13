using UnityEngine;
using UnityEngine.UI;

public class AddSubtractUIControls : MonoBehaviour
{
    public Color normal, active;
    public Button addButton, subtractButton; // References to both buttons
    public GameObject addButtonOutline, subtractButtonOutline;

    public static string selectedButton = "add"; // Initially "add" is selected

    void Start()
    {
        addButtonOutline.GetComponent<Image>().color = active; // Set initial active state for "add" button
        subtractButtonOutline.GetComponent<Image>().color = normal; // Set initial normal state for "subtract" button
    }

    public void OnClick()
    {
        string clickedButtonName = GetButtonName(gameObject); // Get the clicked button's name

        if (clickedButtonName != selectedButton) // Check if clicked button is different from selected one
        {
            // Update button states
            UpdateButtonColors(selectedButton, normal); // Set previous selected button to normal
            UpdateButtonColors(clickedButtonName, active); // Set clicked button to active
            selectedButton = clickedButtonName; // Update currently selected button
        }

        //Debug.Log("Button Clicked: " + clickedButtonName); // Log clicked button for debugging
    }

    // Helper function to get button name based on clicked GameObject
    private string GetButtonName(GameObject clickedObject)
    {
        if (clickedObject == addButton.gameObject)
        {
            return "add";
        }
        else if (clickedObject == subtractButton.gameObject)
        {
            return "subtract";
        }
        else
        {
            Debug.LogError("Unexpected button clicked!");
            return ""; // Handle unexpected clicks (optional)
        }
    }

    // Helper function to update button image color
    private void UpdateButtonColors(string buttonName, Color color)
    {
        if (buttonName == "add")
        {
            addButtonOutline.GetComponent<Image>().color = color;
        }
        else if (buttonName == "subtract")
        {
            subtractButtonOutline.GetComponent<Image>().color = color;
        }
        else
        {
            Debug.LogError("Invalid button name for color update!");
        }
    }
}

