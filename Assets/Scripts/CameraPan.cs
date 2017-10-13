using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {

	Vector2 lastMousePos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Mouse Zoom
		GetComponent<Camera>().orthographicSize = Mathf.Clamp( 
			GetComponent<Camera>().orthographicSize * (1f-Input.GetAxis("Mouse ScrollWheel")), 
			1, 5 );


		// Mouse Pan
		Vector2 currMousePos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

		if(Input.GetMouseButtonDown(0)) {
			// Mouse was clicked this frame, so not dragging yet.
		}
		else if(Input.GetMouseButton(0)) {
			// Mouse button still down -- let's move the camera!
			transform.Translate( lastMousePos - currMousePos );
			currMousePos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
		}

		lastMousePos = currMousePos;
	}
}
