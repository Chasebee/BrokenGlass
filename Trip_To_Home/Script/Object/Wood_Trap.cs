using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood_Trap : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    public float knockback;
    public int rotation;
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        anim.SetInteger("Rotation", rotation);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Move>().Stun(1.5f);
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(transform.position);
            GameManager.instance.hp -= (int)(GameManager.instance.maxhp * 0.1);
        }
    }
}
