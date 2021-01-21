using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
	public GameObject[] wedges;
	public GameObject center;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private int delay = 0;

    // Update is called once per frame
    void Update()
    {
        if(delay == 15){
			delay++;
			
			bool anyWedgesActive = false;
			
			for(int i = 0; i < 12; i++)
				if(wedges[i].activeInHierarchy){
					anyWedgesActive	= true;
				}
				
			center.SetActive(anyWedgesActive);
		}
		else if (delay < 15){
			delay++;
		}
		
    }
}
