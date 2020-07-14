using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizableCube : MonoBehaviour
{
	// For any edge in this system, there are at most four cubes which contain that edge. A B C are those four cubes excluding this one.
	struct EdgeAdjacentCubes{
		public GameObject A, B, C;
	}
	
	// All of the surrounding cubes. Used to determine visibility of edges and faces.
	public GameObject U = null,
					  UF = null,
					  UB = null,
					  UR = null,
					  ULe = null,
					  Lo = null,
					  LoF = null,
					  LoB = null,
					  LoR = null,
					  LoLe = null,
					  F = null,
					  FR = null,
					  FLe = null,
					  R = null,
					  Le = null,
					  B = null,
					  BR = null,
					  BLe = null;
					  
	public GameObject slopeFace;
	
	public GameObject[] faces, straightEdges, diagonalEdges;
	
	private CubeState CubeState = CubeState.Null;
	
	private GameObject[] cubesAdjacentToFaces = new GameObject[6]; // Used to set visibility of the faces.
	
	private EdgeAdjacentCubes[] edgeAdjacentCubes = new EdgeAdjacentCubes[12];
	
	void Start(){
		// Define which cubes are adjacent to which faces.
		cubesAdjacentToFaces[0] = U;
		cubesAdjacentToFaces[1] = Lo;
		cubesAdjacentToFaces[2] = F;
		cubesAdjacentToFaces[3] = B;
		cubesAdjacentToFaces[4] = R;
		cubesAdjacentToFaces[5] = Le;
		
		
		// Define which cubes are adjacent to which edges.
		edgeAdjacentCubes[0].A = UF;
		edgeAdjacentCubes[0].B = U;
		edgeAdjacentCubes[0].C = F;
		
		edgeAdjacentCubes[1].A = UB;
		edgeAdjacentCubes[1].B = U;
		edgeAdjacentCubes[1].C = B;
		
		edgeAdjacentCubes[2].A = UR;
		edgeAdjacentCubes[2].B = U;
		edgeAdjacentCubes[2].C = R;
		
		edgeAdjacentCubes[3].A = ULe;
		edgeAdjacentCubes[3].B = U;
		edgeAdjacentCubes[3].C = Le;
		
		edgeAdjacentCubes[4].A = LoF;
		edgeAdjacentCubes[4].B = Lo;
		edgeAdjacentCubes[4].C = F;
		
		edgeAdjacentCubes[5].A = LoB;
		edgeAdjacentCubes[5].B = Lo;
		edgeAdjacentCubes[5].C = B;
		
		edgeAdjacentCubes[6].A = LoR;
		edgeAdjacentCubes[6].B = Lo;
		edgeAdjacentCubes[6].C = R;
		
		edgeAdjacentCubes[7].A = LoLe;
		edgeAdjacentCubes[7].B = Lo;
		edgeAdjacentCubes[7].C = Le;
		
		edgeAdjacentCubes[8].A = FR;
		edgeAdjacentCubes[8].B = F;
		edgeAdjacentCubes[8].C = R;
		
		edgeAdjacentCubes[9].A = FLe;
		edgeAdjacentCubes[9].B = F;
		edgeAdjacentCubes[9].C = Le;
		
		edgeAdjacentCubes[10].A = BR;
		edgeAdjacentCubes[10].B = B;
		edgeAdjacentCubes[10].C = R;
			
		edgeAdjacentCubes[11].A = BLe;
		edgeAdjacentCubes[11].B = B;
		edgeAdjacentCubes[11].C = Le;
	}
	
	public void SetCubeState(CubeState s){
		CubeState = s;
	}
	
	
	public void UpdateVisibility(){ // Updates the visibility of the customizable cube's faces and edges based on the CubeStates of the surrounding cubes (if present).
		
		if(CubeState == CubeState.Cube){
			for(int i = 0; i < faces.Length; i++){ // Loop through all of the faces of the cube to set visibility.
				if(cubesAdjacentToFaces[i] != null) // If there is a cube adjacent to the current face.
					faces[i].SetActive(!cubesAdjacentToFaces[i].activeInHierarchy); // If there is another active cube in front of faces[i], deactive that face.
			}
			
			for(int i = 0; i < straightEdges.Length; i++){
				int s = testEdge(edgeAdjacentCubes[i].A) << 2 | testEdge(edgeAdjacentCubes[i].B) << 1 | testEdge(edgeAdjacentCubes[i].C);
				
				if(s == 0b001 || s == 0b010 || s == 0b111)
					straightEdges[i].SetActive(false);
				else
					straightEdges[i].SetActive(true);
			}
			
			for(int i = 0; i < diagonalEdges.Length; i++){
				diagonalEdges[i].SetActive(false);
			}
		}
		
		
		
	}
	
	private int testEdge(GameObject g){
		
		if(g != null && g.activeInHierarchy)
			return 1;
		return 0;
	}
}


















