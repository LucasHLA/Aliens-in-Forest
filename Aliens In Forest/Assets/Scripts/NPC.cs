using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPC : MonoBehaviour
{
    [Header("Basic Dialogue")]
    public GameObject dialogueObj;
    public Text dialogueText;
    public Text nameText;

    [Header("Array Dialogue")]
    public string[] sentences;
    public string[] names;
    public float radius;
    public LayerMask playerLayer;
    public bool playerIsClose;
    public int index;
    public float typingSpeed;

    private void Start()
    {
        dialogueObj.SetActive(false);
        index = 0;
    }
    private void Update()
    {
        Interact();
        StartTalking();

        if(index < sentences.Length)
        {
            nextLine();
        }

        nameText.text = names[index];
    }

    public void StartTalking()
    {
        if(playerIsClose && Input.GetKeyDown(KeyCode.E))
        {
            dialogueObj.SetActive(true);
            StartCoroutine(Typing());
        }
    }

    public void nextLine()
    {
        if (dialogueObj.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                index++;
                dialogueText.text = "";
                nameText.text = "";
                StartCoroutine(Typing());
            }
            
            
            if (index >= sentences.Length)
            {
                zeroText();
            }
        }
    }

    private void zeroText()
    {
        index = 0;
        dialogueText.text = " ";
        nameText.text = " ";
        dialogueObj.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if(hit != null)
        {
            playerIsClose = true;
        }
        else
        {
            playerIsClose = false;
            dialogueObj.SetActive(false);
            index = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
