using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectData : MonoBehaviour
{
    public string playerName;
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

    }
}
