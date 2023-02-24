using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    private int boxes;
    public GameObject eKey;
    private GameObject afterQuest;
    void Start()
    {
        boxes = GameObject.FindGameObjectWithTag("Controller").GetComponent<TDGC>().boxesOrganized;
        afterQuest = GameObject.FindGameObjectWithTag("Controller").GetComponent<TDGC>().afterQuest;
    }

   
    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(this.transform.position, radius, playerLayer);

        if(hit != null)
        {
            Debug.Log(boxes);
            if(boxes == 9)
            {
                eKey.SetActive(true);
                Debug.Log("IT worked with the E object");
            }
            else
            {
                eKey.SetActive(false);
            }

            if(afterQuest.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
            {
                Destroy(this.gameObject);
                eKey.SetActive(false);
            }
            
        }
        else
        {
            eKey.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
