using UnityEngine;

public class ResetPositionAndSpeed : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation; // To store the initial rotation
    private Rigidbody rb;
    private float initialSpeed; // Assuming you have a speed variable you want to reset

    void Start()
{
    initialPosition = transform.position;
    initialRotation = transform.rotation;

    rb = GetComponent<Rigidbody>();

    if (rb != null)
    {
        initialSpeed = rb.velocity.magnitude;
        Debug.Log("Rigidbody component found and initial speed stored: " + initialSpeed);
    }
    else
    {
        Debug.LogError("Rigidbody component not found on this GameObject.");
    }
}

public void ResetToInitial()
{
    transform.position = initialPosition;
    transform.rotation = initialRotation;

    if (rb != null)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Debug.Log("Position and Rigidbody properties reset.");
    }
}

}
