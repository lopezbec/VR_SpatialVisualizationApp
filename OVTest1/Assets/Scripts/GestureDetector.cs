using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GestureDetector : MonoBehaviour
{
	public UnityEvent rightPressed, leftPressed;
	
	/// <summary>
    /// Reference to the managers of the hands.
    /// First item is left hand, second item is right hand
    /// </summary>
    private OVRHand[] m_hands;
	
    // Start is called before the first frame update
    void Start()
    {
        m_hands = new OVRHand[]
        {
            GameObject.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor/OVRHandPrefab").GetComponent<OVRHand>(),
            GameObject.Find("OVRCameraRig/TrackingSpace/RightHandAnchor/OVRHandPrefab").GetComponent<OVRHand>()
        };
    }

    // Update is called once per frame
    void Update()
    {
		if (m_hands[1].GetFingerIsPinching(OVRHand.HandFinger.Middle) ||
			m_hands[1].GetFingerIsPinching(OVRHand.HandFinger.Index)){
				
			rightPressed.Invoke();
		}
		if (m_hands[0].GetFingerIsPinching(OVRHand.HandFinger.Middle) ||
			m_hands[0].GetFingerIsPinching(OVRHand.HandFinger.Index)){
				
			leftPressed.Invoke();
		}
    }
}
