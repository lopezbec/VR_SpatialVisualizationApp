using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelectOrtho : MonoBehaviour
{
	public void PlayOrtho2Dto3D(){
		SceneManager.LoadScene("Ortho2Dto3D");
	}
	
	public void PlayOrtho3Dto2D(){
		SceneManager.LoadScene("Ortho3Dto2D");
	}
	
	public void PlayOrthoMatch(){
		SceneManager.LoadScene("MatchObjectsToDrawings");
	}
}
