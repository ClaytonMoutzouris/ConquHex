using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public static Color[] colors = new Color[] { Color.red, new Color(0, 0.45f, 1), Color.green, Color.yellow };
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
