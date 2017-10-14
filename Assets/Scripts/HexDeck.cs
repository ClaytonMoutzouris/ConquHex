using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexDeck {

    List<HexTileData> deckList;
    Player owner;

    public HexDeck()
    {
        deckList = new List<HexTileData>();
        BuildDeck();
        Shuffle();
    }

    public HexTileData GetNextHex()
    {
        if (deckList.Count > 0)
            return deckList[0];
        else
            return null;

    }

    public void PlaceHex()
    {
        deckList.RemoveAt(0);
    }

    void BuildDeck()
    {
        foreach(KeyValuePair<string, HexTileData> proto in HexManager.current.HexPrototypes)
        {
            int d = 5;
            while (d > 0)
            {
                deckList.Add(proto.Value);
                d--;
            }
        }
    }

    void Shuffle()
    {
        System.Random _random = new System.Random();

        HexTileData temp;

        int n = deckList.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            temp = deckList[r];
            deckList[r] = deckList[i];
            deckList[i] = temp;
        }
    }


}




