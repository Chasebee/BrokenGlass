using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item_Object_6 : MonoBehaviour
{
    public GameObject arrow_pos, target, explosion_circle;
    public RectTransform arrow_Rect_pos;
    public Transform target_pos;
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

        // 레이캐스트 - 박스캐스트
        collision = Physics2D.BoxCast(gameObject.transform.position, gameObject.transform.localScale, 0, Vector2.left, 0, isLayer);
        if (collision.collider != null)
        {
            if (collision.collider.CompareTag("Monster2"))
            {
                target = collision.collider.gameObject;
                target_pos = target.GetComponent<Transform>();
                explosion_circle.GetComponent<Item_Object_6_1>().type = 0;
                Instantiate(explosion_circle, target_pos);

                int dmg = collision.collider.gameObject.GetComponent<Monster2>().mdef - GameManager.instance.atk;
                if (dmg >= 0)
                {
                    dmg = -1;
                }
                collision.collider.gameObject.GetComponent<Monster2>().mhp += dmg;
                dmg *= -1;
                collision.collider.gameObject.GetComponent<Monster2>().dmg = dmg;
                collision.collider.gameObject.GetComponent<Monster2>().hit = true;
            }

            if (collision.collider.CompareTag("Summon")) 
            {
                target = collision.collider.gameObject;
                target_pos = target.GetComponent<Transform>();
                explosion_circle.GetComponent<Item_Object_6_1>().type = 0;
                Instantiate(explosion_circle, target_pos);

                int dmg = collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mdef - GameManager.instance.atk;
                if (dmg >= 0)
                {
                    dmg = -1;
                }
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp += dmg;
                dmg *= -1;
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = dmg;
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;
            }
            DestoryArrow();
        }
        speed = GameManager.instance.arrow_speed + 2.3f;
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
