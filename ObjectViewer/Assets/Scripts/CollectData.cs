using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]

public class CollectData : MonoBehaviour
{
    public string playerName;
    string filepath;
    // Start is called before the first frame update
    void Start()
    {
        actions = new List<PlayerAction>();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            actions.Add(new Click(Input.mousePosition));
        }
    }

    public void loadFile(string name)
    {
        playerName = name;
        filepath = Path.Combine(Application.persistentDataPath, name);
        //if file already exists then I must copy all data from that file into current actions
    }

    public void UpdateFile()
    {

        //takes current object data and overwrites file
    }

    class PlayerData
    {
        List<PlayerAction> actions;
        
        public PlayerData()
        {
            actions = new List<PlayerAction>();
        }
    }

    class PlayerAction
    {
        protected System.DateTime time;
        //stores the time of the action
    }

    class Login : PlayerAction
    {
        public Login()
        {
            this.time = System.DateTime.Now;
        }
    }

    class Click : PlayerAction
    {
        Vector3 mouseLocation;
        public Click(Vector3 location)
        {
            this.time = System.DateTime.Now;
            mouseLocation = location;
        }
    }

    class ButtonPress : PlayerAction
    {
        string buttonName;
        public ButtonPress(string button)
        {
            this.time = System.DateTime.Now;
            buttonName = button;
        }
    }

    class SceneChange : PlayerAction
    {
        string oldSceneName;
        string newSceneName;
        public SceneChange(string oldS, string newS)
        {
            oldSceneName = oldS;
            newSceneName = newS;
        }
    }

    class ChallengeSubmission : PlayerAction
    {
        string challengeName;
        bool success;
        int currentChallengeNum;
        int totalNumChallenges;
        public ChallengeSubmission(string name, bool correct, int chalNumber, int totalNum)
        {
            challengeName = name;
            success = correct;
            currentChallengeNum = chalNumber;
            totalNumChallenges = totalNum;
        }
    }
}
