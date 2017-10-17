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

    void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (status != TileStatus.Placed)
        {
            transform.Rotate(new Vector3(0, 0, -60));
            this.tileData.RotateEdges();
        }
    }

}
