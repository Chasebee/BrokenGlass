using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    BoxCollider2D colid;
    Rigidbody2D rigid;
    Animator anim;
    public float speed, timer;
    void Start() 
    {
        colid = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        rigid.velocity = Vector2.down * speed;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5f) 
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster2")) 
        {
            anim.SetBool("Attack", true);
            // ½ºÅÏ
            int ran = Random.Range(0, 100);
            if (ran >= 70)
            {
                collision.gameObject.GetComponent<Monster2>().mezz[0] = true;
                collision.gameObject.GetComponent<Monster2>().mezz_time[0] = 3.5f;
                GameManager.instance.monster_cc.GetComponent<Mezz_Effect>().mezz_type = 1;
                GameObject mezz_effect = Instantiate(GameManager.instance.monster_cc, collision.gameObject.GetComponent<Monster2>().dmg_pos);
                collision.gameObject.GetComponent<Monster2>().mezz_Effect = mezz_effect;
            }
            rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
            int dmg = (int)(GameManager.instance.atk * -1.2);
            collision.gameObject.GetComponent<Monster2>().mhp += dmg;
            dmg *= -1;
            collision.gameObject.GetComponent<Monster2>().dmg = dmg;
            collision.gameObject.GetComponent<Monster2>().hit = true;

            colid.enabled = false;
        }
    }
    public void Destory_Self() 
    {
        Destroy(gameObject);
    }
}
