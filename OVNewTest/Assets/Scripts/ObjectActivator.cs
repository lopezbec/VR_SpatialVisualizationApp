using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectActivator : MonoBehaviour
{
    public GameObject objectToActivate; // Reference to the object to activate

    // Method to activate the object
    public void ActivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }

    // Optional: Method to deactivate the object
    public void DeactivateObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }
}
