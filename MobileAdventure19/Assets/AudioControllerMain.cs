using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerMain : MonoBehaviour
{
    public AudioSource mainMusicLoop;
    public AudioSource pathCorrect;
    public AudioSource pathWrong;
    public AudioSource startGame;
    public AudioSource blockOver;
    public AudioSource blockSpawn;

    public bool playingMML = false;

    // Update is called once per frame
    private void Start()
    {
        mainMusicLoop.enabled = false;
    }

    void Update()
    {
        //Find gamemode script
        GameObject audioController = GameObject.Find("AudioControllerStart");
        AudioController audioControllerScript = audioController.GetComponent<AudioController>();

        if (playingMML)
        {
            mainMusicLoop.enabled = true;
            audioControllerScript.playingMM = false;
        }
    }
}
