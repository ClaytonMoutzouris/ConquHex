using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileData {

    public int[] edges;
    public string name;

    public HexTileData(string name, int[] edges)
    {
        this.edges = edges;
        this.name = name;

    }

    public HexTileData()
    {
        name = "DEFAULT_NAME";
    }


}
