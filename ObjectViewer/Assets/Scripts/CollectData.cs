using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class CollectData : MonoBehaviour
{
    int sessionNum = -1;
    string filepath;
    public string currentScene;
    public string playerName;
    public List<PlayerAction> actions;
    
    // Start is called before the first frame update
    void Start()
    {
        actions = new List<PlayerAction>();
        DontDestroyOnLoad(this);
        currentScene = "Login";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && sessionNum > 0)
        {
            Vector3 mp = Input.mousePosition;
            actions.Add(new Click(mp, sessionNum));
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpdateFile();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        string sceneName = SceneManager.GetActiveScene().name;
        if (currentScene != sceneName)
        {
            actions.Add(new SceneChange(currentScene, sceneName, sessionNum));
            currentScene = sceneName;
        }
    }

    public void loadFile(string name) //is called when player presses submit on login screen, so no data is taken before this point
    {
        Debug.Log("loading");
        playerName = name;
        filepath = Path.Combine(Application.persistentDataPath, name);
        //checks if the file exists, if so checks which session number it is.
        if (File.Exists(filepath))
        {
            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    string str = reader.ReadLine();
                    while (!string.IsNullOrEmpty(str))
                    {
                        sessionNum = int.Parse(str.Split(' ')[0]) + 1;
                        Debug.Log(str[0]);
                        str = reader.ReadLine();
                    }

                }
                Debug.Log("Reading File Successful");
            }
            catch (Exception E)
            {
                Debug.Log("Failed to Read File");
            }
        }
        else sessionNum = 1;
    }

    public void UpdateFile() //takes current object data and overwrites file
    {
        Debug.Log("updating");

        try
        {
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {//makes a writer that adds onto file instead of overwriting it.)
                foreach(PlayerAction act in actions)
                {
                    writer.WriteLine(PlayerActionToString(act));
                }
                
            }
            Debug.Log("Writing to File Successful");
        }
        catch(Exception E)
        {
            Debug.Log("Failed to Write to File");
        }
    }

    public string PlayerActionToString(PlayerAction action)
    {
        string str = "";
        str += sessionNum;
        str += " " + action.year;
        str += " " + action.month;
        str += " " + action.day;
        str += " " + action.hour;
        str += " " + action.minute;
        str += " " + action.second;
        if (action is Login)
        {
            str += " Login";
        }
        else if (action is Click)
        {
            Click act = action as Click;
            str += " Click";
            str += " " + act.mouseLocation.x;
            str += " " + act.mouseLocation.y;
            str += " " + act.mouseLocation.z;
        }
        else if (action is SceneChange)
        {
            SceneChange act = action as SceneChange;
            str += " SceneChange";
            str += " " + act.oldSceneName;
            str += " " + act.newSceneName;
        }
        else if (action is ChallengeSubmission)
        {
            ChallengeSubmission act = action as ChallengeSubmission;
            str += " ChallengeSubmission";
            str += " " + act.challengeName;
            str += " " + act.success;
            str += " " + act.currentChallengeNum;
            str += " " + act.totalNumChallenges;
        }
        else if (action is ButtonPress)
        {
            ButtonPress act = action as ButtonPress;
            str += " ButtonPress";
            str += " " + act.buttonName;
        }
        return str;
    }

    public PlayerAction lineToAction(String fileLine)
    {
        string[] line = fileLine.Split(' ');
        PlayerAction action;
        if (line[7].Equals("Login"))
        {
            action = new Login(int.Parse(line[0]));
        }
        else if (line[7].Equals("Click"))
        {
            Vector3 click = new Vector3(int.Parse(line[8]), int.Parse(line[9]), int.Parse(line[10]));
            action = new Click(click, int.Parse(line[0]));
        }
        else if (line[7].Equals("SceneChange"))
        {
            action = new SceneChange(line[8], line[9], int.Parse(line[0]));
        }
        else if (line[7].Equals("ChallengeSubmission"))
        {
            action = new ChallengeSubmission(line[8], bool.Parse(line[9]), int.Parse(line[10]), int.Parse(line[11]), int.Parse(line[0]));
        }
        else //else if (line[7].Equals("ButtonPress"))
        {
            action = new ButtonPress(line[8], int.Parse(line[0]));
        }
        action.year = int.Parse(line[1]);
        action.month = int.Parse(line[2]);
        action.day = int.Parse(line[3]);
        action.hour = int.Parse(line[4]);
        action.minute = int.Parse(line[5]);
        action.second = int.Parse(line[6]);
        return action;
    }

    public void newLogin()
    {
        actions.Add(new Login(sessionNum));
    }

    public void newButton(string name)
    {
        actions.Add(new ButtonPress(name, sessionNum));
    }

    public class PlayerAction
    {
        public int year;
        public int month;
        public int day;
        public int hour;
        public int minute;
        public int second;
        public int session;

        public PlayerAction()
        {
            System.DateTime time = System.DateTime.Now;
            year = time.Year;
            month = time.Month;
            day = time.Day;
            hour = time.Hour;
            minute = time.Minute;
            second = time.Second;
        }
        //stores the time of the action
    }

    public class Login : PlayerAction
    {
        public Login(int sessionNum)
        {
            System.DateTime time = System.DateTime.Now;
            year = time.Year;
            month = time.Month;
            day = time.Day;
            hour = time.Hour;
            minute = time.Minute;
            second = time.Second;
            session = sessionNum;
        }
    }

    public class Click : PlayerAction
    {
        public Vector3 mouseLocation;
        public Click(Vector3 location, int sessionNum)
        {
            System.DateTime time = System.DateTime.Now;
            year = time.Year;
            month = time.Month;
            day = time.Day;
            hour = time.Hour;
            minute = time.Minute;
            second = time.Second;
            mouseLocation = location;
            session = sessionNum;
        }
    }

    public class ButtonPress : PlayerAction
    {
        public string buttonName;
        public ButtonPress(string button, int sessionNum)
        {
            System.DateTime time = System.DateTime.Now;
            year = time.Year;
            month = time.Month;
            day = time.Day;
            hour = time.Hour;
            minute = time.Minute;
            second = time.Second;
            buttonName = button;
            session = sessionNum;
        }
    }

    public class SceneChange : PlayerAction
    {
        public string oldSceneName;
        public string newSceneName;
        public SceneChange(string oldS, string newS, int sessionNum)
        {
            System.DateTime time = System.DateTime.Now;
            year = time.Year;
            month = time.Month;
            day = time.Day;
            hour = time.Hour;
            minute = time.Minute;
            second = time.Second;
            oldSceneName = oldS;
            newSceneName = newS;
            session = sessionNum;
        }
    }

    public class ChallengeSubmission : PlayerAction
    {
        public string challengeName;
        public bool success;
        public int currentChallengeNum;
        public int totalNumChallenges;
        public ChallengeSubmission(string name, bool correct, int chalNumber, int totalNum, int sessionNum)
        {
            System.DateTime time = System.DateTime.Now;
            year = time.Year;
            month = time.Month;
            day = time.Day;
            hour = time.Hour;
            minute = time.Minute;
            second = time.Second;
            challengeName = name;
            success = correct;
            currentChallengeNum = chalNumber;
            totalNumChallenges = totalNum;
            session = sessionNum;
        }
    }
}
