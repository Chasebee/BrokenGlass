using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_2_Sun_Effect : MonoBehaviour
{
    public Animator anim;
    public float dmg_delay, dmg_deal, time, timmer;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update() 
    {
        if (anim.GetBool("Effect") == true) 
        {
            if (time >= timmer) 
            {
                timmer += Time.deltaTime;
                dmg_delay += Time.deltaTime;
            }

            if (dmg_delay >= dmg_deal) 
            {
                GameObject.FindWithTag("Player").GetComponent<Player_Move>().PlayerHit(GameObject.FindWithTag("Player").transform.position);
                GameManager.instance.hp -= (int)(GameManager.instance.maxhp * 0.1);
                dmg_delay = 0f;
            }

            if (timmer >= time) 
            {
                anim.SetBool("Effect", false);
                timmer = 0f;
            }
        }
    }

    public void Image_Set() 
    {
        gameObject.GetComponent<Image>().enabled = false;
    }
}
