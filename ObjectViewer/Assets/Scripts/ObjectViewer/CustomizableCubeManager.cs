using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableCubeManager : MonoBehaviour
{
	public GameObject[] cubes;
	

	
	public struct ObjectDescription{
		public CubeState[] cubes;
	}
	
    public ObjectDescription[] objectDescriptions = new ObjectDescription[27];
	
    void Start()
    {
		objectDescriptions[0].cubes = new CubeState[27];
		for(int i = 0; i < 27; i++){
			objectDescriptions[0].cubes[i] = CubeState.Cube;
		}
		objectDescriptions[0].cubes[2] = CubeState.Null;
        objectDescriptions[0].cubes[11] = CubeState.Null;
    }

    
    void Update()
    {
		if(Input.GetKeyUp("u")){
			for(int i = 0; i < cubes.Length; i++){ // Set the CubeState of each of the cubes.
				cubes[i].GetComponent<CustomizableCube>().SetCubeState(objectDescriptions[0].cubes[i]); // Set the internal state variable of the cube.
				
				cubes[i].SetActive(objectDescriptions[0].cubes[i] != CubeState.Null); // If the state of the current cube is null, deactivate that cube entirely.
			}
			
			for(int i = 0; i < cubes.Length; i++){ // Update the visibility of each of the cubes so they all display properly.
				if(cubes[i].activeInHierarchy)
					cubes[i].GetComponent<CustomizableCube>().UpdateVisibility();
				
			}
		}
        
    }
}

// Define all the different state the customizable cubes can be in.
public enum CubeState{
	Null, // Indicates that this custom cube should not be visible
	Cube, // The cube should be in the default "cube CubeState"
	UF, // These next 12 CubeStates each indicate one orientation of the "wedge" shape the cube can make.
	UB, // As an example for the naming scheme, UB means "upper-back" which refers to the position of
	UR, // the edge of the wedge furthest from the slope face.
	ULe,
	LoF,
	LoB,
	LoR,
	LoLe,
	FR,
	FLe,
	BR,
	BLe
}