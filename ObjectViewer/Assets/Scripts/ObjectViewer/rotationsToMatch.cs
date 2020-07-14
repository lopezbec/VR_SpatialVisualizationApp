using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationsToMatch : MonoBehaviour
{
	public Quaternion[] rotationToMatch;
    
	Quaternion Get(int i){
		if(i < rotationToMatch.Length)
			return rotationToMatch[i];
		
		return rotationToMatch[0];
	}
	
}
