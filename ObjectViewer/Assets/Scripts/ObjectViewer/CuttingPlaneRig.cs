using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingPlaneRig : MonoBehaviour
{
    public Transform t;

    public float rotX = 0f, rotY = 0f, rotZ = 0f, rotationDelta = 7, posY = 3f, positionDelta = 0.1f;

    public Vector3 defaultScale = new Vector3();

    private Vector3 rotation = new Vector3(), position = new Vector3();
	

    void start()
    {
		
    }

    // Using FixedUpdate instead of Update because I am using physics
    void FixedUpdate()
    {
		
		
        rotationDelta = 4;
        positionDelta = 0.1f;
        defaultScale.Set(3f, 0.05f, 3f);

        // Rotation controls
        if (Input.GetKey("q"))
            rotX += rotationDelta;
        if (Input.GetKey("a"))
            rotX -= rotationDelta;
        if (Input.GetKey("w"))
            rotY += rotationDelta;
        if (Input.GetKey("s"))
            rotY -= rotationDelta;
        if (Input.GetKey("e"))
            rotZ += rotationDelta;
        if (Input.GetKey("d"))
            rotZ -= rotationDelta;
        if (Input.GetKey("t"))
            posY += positionDelta;
        if (Input.GetKey("g"))
            posY -= positionDelta;

        if (Input.GetKey("r"))
        {
            rotX = 0; rotY = 0; rotZ = 0; posY = 0;
        }

        t.localScale = defaultScale;
        rotation.Set(rotX, rotY, rotZ);
        position.Set(4.8f, posY, -5.5f);
        t.localEulerAngles = rotation;
        t.position = position;
    }
}
