using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyRotationChallengeManager : MonoBehaviour
{
	public GameObject[] progressBar;
	public Sprite progressCircleFinished;
	public GameObject completedText, imageToMatchObject, tryAnother, pressEnter, objectManager;
	public GameObject matchObject;
	
	public float range = 0.1f;
	
	public Quaternion[] rotationToMatch;
	public int[] correctActiveObject;
	
	private int numberOfChallenges = 9;
	private int progress = 0;
	private Transform controlledObject;
	
	void Start(){		
		controlledObject = objectManager.GetComponent<Transform>();
		numberOfChallenges = rotationToMatch.Length + 1; // The number of challenges is given by the number of rotations entered into the array.
		
		matchObject.GetComponent<Transform>().rotation = rotationToMatch[0]; // Make sure the active object is the correct one.
	}

    void Update()
    {
		if(Input.GetKeyUp("i")) // Used for debugging. Checks if the current rotation of the object is correct or not without moving on to the next rotation.
			Debug.Log(CompareQuaternions(controlledObject.rotation, rotationToMatch[progress], 0.1f));
		
		if(Input.anyKey) // Gets rid of the "Try another" text.
			tryAnother.SetActive(false);
			
		if(Input.GetKeyUp(KeyCode.Return))
			if(CompareQuaternions(controlledObject.rotation, rotationToMatch[progress], range)){ // If the object is approximately aligned to the target rotation for this challenge.
		
				if(progress < numberOfChallenges)
				{
					progressBar[progress++].GetComponent<Image>().sprite = progressCircleFinished; // Set the next progress dot to the finished sprite.
					
					objectManager.GetComponent<ObjectManager>().SetActive(correctActiveObject[progress]); // Set the next correct object to be active.
					matchObject.GetComponent<ObjectManager>().SetActive(correctActiveObject[progress]);
					
					if(progress >= numberOfChallenges - 1){ // If the user has finished all the challenges, display the ending message.
						completedText.SetActive(true);
						imageToMatchObject.SetActive(false);
						pressEnter.SetActive(false);
					}
					else{
						matchObject.GetComponent<Transform>().rotation = rotationToMatch[progress]; // If the user has not finished the challenge, rotate the match object to the desired position.
					}
				}
			}
			else
				tryAnother.SetActive(true); // Set the "try another rotation!" text to visible if the user enters an incorrect rotation.
		
    }
	
	bool CompareQuaternions(Quaternion a, Quaternion b, float range) // Returns true if all the components of a are within "range" of b. In other words, if a and b are "mostly" equal.
	{
		return (Mathf.Abs(a.x) <= Mathf.Abs(b.x) + range) && (Mathf.Abs(a.x) >= Mathf.Abs(b.x) - range) &&
				(Mathf.Abs(a.y) <= Mathf.Abs(b.y) + range) && (Mathf.Abs(a.y) >= Mathf.Abs(b.y) - range) &&
				(Mathf.Abs(a.z) <= Mathf.Abs(b.z) + range) && (Mathf.Abs(a.z) >= Mathf.Abs(b.z) - range) &&
				(Mathf.Abs(a.w) <= Mathf.Abs(b.w) + range) && (Mathf.Abs(a.w) >= Mathf.Abs(b.w) - range);
	}
}
