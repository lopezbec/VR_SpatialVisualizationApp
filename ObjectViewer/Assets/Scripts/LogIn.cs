using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogIn : MonoBehaviour
{
    public InputField userName;
    public CollectData user;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void submit()
    {
        user.playerName = userName.text;
        SceneManager.LoadScene("Menu");
    }
}
