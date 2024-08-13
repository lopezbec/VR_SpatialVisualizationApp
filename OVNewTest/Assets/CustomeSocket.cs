using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FreezeOnEnter : MonoBehaviour
{
    public float sphereRadius = 5.0f; // Radius of the spherical area
    public Transform sphereCenter; // Center of the sphere
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        // Try to find the XRGrabInteractable component on the object
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        // Check if the object has a Rigidbody, is within the spherical area, and is not being grabbed
        if (rb != null && IsWithinSphere(other.transform.position) && !IsBeingGrabbed())
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

    // Helper method to check if the object is currently being grabbed
    bool IsBeingGrabbed()
    {
        return grabInteractable != null && grabInteractable.isSelected;
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
