using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlsUI : MonoBehaviour
{
	public GameObject wActive, sActive, aActive, dActive, qActive, eActive;
	public GameObject tabActive, tabNormal, tabClosedActive, tabClosedNormal;
	
	public GameObject controls;
	
	public bool open = true;
	
	private float shiftAmount = 270f;
	
    void Update()
    {
	
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
    }
}
