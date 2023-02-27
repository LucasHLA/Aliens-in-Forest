using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    [Header("Collider related")]
    public float radius;
    public LayerMask playerLayer;
    public bool canInteract;
    public GameObject eKey;
    public int boxOrganized;

    [Header("Audio")]
    private AudioController audioCTRL;
    public AudioClip boxSound;

    private void Start()
    {
        audioCTRL = GameObject.FindObjectOfType<AudioController>().GetComponent<AudioController>();
    }
    void Update()
    {
        if (canInteract)
        {
            eKey.SetActive(true);
        }
        else
        {
            eKey.SetActive(false);
        }
        Clean();


    }

    private void FixedUpdate()
    {
        Collider2D box = Physics2D.OverlapCircle(this.transform.position, radius, playerLayer);

        if (box != null)
        {
            canInteract = true;
        }
        else
        {
            canInteract = false;
        }
    }
    

    public void Clean()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(this.gameObject);
            audioCTRL.PlaySFX(boxSound);
            eKey.SetActive(false);
            GameObject.FindGameObjectWithTag("Controller").GetComponent<TDGC>().boxesOrganized+=3;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
