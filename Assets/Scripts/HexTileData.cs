using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatSymbol { Sword, Shield, Magic };
public enum Edge { Left, TopLeft, TopRight, Right, BottomRight, BottomLeft};

public class HexTileData {

    public Dictionary<Edge, CombatSymbol> edges;
    public string name;

    public HexTileData(string name, Dictionary<Edge, CombatSymbol> edges)
    {
        this.edges = edges;
        this.name = name;

    }

    public HexTileData(HexTileData other)
    {
        if (other != null)
        {
            edges = new Dictionary<Edge, CombatSymbol>(other.edges);
            name = other.name;
        }
    }

    public HexTileData Clone()
    {
        return new HexTileData(this);
    }

    public HexTileData()
    {
        name = "DEFAULT_NAME";
    }

    public void RotateEdges()
    {
        
        CombatSymbol temp, previous;
        previous = edges[(Edge)edges.Count - 1];
        for (int j = 0; j < edges.Count; j++)
        {
            temp = edges[(Edge)j];
            edges[(Edge)j] = previous;
            previous = temp;
        }
        
        /*
        CombatSymbol temp;

        temp = edges[Edge.Left];
        edges[Edge.Left] = edges[Edge.BottomLeft];
        edges[Edge.BottomLeft] = edges[Edge.BottomRight];
        edges[Edge.BottomRight] = edges[Edge.Right];
        edges[Edge.Right] = edges[Edge.TopRight];
        edges[Edge.TopRight] = edges[Edge.TopLeft];
        edges[Edge.TopLeft] = temp;


    */
    }

    public int Battle(CombatSymbol cPlayer, CombatSymbol opponent)
    {
        int outcome = 0;
        Debug.Log(cPlayer.ToString() + " versus " + opponent.ToString());
        switch (cPlayer)
        {
            case CombatSymbol.Magic:
                switch (opponent)
                {
                    case (CombatSymbol.Magic):
                        outcome = 0;
                        break;
                    case (CombatSymbol.Sword):
                        outcome = -1;                   
                        break;
                    case (CombatSymbol.Shield):
                        outcome = 1;
                        break;
                }
                break;
            case CombatSymbol.Sword:
                switch (opponent)
                {
                    case (CombatSymbol.Magic):
                        outcome = 1;
                        break;
                    case (CombatSymbol.Sword):
                        outcome = 0;
                        break;
                    case (CombatSymbol.Shield):
                        outcome = -1;
                        break;
                }
                break;
            case CombatSymbol.Shield:
                switch (opponent)
                {
                    case (CombatSymbol.Magic):
                        outcome = -1;
                        break;
                    case (CombatSymbol.Sword):
                        outcome = 1;
                        break;
                    case (CombatSymbol.Shield):
                        outcome = 0;
                        break;
                }
                break;

        }

        return outcome;

    }

}
