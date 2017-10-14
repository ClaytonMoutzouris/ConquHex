using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {  };

public class GameManager : MonoBehaviour {

    List<Player> players;
    Player currentPlayer;
    public int numPlayers;

    public Player CurrentPlayer
    {
        get
        {
            return currentPlayer;
        }

        set
        {
            currentPlayer = value;
        }
    }


    static public GameManager _current;
	static public GameManager current {
		get {
			if(_current == null) {
				_current = GameObject.FindObjectOfType<GameManager>();
			}

			return _current;
		}
	}


    // Use this for initialization
    void Start () {

        players = new List<Player>();
		for(int i = 0; i < numPlayers; i++)
        {
            players.Add(new Player());
        }

        CurrentPlayer = players[0];

        HexManager.current.BeginGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void NextPlayer()
    {

    }

    public bool IsGameOver()
    {
        return (currentPlayer.Deck.GetNextHex() == null);
    }
}
