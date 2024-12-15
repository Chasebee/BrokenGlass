using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_Object : MonoBehaviour
{
    public GameObject[] objts;
    public int dmg;
    public int dir;
    Animator anim;
    Rigidbody2D rigid;
    BoxCollider2D boxcol;
    void Start() 
    {
        rigid = GetComponent<Rigidbody2D>();
        boxcol = GetComponent<BoxCollider2D>();
        objts[0].GetComponent<Lava_Rock_Summon_Arrow>().dmgs = dmg;
        objts[1].GetComponent<Lava_Slime_Fire>().atk = dmg;
        if (dir == 1)
        {
            rigid.velocity = Vector2.left * 10f;
        }
        else if(dir == -1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rigid.velocity = Vector2.right * 10f;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(gameObject.transform.position);
            GameManager.instance.hp += GameManager.instance.def - dmg;
            Destroy_Object();
        }
        if (collision.gameObject.CompareTag("Ground")) 
        {
            Destroy_Object();
        }
    }

    public void Destroy_Object() 
    {
        int num = Random.Range(5, 9);
        for (int i = 0; i < num; i++) 
        {
            Instantiate(objts[0], transform.position, transform.rotation);
        }
        Instantiate(objts[1], transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
