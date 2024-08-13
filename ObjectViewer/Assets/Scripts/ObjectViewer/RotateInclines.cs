using UnityEngine;

public class RotateInclines : MonoBehaviour
{
    private string[] inputs = { "w", "s", "a", "d", "q", "e" };

    private static GameObject currentSubObject;
    private static string currentDirection = "X";
    private static string currentTriangle = "1";
    
    public static bool isRotating = false;
    
    private float lastExecutionTime = 0f;
    private float cooldown = 0.3f; // 1 second cooldown for rotation
    
    // Start is called before the first frame update
    void Start()
    {
        //is set in order to disable the main objects rotation when rotating the incline 
        isRotating = true;
        //enables default triangle
        EnableSubObjectByName(currentDirection, currentTriangle);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.L) && isRotating)
        {
            isRotating = false;
            DeleteInactiveObjects();
            
            //Deletes script after done
            Destroy(this);
        }
    }

    void FixedUpdate()
    {

        // Check if atleast 0.3 seconds has passed before user is able to rotate the incline object
        if (Time.time >= lastExecutionTime + cooldown)
        {
            //Checks through every possible input 
            for (int i = 0; i < inputs.Length; i++)
            {
                if (Input.GetKey(inputs[i]))
                {
                    string key = inputs[i];
                    //switch statement that changes the incline object based on the direction and axis 
                    switch (currentDirection)
                    {
                        case "X":
                            switch (currentTriangle)
                            {
                                case "1":
                                    if (key == "w") EnableSubObjectByName("Y", "1");
                                    else if (key == "a") EnableSubObjectByName("Z", "1");
                                    else if (key == "s") EnableSubObjectByName("Y", "4");
                                    else if (key == "d") EnableSubObjectByName("Z", "2");
                                    else if (key == "q") EnableSubObjectByName("X", "2");
                                    else if (key == "e") EnableSubObjectByName("X", "4");
                                    break;
                                case "2":
                                    if (key == "w") EnableSubObjectByName("Y", "2");
                                    else if (key == "a") EnableSubObjectByName("Z", "2");
                                    else if (key == "s") EnableSubObjectByName("Y", "3");
                                    else if (key == "d") EnableSubObjectByName("Z", "1");
                                    else if (key == "q") EnableSubObjectByName("X", "3");
                                    else if (key == "e") EnableSubObjectByName("X", "1");
                                    break;
                                case "3":
                                    if (key == "w") EnableSubObjectByName("Y", "3");
                                    else if (key == "a") EnableSubObjectByName("Z", "3");
                                    else if (key == "s") EnableSubObjectByName("Y", "1");
                                    else if (key == "d") EnableSubObjectByName("Z", "4");
                                    else if (key == "q") EnableSubObjectByName("X", "4");
                                    else if (key == "e") EnableSubObjectByName("X", "2");
                                    break;
                                case "4":
                                    if (key == "w") EnableSubObjectByName("Y", "4");
                                    else if (key == "a") EnableSubObjectByName("Z", "4");
                                    else if (key == "s") EnableSubObjectByName("Y", "1");
                                    else if (key == "d") EnableSubObjectByName("Z", "3");
                                    else if (key == "q") EnableSubObjectByName("X", "1");
                                    else if (key == "e") EnableSubObjectByName("X", "3");
                                    break;

                                default:
                                    // Handle any other cases or provide a default behavior
                                    Debug.Log("Rotation exception. Unknown control key: " + key);
                                    EnableSubObjectByName("Y", "1");
                                    break;
                            }

                            break;
                        case "Y":
                            switch (currentTriangle)
                            {
                                case "1":
                                    if (key == "w") EnableSubObjectByName("X", "4");
                                    else if (key == "a") EnableSubObjectByName("Y", "4");
                                    else if (key == "s") EnableSubObjectByName("X", "1");
                                    else if (key == "d") EnableSubObjectByName("Y", "2");
                                    else if (key == "q") EnableSubObjectByName("Z", "2");
                                    else if (key == "e") EnableSubObjectByName("Z", "3");
                                    break;
                                case "2":
                                    if (key == "w") EnableSubObjectByName("X", "3");
                                    else if (key == "a") EnableSubObjectByName("Y", "1");
                                    else if (key == "s") EnableSubObjectByName("X", "2");
                                    else if (key == "d") EnableSubObjectByName("Y", "3");
                                    else if (key == "q") EnableSubObjectByName("Z", "3");
                                    else if (key == "e") EnableSubObjectByName("Z", "2");
                                    break;
                                case "3":
                                    if (key == "w") EnableSubObjectByName("X", "2");
                                    else if (key == "a") EnableSubObjectByName("Y", "2");
                                    else if (key == "s") EnableSubObjectByName("X", "3");
                                    else if (key == "d") EnableSubObjectByName("Y", "4");
                                    else if (key == "q") EnableSubObjectByName("Z", "4");
                                    else if (key == "e") EnableSubObjectByName("Z", "1");
                                    break;
                                case "4":
                                    if (key == "w") EnableSubObjectByName("X", "1");
                                    else if (key == "a") EnableSubObjectByName("Y", "3");
                                    else if (key == "s") EnableSubObjectByName("X", "4");
                                    else if (key == "d") EnableSubObjectByName("Y", "1");
                                    else if (key == "q") EnableSubObjectByName("Z", "1");
                                    else if (key == "e") EnableSubObjectByName("Z", "4");
                                    break;

                                default:
                                    // Handle any other cases or provide a default behavior
                                    Debug.Log("Rotation exception. Unknown control key: " + key);
                                    EnableSubObjectByName("Y", "1");
                                    break;
                            }
    
                            break;
                        case "Z":
                            switch (currentTriangle)
                            {
                                case "1":
                                    if (key == "w") EnableSubObjectByName("Z", "2");
                                    else if (key == "a") EnableSubObjectByName("X", "2");
                                    else if (key == "s") EnableSubObjectByName("Z", "4");
                                    else if (key == "d") EnableSubObjectByName("X", "1");
                                    else if (key == "q") EnableSubObjectByName("Y", "3");
                                    else if (key == "e") EnableSubObjectByName("Y", "4");
                                    break;
                                case "2":
                                    if (key == "w") EnableSubObjectByName("Z", "3");
                                    else if (key == "a") EnableSubObjectByName("X", "1");
                                    else if (key == "s") EnableSubObjectByName("Z", "1");
                                    else if (key == "d") EnableSubObjectByName("X", "2");
                                    else if (key == "q") EnableSubObjectByName("Y", "2");
                                    else if (key == "e") EnableSubObjectByName("Y", "1");
                                    break;
                                case "3":
                                    if (key == "w") EnableSubObjectByName("Z", "4");
                                    else if (key == "a") EnableSubObjectByName("X", "4");
                                    else if (key == "s") EnableSubObjectByName("Z", "2");
                                    else if (key == "d") EnableSubObjectByName("X", "3");
                                    else if (key == "q") EnableSubObjectByName("Y", "1");
                                    else if (key == "e") EnableSubObjectByName("Y", "2");
                                    break;
                                case "4":
                                    if (key == "w") EnableSubObjectByName("Z", "1");
                                    else if (key == "a") EnableSubObjectByName("X", "3");
                                    else if (key == "s") EnableSubObjectByName("Z", "3");
                                    else if (key == "d") EnableSubObjectByName("X", "4");
                                    else if (key == "q") EnableSubObjectByName("Y", "4");
                                    else if (key == "e") EnableSubObjectByName("Y", "3");
                                    break;

                                default:
                                    // Handle any other cases or provide a default behavior
                                    Debug.Log("Rotation exception. Unknown control key: " + key);
                                    EnableSubObjectByName("Y", "1");
                                    break;
                            }
                            
                            break;
                    }
                    lastExecutionTime = Time.time;
                }
            }
        }
    }

    /*
     * Chooses which triangle object to enable within the template through direction (X,Y,Z) and triangle number (1-4)
     */
    private void EnableSubObjectByName(string direction, string triangle)
    {
        // Reenables any disabled colliding side for adjacent objects
        if (currentSubObject != null)
        {
            string[] targetSubObjectNames = { "Rect Side 3", "Bottom" };
            foreach (string subObjectName in targetSubObjectNames)
            {
                Transform subObjectTransform = currentSubObject.transform.Find(subObjectName);
                VoxelAdder subObjectVoxelAdder = subObjectTransform.GetComponent<VoxelAdder>();


                GameObject collidedObject = subObjectVoxelAdder.collidingObject;
                if (collidedObject != null)
                {
                    foreach (Transform child in collidedObject.transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                else
                {
                    Debug.Log($"Couldn't find collided object {collidedObject} {subObjectVoxelAdder}");
                }
                
            }
        }


        currentDirection = direction;
        currentTriangle = triangle;
        

        triangle = $"{direction}-Triangle {triangle}";
        //sets all objects to disabled in the template
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);

        }
        // Find the sub child triangle by name
        Transform subObject = gameObject.transform.Find(triangle);
        if (subObject == null)
        {
            Debug.LogWarning($"Sub child object '{triangle}' not found under '{direction}'.");
            return;
        }
        //sets the current triangle child object 
        currentSubObject = subObject.gameObject;

        // Enable the sub child if it's currently disabled
        if (!subObject.gameObject.activeSelf)
        {
            subObject.gameObject.SetActive(true);
            Debug.Log($"Sub child object '{triangle}' under '{direction}' has been enabled.");
        }
        else
        {
            Debug.Log($"Sub child object '{triangle}' under '{direction}' is already enabled.");
        }
    }
    /*
     * Runs when user presses "L" on keyboard.
     * Deletes all inactive children of the Triangle template
     */
    void DeleteInactiveObjects()
    {
            // Flag to check if we should delete the entire parent
            bool deleteParent = false;

            // Create a list of children to delete (to avoid modifying the collection during iteration)
            var childrenToDelete = new System.Collections.Generic.List<Transform>();

            // Iterate through all children of X, Y, Z (T1, T2, T3, T4)
            foreach (Transform child in transform)
            {
                if (!child.gameObject.activeSelf)
                {
                    childrenToDelete.Add(child);
                }
                else
                {
                    // If any T object is active, we keep its parent and its children
                    deleteParent = true;
                }
            }

            // If no active child was found, delete the parent
            if (deleteParent)
            {
                // Delete all the inactive children under this parent
                foreach (Transform child in childrenToDelete)
                {
                    Destroy(child.gameObject);
                }
            }

    }

}
