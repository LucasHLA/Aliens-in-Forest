using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : MonoBehaviour
{
    private AudioSource audioSRC;
    [Header("Owl SFX Related")]
    [SerializeField] private AudioClip[] owlSoundFXS;
    private int randomOwlSFX;
    [SerializeField] private float owlTimer;
    void Start()
    {
        audioSRC = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        owlTimer += Time.deltaTime;
        randomOwlSFX = Random.Range(0, owlSoundFXS.Length);
        Hool();
    }

    private void Hool()
    {
        if(owlTimer >= 15f)
        {
            audioSRC.clip = owlSoundFXS[randomOwlSFX];
            audioSRC.Play();
            owlTimer = 0f;
        }
    }
}
