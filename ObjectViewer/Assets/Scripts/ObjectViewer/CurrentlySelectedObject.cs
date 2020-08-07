using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentlySelectedObject : MonoBehaviour
{
	public Color active;
	public GameObject challengeManager;
	public GameObject[] numbersObjects;
	
	private Image[] numbers = new Image[20];
	
	private int currentlyActive = 1;

    void Start()
    {
		for(int i = 0; i < numbersObjects.Length; i++)
			numbers[i] = numbersObjects[i].GetComponent<Image>();
		
		numbers[currentlyActive - 1].color = active;
    }

    void Update()
    {
		if(currentlyActive != challengeManager.GetComponent<MultipleChoice2Dto3D>().currentActiveObject){
			numbers[currentlyActive - 1].color = Color.black;
			currentlyActive = challengeManager.GetComponent<MultipleChoice2Dto3D>().currentActiveObject;
			numbers[currentlyActive - 1].color = active;
		}
    }
}
