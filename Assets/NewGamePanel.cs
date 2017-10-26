using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGamePanel : MonoBehaviour {

    public Dropdown numPlayerDrop;
    public List<GameObject> playerPanels = new List<GameObject>();
    


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
            newPlayers.Add(new Player(i, getPlayerInfo(i)));
        }
        GameDataScript.instance.Players = newPlayers;
        SceneManager.LoadScene("GameScene");
    }

    public string getPlayerInfo(int index)
    {
        InputField iField = playerPanels[index].GetComponentInChildren<InputField>();
        if(iField.text == "")
        {
            return iField.placeholder.GetComponent<Text>().text;
        }
        return playerPanels[index].GetComponentInChildren<InputField>().text;

    }
}
