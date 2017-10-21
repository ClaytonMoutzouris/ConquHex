using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScore : MonoBehaviour {
    public int playerIndex;
    Text score;

    public int PlayerIndex
    {
        get
        {
            return playerIndex;
        }

        set
        {
            playerIndex = value;
        }
    }

    // Use this for initialization
    void Start () {
        score = gameObject.GetComponent<Text>();
        updateScore(0);

    }
	
    public void updateScore(int s){
        score.text = "Player " + PlayerIndex + ": " + s;

        }
}
