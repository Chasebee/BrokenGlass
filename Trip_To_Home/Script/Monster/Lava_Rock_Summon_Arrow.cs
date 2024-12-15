using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava_Rock_Summon_Arrow : MonoBehaviour
{
    public int type, dmgs;
    public float timmer, time;
    public Sprite[] img;
    public bool dmg_cal;

    BoxCollider2D boxcol;
    Rigidbody2D rigid;
    SpriteRenderer rend;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        boxcol = GetComponent<BoxCollider2D>();

        int random = Random.Range(0, 3);
        rend.sprite = img[random];
        switch (random)
        {
            case 0:
                boxcol.size = new Vector2(0.8f, 0.7f);
                break;
            case 1:
                boxcol.size = new Vector2(0.5f, 0.5f);
                break;
            case 2:
                boxcol.size = new Vector2(0.5f, 0.5f);
                break;
        }

        int x = Random.Range(-4, 4);
        int y  =Random.Range(5, 12);
        rigid.velocity = new Vector2(x, y);
    }

    void Update()
    {
        if (time > timmer) 
        {
            timmer += Time.deltaTime;
        }
        if (timmer > time) 
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && dmg_cal == true) 
        {
            int rnds = Random.Range(0, 101);
            if (rnds >= 71) 
            {
                collision.gameObject.GetComponent<Player_Move>().Stun(1.2f);
            }
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(gameObject.transform.position);
            if (GameManager.instance.attack_object[18] == false)
            {
                int dmg = GameManager.instance.def - dmgs;
                if (dmg >= 1)
                {
                    dmg = 0;
                }
                GameManager.instance.hp += dmg;
            }
            else if (GameManager.instance.attack_object[18] == true)
            {
                int rnd = Random.Range(0, 10);
                if (rnd >= 6)
                {
                    int dmg = GameManager.instance.def - (dmgs / 2);
                    if (dmg >= 1)
                    {
                        dmg = 0;
                    }
                    GameManager.instance.hp += dmg;
                }
                else
                {
                    GameManager.instance.hp += GameManager.instance.def - dmgs;
                }
            }
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            gameObject.layer = 15;
            dmg_cal = false;
        }
    }
}
