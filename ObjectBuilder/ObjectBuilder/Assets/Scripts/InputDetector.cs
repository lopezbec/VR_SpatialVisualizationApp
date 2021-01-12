using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputDetector : MonoBehaviour
{
	public UnityEvent calculate;
	public Transform camera;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
		if(Input.GetKeyDown("c")){
			calculate.Invoke();
		}
        
		/* Camera controls */
		camera.LookAt(Vector3.zero);
		if(Input.GetKey("a"))
			camera.localPosition = camera.localPosition + -0.07f * camera.right;
		else if(Input.GetKey("d"))
			camera.localPosition = camera.localPosition + 0.07f * camera.right;
		
		if(Input.GetKey("w"))
			camera.localPosition = camera.localPosition + 0.07f * camera.up;
		else if(Input.GetKey("s"))
			camera.localPosition = camera.localPosition + -0.07f * camera.up;
	
		if(Input.GetKey("q") && camera.localPosition.z < -0.5f)
			camera.localPosition = camera.localPosition + 0.07f * camera.forward;
		else if(Input.GetKey("e"))
			camera.localPosition = camera.localPosition + -0.07f * camera.forward;
	
    }
}
