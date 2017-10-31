using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class HexLegal : MonoBehaviour {

    public int x;
    public int y;

    public List<HexLegal> legalNeighbours;


    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {

    }

    void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (MouseManager.IsDragging)
            return;

        //AudioSource.PlayClipAtPoint(cardDropSound, Camera.main.transform.position);
        if (HexManager.current.queuedHex.status != TileStatus.OnBoard)
        {
            AudioSource.PlayClipAtPoint(AudioManager.current.soundFX[2], Camera.main.transform.position, 0.5f);
        } else
        {
            AudioSource.PlayClipAtPoint(AudioManager.current.soundFX[1], Camera.main.transform.position, 0.5f);

        }

        HexManager.current.PlaceQueuedHexOnBoard(x, y);

    }
}
