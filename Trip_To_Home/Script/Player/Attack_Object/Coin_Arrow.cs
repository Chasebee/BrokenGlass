using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Arrow : MonoBehaviour
{
    public int dmg;

    public GameObject arrow_pos;
    public RectTransform arrow_Rect_pos;

    public float speed;
    public float way;

    public float distance;
    public LayerMask isLayer;
    public GameObject coin;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        arrow_pos = GameObject.FindWithTag("arrowpos");
        arrow_Rect_pos = arrow_pos.GetComponent<RectTransform>();
        Invoke("DestoryArrow", 0.5f);
        if (GameObject.FindWithTag("Player").GetComponent<Player_Move>().direction == -1)
        {
            way = 1;
        }
        else if (GameObject.FindWithTag("Player").GetComponent<Player_Move>().direction == 1)
        {
            way = 2;
        }
    }

    void Update()
    {
        speed = GameManager.instance.arrow_speed + 3.3f;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if (ray.collider != null)
        {
            if (ray.collider.CompareTag("Monster2"))
            {
                // 固胶贸府
                if (dmg >= 0)
                {
                    dmg = -1;
                }
                ray.collider.GetComponent<Monster2>().mhp += dmg;
                dmg = dmg * -1;
                ray.collider.GetComponent<Monster2>().dmg = dmg;
                ray.collider.GetComponent<Monster2>().hit = true;
                if (ray.collider.GetComponent<Monster2>().mhp <= 0) 
                {
                    int rnd = Random.Range(0, 100);
                    if (rnd >= 90) 
                    {
                        Instantiate(coin, transform.position, transform.rotation);
                    }
                }
            }

            if (ray.collider.CompareTag("Summon"))
            {
                // 固胶贸府
                if (dmg >= 0)
                {
                    dmg = -1;
                }
                ray.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp += dmg;
                dmg = dmg * -1;
                ray.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = dmg;
                ray.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;
                if (ray.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp <= 0)
                {
                    int rnd = Random.Range(0, 100);
                    if (rnd >= 90)
                    {
                        Instantiate(coin, transform.position, transform.rotation);
                    }
                }
            }
            DestoryArrow();
        }

        if (way == 1)
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
        else if (way == 2)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
    }

    public void DestoryArrow()
    {
        Destroy(gameObject);
    }
}
