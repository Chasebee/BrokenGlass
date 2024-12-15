using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_arrow : MonoBehaviour
{
    private Rigidbody2D rigid;
    public int num, dmg;
    public float speed = 10f;
    public float timmer, time;
    public bool boss;
    public Sprite[] img;
    SpriteRenderer ren;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        ren = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = speed * transform.right;

        if (num == 10)
        {
            num = 0;
            Circle_On();
        }
        else if (num == 14) 
        {
            num = 1;
            Box_On();
            time = 1.2f;
        }
        anim.SetInteger("Num", num);
        gameObject.GetComponent<SpriteRenderer>().sprite = img[num];
        
    }

    void Update()
    {
        timmer += Time.deltaTime;
        if (timmer >= time) 
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(gameObject.transform.position);
            GameManager.instance.hp += GameManager.instance.def - dmg;
        }
        if (collision.gameObject.CompareTag("Ground")) 
        {
            if (boss == false) 
            {
                Destroy(gameObject);
            }
        }
    }

    public void Circle_On()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void Box_On()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
