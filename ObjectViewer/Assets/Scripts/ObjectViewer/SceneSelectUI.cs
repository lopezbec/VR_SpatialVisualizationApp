using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSelectUI : MonoBehaviour
{
	
	public GameObject sceneSelectObject, labelObject;
	
	public bool open = false;
	private float shiftAmount, activeRange = 0.8f, closedX, openX, movementDelta, rotationDelta = 4f;
	private RectTransform buttons, label;
	
	void Start() {
		
		shiftAmount = Screen.width * 0.17f;
		movementDelta = shiftAmount * 0.05f;
		
		
		buttons = sceneSelectObject.GetComponent<RectTransform>();
		label = labelObject.GetComponent<RectTransform>();
		
		closedX = buttons.position.x;
		openX = closedX - shiftAmount;
		
	}
	
    void FixedUpdate()
    {
		Vector3 pos = buttons.position, rot = label.eulerAngles;
		
		if(Input.mousePosition.x > activeRange * Screen.width) {
			
			if(pos.x >= openX)
				pos.x -= movementDelta;
			
			if(rot.z > 90)
				rot.z -= rotationDelta;
			if(rot.z < 90)
				rot.z = 90;
		}
		else {
			if(pos.x <= closedX)
				pos.x += movementDelta;
			
			if(rot.z < 180)
				rot.z += rotationDelta;
			if(rot.z > 180)
				rot.z = 180;
		}
		
		label.eulerAngles = rot;
		buttons.position = pos;
    }
}
