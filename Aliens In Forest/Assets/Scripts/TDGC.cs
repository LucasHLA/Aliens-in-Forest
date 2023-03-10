using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDGC : MonoBehaviour
{
    [Header("Organize Crates Quest")]
    public static TDGC instance;
    public int boxesOrganized;
    public Text totalBoxes;
    public Box box;
    public bool interaction;
    public GameObject quest;
    public GameObject afterQuest;
    public GameObject exit;
    public GameObject gun;
    private Animator camAnimator;
    [SerializeField] private float timer;

    [Header("Audio")]
    private AudioController audioCTRL;
    public AudioClip earthquake;
    void Start()
    {
        instance = this;
        camAnimator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        audioCTRL = GameObject.FindObjectOfType<AudioController>().GetComponent<AudioController>();
    }


    void Update()
    {
        totalBoxes.text = boxesOrganized.ToString();
        if (boxesOrganized == 9)
        {
            quest.SetActive(false);
            camAnimator.SetBool("isShaking", true);
            timer += Time.deltaTime;
            if(timer >= 2f)
            {
                camAnimator.SetBool("isShaking", false);
                
                if(timer >= 3.5f)
                {
                    afterQuest.SetActive(true);
                }
            }
        }

        if(gun.activeInHierarchy)
        {
            exit.SetActive(false);
        }
        else
        {
            exit.SetActive(true);
        }
    }
}

    

