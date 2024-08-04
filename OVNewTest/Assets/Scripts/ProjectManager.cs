using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ProjectManager : MonoBehaviour
{
    public bool hasStarted = false;
    public GameObject[] progressBar;
    public Sprite progressCircleFinished;
    public GameObject completedText, imageToMatchObject, tryAnother, pressEnter, objectManager;
    public GameObject matchObject;
    public float range = 0.1f;
    public Quaternion[] rotationToMatch;
    public int[] correctActiveObject, correctMatchingActiveObject;

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
        controlledObject = objectManager.GetComponent<Transform>();
        numberOfChallenges = rotationToMatch.Length + 1; 

        Debug.Log("Number of challenges: " + numberOfChallenges);
        Debug.Log("Setting initial active objects.");
        
        objectManager.GetComponent<ObjectManager>().SetActive(correctActiveObject[progress]); 
        matchObject.GetComponent<ObjectManager>().SetActive(correctMatchingActiveObject[progress]);

        matchObjectTransform = matchObject.GetComponent<Transform>();
        matchObjectTransform.eulerAngles = new Vector3(0, 0, 0);

        for (int i = 0; i < progressBar.Length; i++)
            progressBar[i].SetActive(i < numberOfChallenges - 1);
    }

    void Update()
    {
        if (!hasStarted)
        {
            return;
        }

        float speed = 1f;

        if (animationState == 0)
        {
            if (delayCounter >= initialDelay)
                animationState = 1;
            else
                delayCounter++;
        }
        else if (animationState == 1)
        {
            matchObjectTransform.rotation = Quaternion.RotateTowards(matchObjectTransform.rotation, rotationToMatch[progress], speed);

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
        CheckCondition();
    }

    private void CheckCondition()
    {
        if (CompareQuaternions(controlledObject.rotation, rotationToMatch[progress], range))
        {
            Debug.Log("Rotation matched. Proceeding to next challenge.");
            controlledObject.eulerAngles = new Vector3(0, 0, 0);

            if (progress < numberOfChallenges)
            {
                progressBar[progress++].GetComponent<Image>().sprite = progressCircleFinished;

                Debug.Log("Setting active objects for progress: " + progress);

                objectManager.GetComponent<ObjectManager>().SetActive(correctActiveObject[progress]);
                matchObject.GetComponent<ObjectManager>().SetActive(correctMatchingActiveObject[progress]);

                if (progress >= numberOfChallenges - 1)
                {
                    Debug.Log("All challenges completed.");
                    completedText.SetActive(true);
                    imageToMatchObject.SetActive(false);
                    pressEnter.SetActive(false);
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
            Debug.Log("Rotation did not match. Try again.");
            tryAnother.SetActive(true);
        }
    }

    private bool CompareQuaternions(Quaternion a, Quaternion b, float range)
    {
        return (Mathf.Abs(a.x) <= Mathf.Abs(b.x) + range) && (Mathf.Abs(a.x) >= Mathf.Abs(b.x) - range) &&
               (Mathf.Abs(a.y) <= Mathf.Abs(b.y) + range) && (Mathf.Abs(a.y) >= Mathf.Abs(b.y) - range) &&
               (Mathf.Abs(a.z) <= Mathf.Abs(b.z) + range) && (Mathf.Abs(a.z) >= Mathf.Abs(b.z) - range) &&
               (Mathf.Abs(a.w) <= Mathf.Abs(b.w) + range) && (Mathf.Abs(a.w) >= Mathf.Abs(b.w) - range);
    }

    public void StartProcess()
    {
        Debug.Log("Starting process.");
        hasStarted = true;
    }
}
