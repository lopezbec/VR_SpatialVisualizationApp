
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    // Function to rotate active children 90 degrees around the Z axis
    public void RotateActiveChildrenZplus()
    {
        RotateActiveChildren(Vector3.forward);
    }
    public void RotateActiveChildrenZminus()
    {
        RotateActiveChildren(Vector3.back);
    }

    // Function to rotate active children 90 degrees around the Y axis
    public void RotateActiveChildrenYplus()
    {
        RotateActiveChildren(Vector3.up);
    }
    public void RotateActiveChildrenYminus()
    {
        RotateActiveChildren(Vector3.down);
    }

    // Function to rotate active children 90 degrees around the X axis
    public void RotateActiveChildrenXplus()
    {
        RotateActiveChildren(Vector3.right);
    }
    public void RotateActiveChildrenXminus()
    {
        RotateActiveChildren(Vector3.left);
    }

    private void RotateActiveChildren(Vector3 axis)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                child.Rotate(axis * 90);
            }
        }
    }
}
