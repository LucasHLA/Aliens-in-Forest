using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAudio : MonoBehaviour
{
    private AudioSource audioSRC;
    [SerializeField] private AudioClip voice;
    [SerializeField] private AudioClip deathSound;
    void Start()
    {
        audioSRC = GetComponent<AudioSource>();
    }

    void PlayVoice()
    {
        audioSRC.PlayOneShot(voice);
    }

    void PlayRileyVoice()
    {
        audioSRC.PlayOneShot(deathSound);
    }
}
