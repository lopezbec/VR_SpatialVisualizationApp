using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityDetectorControl : MonoBehaviour
{
	public GameObject detector;
	public GameObject filling;
	public GameObject user;
	public float size = 0.001f;
	
	
	public MeshCollider col = null;
	
	private Vector3 position = new Vector3(), scale = new Vector3();
	public int enters = 0;
	
    // Update is called once per frame
    void Update()
    {
		// Set the position of the detector to be between the user and the filling.
		position.x = ((user.transform.position.x) + (filling.transform.position.x)) / 2;
		position.y = ((user.transform.position.y) + (filling.transform.position.y)) / 2;
		position.z = ((user.transform.position.z) + (filling.transform.position.z)) / 2;
		detector.transform.position = position;
		
		// Set the scale of the detector. 
		scale.x = size;
		scale.y = size;
		scale.z = 0.9f * Vector3.Distance(user.transform.position, filling.transform.position);
			
		detector.transform.localScale = scale;
		
		detector.transform.LookAt(user.transform); // Make sure the detector is pointing at the user.

		if(enters == 0)
			filling.GetComponent<Renderer>().enabled = true; // Show the filling if there is nothing obstucting it and the position of "user".
		else
			filling.GetComponent<Renderer>().enabled = false; // Hide the filling otherwise.

    }
	
	void OnEnable()
    {
        enters = 0; // This prevents the enable from growing larger as the object is disabled and enabled.
    }

	void OnTriggerEnter(Collider col)
	{
		//Debug.Log("Entered!");
		if(col.gameObject.tag == "Object")
			enters++;
	}
	
	void OnTriggerExit(Collider col)
	{
		//Debug.Log("Exited!");
		if(col.gameObject.tag == "Object")
			enters--;
	}
}
















