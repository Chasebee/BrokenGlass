using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Arrow : MonoBehaviour
{

    public float speed, life;
    public int dir, dmg;

    public void Start()
    {
        Invoke("Destroy_Self", life);
    }

    void Update()
    {
        // ¿ÞÂÊ
        if (dir == -1) 
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
        // ¿À¸¥ÂÊ
        if (dir == 1) 
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.hp -= (int)(GameManager.instance.maxhp * 0.07);
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(gameObject.transform.position);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy_Self();
        }
    }

    public void Destroy_Self()
    {
        Destroy(gameObject);
    }
}
