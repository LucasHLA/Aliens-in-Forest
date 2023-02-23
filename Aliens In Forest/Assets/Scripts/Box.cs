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
            eKey.SetActive(false);
            TDGC.instance.boxesOrganized++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
