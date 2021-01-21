using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
	public GameObject[] centers;
	public Transform position;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private int delay = 0;

    // Update is called once per frame
    void Update()
    {
     
        if(delay == 30){
			delay++;
			
			for(int i = 0; i < 27; i++){
				
				if(centers[i].activeInHierarchy)
					position.position += centers[i].GetComponent<Transform>().position;
				
			}
			
			//gameObject.GetComponent<Transform>();
			
		}
		else if (delay < 30){
			delay++;
		}
	 
    }
}
