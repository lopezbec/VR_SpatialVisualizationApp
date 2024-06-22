using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinatorControlsUI : MonoBehaviour
{
	
	public Color normal, active;
	public GameObject[] buttonObjects;
	
	private Image[] buttons = new Image[14];
	
	private KeyCode[] inputs = {KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.R, KeyCode.Space, KeyCode.Z, KeyCode.UpArrow, KeyCode.X, KeyCode.LeftArrow, KeyCode.DownArrow,  KeyCode.RightArrow };
	
	void Start() {
		for (int i = 0; i < buttonObjects.Length; i++)
		{
			buttons[i] = buttonObjects[i].GetComponent<Image>();
		}
	}
	
    void Update()
    {
		for (int i = 0; i < buttonObjects.Length; i++)
			if (Input.GetKey(inputs[i]))
			{
				buttons[i].color = active;
			}
			else
			{
				buttons[i].color = normal;
			}
    }
}
