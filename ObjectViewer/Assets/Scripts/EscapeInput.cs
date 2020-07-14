using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeInput : MonoBehaviour
{
	public string scene;
	
    void Update()
    {
        if (Input.GetKey("escape"))
			SceneManager.LoadScene(scene);
    }
}
