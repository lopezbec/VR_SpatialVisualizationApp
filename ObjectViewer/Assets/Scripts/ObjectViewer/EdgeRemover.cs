using UnityEngine;

public class EdgeRemover : MonoBehaviour
{
    private GameObject collidingObject; //keeps track of side it is colliding with if colliding

    void OnTriggerEnter(Collider other)
    {
        // disables child objects when any collider enters the trigger area
        // Get all child GameObjects under dotted line object, including nested ones and checks that the side parent object is of the same type
        if (other.transform.parent.name == transform.parent.name)
        {
            if (CompareTag("SideOutline") && other.CompareTag("SideOutline"))
            {
                if (other.transform.parent.parent != transform.parent.parent)
                {
                


                    foreach (Transform child in transform)
                    {
                        child.gameObject.SetActive(false);
                    }

                    collidingObject = other.gameObject;
                }
            }
            else
            {
                Debug.Log("Break1");
            }
        }
        /*
         * If you are reading this then I have not be able to remove outlines on inclined objects.
         * Below is some code to continue the if statement but it only works sometimes and creates glitches for the cube
         */ 
        
        // else
        // {
        //     if (CompareTag("SideInclineOutline") || other.CompareTag("SideInclineOutline"))
        //     {
        //         if (other.transform.parent.parent != transform.parent.parent)
        //         {
        //             foreach (Transform child in transform)
        //             {
        //                 child.gameObject.SetActive(false);
        //             }
        //
        //             collidingObject = other.gameObject;
        //             
        //         }
        //         else
        //         {
        //             Debug.Log("Break2");
        //         }
        //     }
        //     else
        //     {
        //         Debug.Log("Break3");
        //     }
        // }
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
