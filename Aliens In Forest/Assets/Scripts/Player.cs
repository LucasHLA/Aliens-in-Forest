using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;

    public int health;
    [SerializeField] private float healingTimer;
    [SerializeField] private float recoveryTime;
    [SerializeField] private float speed;
    [SerializeField] private bool isShooting;
    [SerializeField] private bool isRunning;
    [SerializeField] private bool recovery;
    public bool takeDMG;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private HealthBar healthBar;
    private GameObject dog;

    [Header("Audio Related")]
    public AudioClip step;
    public AudioClip shot;
    private AudioController audioCtrl;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        takeDMG = true;
        healthBar.setMaxHealth(health);
        audioCtrl = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioController>();
        
    }

    
    void FixedUpdate()
    {  
       Move();
    }

    private void Update()
    {
        Shooting();
        healthBar.setHealth(health);
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

        recoveryTime += Time.deltaTime;

        if(recoveryTime >= 1f && takeDMG)
        {
            health -= dmg;
            anim.SetTrigger("hit");
            recoveryTime = 0;
        }

        if (health <= 0 && !recovery)
        {
            Death();
            GameController.instance.ShowGameOver();
            Destroy(this.gameObject, 1f);
            Destroy(GameObject.FindGameObjectWithTag("dog").gameObject, 1f);
        }
    }

    private void Death()
    {
        anim.SetTrigger("dead");
        speed = 0;
        
    }

    private void Steps()
    {
        audioCtrl.PlaySFX(step);
    }

    private void Shot()
    {
        audioCtrl.PlaySFX(shot);
    }

    public void Healing()
    {
        healingTimer += Time.deltaTime;

        if (healingTimer >= 1.7f && health < 10)
        {
            health++;
            healingTimer = 0f;
        }
    }
}
