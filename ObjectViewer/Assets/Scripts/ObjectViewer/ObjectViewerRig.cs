using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectViewerRig : MonoBehaviour
{
    public Transform t;
    public float rotationDelta = 3, movementDelta = 0.1f;
	public Vector3 defaultPosition = new Vector3(0, 0, 0);
	
	public bool translation = false, // Set this to true and the user will be able to translate the object with the arrow keys.
				singleRotationMode = false;	// Set this to true and the object will only rotate by "rotationDelta" many degress for every one keypress.

    private Vector3 position = new Vector3();
	
	private Vector3 target = new Vector3(5.0f, 0.0f, 0.0f);
	
	private string[] inputs = {"w", "s", "a", "d", "q", "e"};
	private Vector3[] rotations = new Vector3[6], quaterRotations = new Vector3[6];
	private bool objectHasRotated = false;
	
	void Start(){
		reset();
		
		rotations[0].Set(rotationDelta, 0, 0);
		rotations[1].Set(-rotationDelta, 0, 0);
		rotations[2].Set(0, rotationDelta, 0);
		rotations[3].Set(0, -rotationDelta, 0);
		rotations[4].Set(0, 0, rotationDelta);
		rotations[5].Set(0, 0, -rotationDelta);
		
		quaterRotations[0].Set(90f, 0, 0);
		quaterRotations[1].Set(-90f, 0, 0);
		quaterRotations[2].Set(0, 90f, 0);
		quaterRotations[3].Set(0, -90f, 0);
		quaterRotations[4].Set(0, 0, 90f);
		quaterRotations[5].Set(0, 0, -90f);
	}

    void FixedUpdate()
    {		
		
		// Rotation controls.
		if(!singleRotationMode)
			for(int i = 0; i < inputs.Length; i++){
				if(Input.GetKey(inputs[i])) {
					if(!Input.GetKey(KeyCode.Space))
						t.Rotate(rotations[i], Space.World);
					else if(!objectHasRotated)
						t.Rotate(quaterRotations[i], Space.World);
					
					objectHasRotated = true;
				}
			}
			
		else
			for(int i = 0; i < inputs.Length; i++){
				if(Input.GetKey(inputs[i]) && !objectHasRotated) {
					t.Rotate(rotations[i], Space.World);
					
					objectHasRotated = true;
				}
			}
			
		
		// Movement controls
		if(translation){
			if(Input.GetKey(KeyCode.UpArrow)){
				if(Input.GetKey(KeyCode.Space))
					position.y += movementDelta;
				else 
					position.z += movementDelta;
			}
			else if(Input.GetKey(KeyCode.DownArrow)){
				if(Input.GetKey(KeyCode.Space))
					position.y -= movementDelta;
				else 
					position.z -= movementDelta;
			}
			if(Input.GetKey(KeyCode.LeftArrow))
				position.x -= movementDelta;
			if(Input.GetKey(KeyCode.RightArrow))
				position.x += movementDelta;
		}
		
		t.localPosition = position;
    }
	
	void Update(){
		if(Input.GetKeyUp("l")) // Used for debugging.
			Debug.Log(t.rotation.ToString());
		
		for(int i = 0; i < inputs.Length; i++)
			if(Input.GetKeyUp(inputs[i]))
				objectHasRotated = false;
		
		if (Input.GetKeyUp("r")) // Reset
			reset();
	}
	
	public void reset()
	{
		t.eulerAngles = new Vector3(0, 0, 0);
		position.Set(defaultPosition.x, defaultPosition.y, defaultPosition.z);
	}
}
