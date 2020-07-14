using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRig : MonoBehaviour
{
    private int time = 0;
	
    void Start()
    {
        
    }

	void FixedUpdate(){
		time++;
		
		if(time < 90)
			transform.Rotate(0, 1f, 0, Space.World);
		else if(time > 180 && time < 270)
			transform.Rotate(0, -1f, 0, Space.World);
	}
}
