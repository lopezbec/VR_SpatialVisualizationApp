using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ProjectManager : MonoBehaviour
{
    public bool hasStarted = false;
    public GameObject[] progressBar;
    public Sprite progressCircleFinished;
    public GameObject objectManager;
    // public GameObject completedText;
    public GameObject matchObject;
    public float range = 0.2f;
    public Quaternion[] rotationToMatch;
    public int[] correctActiveObject, correctMatchingActiveObject;
    public float rotationSpeed = 0.35f; // Adjust this value to make the rotation slower

    private int numberOfChallenges = 6;
    private int progress = 0;
    private Transform controlledObject, matchObjectTransform;
    private GameObject collect;
    private int animationState = 0; 
    private int delayCounter = 0, initialDelay = 60, finalDelay = 60;
    private Coroutine currentCoroutine;

    void Start()
    {
        Debug.Log("Starting ProjectManager script.");
        collect = GameObject.Find("CollectData");
        numberOfChallenges = rotationToMatch.Length + 1; 

        Debug.Log("Number of challenges: " + numberOfChallenges);
        Debug.Log("Setting initial active objects.");
        // Activate the right objects 
        objectManager.GetComponent<ObjectManager>().SetActive(correctActiveObject[progress]); 
        matchObject.GetComponent<ObjectManager>().SetActive(correctMatchingActiveObject[progress]);
    
        matchObjectTransform = matchObject.GetComponent<Transform>();
        matchObjectTransform.eulerAngles = new Vector3(0, 0, 0);

        controlledObject = objectManager.transform.GetChild(correctActiveObject[progress]).GetComponent<Transform>();
        for (int i = 0; i < progressBar.Length; i++)
            progressBar[i].SetActive(i < numberOfChallenges - 1);
    }

    void Update()
    {
        if (!hasStarted)
        {
            return;
        }
        //    // TESTING ROTATION // COMMENT THIS AFTER TESTING 
        //     Debug.Log("Before Rotation: " + controlledObject.rotation.eulerAngles);
        //     controlledObject.Rotate(Vector3.up, 10f * Time.deltaTime); // Example rotation
        //     Debug.Log("After Rotation: " + controlledObject.rotation.eulerAngles);
        //     // END OF TESTING 
        if (animationState == 0)
        {
            if (delayCounter >= initialDelay)
                animationState = 1;
            else
                delayCounter++;
        }
        else if (animationState == 1)
        {
            matchObjectTransform.rotation = Quaternion.RotateTowards(matchObjectTransform.rotation, rotationToMatch[progress], rotationSpeed);
            // Compares to the objective transformation the object has to do for the animation ( not the match object)
            if (CompareQuaternions(matchObjectTransform.rotation, rotationToMatch[progress], 0.01f))
            {
                animationState = 2;
                delayCounter = 0;
            }
        }
        else if (animationState == 2)
        {
            if (delayCounter >= finalDelay)
            {
                animationState = 0;
                delayCounter = 0;
                matchObjectTransform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
                delayCounter++;
        }
    }

    public void OnCheckCondition()
    {
        Debug.Log("OnCheckCondition called.");
        CheckCondition();
    }

private void CheckCondition()
{
    if (CompareQuaternions(controlledObject.rotation, rotationToMatch[progress], range))
    {
        Debug.Log("Rotation matched. Proceeding to the next challenge.");
        controlledObject.eulerAngles = new Vector3(0, 0, 0);

        if (progress < numberOfChallenges)
        {
            if (progressBar[progress] == null)
            {
                Debug.LogError("progressBar[" + progress + "] is null.");
                return;
            }

            Image imageComponent = progressBar[progress].GetComponent<Image>();
            if (imageComponent == null)
            {
                Debug.LogError("No Image component found on progressBar[" + progress + "].");
                return;
            }

            if (progressCircleFinished == null)
            {
                Debug.LogError("progressCircleFinished sprite is not assigned.");
                return;
            }

            imageComponent.sprite = progressCircleFinished;

            Debug.Log("Setting active objects for progress: " + progress);
            progress++;

            // Set active objects
            objectManager.GetComponent<ObjectManager>().SetActive(correctActiveObject[progress]);
            matchObject.GetComponent<ObjectManager>().SetActive(correctMatchingActiveObject[progress]);

            // Update controlledObject to the new active child
            controlledObject = objectManager.transform.GetChild(correctActiveObject[progress]).GetComponent<Transform>();
        
            Debug.Log("New controlled object set: " + controlledObject.name);

            if (progress >= numberOfChallenges - 1)
            {
                Debug.Log("All challenges completed.");
                animationState = -1;
            }
            else
            {
                animationState = 0;
                delayCounter = 0;
                matchObjectTransform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
    else
    {
        Vector3 rotationDifference = CalculateRotationDifference(controlledObject.rotation, rotationToMatch[progress]);
        Debug.Log("Rotation did not match. Try again. Required rotation difference: " + rotationDifference);
    }
}

    private bool CompareQuaternions(Quaternion a, Quaternion b, float allowedAngleDifference)
{
    float angle = Quaternion.Angle(a, b);
    return angle <= allowedAngleDifference;
}

    private Vector3 CalculateRotationDifference(Quaternion currentRotation, Quaternion targetRotation)
    {
        // Calculate the difference quaternion
        Quaternion difference = Quaternion.Inverse(currentRotation) * targetRotation;

        // Convert the difference quaternion to Euler angles
        Vector3 differenceEulerAngles = difference.eulerAngles;

        return differenceEulerAngles;
    }

    public void StartProcess()
    {
        Debug.Log("Starting process.");
        hasStarted = true;
    }
}
