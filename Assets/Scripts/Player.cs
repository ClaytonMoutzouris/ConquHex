using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public static Color[] colors = new Color[] { Color.red, Color.blue };
    public int index;
    HexDeck deck;
    int score;

    public HexDeck Deck
    {
        get
        {
            return deck;
        }

        set
        {
            deck = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public Player(int index)
    {
        Deck = new HexDeck();
        Score = 0;
        this.index = index;
    }

   
}
