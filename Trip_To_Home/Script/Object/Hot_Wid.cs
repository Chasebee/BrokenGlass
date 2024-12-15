using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hot_Wid : MonoBehaviour
{
    public float time;
    Animator anim;
    void Start() 
    {
        anim = GetComponent<Animator>();
        Invoke("Object_On", time);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer == 8) 
        {
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(transform.position);
            GameManager.instance.hp -= (int)(GameManager.instance.maxhp * 0.1);
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(transform.position);
            GameManager.instance.hp -= (int)(GameManager.instance.maxhp * 0.1);
        }
    }

    public void Object_Off() 
    {
        anim.SetBool("Active", false);
        Invoke("Object_On", 5f);
    }
    public void Object_On() 
    {
        anim.SetBool("Active", true);
    }
}
