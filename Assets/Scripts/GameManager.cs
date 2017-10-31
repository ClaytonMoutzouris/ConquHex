using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState { Active, Paused };

public class GameManager : MonoBehaviour {

    List<Player> players;
    int currentPlayerIndex;
    public int numPlayers;
    bool isGameOver = false;
    GameState state = GameState.Active;
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
        HexManager.current.Init();
        StartGame();
        
	}

    public void StartGame()
    {
        //AudioManager.current.ChangeTunes(0);
        Players = new List<Player>();
        //If we are loading from the main menu, use the players set up there
        //otherwise, we have to make some default players (this is mainly for testing)
        if (GameDataScript.instance != null && GameDataScript.instance.Players.Count > 0)
        {
            numPlayers = GameDataScript.instance.Players.Count;
            Players.AddRange(GameDataScript.instance.Players);
        } else
        {
            
            for (int i = 0; i < numPlayers; i++)
            {
                Players.Add(new Player(i, ("Player" + (i+1))));
            }
        }
        

        //Give each player a deck
        foreach(Player p in Players)
        {
            p.PrepareForNewGame();
        }

        currentPlayerIndex = 0;
        UIManager.current.SetActivePlayers(numPlayers);
        HexManager.current.BeginGame();

    }




    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
	}

    void Pause()
    {
        UIManager.current.PauseMenu();
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

        //UIManager.current.UpdateRemainingTiles();

    }

    public Player getCurrentPlayer()
    {
        return Players[currentPlayerIndex];
    }

    public bool checkGameOver()
    {
        bool result = true;
        foreach(Player p in Players)
        {
            if(p.Deck.DeckList.Count != 0)
            {
                result = false;
            }
        }

        return result;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");

    }

    public void NewGame()
    {
        SceneManager.LoadScene("GameScene");

    }

    public Player getWinner()
    {
        Player winner = players[0];
        

        foreach(Player p in players)
        {
            if (p.Score > winner.Score)
                winner = p;
        }

        return winner;
    }

}
