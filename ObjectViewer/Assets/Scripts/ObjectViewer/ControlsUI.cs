using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{
	
	public Color normal, active;
	public GameObject[] buttonObjects;
	
	private Image[] buttons = new Image[8];
	
	private string[] inputs = {"q", "w", "e", "a", "s", "d", "r", "space"};
	
	void Start() {
		for(int i = 0; i < buttonObjects.Length; i++)
			buttons[i] = buttonObjects[i].GetComponent<Image>();
	}
	
    void Update()
    {
		for(int i = 0; i < buttonObjects.Length; i++)
			if(Input.GetKey(inputs[i]))
				buttons[i].color = active;
			else
				buttons[i].color = normal;
    }
}
