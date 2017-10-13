using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexLegal : MonoBehaviour {

    public int x;
    public int y;

    public List<HexLegal> legalNeighbours;
    bool isDragging = false;
    Vector3 lastMousePos;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        if (Input.GetMouseButton(0) == false)
        {
            isDragging = false;
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
                    isDragging = true;
                }
            }
        }

        lastMousePos = Input.mousePosition;
    }

    void OnMouseUp()
    {
        if (isDragging)
            return;

        //AudioSource.PlayClipAtPoint(cardDropSound, Camera.main.transform.position);

       HexManager.current.PlaceQueuedHex(x, y);
    }
}
