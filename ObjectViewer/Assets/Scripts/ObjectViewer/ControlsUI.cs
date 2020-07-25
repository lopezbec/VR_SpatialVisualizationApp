using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{
	
	public Color normal, active;
	public GameObject q, w, e, a, s, d, r, space;
	
	private Image[] buttons = new Image[8];
	
	private string[] inputs = {"q", "w", "e", "a", "s", "d", "r", "space"};
	
	void Start() {
		buttons[0] = q.GetComponent<Image>();
		buttons[1] = w.GetComponent<Image>();
		buttons[2] = e.GetComponent<Image>();
		buttons[3] = a.GetComponent<Image>();
		buttons[4] = s.GetComponent<Image>();
		buttons[5] = d.GetComponent<Image>();
		buttons[6] = r.GetComponent<Image>();
		buttons[7] = space.GetComponent<Image>();
	}
	
    void Update()
    {
		for(int i = 0; i < 8; i++)
			if(Input.GetKey(inputs[i]))
				buttons[i].color = active;
			else
				buttons[i].color = normal;
		/*
		if (Input.GetKeyUp("tab")) // Hide or reveal the controls UI if tab is released.
		{
			Vector3 temp = controls.GetComponent<RectTransform>().position;
			
			if(open){
				temp.x -= shiftAmount;
				tabActive.SetActive(false);
				tabClosedNormal.SetActive(true);
			}
			else{
				temp.x += shiftAmount;
				tabClosedActive.SetActive(false);
				tabNormal.SetActive(true);
			}
			
			controls.GetComponent<RectTransform>().position = temp;
			open = !open;
		}
		
		if(Input.GetKey("tab"))
		{
			if(open){
				tabNormal.SetActive(false);
				tabActive.SetActive(true);
			}
			else{
				tabClosedNormal.SetActive(false);
				tabClosedActive.SetActive(true);
			}
		}
		
		// Rotation indicator controls.
		if (Input.GetKey("w"))
			wActive.SetActive(true);
		else
			wActive.SetActive(false);
		
		if (Input.GetKey("s"))
			sActive.SetActive(true);
		else
			sActive.SetActive(false);
		
		if (Input.GetKey("a"))
			aActive.SetActive(true);
		else
			aActive.SetActive(false);
		
		if (Input.GetKey("d"))
			dActive.SetActive(true);
		else
			dActive.SetActive(false);
		
		if (Input.GetKey("q"))
			qActive.SetActive(true);
		else
			qActive.SetActive(false);
		
		if (Input.GetKey("e"))
			eActive.SetActive(true);
		else
			eActive.SetActive(false);
		
		*/
    }
}
