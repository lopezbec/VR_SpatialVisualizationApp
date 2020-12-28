using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenLineCameraManager : MonoBehaviour
{
	public Camera dottedLines, solidLines, rightDotted, rightSolid;
	public bool perspective = false;
	public float frequency = 0.2f;
	
	
	private int count, period;
	private bool objectPositionChanged = true;
	
    // Start is called before the first frame update
    void Start()
    {
        dottedLines.enabled = false;
		solidLines.enabled = false;
		rightDotted.enabled = false;
		rightSolid.enabled = false;
		period = (int)(60f/frequency);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(perspective){
			if(count++ > period){
				count = 0;
				if(objectPositionChanged){
					objectPositionChanged = false;
					dottedLines.Render();
					solidLines.Render();
				}
			}
		}
		else {
			if(count++ > period){
				count = 0;
				if(objectPositionChanged){
					objectPositionChanged = false;
					rightDotted.Render();
					rightSolid.Render();
				}
			}
		}        
    }
	
	public void ObjectPositionChanged(){
		objectPositionChanged = true;
	}
}
