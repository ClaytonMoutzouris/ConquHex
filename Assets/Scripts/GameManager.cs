using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {  };

public class GameManager : MonoBehaviour {

    List<Player> players;
    int currentPlayerIndex;
    public int numPlayers;
    bool isGameOver = false;
    //public GameObject scoreboard;

    static public GameManager _current;
	static public GameManager current {
		get {
			if(_current == null) {
				_current = FindObjectOfType<GameManager>();
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

    public List<Player> Players
    {
        get
        {
            return players;
        }

        set
        {
            players = value;
        }
    }


    // Use this for initialization
    void Start () {

        Players = new List<Player>();
		for(int i = 0; i < numPlayers; i++)
        {
            Players.Add(new Player(i));
        }

        currentPlayerIndex = 0;
        UIManager.current.SetActivePlayers(numPlayers);
        HexManager.current.BeginGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextPlayer()
    {
        if(currentPlayerIndex < Players.Count - 1)
        {
            currentPlayerIndex++;
        } else
        {
            currentPlayerIndex = 0;
        }
    }

    public Player getCurrentPlayer()
    {
        return Players[currentPlayerIndex];
    }


}
