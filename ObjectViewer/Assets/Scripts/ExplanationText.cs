using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationText : MonoBehaviour
{
	public GameObject explanationText; // Put the explanation text to be controlled here.
	
	void Start(){
		explanationText.SetActive(true);
	}
	
    void Update(){
		if(explanationText.activeSelf){
			if(Input.anyKeyDown) // Input.GetKey("return")
				explanationText.SetActive(false);
		}
        else if(Input.GetKeyUp(KeyCode.Backslash))
			explanationText.SetActive(true);
    }
}
