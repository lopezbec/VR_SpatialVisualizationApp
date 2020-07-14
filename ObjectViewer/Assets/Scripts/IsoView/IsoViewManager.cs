using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoViewManager : MonoBehaviour
{
	public Transform cameraHolder;
	public Transform isoObjectManager;
	public Transform cam;
	public GameObject detector;
	
	private OrientationDetector orientationDetector;
	private int currentCube = -1;
	
	private int d = 4;
	private Vector3[] positions = new Vector3[8];
	
    void Start()
    {
		orientationDetector = detector.GetComponent<OrientationDetector>();
		
		positions[0].Set(d,d,d);
		positions[1].Set(-d,d,d);
		positions[2].Set(-d,d,-d);
		positions[3].Set(d,d,-d);
		positions[4].Set(-d,-d,d);
		positions[5].Set(-d,-d,-d);
		positions[6].Set(d,-d,-d);
		positions[7].Set(d,-d,d);
    }

    void Update()
    {
		if(currentCube != orientationDetector.currentCube){ // If the desired octant has changed since the last update.
			currentCube = orientationDetector.currentCube;
			
			//cameraHolder.LookAt(isoObjectManager);
			cameraHolder.localPosition = positions[currentCube];
			cam.LookAt(isoObjectManager);
		}
        
    }
}
