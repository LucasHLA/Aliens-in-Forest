using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDCAM : MonoBehaviour
{
    private AudioController audioCTRL;
    public AudioClip earthquake;
    void Start()
    {
        audioCTRL = GameObject.FindObjectOfType<AudioController>().GetComponent<AudioController>();
    }

    public void Earthquake()
    {
        audioCTRL.PlaySFX(earthquake);
    }
}
