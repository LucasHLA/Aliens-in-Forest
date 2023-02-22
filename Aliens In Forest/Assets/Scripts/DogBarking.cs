using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBarking : MonoBehaviour
{

    private AudioController audioSFX;

    [Header("Dog Sound Related")]
    private int dogIndex;
    [SerializeField] private AudioClip bark;
    private bool isPlaying;

    void Start()
    {
        audioSFX = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        dogIndex = GameObject.FindGameObjectWithTag("dog").GetComponent<NPC>().index;
        Barking();
    }

    public void Barking()
    {

        if (dogIndex == 1 && !isPlaying)
        {
            audioSFX.PlaySFX(bark);
            isPlaying = true;
        }

        if (dogIndex == 0)
        {
            isPlaying = false;
        }
    }
}
