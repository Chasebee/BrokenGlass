using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Duality_Arrow : MonoBehaviour
{
    public GameObject arrow_pos;
    public RectTransform arrow_Rect_pos;
    public Sprite[] arrow_img;
    public float speed;
    public float way;
    public LayerMask isLayer;

    private int dmg;
    SpriteRenderer rend;
    RaycastHit2D collision;
    void Start()
    {
        arrow_pos = GameObject.FindWithTag("arrowpos");
        arrow_Rect_pos = arrow_pos.GetComponent<RectTransform>();
        rend = GetComponent<SpriteRenderer>();
        Invoke("DestoryArrow", 0.5f);
        
        if (GameObject.FindWithTag("Player").GetComponent<Player_Move>().direction == -1)
        {
            way = 1;
        }
        else if (GameObject.FindWithTag("Player").GetComponent<Player_Move>().direction == 1)
        {
            way = 2;
        }

        // 이미지 설정
        if (GameManager.instance.active_buff[1] == true)
        {
            rend.sprite = arrow_img[0];
        }
        else 
        {
            rend.sprite = arrow_img[1];
        }

    }

    void Update()
    {
        speed = GameManager.instance.arrow_speed - 1.3f;

        // 레이캐스트 - 박스캐스트
        collision = Physics2D.BoxCast(gameObject.transform.position, gameObject.transform.localScale, 0, Vector2.left, 0, isLayer);
        if (collision.collider != null)
        {
            if (collision.collider.CompareTag("Monster2"))
            {
                if (GameManager.instance.active_buff[1] == true)
                {
                    dmg = (int)(GameManager.instance.atk * 2.35);
                }
                else if (GameManager.instance.active_buff[1] == false)
                {
                    dmg = GameManager.instance.atk;
                    int heal = (int)(GameManager.instance.maxhp * 0.015);
                    if (heal <= 0)
                    {
                        heal = 1;
                    }
                    GameManager.instance.hp += heal;
                }
                collision.collider.GetComponent<Monster2>().mhp -= dmg;
                collision.collider.GetComponent<Monster2>().dmg = dmg;
                collision.collider.GetComponent<Monster2>().hit = true;
                // CC기
                collision.collider.GetComponent<Monster2>().mezz[0] = true;
                collision.collider.GetComponent<Monster2>().mezz_time[0] = GameManager.instance.attack_object_cnt[4];
            }
            if (collision.collider.CompareTag("Summon")) 
            {
                if (GameManager.instance.active_buff[1] == true)
                {
                    dmg = (int)(GameManager.instance.atk * 2.35);
                }
                else if (GameManager.instance.active_buff[1] == false)
                {
                    dmg = GameManager.instance.atk;
                    int heal = (int)(GameManager.instance.maxhp * 0.015);
                    if (heal <= 0)
                    {
                        heal = 1;
                    }
                    GameManager.instance.hp += heal;
                }
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp -= dmg;
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = dmg;
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;
                // CC기
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mezz[0] = true;
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mezz_time[0] = GameManager.instance.attack_object_cnt[4];
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
