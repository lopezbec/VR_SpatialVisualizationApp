using UnityEngine;
using UnityEngine.UI;

public class BoxSlopeUIControls : MonoBehaviour
{
    public Color normal, active;
    public Button boxButton, slopeButton; // References to both buttons
    public GameObject boxButtonOutline, slopeButtonOutline;

    public static string selectedButton = "box"; // Initially "add" is selected

    void Start()
    {
        boxButtonOutline.GetComponent<Image>().color = active; // Set initial active state for "add" button
        slopeButtonOutline.GetComponent<Image>().color = normal; // Set initial normal state for "subtract" button
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
        if (clickedObject == boxButton.gameObject)
        {
            return "box";
        }
        else if (clickedObject == slopeButton.gameObject)
        {
            return "slope";
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
        if (buttonName == "box")
        {
            boxButtonOutline.GetComponent<Image>().color = color;
        }
        else if (buttonName == "slope")
        {
            slopeButtonOutline.GetComponent<Image>().color = color;
        }
        else
        {
            Debug.LogError("Invalid button name for color update!");
        }
    }
}
