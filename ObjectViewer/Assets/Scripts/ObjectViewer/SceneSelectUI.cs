using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSelectUI : MonoBehaviour
{
	public GameObject closedNormal, closedActive, openNormal, openActive;
	
	public GameObject sceneSelect;
	
	public bool open = false;
	private float shiftAmount = 170f;
	
    void Update()
    {
		
		if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) // Hide or reveal the scene select UI if shift is released.
		{
			Vector3 temp = sceneSelect.GetComponent<RectTransform>().position;
			
			if(open){
				temp.x += shiftAmount;
				openActive.SetActive(false);
				closedNormal.SetActive(true);
			}
			else{
				temp.x -= shiftAmount;
				closedActive.SetActive(false);
				openNormal.SetActive(true);
			}
			
			sceneSelect.GetComponent<RectTransform>().position = temp;
			open = !open;
			
		}
		
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			if(open){
				openNormal.SetActive(false);
				openActive.SetActive(true);
			}
			else{
				closedNormal.SetActive(false);
				closedActive.SetActive(true);
			}
		}
        
    }
}
