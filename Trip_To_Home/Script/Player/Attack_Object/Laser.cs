using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Animator anim;
    GameObject player;
    float direct;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        direct = player.GetComponent<Player_Move>().direction;
        if (direct == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direct == -1) 
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        Invoke("Destroy_self", 0.233f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster2"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            int dmg = 0;
            if (GameManager.instance.attack_object[23] == false)
            {
                dmg = (int)(GameManager.instance.atk * GameManager.instance.attack_object_cnt[22]);
            }
            else 
            {
                dmg = (int)(GameManager.instance.atk * GameManager.instance.attack_object_cnt[22]) * 2;
            }
            if (dmg <= 0) 
            {
                dmg = 1;
            }
            collision.gameObject.GetComponent<Monster2>().mhp -= dmg;
            collision.gameObject.GetComponent<Monster2>().dmg = dmg;
            collision.gameObject.GetComponent<Monster2>().hit = true;
        }


        if (collision.gameObject.CompareTag("Summon"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            int dmg = 0;
            if (GameManager.instance.attack_object[23] == false)
            {
                dmg = (int)(GameManager.instance.atk * GameManager.instance.attack_object_cnt[22]);
            }
            else
            {
                dmg = (int)(GameManager.instance.atk * GameManager.instance.attack_object_cnt[22]) * 2;
            }
            if (dmg <= 0)
            {
                dmg = 1;
            }
            collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp -= dmg;
            collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = dmg;
            collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;
        }
    }

    public void Destroy_self()
    {
        Destroy(gameObject);
    }
}
