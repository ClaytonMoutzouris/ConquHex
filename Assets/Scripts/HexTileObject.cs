using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileObject : MonoBehaviour {


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
        transform.Rotate(new Vector3(0,0,60));
    }

}
