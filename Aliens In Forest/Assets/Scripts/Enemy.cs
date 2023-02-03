using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxVision;
    [SerializeField] private float stopDistance;
    [SerializeField] private bool isFront;
    [SerializeField] private bool isRight;
    [SerializeField] private float speed;
    [SerializeField] private int health;

    [SerializeField] private Transform front;
    [SerializeField] private Transform back;
    private Rigidbody2D rigid;
    private Animator anim;


    private Vector2 direction;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (isRight)
        {
            transform.eulerAngles = new Vector2(0, 0);
            direction = Vector2.right;

        }

        else
        {
            transform.eulerAngles = new Vector2(0, 180);
            direction = Vector2.left;

        }
    }

    private void FixedUpdate()
    {
        GetPlayer();
        Movement();
    }

    private void GetPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(front.position, direction, maxVision);

        if (hit.collider != null)
        {

            if (hit.transform.CompareTag("Player"))
            {

                anim.SetInteger("state", 1);
                isFront = true;
                float distance = Vector2.Distance(transform.position, hit.transform.position);

                if (distance <= stopDistance)
                {
                    Debug.Log("Attacking");
                    isFront = false;
                    rigid.velocity = Vector2.zero;
                    anim.SetInteger("state", 2);
                    hit.transform.GetComponent<Player>().Hit(2);

                    if(hit.transform.GetComponent<Player>().health <= 0)
                    {
                        anim.SetInteger("state", 0);
                        hit.transform.GetComponent<Player>().takeDMG = false;
                    }
                }
                
            }
        }


        RaycastHit2D behindHit = Physics2D.Raycast(back.position, -direction, maxVision);

        if (behindHit.collider != null)
        {
            if (behindHit.transform.CompareTag("Player"))
            {
                isRight = !isRight;
                isFront = true;
            }

        }

        if (hit.collider == null && behindHit.collider == null)
        {

            anim.SetInteger("state", 0);
            isFront = false;
        }

    }

    private void Movement()
    {
        if (isFront)
        {
            anim.SetInteger("state", 1);
            if (isRight)
            {
                transform.eulerAngles = new Vector2(0, 0);
                direction = Vector2.right;
                rigid.velocity = new Vector2(speed, rigid.velocity.y);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
                direction = Vector2.left;
                rigid.velocity = new Vector2(-speed, rigid.velocity.y);
            }
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(front.position, direction * maxVision);
        Gizmos.DrawRay(back.position, -direction * maxVision);
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
        rigid.velocity = Vector2.zero;
        Destroy(this.gameObject, 0.6f);

    }
}
