using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

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

    public Player()
    {
        Deck = new HexDeck();
        Score = 0;
    }

   
}
