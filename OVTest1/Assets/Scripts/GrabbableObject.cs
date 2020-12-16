using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
  	public Transform rightHand, leftHand, userObject;
	
	private int rightPressedTime, leftPressedTime;
	
	private Vector3 initialPosition;
	private Quaternion initialRotation;
	
    // Start is called before the first frame update
    void Start() {
		initialRotation = userObject.rotation; // Save the initial position of the object so it can be reset later.
		initialPosition = userObject.position;
		
		rightPressedTime = 0;
		leftPressedTime = 0;
    }

    // Update is called once per frame
    void Update(){
		
		if(rightPressedTime++ > 4){ // If the user does not currently have their right hand in the "pressed" position.
			rightPressedTime = 5;
			
			rightHand.DetachChildren();
		}
		else { // If the user does have their right hand in the pressed position.
			userObject.SetParent(rightHand);
		}
		
		if(leftPressedTime++ > 4){ // If the user does not currently have their right hand in the "pressed" position.
			leftPressedTime = 5;
			
			leftHand.DetachChildren();
		}
		else { // If the user does have their right hand in the pressed position.
			userObject.SetParent(leftHand);
		}

    }
	
	public void setRightPressed(){
		if(Vector3.Distance(rightHand.position, userObject.position) < 0.2f)
			rightPressedTime = 0;
	}
	
	public void setLeftPressed(){
		if(Vector3.Distance(leftHand.position, userObject.position) < 0.2f)
			leftPressedTime = 0;
	}
	
	public void resetPosition(){
		//userObject.position = reset.position;
		//userObject.rotation = reset.rotation;
		userObject.position = initialPosition;
		userObject.rotation = initialRotation;
	}
}
