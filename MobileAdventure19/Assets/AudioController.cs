using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource musicStart;
    public bool playingMM;
    public bool flag;

    // Update is called once per frame
    void Update()
    {

        if (playingMM && flag == false)
        {
            Playing();
        }

        if (playingMM == false)
        {
            flag = false;
            musicStart.enabled = false;
        }

        if (!musicStart.isPlaying)
        {
            //Find gamemode script
            GameObject audioControllerMain = GameObject.Find("AudioControllerMain");
            AudioControllerMain audioControllerMainScript = audioControllerMain.GetComponent<AudioControllerMain>();

            audioControllerMainScript.playingMML = true;
        }
    }

    void Playing()
    {
        flag = true;
        musicStart.Play();
    }
}
