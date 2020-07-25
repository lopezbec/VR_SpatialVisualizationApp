using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoice2Dto3D : MonoBehaviour
{
	public GameObject[] progressBar;
	public Sprite progressCircleFinished;
	public GameObject completedText, imageToMatchObject, tryAnother, correctGuessText, matchObject, userObject, pressEnterText;
	
	public float range = 0.1f;
	
	private int[,] userObjectsForChallenges = {{1, 2, 3, 4}, {2, 3, 4, 5}, {1, 4, 5, 6}, {1, 0, 3, 4}}; // Indicates which user objects should be available for each challenge.
	private int[] answerPositionForChallenges = {3, 1, 4, 4}; // Indicates which position the correct answer image will be based on the keyboard key for each challenge.
	private int[] matchObjectsForChallenges = {3, 2, 6, 4};
	
	private Quaternion[] matchRotations = {new Quaternion(0, 1, 0, 0), 
										new Quaternion(0, 0.7f, 0, -0.7f),
										new Quaternion(0, 0.7f, 0, 0.7f),
										new Quaternion(0.7f, 0, 0, 0.7f),
										new Quaternion(1, 0, 0, 0),
										new Quaternion(0.7f, 0, 0, -0.7f),
										new Quaternion(-0.7f, 0, 0, 0.7f),
										new Quaternion(0, 0, 0.7f, 0.7f),
										new Quaternion(0, 0, 1, 0),
										new Quaternion(0, 0, 0.7f, 0.7f),
										new Quaternion(0.5f, 0.5f, 0.5f, 0.5f),
										new Quaternion(0, 0.7f, 0.7f, 0),
										new Quaternion(0.5f, 0.5f, 0.5f, -0.5f),
										new Quaternion(0.5f, 0.5f, -0.5f, 0.5f),
										new Quaternion(0.7f, 0.7f, 0, 0)};
	
	public int currentActiveObject = 1;
	private int numberOfChallenges = 9, progress = 0, messageDelayCount, messageDelay = 500;
	private Transform matchTransform;
	private string[] inputs = {"1", "2", "3", "4"};
	
	void Start(){		
	
		messageDelayCount = messageDelay + 1; // messageDelayCount is used to display a message for some amount of time (messageDelay) after the user enters an input. This step is to prevent starting the challenge with a message showing.
	
		numberOfChallenges = answerPositionForChallenges.Length;
	
		matchTransform = matchObject.GetComponent<Transform>(); // Get the transform of the match object so we can show different rotations for different rounds.
		
		for(int i = 0; i < progressBar.Length; i++) // Make sure the correct number of progress dots are displayed.
			progressBar[i].SetActive(i < numberOfChallenges);
		
		userObject.GetComponent<ObjectManager>().SetActive(userObjectsForChallenges[0, 0]); // Set the next correct object to be active.
		
		matchObject.GetComponent<ObjectManager>().SetActive(matchObjectsForChallenges[0]); // Set the match object to be the desired object.
		matchTransform.rotation = matchRotations[0]; // Set the rotation.
	}

    void Update()
    {
		if(messageDelayCount < messageDelay){ // If the user has entered an input, stop for a bit to show a message.
			
			messageDelayCount++;
			
			if(messageDelayCount >= messageDelay){ // If the delay has elapsed we can stop showing the message.
				
				if(correctGuessText.activeInHierarchy){					
					
					matchObject.GetComponent<ObjectManager>().SetActive(matchObjectsForChallenges[progress]); // Get the new set of match objects.
					matchTransform.rotation = matchRotations[progress]; // rotate all the match objects to the desired orientation.
					
					userObject.GetComponent<ObjectManager>().SetActive(userObjectsForChallenges[progress, 0]); // Set the next correct user object to be active.
					userObject.GetComponent<ObjectViewerRig>().reset();
					currentActiveObject = 1;
					
					correctGuessText.SetActive(false);
				}
				else
					tryAnother.SetActive(false);
			}
		}
		else{ // otherwise check for user input.
			for(int i = 0; i < inputs.Length; i++) // Check if the user has pressed any of the image selection keys (1, 2, etc...).
				if(Input.GetKeyUp(inputs[i])){
					userObject.GetComponent<ObjectManager>().SetActive(userObjectsForChallenges[progress, i]);
					
					currentActiveObject = i+1;
				}
			if(Input.GetKeyUp(KeyCode.Return)){
				messageDelayCount = 0; // The user has guessed something, so we're going to display some message.
					
					if(currentActiveObject == answerPositionForChallenges[progress]){ // If the user has correctly guessed.
					
						progressBar[progress++].GetComponent<Image>().sprite = progressCircleFinished; // Set the next progress dot to the finished sprite.

						if(progress >= numberOfChallenges){ // If the user has finished all the challenges, display the ending message.
							completedText.SetActive(true);
							imageToMatchObject.SetActive(false);
							pressEnterText.SetActive(false);
							this.enabled = false;
						}
						else{ // else the user has more challenges to do, display the message.
							correctGuessText.SetActive(true);
						}
						
					}
					else // If the user has incorrectly guessed, display a message saying so.
						tryAnother.SetActive(true);
			}
		}
	}
}








