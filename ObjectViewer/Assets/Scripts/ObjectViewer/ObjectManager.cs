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
	
	void Start(){
		// Set the object indicated by "active" to be active. This is here just in case the wrong object or multiple objects are active up in the editor.
		for(int i = 0; i < objects.Length; i++)
			objects[i].SetActive(false);
		
		objects[active].SetActive(true);
	}
	
    void Update(){
		if (activateKeyControl)
		{
			

			for (int i = 0; i < keys.Length; i++)
			{ // Checks if the user has released any of the number keys and sets the active object based on that.
				if (Input.GetKeyUp(keys[i]) && i < objects.Length)
				{
					int temp = i;

					if (Input.GetKey(KeyCode.Space) && i + 9 < objects.Length) // Holding the spacebar allows the user to select other objects with the number keys.
						temp = temp + 9;

					//if we have an object set to dontCopy, makes sure that the shape won't change to that object
					if (dontCopy != null)
					{
						if(temp == dontCopy.active)
                        {
							temp = active;
						}
					}
					objects[active].SetActive(false);
					active = temp;
					objects[active].SetActive(true);
					
				}
			}
		}
		

	}
	
	public void SetActive(int i){ // Sets the ith game object to be the active object active.
	
			objects[active].SetActive(false);
			active = i;
			objects[active].SetActive(true);
	}
	
}
