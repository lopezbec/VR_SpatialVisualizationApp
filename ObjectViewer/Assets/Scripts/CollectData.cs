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
        List<playerAction> actions = new List<playerAction>();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    class playerAction
    {
        string actionType;

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
}
