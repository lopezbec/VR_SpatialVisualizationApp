using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.Events;

public class HandleTextFile : MonoBehaviour
{
	public GameObject[] voxels;
	public Transform tCamera;
	public Camera mainCamera;
	
	private string input;
	private int numberOfObjects = 0;
	
    void ReadString()
    {
        string path = "Assets/Resources/input.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
		input = reader.ReadToEnd();
        //Debug.Log(input);
        reader.Close();
    }
	
    // Start is called before the first frame update
    void Start()
    {
        ReadString(); // Read the input from input.txt
		numberOfObjects = 1 + input.Length / (14*27); // This line is based on multiple objects in the input file, which is not supported right now.
		
		setObject(0);
    }

    // Update is called once per frame
    void Update()
    {	
		
    }
	
	/* Increments through each character in 'input' and activates or deactivates each wedge appropriately */
	private void setObject(int startingIndex)
	{
		int inputIndex = startingIndex;
		
		for(int voxelIndex = 0; voxelIndex < 27; voxelIndex++){
			for(int wedgeIndex = 0; wedgeIndex < 12; wedgeIndex++){
				//Debug.Log(input[inputIndex]);
				voxels[voxelIndex].GetComponent<Voxel>().wedges[wedgeIndex].SetActive(input[inputIndex] == '1');
				inputIndex++;
			}
			
			for(; inputIndex < input.Length && (input[inputIndex] != '1' && input[inputIndex] != '0'); inputIndex++); // Skip new line character(s);
		}
		
		
	}
}

















