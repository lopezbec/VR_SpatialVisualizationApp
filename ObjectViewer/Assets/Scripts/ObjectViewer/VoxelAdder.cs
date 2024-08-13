using UnityEngine;

public class VoxelAdder : MonoBehaviour
{
    public string cubePrefabPath = "Prefabs/baseVoxelObject"; //prefab path 
    public string inclinePrefabPath = "Prefabs/inclineVoxelObject_X_1"; //prefab path 
    private GameObject cubeVoxelPrefab;// Prefab for the new voxel object 
    private GameObject inclineVoxelPrefab;// Prefab for the new incline voxel object 
    public Color inclineSelectionColor = new Color(1, 1, 1, 0.5f); // Semi-transparent white
    
    public int sideIndex; //dictates which side is being pressed on
    public GameObject childObject; // Reference to the child object to highlight (from original script)
    
    public Material originalMaterial; // Store the original color of the child (from original script)
    public Color highlightColor; // Set your desired highlight color (from original script)

    public GameObject collidingObject; //keeps track of side it is colliding with if colliding
    public bool isCollided = false; //keeps track if side is colliding

    void Start()
    {
        //must do this in order to copy prefab and not the instance itself
        cubeVoxelPrefab = Resources.Load<GameObject>(cubePrefabPath);
        inclineVoxelPrefab = Resources.Load<GameObject>(inclinePrefabPath);
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
        if (childObject != null && AddSubtractUIControls.selectedButton == "add" &&
            !isCollided) // Check if a side is highlighted
        {
            // Find the parent cube object (assuming it's the immediate parent)
            Transform parentCubeTransform = transform.parent;

            // Get positions of BaseVoxel and parent cube
            Vector3 baseVoxelPosition = parentCubeTransform.parent.position; // BaseVoxel position

            GameObject newVoxel;

            if (BoxSlopeUIControls.selectedButton == "box")
            {
                // Create new voxel with parent cube's position and rotation
                newVoxel = Instantiate(cubeVoxelPrefab, baseVoxelPosition, transform.parent.rotation);
                
                newVoxel.transform.parent = parentCubeTransform.parent.parent; // Set BaseVoxel as parent

                // Calculate offset based on clicked side
                Vector3 offset = GetSideOffset(sideIndex);

                // Set Child Cube position with offset (considering rotation)
                newVoxel.transform.GetChild(0).localPosition = transform.parent.localPosition + offset;

            }
            else if (BoxSlopeUIControls.selectedButton == "slope")
            {
                // Create new voxel with parent cube's position and rotation
                newVoxel = Instantiate(inclineVoxelPrefab, baseVoxelPosition, transform.parent.rotation);
                newVoxel.transform.parent = parentCubeTransform.parent.parent; // Set BaseVoxel as parent
                
                // Calculate offset based on clicked side
                //TODO: SideIndex is not yet set for incline. Also there is some positioning issues when placing an object off incline
                Vector3 offset = GetSideOffset(sideIndex);

                // Set incline position with offset (considering rotation)
                newVoxel.transform.localPosition = transform.parent.localPosition + offset - new Vector3(-1, 1, 1);
            }
            else
            {
                newVoxel = Instantiate(cubeVoxelPrefab, baseVoxelPosition, transform.parent.rotation);
                Debug.Log("Something went wrong with the button selection");
            }

            
        }
        else if (childObject != null && AddSubtractUIControls.selectedButton == "subtract" && gameObject.transform.parent.parent.name != "baseObject" && !isCollided) // Check if a side is highlighted
        {
            // Find the parent cube object (assuming it's the immediate parent)
            GameObject parentCube = gameObject.transform.parent.parent.gameObject;
            Destroy(parentCube);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // disables child objects when any collider enters the trigger area
        // Get all child GameObjects under "Side1", including nested ones
        if (CompareTag("SideObject") && other.CompareTag("SideObject"))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            collidingObject = other.gameObject;
            isCollided = true;
        }

    }
    void OnDestroy() {
        if (collidingObject != null && collidingObject.activeInHierarchy)
        {
            //enabling sides that were disabled as a result of placing cube
            foreach (Transform child in collidingObject.transform)
            {
                child.gameObject.SetActive(true);
            }

            collidingObject.GetComponent<VoxelAdder>().SetCollided(false);
        }
    }
    Vector3 GetSideOffset(int sideIndex)
    {
        float voxelSize = 1.0f; // Voxel prefab size
        Vector3[] offsets = {
            Vector3.back * voxelSize, 
            Vector3.forward * voxelSize,
            Vector3.up * voxelSize,
            Vector3.down * voxelSize,
            Vector3.right * voxelSize,
            Vector3.left * voxelSize,
        };

        return sideIndex >= 0 && sideIndex < offsets.Length ? offsets[sideIndex] : Vector3.zero;
    }
    public void SetCollided(bool collided)
    {
        isCollided = collided;
    }

}
