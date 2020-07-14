using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoViewManager : MonoBehaviour
{
	public RenderTexture cameraTexture;
	public RenderTexture[] views;
	public Transform cameraHolder;
	public Transform objectManager; // Note this should be the othroView object manager, not the one the user is directly looking at.
	
	private int offset = 15, state = 0, delay = 0;
	private Vector3[] positions = new Vector3[3]; 
	
    void Start()
    {
		positions[0].Set(0,0, -offset);
		positions[1].Set(offset, 0, 0);
		positions[2].Set(0, offset, 0);
    }

    void FixedUpdate()
    {
		if(delay >= 7){
			updateViews();
			delay = 0;
		}
		else
			delay++;
    }
	
	private void updateViews(){
		Graphics.CopyTexture(cameraTexture, views[state]);
		
		state++;
		if(state >= 3) state = 0;
		
		cameraHolder.localPosition = positions[state];
		cameraHolder.LookAt(objectManager);
	}
}
