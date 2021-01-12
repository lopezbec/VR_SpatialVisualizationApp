using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter(Collider col) 
	{
		//edge.GetComponent<MeshRenderer>().enabled = false;
		
		Debug.Log("Detected!");
		//edge.SetActive(false);
	}
}
