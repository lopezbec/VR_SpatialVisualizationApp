using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchObjectsToDrawings : MonoBehaviour
{
    public GameObject[] progressBar;
	public Sprite progressCircleFinished;
	public GameObject completedText, imageToMatchObject, tryAnother, correctGuessText, matchObject, userObject, pressEnterText;
	
	public float range = 0.1f;
	
	private int[,] matchObjectsForChallenges = {{1, 2, 3, 4}, {3, 4, 5, 6}}; // Indicates which user objects should be available for each challenge.
	private int[,] userObjectsForChallenges = {{4, 3, 2, 1}, {5, 6, 4, 3}};
	private int[,] answerPositionForChallenges = {{4, 3, 2, 1}, {4, 3, 1, 2}}; // Indicates which position the correct answer image will be based on the keyboard key for each challenge.
	public int[] numberStates = {1,0,0,0,1,0,0,0};
	
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
	
	public int currentActiveObject = 1, currentActiveMatchObject = 1;
	private int numberOfChallenges = 9, progress = 0, messageDelayCount, messageDelay = 500, correctThisRound = 0;
	private Transform matchTransform;
	private string[] inputs = {"1", "2", "3", "4"};
	
	void Start(){		
	
		messageDelayCount = messageDelay + 1; // messageDelayCount is used to display a message for some amount of time (messageDelay) after the user enters an input. This step is to prevent starting the challenge with a message showing.
	
		numberOfChallenges = matchObjectsForChallenges.GetLength(0);
	
		matchTransform = matchObject.GetComponent<Transform>(); // Get the transform of the match object so we can show different rotations for different rounds.
		
		for(int i = 0; i < progressBar.Length; i++) // Make sure the correct number of progress dots are displayed.
			progressBar[i].SetActive(i < numberOfChallenges);
		
		userObject.GetComponent<ObjectManager>().SetActive(userObjectsForChallenges[0, 0]); // Set the next correct object to be active.
		
		matchObject.GetComponent<ObjectManager>().SetActive(matchObjectsForChallenges[0, 0]); // Set the match object to be the desired object.
		matchTransform.rotation = matchRotations[0]; // Set the rotation.
	}

    void Update()
    {
		if(messageDelayCount < messageDelay){ // If the user has entered an input, stop for a bit to show a message.
			
			messageDelayCount++;
			
			if(messageDelayCount >= messageDelay){ // If the delay has elapsed we can stop showing the message.
				
				if(correctGuessText.activeInHierarchy){					
					
					matchObject.GetComponent<ObjectManager>().SetActive(matchObjectsForChallenges[progress, 0]); // Get the new set of match objects.
					matchTransform.rotation = matchRotations[progress]; // rotate all the match objects to the desired orientation.
					
					userObject.GetComponent<ObjectManager>().SetActive(userObjectsForChallenges[progress, 0]); // Set the next correct user object to be active.
					userObject.GetComponent<ObjectViewerRig>().reset();
					currentActiveObject = 1;
					currentActiveMatchObject = 1;
					
					for(int i = 0; i < numberStates.Length; i++)
						numberStates[i] = 0;
					
					numberStates[0] = 1;
					numberStates[4] = 1;
					
					correctGuessText.SetActive(false);
				}
				else
					tryAnother.SetActive(false);
			}
		}
		else{ // otherwise check for user input.
			for(int i = 0; i < inputs.Length; i++) // Check if the user has pressed any of the image selection keys (1, 2, etc...).
				if(Input.GetKeyUp(inputs[i])){
					if(Input.GetKey(KeyCode.Space) && numberStates[i + 4] == 0){
						matchObject.GetComponent<ObjectManager>().SetActive(matchObjectsForChallenges[progress, i]);
						
						numberStates[currentActiveMatchObject - 1 + 4] = 0;
						numberStates[i + 4] = 1;
						currentActiveMatchObject = i+1;
					}
					else if (numberStates[i] == 0){
						numberStates[currentActiveObject - 1] = 0;
						numberStates[i] = 1;
						userObject.GetComponent<ObjectManager>().SetActive(userObjectsForChallenges[progress, i]);
						currentActiveObject = i+1;
					}
					
					
				}
			if(Input.GetKeyUp(KeyCode.Return)){
				messageDelayCount = 0; // The user has guessed something, so we're going to display some message.
					
					if(currentActiveObject == answerPositionForChallenges[progress, currentActiveMatchObject - 1]){ // If the user has correctly guessed.
					
						if(numberStates[currentActiveObject - 1] != 2){ // If the user has not made this connection yet.
							if(correctThisRound >= 3){			
								correctThisRound = 0;
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
							else{
								numberStates[currentActiveObject - 1] = 2;
								numberStates[currentActiveMatchObject + 3] = 2;
								
								for(int i = 0; i < 4; i++)
									if(numberStates[i] == 0){
										numberStates[i] = 1;
										currentActiveObject = i+1;
										userObject.GetComponent<ObjectManager>().SetActive(userObjectsForChallenges[progress, i]);
										break;
									}
								for(int i = 0; i < 4; i++)
									if(numberStates[i + 4] == 0){
										numberStates[i + 4] = 1;
										currentActiveMatchObject = i+1;
										matchObject.GetComponent<ObjectManager>().SetActive(matchObjectsForChallenges[progress, i]);
										break;
									}
								
								
								
								correctThisRound++;
							}
						}
						else // The user has already guessed this link!
							Debug.Log("Already did that one!");
					}
					else // If the user has incorrectly guessed, display a message saying so.
						tryAnother.SetActive(true);
			}
		}
	}
}
