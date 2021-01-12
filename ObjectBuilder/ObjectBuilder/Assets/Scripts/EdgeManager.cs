using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeManager : MonoBehaviour
{
	public GameObject edge;
	
	public bool firstCollider = true;
	
	public bool active = true;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        edge.SetActive(active);
    }
	
	
	void OnTriggerEnter(Collider collision_info) 
	{
		if(!firstCollider){
			//edge.GetComponent<MeshRenderer>().enabled = false;
			//Debug.Log("Detected!");
			
			active = false;
			//edge.SetActive(false);
		}
		else{
			firstCollider = false;
		}
	}
	
}
