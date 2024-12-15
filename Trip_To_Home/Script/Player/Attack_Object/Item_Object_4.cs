using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item_Object_4 : MonoBehaviour
{
    public GameObject arrow_pos;
    public RectTransform arrow_Rect_pos;

    public float speed;
    public float way;
    public LayerMask isLayer;

    RaycastHit2D collision;

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
        collision = Physics2D.BoxCast(gameObject.transform.position, gameObject.transform.localScale, 0, Vector2.left, 0, isLayer);
        if (collision.collider != null)
        {
            if (collision.collider.CompareTag("Monster2"))
            {
                int dmg = (int)(GameManager.instance.atk * 0.65);
                if (dmg <= 0)
                {
                    dmg = 1;
                }
                collision.collider.GetComponent<Monster2>().mhp -= dmg;
                collision.collider.GetComponent<Monster2>().dmg = dmg;
                collision.collider.GetComponent<Monster2>().hit = true;
                // CC±â
                collision.collider.GetComponent<Monster2>().mezz[0] = true;
                collision.collider.GetComponent<Monster2>().mezz_time[0] = GameManager.instance.attack_object_cnt[4];
            }

            if (collision.collider.CompareTag("Summon"))
            {
                int dmg = (int)(GameManager.instance.atk * 0.65);
                if (dmg <= 0)
                {
                    dmg = 1;
                }
                Debug.Log(dmg);
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp -= dmg;
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = dmg;
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;
                // CC±â
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mezz[0] = true;
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mezz_time[0] = GameManager.instance.attack_object_cnt[4];
            }
            DestoryArrow();
        }
        speed = GameManager.instance.arrow_speed - 1.5f;
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
