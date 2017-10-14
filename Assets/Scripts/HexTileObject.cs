using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileStatus { inDeck, inQueue, Selected, Placed };

public class HexTileObject : MonoBehaviour {

    public TileStatus status = TileStatus.inDeck;
    public int x;
    public int y;
    public List<HexTileObject> neighbours;
    public HexTileData tileData;
    public string tileName;
    public int[] edges;
    public string tileText;
    public Sprite tileSprite;

    void OnMouseUp()
    {
        if(status != TileStatus.Placed)
        transform.Rotate(new Vector3(0,0,60));
    }

}
