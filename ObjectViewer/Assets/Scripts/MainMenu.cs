using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static string name = "";

    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("PlayerName"));

        if (name.Equals(""))
        {
            GameObject collect = GameObject.Find("CollectData");
            if (collect != null)
            {
                CollectData data = collect.GetComponent<CollectData>() as CollectData;
                name = data.playerName;
            }
        }
        PlayerPrefs.SetString("NextScene"+name, "");
        
         //PlayerPrefs.SetInt("RanIntro" + name, 0);
        // Debug.Log(PlayerPrefs.GetInt("RanIntro" + name));
    }

    public void PlayObjectViewer()
    {
        SceneManager.LoadScene("OVFreeView"); // Load OVExplanation scene.
    }

    public void PlayCuttingPlane()
    {
        PlayerPrefs.SetString("NextScene"+name, "CuttingPlane");
        PlayObjectViewer();
    }

    public void PlayOrthoViewer()
    {
        PlayerPrefs.SetString("NextScene"+name, "OrthoFreeView");
        PlayObjectViewer();
    }

    public void PlayObjectCombinator()
    {
        PlayerPrefs.SetString("NextScene"+name, "ObjectCombinator");
        PlayObjectViewer();
    }


    public void ExitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}