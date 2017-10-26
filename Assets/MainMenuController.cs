using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
    public List<GameObject> menuPanels;
    int activePanelIndex = 0;

    public void ChangePanelTo(int index)
    {
        menuPanels[activePanelIndex].SetActive(false);
        menuPanels[index].SetActive(true);
        activePanelIndex = index;
    }
}
