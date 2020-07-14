using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRPanorama;

public class HideRig : MonoBehaviour {
    private Camera cam;
    private VRCapture VRcap;
    private GameObject rig;
    private GameObject[] rigObjects;
    private int iterations = 10;
    private int i = 0;
	// Use this for initialization
	void Start () {
        VRcap =  gameObject.GetComponent<VRCapture>();
	}
	
	// Update is called once per frame
	void Update () {
        if (i < iterations)
        {
            cam = GetComponent<Camera>();
            cam.cullingMask |= (1 << 10);
            rig = VRcap.renderPanorama;
            rigObjects = rig.transform.GetComponentsInChildren<GameObject>();
            foreach (GameObject go in rigObjects)
            {
                // go.layer |= (1 << 10); 
                go.layer = 10;
            }
            i++;
        }
        
    }
}
