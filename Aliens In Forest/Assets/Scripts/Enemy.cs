using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private float maxVision;
    [SerializeField] private float stopDistance;


    private Rigidbody2D rigid;
    private Animator anim;

    [SerializeField] private Transform front;
    [SerializeField] private Transform back;

    private Vector2 direction;
    private bool isFront;
    private bool isRight;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        
    }

    private void GetPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(front.position, direction, maxVision);

        if(hit.collider != null)
        {
            if (hit.transform.CompareTag("Player"))
            {
                anim.SetInteger("state", 1);
                isFront = true;
                float distance = Vector2.Distance(transform.position, hit.transform.position);

                if(distance <= stopDistance)
                {
                    isFront = false;
                    rigid.velocity = Vector2.zero;
                    anim.SetInteger("state", 2);
                    hit.transform.GetComponent<Player>().Hit(1);
                }
            }
        }

        RaycastHit2D behindHit = Physics2D.Raycast(back.position, -direction, maxVision);

        //continue the back raycast here
    }

    public void Hit(int dmg)
    {
        health -= dmg;
        anim.SetTrigger("hit");

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        anim.SetTrigger("dead");
        speed = 0;
        Destroy(this.gameObject, .7f);
    }

}
