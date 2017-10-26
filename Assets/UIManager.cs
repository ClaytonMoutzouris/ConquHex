using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    public GameObject scoreboard;
    public GameObject gameOverScreen;

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
    }
}
