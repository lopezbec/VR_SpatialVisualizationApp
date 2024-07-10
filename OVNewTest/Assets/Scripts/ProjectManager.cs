using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ProjectManager : MonoBehaviour
{
    public GameObject[] progressBar;
    public Sprite progressCircleFinished;
    public GameObject completedText, imageToMatchObject, tryAnother, pressEnter, objectManager;
    public GameObject matchObject;
    
    public float range = 0.1f;
    
    public Quaternion[] rotationToMatch;
    public int[] correctActiveObject, // Set from the inspector. Indicates which object should be active for each challenge.
                correctMatchingActiveObject; // Set from the inspector. Indicates which object the animation view should be set to.
    
    private int numberOfChallenges = 6;
    private int progress = 0;
    private Transform controlledObject, matchObjectTransform;
    GameObject collect;

    void Start()
    {
        Debug.Log("Starting ProjectManager script.");

        collect = GameObject.Find("CollectData");
        controlledObject = objectManager.GetComponent<Transform>();
        numberOfChallenges = rotationToMatch.Length + 1; // The number of challenges is given by the number of rotations entered into the array.

        Debug.Log("Number of challenges: " + numberOfChallenges);
        Debug.Log("Setting initial active objects.");

        objectManager.GetComponent<ObjectManager>().SetActive(correctActiveObject[progress]); // Set the next correct object to be active.
        matchObject.GetComponent<ObjectManager>().SetActive(correctMatchingActiveObject[progress]);
        
        matchObjectTransform = matchObject.GetComponent<Transform>();
        matchObjectTransform.eulerAngles = new Vector3(0, 0, 0);
        
        for (int i = 0; i < progressBar.Length; i++) // Make sure the correct number of progress dots are displayed.
            progressBar[i].SetActive(i < numberOfChallenges - 1);
    }

    private int animationState = 0; // 0 = delay before rotation, 1 = rotating, 2 = delay after rotation.
    private int delayCounter = 0, initialDelay = 60, finalDelay = 60;

    void FixedUpdate()
    {
        float speed = 1f;
        
        // Animation controls.
        if (animationState == 0) // delay before rotation.
        {
            if (delayCounter >= initialDelay)
                animationState = 1;
            else
                delayCounter++;
        }
        else if (animationState == 1) // Rotate the matching view towards the goal rotation for this challenge.
        {
            matchObjectTransform.rotation = Quaternion.RotateTowards(matchObjectTransform.rotation, rotationToMatch[progress], speed);

            if (CompareQuaternions(matchObjectTransform.rotation, rotationToMatch[progress], 0.01f))
            {
                animationState = 2;
                delayCounter = 0;
            }
        }
        else if (animationState == 2) // Delay after rotation.
        {
            if (delayCounter >= finalDelay)
            {
                animationState = 0; // Reset the animation and start all over.
                delayCounter = 0;
                matchObjectTransform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
                delayCounter++;
        }
    }

    void Update()
    {
        if (XRControllerHasActiveObject(controlledObject)) // Replace keyboard input with XR controller logic
        {
            if (CompareQuaternions(controlledObject.rotation, rotationToMatch[progress], range)) // If the object is approximately aligned to the target rotation for this challenge.
            {
                Debug.Log("Rotation matched. Proceeding to next challenge.");

                controlledObject.eulerAngles = new Vector3(0, 0, 0);

                // This section is the progression code. Makes sure the dots match the 3D cubes. 
                if (progress < numberOfChallenges)
                {
                    progressBar[progress++].GetComponent<Image>().sprite = progressCircleFinished; // Set the next progress dot to the finished sprite.
                    
                    Debug.Log("Setting active objects for progress: " + progress);

                    objectManager.GetComponent<ObjectManager>().SetActive(correctActiveObject[progress]); // Set the next correct object to be active.
                    matchObject.GetComponent<ObjectManager>().SetActive(correctMatchingActiveObject[progress]);

                    if (progress >= numberOfChallenges - 1) // If the user has finished all the challenges, display the ending message.
                    {
                        Debug.Log("All challenges completed.");
                        completedText.SetActive(true);
                        imageToMatchObject.SetActive(false);
                        pressEnter.SetActive(false);
                        animationState = -1; // Stop the animation from playing.
                    }
                    else
                    {
                        animationState = 0; // Reset the animation.
                        delayCounter = 0;
                        matchObjectTransform.eulerAngles = new Vector3(0, 0, 0);
                    }
                }
            }
            else
            {
                Debug.Log("Rotation did not match. Try again.");
                tryAnother.SetActive(true); // Set the "try another rotation!" text to visible if the user enters an incorrect rotation.
            }
        }
    }
    
    bool CompareQuaternions(Quaternion a, Quaternion b, float range) // Returns true if all the components of a are within "range" of b. In other words, if a and b are "mostly" equal.
    {
        return (Mathf.Abs(a.x) <= Mathf.Abs(b.x) + range) && (Mathf.Abs(a.x) >= Mathf.Abs(b.x) - range) &&
               (Mathf.Abs(a.y) <= Mathf.Abs(b.y) + range) && (Mathf.Abs(a.y) >= Mathf.Abs(b.y) - range) &&
               (Mathf.Abs(a.z) <= Mathf.Abs(b.z) + range) && (Mathf.Abs(a.z) >= Mathf.Abs(b.z) - range) &&
               (Mathf.Abs(a.w) <= Mathf.Abs(b.w) + range) && (Mathf.Abs(a.w) >= Mathf.Abs(b.w) - range);
    }

    bool XRControllerHasActiveObject(Transform controlledObject)
    {
        // This method should contain logic to determine if the XR controller is manipulating the controlled object.
        // You can use XR Grab Interactables or any custom logic specific to your setup.
        // Return true if the controlledObject is being manipulated.
        // Placeholder logic:
        return true;
    }
}
