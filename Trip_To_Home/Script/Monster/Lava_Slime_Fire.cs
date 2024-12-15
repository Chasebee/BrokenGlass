using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lava_Slime_Fire : MonoBehaviour
{
    public int atk;

    public float distance;
    public float[] chase_scale;
    public LayerMask isLayer;

    RaycastHit2D col;
    public BoxCollider2D boxcolid;


    public void Destroy_Object() 
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(gameObject.transform.position);
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
            gameObject.layer = 15;
        }
    }
}
