using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item_Object_2 : MonoBehaviour
{
    public int dmg;

    public GameObject arrow_pos;
    public RectTransform arrow_Rect_pos;

    public float speed;
    public float way;

    public float distance;
    public LayerMask isLayer;

    void Start()
    {
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
        speed = GameManager.instance.arrow_speed - 2.3f;
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if (ray.collider != null)
        {
            if (ray.collider.CompareTag("Monster2"))
            {
                // �̽�ó��
                if (dmg >= 0)
                {
                    dmg = -1;
                }
                ray.collider.GetComponent<Monster2>().mhp += dmg;
                dmg = dmg * -1;
                ray.collider.GetComponent<Monster2>().dmg = dmg;
                ray.collider.GetComponent<Monster2>().hit = true;
            }

            if (ray.collider.CompareTag("Summon")) 
            {
                // �̽�ó��
                if (dmg >= 0)
                {
                    dmg = -1;
                }
                ray.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp += dmg;
                dmg = dmg * -1;
                ray.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = dmg;
                ray.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;
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
