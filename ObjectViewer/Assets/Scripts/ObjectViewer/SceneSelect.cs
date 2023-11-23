using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
	public void PlayCopyRotationAnimation(){
		SceneManager.LoadScene("CopyRotationAnimation");
	}
	
	public void PlayCopyRotationAsTo(){
		SceneManager.LoadScene("CopyRotationAsTo");
	}

	public void PlayCopyRotationImage(){
		SceneManager.LoadScene("CopyRotationImage");
	}
	
	public void PlayCopyRotationImageHard(){
		SceneManager.LoadScene("CopyRotationImageHard");
	}
	
	public void PlayMultipleChoice2Dto3D(){
		SceneManager.LoadScene("MultipleChoice2Dto3D");
	}
	
	public void PlayMultipleChoice3Dto2D(){
		SceneManager.LoadScene("MultipleChoice3Dto2D");
	}
	
	public void PlayCopyRotationAnimationEasy(){
		SceneManager.LoadScene("CopyRotationAnimationEasy");
	}

	public void recordButton(string name)
    {
		GameObject collect = GameObject.Find("CollectData");
		if(collect != null)
        {
			CollectData data = collect.GetComponent<CollectData>() as CollectData;
			data.newButton(name);
        }
    }
}
