using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelAdder : MonoBehaviour
{
    public string prefabPath; //prefab path
    private GameObject voxelPrefab;// Prefab for the new voxel object
    public int sideIndex; //dictates which side is being pressed on
    public GameObject childObject; // Reference to the child object to highlight (from original script)
    public Material originalMaterial; // Store the original color of the child (from original script)
    public Color highlightColor; // Set your desired highlight color (from original script)

    private GameObject collidingObject; //keeps track of side it is colliding with if colliding

    void Start()
    {
        //must do this in order to copy prefab and not instance
        voxelPrefab = Resources.Load<GameObject>(prefabPath);
        /*Debug.Log(voxelPrefab);*/
    }
    void OnMouseEnter()
    {
        childObject.GetComponent<Renderer>().material.color = highlightColor;
    }

    void OnMouseExit()
    {
        childObject.GetComponent<Renderer>().material.color = originalMaterial.color;
        
    }
   
    void OnMouseUp()
    {
        if (childObject != null && AddSubtractUIControls.selectedButton == "add") // Check if a side is highlighted
        {
            // Find the parent cube object (assuming it's the immediate parent)
            Transform parentCubeTransform = transform.parent;

            // Get positions of BaseVoxel and parent cube
            Vector3 baseVoxelPosition = parentCubeTransform.parent.position; // BaseVoxel position
            Vector3 parentCubeLocal = parentCubeTransform.position; // Cube position

            // Create new voxel with parent cube's position and rotation
            GameObject newVoxel = Instantiate(voxelPrefab, baseVoxelPosition, transform.parent.rotation);
            //newVoxel.transform.GetChild(0).position = parentCubeLocal + new Vector3(0, 0, 1.0f / 2.0f); //Set Child Cube position equal.
            newVoxel.transform.parent = parentCubeTransform.parent.parent; // Set BaseVoxel as parent

            // Calculate offset based on clicked side (assuming sideIndex is set elsewhere)
            Vector3 offset = GetSideOffset(sideIndex);

            // Set Child Cube position with offset (considering rotation)
            newVoxel.transform.GetChild(0).localPosition = transform.parent.localPosition + (offset);
        }
        else if (childObject != null && AddSubtractUIControls.selectedButton == "subtract" && gameObject.transform.parent.parent.name != "baseObject") // Check if a side is highlighted
        {
            // Find the parent cube object (assuming it's the immediate parent)
            GameObject parentCube = gameObject.transform.parent.parent.gameObject;
            Destroy(parentCube);
        }
    }
    void OnTriggerEnter(Collider other) {
        // disables child objects when any collider enters the trigger area
        // Get all child GameObjects under "Side1", including nested ones
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child = gameObject.transform.GetChild(i).gameObject;
            if (child != null)
                child.SetActive(false);
        }
        //Debug.Log("disappear");
        collidingObject = other.gameObject;


    }
    void OnDestroy() {
        //enabling sides that were diasabled as a result of placing cube
        if (collidingObject != null)
        {
            for (int i = 0; i < collidingObject.transform.childCount; i++)
            {
                var child = collidingObject.transform.GetChild(i).gameObject;
                if (child != null)
                    child.SetActive(true);
            }
        }
    }
    Vector3 GetSideOffset(int sideIndex)
    {
        float voxelSize = 1.0f; // Replace with your actual voxel prefab size
        Vector3 offset = Vector3.zero;

        switch (sideIndex)
        {
            case 0: // Front face
                offset = new Vector3(0, 0, -voxelSize);
                break;
            case 1: // Back face
                offset = new Vector3(0, 0, voxelSize);
                break;
            case 2: // Top face
                offset = new Vector3(0, voxelSize, 0);
                break;
            case 3: // Bottom face
                offset = new Vector3(0, -voxelSize, 0);
                break;
            case 4: // Right face
                offset = new Vector3(voxelSize, 0, 0);
                break;
            case 5: // Left face
                offset = new Vector3(-voxelSize, 0, 0);
                break;
        }

        return offset;
    }

}
