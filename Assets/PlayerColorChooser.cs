using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerColorChooser : MonoBehaviour {

    public Image playerColor;
    public int playerIndex;
    int colorIndex;

    public int ColorIndex
    {
        get
        {
            return colorIndex;
        }

        set
        {
            colorIndex = value;
        }
    }

    // Use this for initialization
    void Awake () {
        ColorIndex = playerIndex;
        playerColor.color = NewGamePanel.current.PlayerColors[ColorIndex];

    }
}
