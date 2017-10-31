using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    public GameObject scoreboard;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public Text remainingTiles;
    public List<GameObject> playerScores;

    static public UIManager _current;
    static public UIManager current
    {
        get
        {
            if (_current == null)
            {
                _current = FindObjectOfType<UIManager>();
            }

            return _current;
        }
    }

    public void Start()
    {

    }

    public void SetActivePlayers(int n)
    {
        for (int i = 0; i < 4; i++)
        {
            playerScores[i].SetActive(false);
        }

        for (int i = 0; i<n; i++)
        {
            playerScores[i].SetActive(true);
        }

    }

    public void UpdateScore(Player p)
    {
        //scoreboard.GetComponentInChildren<Text>().
        //newCharacter.transform.Find("Body").GetComponent[MeshRenderer]();
        //string obj = "P" + index + "Score";
        playerScores[p.index].GetComponent<Text>().text = p.Name + ": " + p.Score;
        
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Text winDisplay = gameOverScreen.GetComponentInChildren<Text>();
        //This should be a specific reference in the future, rather than relying on the fact that the right text is first
        winDisplay.text = GameManager.current.getWinner().Name + " is the winner with " + GameManager.current.getWinner().Score + " points!";
    }

    public void UpdateRemainingTiles()
    {
        remainingTiles.text = "Tiles Remaining: " + GameManager.current.getCurrentPlayer().Deck.DeckList.Count;
    }

    public void PauseMenu()
    {
        if (pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(false);
        } else
        {
            pauseScreen.SetActive(true);

        }
    }
}
