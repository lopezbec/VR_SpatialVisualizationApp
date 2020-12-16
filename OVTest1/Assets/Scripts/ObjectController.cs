using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
	private Renderer m_renderer;
	private bool copyRotation;
	public Transform tRightHand, tObject, tRotationContainer;
	private Quaternion reset = new Quaternion();
	
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
		copyRotation = false;
		reset.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
		if(copyRotation){
			//tObject.Rotate(tRightHand.eulerAngles.x, tRightHand.eulerAngles.x, tRightHand.eulerAngles.x, Space.World);
			tRotationContainer.rotation = tRightHand.rotation;
			//gameObject.transform.eulerAngles.x = rightHand.eulerAngles.x;
			//gameObject.transform.eulerAngles.y = rightHand.eulerAngles.y;
			//gameObject.transform.eulerAngles.z = rightHand.eulerAngles.z;
		}
		
    }
	
	public void CopyHandRotation(){
		//m_renderer.material.color = Color.green;
		
		Quaternion temp = tObject.rotation;	
		tRotationContainer.rotation = tRightHand.rotation;
		tObject.rotation = temp;	
		
		copyRotation = true;
	}
	
	public void StopCopyHandRotation(){
		//m_renderer.material.color = Color.blue;
				
		//tRotationContainer.rotation = tObject.rotation;
		//tObject.localRotation = reset;
		
		copyRotation = false;
	}
	
		
	public void rotateX(float x){
		tObject.Rotate(x, 0, 0, Space.World);
	}
	
	public void rotateY(float y){
		tObject.Rotate(0, y, 0, Space.World);
	}
	
	public void rotateZ(float z){
		tObject.Rotate(0, 0, z, Space.World);
	}
	
}
