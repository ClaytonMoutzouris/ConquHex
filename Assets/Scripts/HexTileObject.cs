using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;

public enum TileStatus { inDeck, inQueue, Selected, OnBoard, Placed };

public class HexTileObject : MonoBehaviour {

    public Player owner;
    public TileStatus status = TileStatus.inDeck;
    public int x;
    public int y;
    public List<HexTileObject> neighbours;
    public Dictionary<Edge, HexTileObject> neighbourPositions;
    public HexTileData tileData;
    public string tileName;
    public string tileText;
    public Sprite tileSprite;
    Quaternion startRotation;
    Quaternion endRotation;
    float rotationProgress = -1;
    float rotation = 0;

    void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (MouseManager.IsDragging)
            return;
        if (status != TileStatus.Placed)
        {
            RotateHex();   
        }
    }

    public void RotateHex()
    {
        rotation -= 60;
        // Here we cache the starting and target rotations
        startRotation = transform.rotation;
        //print("start" + startRotation);
        endRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rotation);
        //print("end" + endRotation);
        tileData.edges.RotateEdges();
        AudioSource.PlayClipAtPoint(AudioManager.current.soundFX[1], Camera.main.transform.position, 0.5f);
        // This starts the rotation, but you can use a boolean flag if it's clearer for you
        rotationProgress = 0;
       
        /*
        transform.Rotate(new Vector3(0, 0, -60));
        
        
        */
    }

    public void RotateHex(int n)
    {
        for (int i = 0; i < n; i++)
        {
            transform.Rotate(new Vector3(0, 0, -60));
            this.tileData.edges.RotateEdges();
        }
    }

    public void Update()
    {

        if (rotationProgress < 1 && rotationProgress >= 0)
        {
            //print(rotationProgress);
            rotationProgress += Time.deltaTime * 5;

            // Here we assign the interpolated rotation to transform.rotation
            // It will range from startRotation (rotationProgress == 0) to endRotation (rotationProgress >= 1)
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, rotationProgress);
        }

    }

}
