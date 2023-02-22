using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSRC;
    [SerializeField] private float volume;

    void Start()
    {
        audioSRC = GetComponent<AudioSource>();
    }
    void Update()
    {
        audioSRC.volume = volume;

    }

    public void PlaySFX(AudioClip sfx)
    {
        audioSRC.PlayOneShot(sfx);
        
    }

    public void stopPlaying()
    {
        audioSRC.Stop();
    }

    
    
}
