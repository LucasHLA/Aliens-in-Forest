using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;

    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private bool isShooting;
    [SerializeField] private bool isRunning;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Shooting();
        
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        rigid.velocity = new Vector2(horizontal * speed, rigid.velocity.y);

        if(horizontal > 0)
        {
            if (!isShooting)
            {
                anim.SetInteger("state", 1);
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(horizontal < 0)
        {
            if (!isShooting)
            {
                anim.SetInteger("state", 1);
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(horizontal == 0)
        {
            if (!isShooting)
            {
                anim.SetInteger("state", 0);
            }
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    IEnumerator OnShooting()
    {
        yield return new WaitForSeconds(.4f);
        isShooting = false;
    }

    IEnumerator PreShooting()
    {
        yield return new WaitForSeconds(.4f);
        Shoot();
    }

    private void Shooting()
    {
        if( Input.GetButtonDown("Fire1") && !isShooting)
        {
            anim.SetInteger("state", 2);
            isShooting = true;
            StartCoroutine(PreShooting());
            StartCoroutine(OnShooting());   
        }
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
        
    }
}
