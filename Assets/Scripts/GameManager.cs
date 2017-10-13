using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {  };

public class GameManager : MonoBehaviour {


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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
