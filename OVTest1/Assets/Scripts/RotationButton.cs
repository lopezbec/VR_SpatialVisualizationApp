using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class RotationButton : MonoBehaviour
{
	public UnityEvent enter, exit;
	public Transform tLeftHand, tRightHand, tCube;
	public float cubeSize = 0.1f;
	
	private bool handInButton = false;
	
	private Renderer m_renderer;
	
    // Start is called before the first frame update
    void Start()
    {
		m_renderer = GetComponent<Renderer>();
		tCube.localScale = new Vector3(2 * cubeSize, 2 * cubeSize, 2 * cubeSize);
        
    }

    // Update is called once per frame
    void Update()
    {
		if( (tLeftHand.position.x > tCube.position.x - cubeSize && 
		       tLeftHand.position.x < tCube.position.x + cubeSize &&
			   tLeftHand.position.y > tCube.position.y - cubeSize && 
		       tLeftHand.position.y < tCube.position.y + cubeSize &&
		       tLeftHand.position.z > tCube.position.z - cubeSize && 
		       tLeftHand.position.z < tCube.position.z + cubeSize) || 
		   	   (tRightHand.position.x > tCube.position.x - cubeSize && 
		       tRightHand.position.x < tCube.position.x + cubeSize &&
		       tRightHand.position.y > tCube.position.y - cubeSize && 
		       tRightHand.position.y < tCube.position.y + cubeSize &&
		       tRightHand.position.z > tCube.position.z - cubeSize && 
		       tRightHand.position.z < tCube.position.z + cubeSize) )
		{
			m_renderer.material.color = Color.red;
			
			if(!handInButton){
				enter.Invoke();
				handInButton = true;
			}
		}
		else{
			m_renderer.material.color = Color.white;
			
			if(handInButton){
				exit.Invoke();
				handInButton = false;
			}
		}
    }

}
