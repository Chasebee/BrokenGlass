using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Melee : MonoBehaviour
{
    public int atk;
    public int number;
    public int dir;
    Animator anim;
    BoxCollider2D col;
    public GameObject parent_monster;
    public RectTransform monster_pos;
    public GameObject[] arrow_Summon;
    int count;
    private void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        anim.SetInteger("Arrow_num", number);

        if (dir == 1) 
        {
            transform.eulerAngles = new Vector3(0, -180);
        }
        transform.localScale = new Vector3(parent_monster.GetComponent<Monster2>().arrow_size, parent_monster.GetComponent<Monster2>().arrow_size);
    }
    public void Collider_On() 
    {
        col.enabled = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.attack_object[18] == false)
            {
                int dmg = GameManager.instance.def - atk;
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
                    int dmg = GameManager.instance.def - (atk / 2);
                    if (dmg >= 1)
                    {
                        dmg = 0;
                    }
                    GameManager.instance.hp += dmg;
                }
                else
                {
                    GameManager.instance.hp += GameManager.instance.def - atk;
                }
            }
            if (number == 13)
            {
                int ran = Random.Range(0, 100);
                if (ran >= 70)
                {
                    collision.gameObject.GetComponent<Player_Move>().Stun(2.6f);
                }
            }
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(transform.position);
            // 더이상 상호작용 안함
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void Summon_Arrow() 
    {
        if (number == 16) 
        {
            int rnd = Random.Range(2, 5);

            for (int i = 0; i < rnd; i++)
            {
                arrow_Summon[0].GetComponent<Lava_Rock_Summon_Arrow>().dmgs = atk;
                Instantiate(arrow_Summon[0], gameObject.transform.position, transform.rotation);
            }
        }
    }
    public void Object_Destroy() 
    {
        Destroy(gameObject);
    }

}
