using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {  };

public class GameManager : MonoBehaviour {

    List<Player> players;
    int currentPlayerIndex;
    public int numPlayers;
    bool isGameOver = false;

    static public GameManager _current;
	static public GameManager current {
		get {
			if(_current == null) {
				_current = GameObject.FindObjectOfType<GameManager>();
			}

			return _current;
		}
	}

    public bool IsGameOver
    {
        get
        {
            return isGameOver;
        }

        set
        {
            isGameOver = value;
        }
    }


    // Use this for initialization
    void Start () {

        players = new List<Player>();
		for(int i = 0; i < numPlayers; i++)
        {
            players.Add(new Player(i));
        }

        currentPlayerIndex = 0;

        HexManager.current.BeginGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextPlayer()
    {
        if(currentPlayerIndex < players.Count - 1)
        {
            currentPlayerIndex++;
        } else
        {
            currentPlayerIndex = 0;
        }
    }

    public Player getCurrentPlayer()
    {
        return players[currentPlayerIndex];
    }

}
