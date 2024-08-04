using UnityEngine;

public class PlaneColorChanger : MonoBehaviour
{
    public GameObject plane; // Assign the plane GameObject in the Inspector
    public Transform playerTransform; // Assign the player Transform in the Inspector
    public Transform referencePoint; // Assign the reference point Transform in the Inspector
    private Renderer planeRenderer;

    void Start()
    {
        if (plane != null)
        {
            planeRenderer = plane.GetComponent<Renderer>();
            if (planeRenderer == null)
            {
                Debug.LogError("Renderer component not found on the plane.");
            }
        }
        else
        {
            Debug.LogError("Plane GameObject not assigned.");
        }

        if (referencePoint == null)
        {
            Debug.LogError("Reference point Transform not assigned.");
        }
    }

    void Update()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform not assigned.");
            return;
        }

        if (planeRenderer == null)
        {
            Debug.LogError("Renderer component not found on the plane.");
            return;
        }

        if (referencePoint == null)
        {
            Debug.LogError("Reference point Transform not assigned.");
            return;
        }

        // Calculate the player's position relative to the reference point
        Vector3 relativePosition = referencePoint.InverseTransformPoint(playerTransform.position);
        Debug.Log(relativePosition); 

        // Determine which side the player is on relative to the reference point
        
        if (relativePosition.z > 0)
        {
            // Player is to the left of the reference point
            planeRenderer.material.color = Color.yellow;
            Debug.Log("Player is to the left of the reference point. Color: Yellow");
            if (relativePosition.x > 0)
        {
            // Player is to the right of the reference point
             planeRenderer.material.color = Color.red;
            Debug.Log("Player is behind the reference point. Color: Red");
      
        }
        }
        //When player is in the right side

        else if (relativePosition.z < 0)
        {
            // Player is behind the reference point
            planeRenderer.material.color = Color.blue;
            Debug.Log("Player is to the right of the reference point. Color: Blue");
        }
        
        else if (relativePosition.x < 0)
        {
            
            planeRenderer.material.color = Color.green;
            Debug.Log("Player is in front of the reference point. Color: Green");
            
        }
        else
        {
            // Player is exactly at the reference point
            planeRenderer.material.color = Color.white;
            Debug.Log("Player is exactly at the reference point. Color: White");
        }
    }
}
