using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas_Trap : MonoBehaviour
{
    public float time, timmer;
    BoxCollider2D box;
    Rigidbody2D rigid;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (timmer < time)
        {
            timmer += Time.deltaTime;
        }

        if (timmer >= time)
        {
            anim.SetBool("Trap_On", true);
            box.enabled = true;       
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<Player_Move>().Poison(5, 0.9f, (int)(GameManager.instance.maxhp * 0.05));
        }
    }
    public void Trap_Off() 
    {
        anim.SetBool("Trap_On", false);
        timmer = 0f;
        box.enabled = false;
    }
}
