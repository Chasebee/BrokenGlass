using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Attack : MonoBehaviour
{
    public int num;
    public RectTransform[] attack_Pos;
    Animator anim;
    public int dir;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("cnt", num);
    }

    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster2")) 
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            int dmg = collision.gameObject.GetComponent<Monster2>().mdef - (int)(GameManager.instance.atk * 2.3f);
            if (dmg > 0) 
            {
                dmg = 1;
            }
            collision.gameObject.GetComponent<Monster2>().mhp += dmg;
            dmg *= -1;
            collision.gameObject.GetComponent<Monster2>().dmg = dmg;
            collision.gameObject.GetComponent<Monster2>().hit = true;
        }
    }

    public void Destroy_Self()
    {
        Destroy(gameObject);
    }
}
