using UnityEngine;

public class FollowHand : MonoBehaviour
{
    public Transform handTransform; // Reference to the hand transform

    private void Update()
    {
        if (handTransform != null)
        {
            // Update the position and rotation of the canvas to match the hand
            transform.position = handTransform.position;
            transform.rotation = handTransform.rotation;
        }
    }
}
