using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationDetector : MonoBehaviour
{
	public GameObject detector;
	public Transform cameraPosition, objects;
	public float size = 0.001f;
	public int currentCube = 0;
	
	private Vector3 position = new Vector3(), scale = new Vector3();
	
	private string[] collisionCubeNames = {"0", "1", "2", "3", "4", "5", "6", "7"};
	
	
    void Update()
    {
		// Set the position of the detector to be between the user and the filling.
		position.x = ((cameraPosition.position.x) + (objects.position.x)) / 2;
		position.y = ((cameraPosition.position.y) + (objects.position.y)) / 2;
		position.z = ((cameraPosition.position.z) + (objects.position.z)) / 2;
		detector.transform.position = position;
		
		// Set the scale of the detector. 
		scale.x = size;
		scale.y = size;
		scale.z = 0.9f * Vector3.Distance(cameraPosition.position, objects.position);
			
		detector.transform.localScale = scale;
		
		detector.transform.LookAt(cameraPosition); // Make sure the detector is pointing at the camera.
    }
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "OrientationCube"){
			
			for(int i = 0; i < 8; i++)
				if(col.gameObject.name == collisionCubeNames[i])
					currentCube = i;
		}
	}
}
