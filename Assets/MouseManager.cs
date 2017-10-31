using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

    static bool isDragging = false;
    Vector3 lastMousePos;

    public static bool IsDragging
    {
        get
        {
            return isDragging;
        }

        set
        {
            isDragging = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) == false)
        {
            IsDragging = false;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Just went down this frame.
            }
            else
            {
                if (lastMousePos != Input.mousePosition)
                {
                    IsDragging = true;
                }
            }
        }

        lastMousePos = Input.mousePosition;
    }
}
