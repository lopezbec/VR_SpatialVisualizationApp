using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
 
/// <summary>
/// Manages the Cube with the Oculus Hands base functionalities
/// When the Left Index touches the cube, it becomes blue;
/// When the Right Index touches the cube, it become green;
/// When the Left hand pinches the middle finger, it becomes red;
/// </summary>
[RequireComponent(typeof(Collider))]
public class Cube : MonoBehaviour
{
    /// <summary>
    /// Renderer of this cube
    /// </summary>
    private Renderer m_renderer;
 
    /// <summary>
    /// Reference to the managers of the hands.
    /// First item is left hand, second item is right hand
    /// </summary>
    private OVRHand[] m_hands;
 
    /// <summary>
    /// True if an index tip is inside the cube, false otherwise.
    /// First item is left hand, second item is right hand
    /// </summary>
    private bool[] m_isIndexStaying;
 
	public UnityEvent enter, exit, pressed;
 
    /// <summary>
    /// Start
    /// </summary>
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_hands = new OVRHand[]
        {
            GameObject.Find("OVRCameraRig/TrackingSpace/LeftHandAnchor/OVRHandPrefab").GetComponent<OVRHand>(),
            GameObject.Find("OVRCameraRig/TrackingSpace/RightHandAnchor/OVRHandPrefab").GetComponent<OVRHand>()
        };
        m_isIndexStaying = new bool[2] { false, false };
 
        //we don't want the cube to move over collision, so let's just use a trigger
        GetComponent<Collider>().isTrigger = true;
    }
 
    /// <summary>
    /// Update
    /// </summary>
    void Update()
    {
        //check for middle finger pinch on the left hand, and make che cube red in this case
        if (m_hands[1].GetFingerIsPinching(OVRHand.HandFinger.Middle) ||
			m_hands[1].GetFingerIsPinching(OVRHand.HandFinger.Index)){
            //m_renderer.material.color = Color.red;
			pressed.Invoke();
		}
        //if no pinch, and the cube was red, make it white again
        else if (m_renderer.material.color == Color.red)
            m_renderer.material.color = Color.white;
    }
 
    /// <summary>
    /// Trigger enter.
    /// Notice that this gameobject must have a trigger collider
    /// </summary>
    /// <param name="collider">Collider of interest</param>
    /*private void OnTriggerEnter(Collider collider)
    {
        //get hand associated with trigger
        int handIdx = GetIndexFingerHandId(collider);
 
        //if there is an associated hand, it means that an index of one of two hands is entering the cube
        //change the color of the cube accordingly (blue for left hand, green for right one)
        if (handIdx != -1)
        {
			enter.Invoke();
            m_renderer.material.color = handIdx == 0 ? m_renderer.material.color = Color.blue : m_renderer.material.color = Color.green;
            m_isIndexStaying[handIdx] = true;
        }
    }*/
 
    /// <summary>
    /// Trigger Exit.
    /// Notice that this gameobject must have a trigger collider
    /// </summary>
    /// <param name="collider">Collider of interest</param>
    /*private void OnTriggerExit(Collider collider)
    {
        //get hand associated with trigger
        int handIdx = GetIndexFingerHandId(collider);
 
        //if there is an associated hand, it means that an index of one of two hands is levaing the cube,
        //so set the color of the cube back to white, or to the one of the other hand, if it is in
        if (handIdx != -1)
        {
			exit.Invoke();
            m_isIndexStaying[handIdx] = false;
            m_renderer.material.color = m_isIndexStaying[0] ? m_renderer.material.color = Color.blue :
                                        (m_isIndexStaying[1] ? m_renderer.material.color = Color.green : Color.white);
        }
    }*/
 
    /// <summary>
    /// Gets the hand id associated with the index finger of the collider passed as parameter, if any
    /// </summary>
    /// <param name="collider">Collider of interest</param>
    /// <returns>0 if the collider represents the finger tip of left hand, 1 if it is the one of right hand, -1 if it is not an index fingertip</returns>
    /*private int GetIndexFingerHandId(Collider collider)
    {
        //Checking Oculus code, it is possible to see that physics capsules gameobjects always end with _CapsuleCollider
        if (collider.gameObject.name.Contains("_CapsuleCollider"))
        {
            //get the name of the bone from the name of the gameobject, and convert it to an enum value
            string boneName = collider.gameObject.name.Substring(0, collider.gameObject.name.Length - 16);
            OVRPlugin.BoneId boneId = (OVRPlugin.BoneId)Enum.Parse(typeof(OVRPlugin.BoneId), boneName);
 
            //if it is the tip of the Index
            if (boneId == OVRPlugin.BoneId.Hand_Index3)
                //check if it is left or right hand, and change color accordingly.
                //Notice that absurdly, we don't have a way to detect the type of the hand
                //so we have to use the hierarchy to detect current hand
                if (collider.transform.IsChildOf(m_hands[0].transform))
                {
                    return 0;
                }
                else if (collider.transform.IsChildOf(m_hands[1].transform))
                {
                    return 1;
                }
        }
 
        return -1;
    }
 */
}