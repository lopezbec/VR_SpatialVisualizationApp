using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeRemover : MonoBehaviour
{
    private GameObject collidingObject; //keeps track of side it is colliding with if colliding

    void OnTriggerEnter(Collider other)
    {
        // disables child objects when any collider enters the trigger area
        // Get all child GameObjects under dotted line object, including nested ones and checks that the side parent object is of the same type
        if (other.transform.parent.parent != transform.parent.parent && other.transform.parent.name == transform.parent.name)
        {
            if (CompareTag("SideOutline") && other.CompareTag("SideOutline"))
            {
                 foreach (Transform child in transform)
                 {
                     child.gameObject.SetActive(false);
                 }
                collidingObject = other.gameObject;
            }
        }
    }
    void OnDestroy()
    {
        if (collidingObject != null && collidingObject.activeInHierarchy)
        {
            //enabling sides that were disabled as a result of placing cube
            foreach (Transform child in collidingObject.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
