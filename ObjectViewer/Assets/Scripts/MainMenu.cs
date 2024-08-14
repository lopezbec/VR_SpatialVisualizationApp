using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
		Debug.Log(PlayerPrefs.GetString("PlayerName"));
	}
    public void PlayObjectViewer()
	{
		SceneManager.LoadScene("OVFreeView"); // Load OVExplanation scene.
	}
	
	public void PlayCuttingPlane()
	{
		SceneManager.LoadScene("CuttingPlane");
	}
	
	public void PlayOrthoViewer()
	{
		SceneManager.LoadScene("OrthoFreeView");
	}
    
    public void PlayObjectCombinator()
    	{
    		SceneManager.LoadScene("ObjectCombinator");
    	}
	
	public void ExitGame()
	{
		Debug.Log("QUIT!");
		Application.Quit();
	}
}
