using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButton : MonoBehaviour {
    public GameObject legend;

    public void ToggleLegend()
    {
        legend.SetActive(!legend.activeSelf);
    }
}
