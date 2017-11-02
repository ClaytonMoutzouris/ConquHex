using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public static Color[] colors = new Color[] { Color.red, new Color(0, 0.45f, 1), Color.green, Color.yellow };
    public Color pColor;
    public int index;
    HexDeck deck;
    int score = 0;
    string name;

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

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public Player(int index, string n, Color p)
    {
        Name = n;
        //Deck = new HexDeck();
        this.index = index;
        pColor = p;
    }

    public void PrepareForNewGame()
    {
        Score = 0;
        MakeDeck();
    }

    public void MakeDeck()
    {
        Deck = new HexDeck();
    }
   
}
