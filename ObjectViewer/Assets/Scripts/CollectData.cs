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
    public List<KeyCode> keysPressed;
    float oldRotationX = 0;
    float oldRotationY = 0;
    float oldRotationZ = 0;
    //float oldRotationW = 0;
    bool rotating = false;
    // Start is called before the first frame update
    StreamWriter writer;
    int pixelsWidth;
    int pixelsHeight;
   
    void Start()
    {
        actions = new List<PlayerAction>();
        keysPressed = new List<KeyCode>();
        DontDestroyOnLoad(this);
        currentScene = "Login";
        pixelsWidth = Screen.width;
        pixelsHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && sessionNum > 0) //when the mouse button is clicked, record it
        {
            Vector3 mp = Input.mousePosition;
            actions.Add(new Click(mp, sessionNum));
        }

        if(actions.Count != 0)
        {
            UpdateFile();
        }


        if (Input.GetKeyDown(KeyCode.U)) //updates the file when the U key is pressed
        {
            UpdateFile();
            Debug.Log("Writing to File Complete");
        }
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }*/
        string sceneName = SceneManager.GetActiveScene().name; //acquires current scene name
        if (currentScene != sceneName) //if the scene name has changed, record a scene change
        {
            actions.Add(new SceneChange(currentScene, sceneName, sessionNum));
            currentScene = sceneName;
            //if the scene that we changed to was one with an object in it, reset the object's rotation value
            if(sceneName.Equals("OVFreeView") || sceneName.Equals("CopyRotationAnimation") || sceneName.Equals("CopyRotationAnimationEasy") || sceneName.Equals("CopyRotationAnimationAsTo") || sceneName.Equals("CopyRotationImage") || sceneName.Equals("CopyRotationImageHard"))
            {
                resetRotations();
            }
        }

        if(Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2)) //if any key is pressed
        {
            foreach(KeyCode code in System.Enum.GetValues(typeof(KeyCode))) 
            {
                if (Input.GetKeyDown(code)) //determine which key was just pressed down
                {
                    keysPressed.Add(code); //add the key to a list of keys that were pressed down
                    actions.Add(new keyPress(code.ToString(), sessionNum)); //record that a key was pressed down
                    //if a rotation key was pressed, it has not been previously pressed yet, and it is a scene where rotations can be made
                    if (sceneName.Equals("OVFreeView") || sceneName.Equals("CopyRotationAnimation") || sceneName.Equals("CopyRotationAnimationEasy") || sceneName.Equals("CopyRotationAnimationAsTo") || sceneName.Equals("CopyRotationImage") || sceneName.Equals("CopyRotationImageHard"))
                    {
                        if (code == KeyCode.W || code == KeyCode.S || code == KeyCode.Q || code == KeyCode.E || code == KeyCode.A || code == KeyCode.D)
                        {
                            rotating = true;
                            Debug.Log("Rotating true");
                        }
                        else if (code == KeyCode.R)
                        {
                            resetRotations();
                        }
                        /*GameObject shape = GameObject.Find("ObjectManager");
                        if (shape != null)
                        {
                            Transform shapeRot = shape.GetComponent<Transform>();
                            Quaternion quatRot = shapeRot.rotation;
                            rotationX = quatRot.x;
                            rotationY = quatRot.y;
                            rotationZ = quatRot.z;
                            rotationW = quatRot.w;
                        }*/
                    }
                }
            }
        }
        List<KeyCode> removeKeys = new List<KeyCode>();
        foreach (KeyCode code in keysPressed) //for each of the keys that were previously pressed down
        {
            if (!Input.GetKey(code)) //if the key is no longer pressed down, record that it was released 
            {
                actions.Add(new keyReleased(code.ToString(), sessionNum));
                removeKeys.Add(code);
                
            }
        }
        foreach (KeyCode code in removeKeys) //remove the keys from keysPressed that were released
        {
            keysPressed.Remove(code);
        }
        removeKeys.Clear();

        if (rotating) Debug.Log("ROTATING STILL TRUE");
        //if an object was rotating and now none of the rotating keys are pressed
        if(rotating && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
        {
            Debug.Log("Rotating False");
            GameObject shape = GameObject.Find("ObjectManager"); //access the object that holds the rotating shape
            if (shape != null)
            {
                ObjectManager obj = shape.GetComponent<ObjectManager>() as ObjectManager;
                /*Transform shapeRot = shape.GetComponent<Transform>();
                Quaternion quatRot = shapeRot.rotation;
                if (oldRotationW != quatRot.w || oldRotationX != quatRot.x || oldRotationY != quatRot.y || oldRotationZ != quatRot.z) //quaternion rotation values
                {
                    actions.Add(new ObjectRotation(obj.objects[obj.active].name, oldRotationX, oldRotationY, oldRotationZ, oldRotationW, quatRot.x, quatRot.y, quatRot.z, quatRot.w, sessionNum));
                    oldRotationX = quatRot.x;
                    oldRotationY = quatRot.y;
                    oldRotationZ = quatRot.z;
                    oldRotationW = quatRot.w;
                }*/
                
                //record the change in rotation
                Vector3 angles = shape.transform.localEulerAngles;
                if (oldRotationX != angles.x || oldRotationY != angles.y || oldRotationZ != angles.z) //euler rotation values
                {
                    /*actions.Add(new ObjectRotation(obj.objects[obj.active].name, oldRotationX, oldRotationY, oldRotationZ, oldRotationW, quatRot.x, quatRot.y, quatRot.z, quatRot.w, sessionNum));*/
                    actions.Add(new ObjectRotation(obj.objects[obj.active].name, oldRotationX, oldRotationY, oldRotationZ, angles.x, angles.y, angles.z, sessionNum));
                    Debug.Log(oldRotationX + " " + oldRotationY + " " + oldRotationZ + " " + angles.x + " " + angles.y + " " + angles.z);
                    oldRotationX = angles.x;
                    oldRotationY = angles.y;
                    oldRotationZ = angles.z;
                }
            }
            rotating = false;
        }
        //if we were rotating a shape and all of those keys are no longer pressed then we can record the rotation change
        /*if (rotationX != 0 && (!keysPressed.Contains(KeyCode.W) && !keysPressed.Contains(KeyCode.S) && !keysPressed.Contains(KeyCode.Q) && !keysPressed.Contains(KeyCode.E) && !keysPressed.Contains(KeyCode.A) && !keysPressed.Contains(KeyCode.D)))
        {
            GameObject shape = GameObject.Find("ObjectManager");
            if (shape != null)
            {
                ObjectManager obj = shape.GetComponent<ObjectManager>() as ObjectManager;
                Transform shapeRot = shape.GetComponent<Transform>();
                Quaternion quatRot = shapeRot.rotation;
                actions.Add(new ObjectRotation(obj.objects[obj.active].name, rotationX, rotationY, rotationZ, rotationW, quatRot.x, quatRot.y, quatRot.z, quatRot.w, sessionNum));
                rotationX = 0;
                rotationY = 0;
                rotationZ = 0;
                rotationW = 0;
            }
        }*/
    }

    public void resetRotations() //resets all of the rotations to the current state of the object
    {
        GameObject shape = GameObject.Find("ObjectManager");
        if (shape != null)
        {
            /*ObjectManager obj = shape.GetComponent<ObjectManager>() as ObjectManager;
            Transform shapeRot = shape.GetComponent<Transform>();
            Quaternion quatRot = shapeRot.rotation;
            oldRotationX = quatRot.x;
            oldRotationY = quatRot.y;
            oldRotationZ = quatRot.z;*/
            //oldRotationW = quatRot.w;
            Vector3 angles = shape.transform.localEulerAngles;
            oldRotationX = angles.x;
            oldRotationY = angles.y;
            oldRotationZ = angles.z;
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
        writer = new StreamWriter(filepath, true);
    }

    public void UpdateFile() //takes current object data and writes to the file
    {
        
        Debug.Log("updating");

        try
        {
            foreach (PlayerAction act in actions)
            {
                String line = PlayerActionToString(act);
                Debug.Log(line);
                writer.WriteLine(line);
            }
            
            Debug.Log("Writing to File Successful");
            actions.Clear();
            writer.Flush();
        }
        catch(Exception E)
        {
            Debug.Log("Failed to Write to File");
        }
    }

    public string PlayerActionToString(PlayerAction action) //this will take a player action object and turn it into a string to be written to the file.
    {
        string str = "";
        str += sessionNum;
        str += " " + action.year;
        str += " " + action.month;
        str += " " + action.day;
        str += " " + action.hour;
        str += " " + action.minute;
        str += " " + action.second;
        str += " " + action.milli;
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
        else if (action is keyPress)
        {
            keyPress act = action as keyPress;
            str += " KeyPress";
            str += " " + act.keyName;
        }
        else if (action is keyReleased)
        {
            keyReleased act = action as keyReleased;
            str += " KeyReleased";
            str += " " + act.keyName;
        }
        else if (action is ObjectRotation)
        {
            ObjectRotation act = action as ObjectRotation;
            str += " ObjectRotation";
            str += " " + act.objectName;
            str += " " + act.pastX;
            str += " " + act.pastY;
            str += " " + act.pastZ;
            //str += " " + act.pastW;
            str += " " + act.newX;
            str += " " + act.newY;
            str += " " + act.newZ;
            //str += " " + act.newW;
        }
        return str;
    }

    public PlayerAction lineToAction(String fileLine) //this will do the opposite of the previous function, where it will take a line from the file and turn it inot a playerAction object, not up to date
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

    public void newLogin() //creates new login action
    {
        actions.Add(new Login(sessionNum));
    }

    public void newButton(string name) //records new button press
    {
        actions.Add(new ButtonPress(name, sessionNum));
    }

    public void newSubmission(string name, bool correct, int chalNumber, int totalNum) //records a challenge submission
    {
        actions.Add(new ChallengeSubmission(name, correct, chalNumber, totalNum, sessionNum));
    }

    public class PlayerAction //the parent class PlayerAction, holds the current time and session number
    {
        public int year;
        public int month;
        public int day;
        public int hour;
        public int minute;
        public int second;
        public int session;
        public int milli;

        public PlayerAction()
        {
            System.DateTime time = System.DateTime.Now;
            year = time.Year;
            month = time.Month;
            day = time.Day;
            hour = time.Hour;
            minute = time.Minute;
            second = time.Second;
            milli = time.Millisecond;
        }
        //stores the time of ;the action
    }

    public class Login : PlayerAction //child class, holds data of when the player logs in 
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
            milli = time.Millisecond;
            session = sessionNum;
        }
    }

    public class Click : PlayerAction //child class holds data of when the player clicks
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
            milli = time.Millisecond;
            mouseLocation = location;
            session = sessionNum;
        }
    }

    public class ButtonPress : PlayerAction //child class holds data of when the player presses a button
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
            milli = time.Millisecond;
            buttonName = button;
            session = sessionNum;
        }
    }

    public class keyPress : PlayerAction //child class holds data of when a key is pressed
    {
        public string keyName;
        public keyPress(string key, int sessionNum)
        {
            System.DateTime time = System.DateTime.Now;
            year = time.Year;
            month = time.Month;
            day = time.Day;
            hour = time.Hour;
            minute = time.Minute;
            second = time.Second;
            milli = time.Millisecond;
            keyName = key;
            session = sessionNum;
        }
    }

    public class keyReleased : PlayerAction //child class holds data of when a key is released
    {
        public string keyName;
        public keyReleased(string key, int sessionNum)
        {
            System.DateTime time = System.DateTime.Now;
            year = time.Year;
            month = time.Month;
            day = time.Day;
            hour = time.Hour;
            minute = time.Minute;
            second = time.Second;
            milli = time.Millisecond;
            keyName = key;
            session = sessionNum;
        }
    }

    public class SceneChange : PlayerAction //child class holds data of when a scene is changed
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
            milli = time.Millisecond;
            oldSceneName = oldS;
            newSceneName = newS;
            session = sessionNum;
        }
    }

    public class ChallengeSubmission : PlayerAction //child class holds data of when a challenge answer is submitted
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
            milli = time.Millisecond;
            challengeName = name;
            success = correct;
            currentChallengeNum = chalNumber;
            totalNumChallenges = totalNum;
            session = sessionNum;
        }
    }

    public class ObjectRotation : PlayerAction //child class holds data of a rotation of an object/shape
    {
        public string objectName;
        public float pastX;
        public float pastY;
        public float pastZ;
        public float pastW;
        public float newX;
        public float newY;
        public float newZ;
        public float newW;
        /*public ObjectRotation(string objectName_, float pastX_, float pastY_, float pastZ_, float pastW_, float newX_, float newY_, float newZ_, float newW_, int sessionNum)
        {
            System.DateTime time = System.DateTime.Now;
            year = time.Year;
            month = time.Month;
            day = time.Day;
            hour = time.Hour;
            minute = time.Minute;
            second = time.Second;
            milli = time.Millisecond;
            objectName = objectName_;
            pastX = pastX_;
            pastY = pastY_;
            pastZ = pastZ_;
            pastW = pastW_;
            newX = newX_;
            newY = newY_;
            newZ = newZ_;
            newW = newW_;
            session = sessionNum;
        }*/

        public ObjectRotation(string objectName_, float pastX_, float pastY_, float pastZ_, float newX_, float newY_, float newZ_, int sessionNum)
        {
            System.DateTime time = System.DateTime.Now;
            year = time.Year;
            month = time.Month;
            day = time.Day;
            hour = time.Hour;
            minute = time.Minute;
            second = time.Second;
            milli = time.Millisecond;
            objectName = objectName_;
            pastX = pastX_;
            pastY = pastY_;
            pastZ = pastZ_;
            newX = newX_;
            newY = newY_;
            newZ = newZ_;
            session = sessionNum;
        }
    }
}
