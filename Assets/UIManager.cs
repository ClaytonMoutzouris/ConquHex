using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    public GameObject scoreboard;
    public Text p1score;
    public Text p2score;

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

    public void UpdateScore(int index, int score)
    {
        //scoreboard.GetComponentInChildren<Text>().
        //newCharacter.transform.Find("Body").GetComponent[MeshRenderer]();
        string obj = "P" + index + "Score";
        scoreboard.transform.Find(obj).GetComponent<Text>().text = "Player " + index + ": " + score;
        
    }
}
