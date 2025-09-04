using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    [SerializeField] private PlayerController Player;

    [SerializeField] private AudioSource walkingAudio;

    private bool alreadyPlaying;
    // Start is called before the first frame update
    void Start()
    {
        alreadyPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.walking)
        {
            PlayAudio("Walking");
        }
        else
        {
            StopAudio("Walking");
        }
    }

    void PlayAudio(string audioname)
    {
        if (audioname == "Walking" && !alreadyPlaying)
        {
            walkingAudio.Play();
            alreadyPlaying = true;
        }
    }

    void StopAudio(string audioname)
    {
        if (audioname == "Walking" && alreadyPlaying)
        {
            walkingAudio.Stop();
            alreadyPlaying = false;
        }
    }

    
}
