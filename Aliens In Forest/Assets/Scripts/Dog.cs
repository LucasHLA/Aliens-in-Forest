using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float followDistance;
    [SerializeField] private float idleTimer;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    
    void Update()
    {
        Movement();
        Flip();
        SitBehavior();
    }

    void Movement()
    {
        float distance = Mathf.Abs(transform.position.x - player.position.x);

        if (distance > followDistance)
        {
            float direction = Mathf.Sign(player.position.x - transform.position.x);
            anim.SetInteger("state", 1);
            transform.position = new Vector2(transform.position.x + direction * speed * Time.deltaTime, transform.position.y);
            idleTimer = 0f;
        }
        else if (distance <= followDistance)
        {
            anim.SetInteger("state", 0);
            idleTimer += Time.deltaTime;
        }
    }

    void Flip()
    {
        if(transform.position.x < player.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if(transform.position.x > player.position.x)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    IEnumerator SitTransition()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("state", 3);
        
    }
   

    void SitBehavior()
    {
        if(anim.GetInteger("state") == 0 && idleTimer >= 4)
        {  
            anim.SetInteger("state", 2);
            StartCoroutine(SitTransition());
        }
    }
}
