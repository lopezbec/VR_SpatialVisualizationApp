using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] objects;
    public int active = 0;

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
    
    public void SetActive(int i)
    {
        if (i >= 0 && i < objects.Length)
        {
            objects[active].SetActive(false);

            active = i;
            objects[active].SetActive(true);
            
            Debug.Log("Active object set to: " + objects[active].name + " at index: " + active);
        }
        else
        {
            Debug.LogError("Index out of bounds when trying to set active object. Attempted index: " + i);
        }
    }
}
