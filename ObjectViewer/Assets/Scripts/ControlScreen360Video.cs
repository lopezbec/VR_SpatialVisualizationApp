using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScreen360Video : MonoBehaviour
{
	public Transform cam, screen;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        screen.LookAt(cam);
    }
}
