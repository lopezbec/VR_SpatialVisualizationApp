using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultipleChoice3Dto2D : MonoBehaviour
{
	public GameObject[] progressBar;
	public Sprite progressCircleFinished;
	public GameObject completedText, imageToMatchObject, tryAnother, correctGuessText, objectManager;
	public GameObject[] matchObjects;
	
	public float range = 0.1f;
	
	private int[] userObjectForChallenges = {1, 2, 3, 4}; // Indicates which user object should be active for each challenge.
	private int[] answerPositionForChallenges = {2, 3, 1, 4}; // Indicates which position the correct answer image will be on the screen for each challenge.
	
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
	
	private int[,] rotationSets = new int[,]{{0, 1, 2, 3},
									{5, 6, 7, 8},
									{12, 11, 10, 9},
									{13, 1, 12, 3},
									{1, 1, 2, 3}};
									
	private int[,] matchObjectsForChallenges = new int[,]{{0, 1, 2, 3},
								{4, 3, 2, 3},
								{3, 1, 6, 5},
								{1, 2, 3, 4},
								{5, 1, 0, 4}};
	
	private int numberOfChallenges = 9, progress = 0, messageDelayCount, messageDelay = 500;
	private Transform[] matchTransforms = new Transform[4];
	private string[] inputs = {"1", "2", "3", "4"};
	GameObject collect;

	void Start(){
		collect = GameObject.Find("CollectData");
		messageDelayCount = messageDelay + 1; // messageDelayCount is used to display a message for some amount of time (messageDelay) after the user enters an input. This step is to prevent starting the challenge with a message showing.
	
		numberOfChallenges = userObjectForChallenges.Length; // The number of challenges is given by the inputs into this array in the inspector.
	
		for(int i = 0; i < matchObjects.Length; i++) // Get all of the transforms of the match objects so we can give them the correct rotations for the challenges.
			matchTransforms[i] = matchObjects[i].GetComponent<Transform>();
		
		for(int i = 0; i < progressBar.Length; i++) // Make sure the correct number of progress dots are displayed.
			progressBar[i].SetActive(i < numberOfChallenges);
			
		
		objectManager.GetComponent<ObjectManager>().SetActive(userObjectForChallenges[0]); // Set the next correct object to be active.
		for(int i = 0; i < matchObjects.Length; i++){
			matchObjects[i].GetComponent<ObjectManager>().SetActive(matchObjectsForChallenges[0, i]);
			matchTransforms[i].rotation = matchRotations[rotationSets[0, i]];
		}
	}

    void Update()
    {
		
		
		if(messageDelayCount < messageDelay){ // If the user has entered an input, stop for a bit to show a message.
			
			messageDelayCount++;
			
			if(messageDelayCount >= messageDelay){ // If the delay has elapsed we can stop showing the message.
				
				tryAnother.SetActive(false);
			
				if(correctGuessText.activeInHierarchy){
					
					for(int f = 0; f < matchObjects.Length; f++){
						matchObjects[f].SetActive(true);
						matchObjects[f].GetComponent<ObjectManager>().SetActive(matchObjectsForChallenges[progress, f]); // Get the new set of match objects.
						matchTransforms[f].rotation = matchRotations[rotationSets[progress, f]]; // rotate all the match objects to the desired orientation.
					}
					objectManager.GetComponent<ObjectManager>().SetActive(userObjectForChallenges[progress]); // Set the next correct user object to be active.
					
					correctGuessText.SetActive(false);
				}
			}
		}
		else // otherwise check for user input.
			for(int i = 0; i < inputs.Length; i++) // Check if the user has pressed any of the image selection keys (1, 2, etc...).
				if(Input.GetKeyUp(inputs[i])){
					messageDelayCount = 0; // The user has guessed something, so we're going to display some message.

					if (i + 1 == answerPositionForChallenges[progress])
					{ // If the user has correctly guessed.
						if (collect != null)
						{
							CollectData data = collect.GetComponent<CollectData>() as CollectData;
							data.newSubmission(SceneManager.GetActiveScene().name, true, progress + 1, numberOfChallenges-1);
						}
						progressBar[progress++].GetComponent<Image>().sprite = progressCircleFinished; // Set the next progress dot to the finished sprite.

						if (progress >= numberOfChallenges)
						{ // If the user has finished all the challenges, display the ending message.
							completedText.SetActive(true);
							imageToMatchObject.SetActive(false);
						}
						else
						{ // else the user has more challenges to do, display the message and hide the other drawings.
							correctGuessText.SetActive(true);

							matchObjects[0].SetActive(false); // Hide every wrong drawing.
							matchObjects[1].SetActive(false);
							matchObjects[2].SetActive(false);
							matchObjects[3].SetActive(false);
							matchObjects[answerPositionForChallenges[progress - 1] - 1].SetActive(true);
						}

					}
					else
					{
						// If the user has incorrectly guessed, display a message saying so.
						if (collect != null)
						{
							CollectData data = collect.GetComponent<CollectData>() as CollectData;
							data.newSubmission(SceneManager.GetActiveScene().name, false, progress + 1, numberOfChallenges-1);
						}
						tryAnother.SetActive(true);
					}
				}
	}
}








