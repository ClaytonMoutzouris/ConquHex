using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexEdges
{

    List<HexEdge> edges;


    public HexEdges(CombatSymbol left, CombatSymbol topLeft, CombatSymbol topRight, CombatSymbol right, CombatSymbol bottomRight, CombatSymbol bottomLeft)
    {
        edges = new List<HexEdge>();
        edges.Add(new HexEdge(Edge.Left, left));
        edges.Add(new HexEdge(Edge.TopLeft, topLeft));
        edges.Add(new HexEdge(Edge.TopRight, topRight));
        edges.Add(new HexEdge(Edge.Right, right));
        edges.Add(new HexEdge(Edge.BottomRight, bottomRight));
        edges.Add(new HexEdge(Edge.BottomLeft, bottomLeft));



    }



    HexEdges(HexEdges other)
    {
        edges = new List<HexEdge>();
        foreach (HexEdge hE in other.edges)
        {
            edges.Add(hE.Clone());
        }
    }

    public HexEdges Clone()
    {
        return new HexEdges(this);
    }

    public HexEdge GetEdge(Edge e)
    {
        return edges.Find(x => x.Edge == e);
    }

    public void RotateEdges()
    {
        
        CombatSymbol temp;
        temp = GetEdge(Edge.Left).Symbol;
        GetEdge(Edge.Left).Symbol = GetEdge(Edge.BottomLeft).Symbol;
        GetEdge(Edge.BottomLeft).Symbol = GetEdge(Edge.BottomRight).Symbol;
        GetEdge(Edge.BottomRight).Symbol = GetEdge(Edge.Right).Symbol;
        GetEdge(Edge.Right).Symbol = GetEdge(Edge.TopRight).Symbol;
        GetEdge(Edge.TopRight).Symbol = GetEdge(Edge.TopLeft).Symbol;
        GetEdge(Edge.TopLeft).Symbol = temp;


    
    }

    public class HexEdge
    {

        Edge edge;
        CombatSymbol symbol;

        public HexEdge(Edge edge, CombatSymbol symbol)
        {
            this.edge = edge;
            this.symbol = symbol;
        }

        public HexEdge(HexEdge other)
        {
            edge = other.edge;
            symbol = other.symbol;
        }

        public Edge Edge
        {
            get
            {
                return edge;
            }

            set
            {
                edge = value;
            }
        }

        public CombatSymbol Symbol
        {
            get
            {
                return symbol;
            }

            set
            {
                symbol = value;
            }
        }

        public HexEdge Clone()
        {
            return new HexEdge(this);
        }
    }

}