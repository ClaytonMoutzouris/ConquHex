using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatSymbol { Sword, Shield, Magic };
public enum Edge { Left, TopLeft, TopRight, Right, BottomRight, BottomLeft};

public class HexTileData {

    public HexEdges edges;
    public string name;
    public int numberInDeck;

    public HexTileData(string name, int numberInDeck, HexEdges edges)
    {
        this.edges = edges;
        this.name = name;
        this.numberInDeck = numberInDeck;

    }

    public HexTileData(HexTileData other)
    {
        if (other != null)
        {
            edges = other.edges.Clone();
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
        /*
        CombatSymbol temp, previous;
        previous = edges[edges.Count - 1].Symbol;
        for (int j = 0; j < edges.Count; j++)
        {
            temp = edges[j].Symbol;
            edges[j] = previous;
            previous = temp;
        }
        */
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

    public Edge GetCorrespondingEdge(Edge e)
    {
        Edge temp = Edge.Left;
        switch (e)
        {
            case Edge.Left:
                temp = Edge.Right;
                break;
            case Edge.TopLeft:
                temp = Edge.BottomRight;
                break;
            case Edge.TopRight:
                temp = Edge.BottomLeft;
                break;
            case Edge.Right:
                temp = Edge.Left;
                break;
            case Edge.BottomRight:
                temp = Edge.TopLeft;
                break;
            case Edge.BottomLeft:
                temp = Edge.TopRight;
                break;
        }
        // Debug.Log(e.ToString() + " to " + temp.ToString());
        return temp;
    }

    public int Battle(HexTileData opponent, Edge edge)
    {
        CombatSymbol playerSymbol = edges.GetEdge(edge).Symbol;
        CombatSymbol oppSymbol = opponent.edges.GetEdge(GetCorrespondingEdge(edge)).Symbol;

        //queuedHex.owner.Score += queuedHex.tileData.Battle(queuedHex.tileData.edges.GetEdge(n.Key).Symbol, n.Value.tileData.edges.GetEdge(GetCorrespondingEdge(edge)).Symbol);

        int outcome = 0;
        Debug.Log(playerSymbol.ToString() + " versus " + opponent.ToString());
        switch (playerSymbol)
        {
            case CombatSymbol.Magic:
                switch (oppSymbol)
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
                switch (oppSymbol)
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
                switch (oppSymbol)
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

    public bool Assist(HexTileData mate, Edge edge)
    { 

        if(edges.GetEdge(edge).Symbol == mate.edges.GetEdge(GetCorrespondingEdge(edge)).Symbol)
        {
            return true;
        }

        return false;

    }

}
