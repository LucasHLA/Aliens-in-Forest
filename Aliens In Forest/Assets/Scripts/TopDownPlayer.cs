using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayer : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rigid;
    [SerializeField] private AudioClip woodStep;
    [SerializeField] private float speed;
    private AudioController audioCTRL;

    Vector2 movement;
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        audioCTRL = GameObject.FindObjectOfType<AudioController>().GetComponent<AudioController>();
    }

    
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.magnitude);

        if(movement != Vector2.zero)
        {
            anim.SetFloat("HorizontalIdle", movement.x);
            anim.SetFloat("VerticalIdle", movement.y);
        }
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + movement.normalized * speed * Time.deltaTime);
    }

    public void Step()
    {
        audioCTRL.PlaySFX(woodStep);
    }
}
