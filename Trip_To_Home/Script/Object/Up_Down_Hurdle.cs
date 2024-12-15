using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Up_Down_Hurdle : MonoBehaviour
{
    public Transform loca;
    public int up_down_chk;
    public float up_power;
    Rigidbody2D rigid;
    SpriteRenderer sprite;

    void Start() 
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (rigid.velocity.y > 0)
        {
            sprite.flipY = false;
        }
        else
        {
            sprite.flipY = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("patrol"))
        {
            rigid.velocity = Vector2.up * up_power;
        }
        else if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer == 8)
        {
            GameManager.instance.hp -= (int)(GameManager.instance.maxhp * 0.1f);
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(gameObject.transform.position);
        }
    }
}
