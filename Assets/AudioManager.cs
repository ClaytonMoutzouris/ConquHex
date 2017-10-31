using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    Color32 colorOn = new Color32(38, 45, 82, 255);
    Color32 colorOff = new Color32(178, 178, 178, 255);
    public List<AudioClip> tunes;
    public List<AudioClip> soundFX;
    static bool musicState = true;
    public static AudioManager current;

    // Use this for initialization
    void Start()
    {
        if(current != null)
        {
            Destroy(this);
        }

        current = this;
        DontDestroyOnLoad(gameObject);

        if (musicState == false)
            ToggleMute();
    }
     
    public void ToggleMute()
    {
        if (GetComponent<AudioSource>().isPlaying)
        {
            // Someone hates MY music?!?

            musicState = false;
            GetComponent<AudioSource>().Stop();
            //GameObject.Find("MusicPlayingIcon").GetComponent<Image>().color = colorOff;

        }
        else
        {
            musicState = true;
            GetComponent<AudioSource>().Play();
            //GameObject.Find("MusicPlayingIcon").GetComponent<Image>().color = colorOn;

        }
    }

    public void ChangeTunes(int newtune)
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = tunes[newtune];
        GetComponent<AudioSource>().Play();


    }

}
