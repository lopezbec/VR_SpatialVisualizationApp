using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class SceneChanger : MonoBehaviour
{
    // This method will be called when the button is pressed
    public void ChangeScene(string sceneName)
    {
        // Fully unload the current scene and load the new one
        SceneManager.LoadScene(sceneName);
    }

    // Optional: This method loads the scene asynchronously for smoother transitions
    public void ChangeSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Start loading the scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // While the scene is loading, you can show a loading screen or progress bar
        while (!asyncLoad.isDone)
        {
            // Optional: Add loading feedback here
            yield return null;
        }
    }
}