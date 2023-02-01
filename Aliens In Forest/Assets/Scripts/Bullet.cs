using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;
    [SerializeField] private float speed;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rigid.velocity = transform.right * speed;
        Destroy(this.gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            anim.SetTrigger("hit");
            collision.gameObject.GetComponent<Enemy>().Hit(2);
            Destroy(this.gameObject, .2f);
        }
    }
}
