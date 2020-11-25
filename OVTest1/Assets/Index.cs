using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Index : MonoBehaviour
{
	public Transform finger, sphere;
	
	public GameObject handPrefab;
	public bool isIndexFingerPinching;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//sphere.rotation = finger.rotation;
		//var hand = GetComponent<OVRHand>();
        //isIndexFingerPinching = hand.GetFingerIsPinching(HandFinger.Index);
		
		//this.gameobject.renderer.enabled = isIndexFingerPinching;
    }
}
