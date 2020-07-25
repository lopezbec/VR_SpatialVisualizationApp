using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentlySelectedObject : MonoBehaviour
{
	public Color active;
	public GameObject challengeManager;
	public GameObject[] numbersObjects;
	
	private TextMeshProUGUI[] numbers = new TextMeshProUGUI[20];
	
	private int currentlyActive = 1;

	
    // Start is called before the first frame update
    void Start()
    {
		for(int i = 0; i < numbersObjects.Length; i++)
			numbers[i] = numbersObjects[i].GetComponent<TextMeshProUGUI>();
		
		numbers[currentlyActive - 1].color = active;
    }

    void Update()
    {
		if(currentlyActive != challengeManager.GetComponent<MultipleChoice2Dto3D>().currentActiveObject){
			numbers[currentlyActive - 1].color = Color.white;
			currentlyActive = challengeManager.GetComponent<MultipleChoice2Dto3D>().currentActiveObject;
			numbers[currentlyActive - 1].color = active;
		}
    }
}
