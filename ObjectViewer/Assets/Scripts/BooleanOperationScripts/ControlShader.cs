using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlShader : MonoBehaviour
{
	public Material cuttingPlane;

    public float rotX = 0f, rotY = 0f, rotZ = 0f, rotationDelta = 7, posY = 3f, positionDelta = 0.1f;

    public Vector3 defaultScale = new Vector3();

    private Vector3 normal = new Vector3(), position = new Vector3();
	
    void start()
    {
		cuttingPlane.SetVector("_PlaneNormal", normal);
		cuttingPlane.SetVector("_PlanePos", position);
    }

    void Update()
    {
		
		if (Input.GetKey("r"))
        {
			normal.Set(1, 1, 1);
			posY = 4.78f;
        }
		
        positionDelta = 0.01f;

        if (Input.GetKey("1"))
			normal.Set(1, 0, 0);
        if (Input.GetKey("2"))
			normal.Set(0, 1, 0);
        if (Input.GetKey("3"))
			normal.Set(0, 0, 1);
        if (Input.GetKey("4"))
			normal.Set(1, 1, 0);
        if (Input.GetKey("5"))
			normal.Set(1, 0, 1);
        if (Input.GetKey("6"))
			normal.Set(1, 1, 1);
		
        if (Input.GetKey("7"))
            posY += positionDelta;
        if (Input.GetKey("8"))
            posY -= positionDelta;

		
        position.Set(4.8f, posY, -5.5f);
        cuttingPlane.SetVector("_PlaneNormal", normal);
		cuttingPlane.SetVector("_PlanePos", position);
    }
}
