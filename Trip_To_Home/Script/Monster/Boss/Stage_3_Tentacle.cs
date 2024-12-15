using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Stage_3_Tentacle : MonoBehaviour
{
    public GameObject origin;
    public bool sw;
    public int atk;
    public bool hit_sw;
    Animator anim;
    SpriteRenderer rend;
    

    void Start()
    {
        hit_sw = true;
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        if (origin.GetComponent<Monster2>().monster_number == 19) 
        {
            atk = origin.GetComponent<Monster2>().atk;
        }
    }
    void Update()
    {
        if (origin != null)
        {
            if (origin.GetComponent<Monster2>().monster_number == 19)
            {
                if (origin.GetComponent<Monster2>().image_Decoy.GetComponent<BoxCollider2D>().enabled == true)
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
                else
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }
    public void Status() 
    {
        if (sw == true)
        {
            anim.SetBool("Stop", true);
        }
        else if (sw == false) 
        {
            anim.SetBool("Stay", true);
            int rnd = Random.Range(2, 12);
            Invoke("Tentacle_Enable", rnd);
        }
        hit_sw = false;
    }
    public void Tentacle_Enable()
    {
        anim.SetBool("Stay", false);
    }
    public void Destroy_Object() 
    {
        if (origin != null)
        {
            if (origin.GetComponent<Monster2>().stack_magic == true)
            {
                origin.GetComponent<Monster2>().stack_magic = false;
            }
            if (origin.GetComponent<Monster2>().magic_type_light == true)
            {
                origin.GetComponent<Monster2>().magic_type_light = false;
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("dmg_area"))
        {
            origin.GetComponent<Monster2>().deal_area[0] = true;
            if (collision.gameObject.GetComponent<Item_Object_5>().deal_delay_time < origin.GetComponent<Monster2>().deal_area_timmer[0])
            {
                origin.GetComponent<Monster2>().mhp -= collision.gameObject.GetComponent<Item_Object_5>().dmg;
                origin.GetComponent<Monster2>().dmg = collision.gameObject.GetComponent<Item_Object_5>().dmg;
                origin.GetComponent<Monster2>().hit = true;
                origin.GetComponent<Monster2>().deal_area_timmer[0] = 0f;
            }
        }

        if (collision.gameObject.CompareTag("Explosion"))
        {
            int damage = collision.gameObject.GetComponent<Item_Object_6_2>().dmg;
            origin.GetComponent<Monster2>().dmg = damage;
            origin.GetComponent<Monster2>().mhp -= damage;
            origin.GetComponent<Monster2>().hit = true;
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

        if (collision.gameObject.CompareTag("Lightning"))
        {
            origin.GetComponent<Monster2>().deal_area[1] = true;
            if (collision.gameObject.GetComponent<Lightning>().timmer <= origin.GetComponent<Monster2>().deal_area_timmer[1])
            {
                int damage = origin.GetComponent<Monster2>().mdef - GameManager.instance.atk;
                if (damage >= 0)
                {
                    damage = -1;
                }
                origin.GetComponent<Monster2>().mhp += damage;
                origin.GetComponent<Monster2>().dmg = damage * -1;
                origin.GetComponent<Monster2>().hit = true;
                origin.GetComponent<Monster2>().deal_area_timmer[1] = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("dmg_area"))
        {
            origin.GetComponent<Monster2>().deal_area[0] = false;
        }
    }

}
