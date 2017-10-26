using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataScript : MonoBehaviour {

    static public GameDataScript instance;
    List<Player> players = new List<Player>();

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

		if(instance != null)
        {
            Destroy(this);
        } else
        {
            DontDestroyOnLoad(this);
            instance = this;

        }
	}
	


}
