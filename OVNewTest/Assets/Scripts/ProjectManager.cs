using UnityEngine;
using UnityEngine.UI;

public class ProjectManager : MonoBehaviour
{
    public bool hasStarted = false;
    public GameObject[] progressBar;
    public Sprite progressCircleFinished;
    public GameObject[] controlledObjects; // List of objects to control directly
    public GameObject[] matchingObjects; // List of matching objects directly
    public float range = 0.2f;
    public Quaternion[] rotationToMatch;
    public int activeObjects = 5; 
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
        
        // Activate the right objects using direct references
        SetActiveObjects(progress);
    
        matchObjectTransform = matchingObjects[progress].transform;
        matchObjectTransform.eulerAngles = new Vector3(0, 0, 0);

        controlledObject = controlledObjects[progress].transform;

        for (int i = 0; i < progressBar.Length; i++)
            progressBar[i].SetActive(i < numberOfChallenges - 1);
    }

    void Update()
    {
        if (!hasStarted)
        {
            return;
        }

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
                   
                Debug.Log("Previous controlled object set to: " + controlledObject.name);
                
                progress++;
                Debug.Log("Progress value after increment: " + progress);
                
                if (progress < numberOfChallenges - 1)
                {
                    Debug.Log("Setting active objects for progress: " + progress);
                    SetActiveObjects(progress);

                    // Update controlledObject to the new active child
                    controlledObject = controlledObjects[progress].transform;
                    Debug.Log("New controlled object set to: " + controlledObject.name);

                    animationState = 0;
                    delayCounter = 0;
                    matchObjectTransform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    Debug.Log("All challenges completed.");
                    animationState = -1;
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
        Quaternion difference = Quaternion.Inverse(currentRotation) * targetRotation;
        Vector3 differenceEulerAngles = difference.eulerAngles;
        return differenceEulerAngles;
    }

    private void SetActiveObjects(int index)
    {
        // Deactivate all objects first
        foreach (GameObject obj in controlledObjects)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in matchingObjects)
        {
            obj.SetActive(false);
        }

        // Activate the current objects
        controlledObjects[index].SetActive(true);
        matchingObjects[index].SetActive(true);
    }

    public void StartProcess()
    {
        Debug.Log("Starting process.");
        hasStarted = true;
    }
}
