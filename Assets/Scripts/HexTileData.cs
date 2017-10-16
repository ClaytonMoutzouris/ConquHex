using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatSymbol { Sword, Shield, Magic };

public class HexTileData {

    public CombatSymbol[] edges;
    public string name;

    public HexTileData(string name, CombatSymbol[] edges)
    {
        this.edges = edges;
        this.name = name;

    }

    public HexTileData()
    {
        name = "DEFAULT_NAME";
    }

    public void RotateEdges()
    {
        CombatSymbol temp, previous;
        previous = edges[edges.Length - 1];
        for (int j = 0; j < edges.Length; j++)
        {
            temp = edges[j];
            edges[j] = previous;
            previous = temp;
        }
    }

    public static int Battle(CombatSymbol cPlayer, CombatSymbol opponent)
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
