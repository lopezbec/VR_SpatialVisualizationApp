using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentMatchObject : MonoBehaviour
{
	public Color active;
	public GameObject challengeManager;
	public GameObject[] numbersObjects;
	
	private TextMeshProUGUI[] numbers = new TextMeshProUGUI[20];
	
	public int currentlyActive = 1;

    // Start is called before the first frame update
    void Start()
    {
		for(int i = 0; i < numbersObjects.Length; i++)
			numbers[i] = numbersObjects[i].GetComponent<TextMeshProUGUI>();
		
		numbers[currentlyActive - 1].color = active;
    }


    void Update()
    {
		if(challengeManager.GetComponent<Ortho3Dto2D>() != null){
			if(currentlyActive != challengeManager.GetComponent<Ortho3Dto2D>().currentActiveObject){
				numbers[currentlyActive - 1].color = Color.white;
				currentlyActive = challengeManager.GetComponent<Ortho3Dto2D>().currentActiveObject;
				numbers[currentlyActive - 1].color = active;
			}
		}
		else if(currentlyActive != challengeManager.GetComponent<MultipleChoice2Dto3D>().currentActiveObject){
				
				numbers[currentlyActive - 1].color = Color.white;
				currentlyActive = challengeManager.GetComponent<MultipleChoice2Dto3D>().currentActiveObject;
				numbers[currentlyActive - 1].color = active;
			}
    }
}
