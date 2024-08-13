using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] objects;
    public int active = 4;
    
    public bool activateKeyControl = true;
    public ObjectManager dontCopy;
    public int dontCopyActive;
    
    private string[] keys = {"1", "2", "3", "4", "5", "6", "7", "8", "9"};

    // Start activates the right object and deactivates all the others.
    void Start()
    {
        // Deactivate all objects first
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
        
        // Activate the initial object
        if (active < objects.Length)
        {
            objects[active].SetActive(true);
        }
        else
        {
            Debug.LogError("Active index is out of bounds!");
        }
    }
    
    void Update()
    {
        if (activateKeyControl)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (Input.GetKeyUp(keys[i]) && i < objects.Length)
                {
                    int temp = i;

                    if (Input.GetKey(KeyCode.Space) && i + 9 < objects.Length)
                    {
                        temp = temp + 9;
                    }

                    // Check dontCopy condition
                    if (dontCopy != null && temp == dontCopy.active)
                    {
                        temp = active;
                    }

                    objects[active].SetActive(false);
                    active = temp;
                    objects[active].SetActive(true);
                    
                    Debug.Log("Active object changed to: " + objects[active].name);
                }
            }
        }
    }
    
    public void SetActive(int i)
    {
        if (i >= 0 && i < objects.Length) // Ensure the index is within bounds
        {
            objects[active].SetActive(false);
            active = i;
            objects[active].SetActive(true);
            Debug.Log("Active object set to: " + objects[active].name);
        }
        else
        {
            Debug.LogError("Index out of bounds when trying to set active object.");
        }
    }
}
