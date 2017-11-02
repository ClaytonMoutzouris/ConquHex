using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGamePanel : MonoBehaviour {

    public Dropdown numPlayerDrop;
    public List<GameObject> playerPanels = new List<GameObject>();
    public List<PlayerColorChooser> ColorChoosers = new List<PlayerColorChooser>();
    static public NewGamePanel _current;
    static public NewGamePanel current
    {
        get
        {
            if (_current == null)
            {
                _current = FindObjectOfType<NewGamePanel>();
            }

            return _current;
        }
    }

    public List<Color> PlayerColors
    {
        get
        {
            return playerColors;
        }

        set
        {
            playerColors = value;
        }
    }

    [SerializeField] List<Color> playerColors;

    private void Awake()
    {
        
        setActivePanels();

    }

    public void setActivePanels()
    {
        int i = 0;
        int n = getNumPlayers();
        foreach(GameObject go in playerPanels)
        {
            if (i < n)
                go.SetActive(true);
            else
                go.SetActive(false);

            i++;
        }

    }

    public int getNumPlayers()
    {
        List<Dropdown.OptionData> menuOptions = numPlayerDrop.options;
        return int.Parse(menuOptions[numPlayerDrop.value].text);
    }

    public void StartGame()
    {
        List<Player> newPlayers = new List<Player>();
        for(int i = 0; i < getNumPlayers(); i++)
        {
            newPlayers.Add(new Player(i, getPlayerInfo(i), getPlayerColor(i)));
        }
        GameDataScript.instance.Players = newPlayers;
        SceneManager.LoadScene("GameScene");
    }

    public string getPlayerInfo(int index)
    {
        InputField iField = playerPanels[index].GetComponentInChildren<InputField>();
        if (iField.text == "")
        {
            return iField.placeholder.GetComponent<Text>().text;
        }
        return playerPanels[index].GetComponentInChildren<InputField>().text;

    }

    public Color getPlayerColor(int index)
    {
        //Image pColor = playerPanels[index].GetComponentInChildren<InputField>();
        return playerColors[playerPanels[index].GetComponentInChildren<PlayerColorChooser>().ColorIndex];

    }

    public void ColorRight(int playerIndex)
    {
        PlayerColorChooser pc = ColorChoosers[playerIndex];
        if (pc.ColorIndex < (PlayerColors.Count - 1))
            pc.ColorIndex++;
        else
            pc.ColorIndex = 0;

        if (!CheckValidColorIndex(pc))
        {
            ColorRight(playerIndex);
        }

        //Set the players color according to the index
        pc.playerColor.color = PlayerColors[pc.ColorIndex];
    }

    public void ColorLeft(int playerIndex)
    {
        PlayerColorChooser pc = ColorChoosers[playerIndex];

        if (pc.ColorIndex > 0)
            pc.ColorIndex--;
        else
            pc.ColorIndex = (PlayerColors.Count - 1);

        if (!CheckValidColorIndex(pc))
        {
            ColorLeft(playerIndex);
        }

        //Set the players color according to the index
        pc.playerColor.color = PlayerColors[pc.ColorIndex];

    }

    
    public bool CheckValidColorIndex(PlayerColorChooser checking)
    {
        foreach(PlayerColorChooser pc in ColorChoosers)
        {
            if (pc == checking)
                continue;
            if(pc.ColorIndex == checking.ColorIndex)
            {
                return false;
            }
        }
        return true;
    }

    
}
