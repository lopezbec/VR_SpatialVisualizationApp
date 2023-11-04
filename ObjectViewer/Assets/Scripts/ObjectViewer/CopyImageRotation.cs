using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyImageRotation : MonoBehaviour
{
	public GameObject[] progressBar;
	public Sprite progressCircleFinished;
	public GameObject completedText, imageToMatchObject, tryAnother, pressEnter, objectManager; //objectManager is of object we are rotation
	public GameObject matchObject; //matchObject is of object iamge we are copying
	
	public float range = 0.2f;
	
	public Quaternion[] rotationToMatch; //the rotations of objects to be matched
	public int[] correctActiveObject, // Set from the inspector. Indicates which object should be active for each challenge.
				correctMatchingActiveObject; // Set from the inspector. Indicates which object the match view should be set to.
	
	private int numberOfChallenges = 9;
	private int progress = 0;
	private Transform controlledObject;
	
	void Start(){		
		controlledObject = objectManager.GetComponent<Transform>();
		numberOfChallenges = rotationToMatch.Length + 1; // The number of challenges is given by the number of rotations entered into the array.

		for (int i = 0; i < rotationToMatch.Length; i++) {
			rotationToMatch[i].x = Random.Range(-1.0f, 1.0f);
			rotationToMatch[i].y = Random.Range(-1.0f, 1.0f);
			rotationToMatch[i].z = Random.Range(-1.0f, 1.0f);
			rotationToMatch[i].w = Random.Range(-1.0f, 1.0f);
		}

		matchObject.GetComponent<Transform>().rotation = rotationToMatch[0]; // Make sure the active object is the correct one.
		
		for(int i = 0; i < progressBar.Length; i++) // Make sure the correct number of progress dots are displayed.
			progressBar[i].SetActive(i < numberOfChallenges - 1);
			
		
		objectManager.GetComponent<ObjectManager>().SetActive(correctActiveObject[0]); // Set the next correct object to be active.
		matchObject.GetComponent<ObjectManager>().SetActive(correctMatchingActiveObject[0]);	
	}

    void Update()
    {
		if(Input.GetKeyUp("i")) // Used for debugging. Checks if the current rotation of the object is correct or not without moving on to the next rotation.
			Debug.Log(CompareQuaternions(controlledObject.rotation, rotationToMatch[progress], range) + " a= x:" + controlledObject.rotation.x + " y:" + controlledObject.rotation.y  + " z:" + controlledObject.rotation.z + " w:" + controlledObject.rotation.w + " b= x:" + rotationToMatch[progress].x + " y:" + rotationToMatch[progress].y + " z:" + rotationToMatch[progress].z + " w:" + rotationToMatch[progress].w);
		
		if(Input.anyKey) // Gets rid of the "Try another" text.
			tryAnother.SetActive(false);
			
		if(Input.GetKeyUp(KeyCode.Return))
			
			if(CompareQuaternions(controlledObject.rotation, rotationToMatch[progress], range)){ // If the object is approximately aligned to the target rotation for this challenge.
		
				controlledObject.eulerAngles = new Vector3(0, 0, 0);
		
				if(progress < numberOfChallenges)
				{
					progressBar[progress++].GetComponent<Image>().sprite = progressCircleFinished; // Set the next progress dot to the finished sprite.
					
					objectManager.GetComponent<ObjectManager>().SetActive(correctActiveObject[progress]); // Set the next correct object to be active.
					matchObject.GetComponent<ObjectManager>().SetActive(correctMatchingActiveObject[progress]);
					
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
		Debug.Log(Angle(a, b));

		return (Mathf.Abs(a.x) <= Mathf.Abs(b.x) + range) && (Mathf.Abs(a.x) >= Mathf.Abs(b.x) - range) &&
                (Mathf.Abs(a.y) <= Mathf.Abs(b.y) + range) && (Mathf.Abs(a.y) >= Mathf.Abs(b.y) - range) &&
                (Mathf.Abs(a.z) <= Mathf.Abs(b.z) + range) && (Mathf.Abs(a.z) >= Mathf.Abs(b.z) - range) &&
                (Mathf.Abs(a.w) <= Mathf.Abs(b.w) + range) && (Mathf.Abs(a.w) >= Mathf.Abs(b.w) - range);
    }

	float Angle(Quaternion a, Quaternion b)
	{
		var A = new Vector4(a.x, a.y, a.z, a.w);
		var B = new Vector4(b.x, b.y, b.z, b.w);
		var dot = Vector4.Dot(A, B);

		// A and -A represent the same rotation
		if (dot < 0)
		{
			A = -A;
			dot = -dot;
		}

		if (dot < 0.9999)
		{
			return Mathf.Acos(dot) * 360 / Mathf.PI;
		}
		else
		{
			// Small angle approximation
			return Vector4.Distance(A, B) * 360 / Mathf.PI;
		}
	}

}
