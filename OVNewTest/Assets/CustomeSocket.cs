using UnityEngine;
using UnityEngine.InputSystem;
// I need to add a toogle if statement
public class FreezeOnEnter : MonoBehaviour
{
    public float sphereRadius = 5.0f; // Radius of the spherical area
    public Transform sphereCenter; // Center of the sphere
    public InputActionReference toggleGrabAction; // Reference to the input action
    private bool isGrabbed = false; // Tracks if the object is grabbed

    void OnEnable()
    {
        // Subscribe to the input action
        toggleGrabAction.action.performed += OnToggleGrab;
        toggleGrabAction.action.Enable();
    }

    void OnDisable()
    {
        // Unsubscribe from the input action
        toggleGrabAction.action.performed -= OnToggleGrab;
        toggleGrabAction.action.Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        // Check if the object has a Rigidbody, is within the spherical area, and is not being grabbed
        if (rb != null && IsWithinSphere(other.transform.position) && !isGrabbed)
        {
            // Freeze the object's velocity and angular velocity
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Snap the object's rotation to the nearest 90-degree angles
            rb.rotation = Quaternion.Euler(SnapToNearest90(rb.rotation.eulerAngles));

            // Freeze the Rigidbody constraints (Position and Rotation)
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        // Unfreeze the Rigidbody constraints when the object exits the spherical area
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    // Helper method to check if the object is within the spherical area
    bool IsWithinSphere(Vector3 position)
    {
        return Vector3.Distance(position, sphereCenter.position) <= sphereRadius;
    }

    // Callback for the input action to toggle the grabbed state
    void OnToggleGrab(InputAction.CallbackContext context)
    {
        isGrabbed = !isGrabbed;
        Debug.Log("Object Grabbed: " + isGrabbed);
    }

    // Helper method to snap angles to the nearest 90 degrees
    Vector3 SnapToNearest90(Vector3 angles)
    {
        return new Vector3(
            Mathf.Round(angles.x / 90) * 90,
            Mathf.Round(angles.y / 90) * 90,
            Mathf.Round(angles.z / 90) * 90
        );
    }

    void OnDrawGizmosSelected()
    {
        // Draw the spherical area in the Scene view for visualization
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(sphereCenter.position, sphereRadius);
    }
}
