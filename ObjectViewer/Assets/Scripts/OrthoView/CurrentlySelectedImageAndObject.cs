using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentlySelectedImageAndObject : MonoBehaviour
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
		for(int i = 0; i < numbersObjects.Length; i++){
			if(challengeManager.GetComponent<MatchObjectsToDrawings>().numberStates[i] == 0)
				numbers[i].color = Color.white;
			else if(challengeManager.GetComponent<MatchObjectsToDrawings>().numberStates[i] == 1)
				numbers[i].color = active;
			else
				numbers[i].color = Color.green;
		}			
		
    }
}
