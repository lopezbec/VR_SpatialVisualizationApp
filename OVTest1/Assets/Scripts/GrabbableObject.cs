using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabbableObject : MonoBehaviour
{
	public bool moveable = true;
  	public Transform rightHand, leftHand, userObject, rotationContainer;
	public UnityEvent positionChanged;
	
	
	private int rightPressedTime, leftPressedTime;
	
	private Vector3 initialPosition, handPosition;
	private Quaternion initialRotation;
	
	private bool leftPressed = false, rightPressed = false;
	
    // Start is called before the first frame update
    void Start() {
		initialRotation = userObject.rotation; // Save the initial position of the object so it can be reset later.
		initialPosition = userObject.position;
		
		rightPressedTime = 0;
		leftPressedTime = 0;
    }

    // Update is called once per frame
    void Update(){
		
		if(moveable){
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
		else {
			if(rightPressedTime++ > 4){ // If the user does not currently have their right hand in the "pressed" position.
				rightPressedTime = 5;
				
				//Quaternion temp = userObject.rotation;	
				//rotationContainer.rotation = tRightHand.rotation;
				//tObject.rotation = temp;	
				
				rightPressed = false;
				
				rightHand.DetachChildren();
			}
			else { // If the user does have their right hand in the pressed position.
				
				if(!rightPressed){ // If this is the first call of update since the user pressed their right hand.
					rightPressed = true;
				
					//handPosition = rightHand.position;
				
					//Quaternion temp = userObject.rotation;
					//rotationContainer.LookAt(rightHand);
					//userObject.rotation = temp;
				}
				
				//rotationContainer.LookAt(rightHand, Vector3.up);
				
				userObject.SetParent(rightHand);
				userObject.position = initialPosition;
			}
			
			if(leftPressedTime++ > 4){ // If the user does not currently have their right hand in the "pressed" position.
				leftPressedTime = 5;
				
				leftHand.DetachChildren();
			}
			else { // If the user does have their right hand in the pressed position.
	
				//Vector3 relativePos =  leftHand.position - userObject.position;
				
				//Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0,1,0));
				//userObject.rotation = rotation*Quaternion.Euler(0,90,0);
			
				//var lookDir = leftHand.position - userObject.position;
				//lookDir.y = 0f; //this is the critical part, this makes the look direction perpendicular to 'up'
				//userObject.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
				//userObject.position += userObject.forward * 1.5f * Time.deltaTime;
			
				//rotationContainer.LookAt(leftHand, oldUp);
				//oldUp = userObject.up;
				
				userObject.SetParent(leftHand);
				userObject.position = initialPosition;
			}
		}

    }
	
	public void setRightPressed(){
		if(Vector3.Distance(rightHand.position, userObject.position) < 0.3f){
			rightPressedTime = 0;
			positionChanged.Invoke();
		}
	}
	
	public void setLeftPressed(){
		if(Vector3.Distance(leftHand.position, userObject.position) < 0.3f){
			leftPressedTime = 0;
			positionChanged.Invoke();
		}
	}
	
	public void resetPosition(){
		userObject.position = initialPosition;
		userObject.rotation = initialRotation;
	}
}
