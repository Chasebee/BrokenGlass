using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_Arrow : MonoBehaviour
{
    public float speed;
    public int atk, num;
    int way;
    public int move_set;
    public GameObject parent_monster;
    public Sprite[] arrow_img;
    public GameObject[] summon_Object;
    Animator anim;
    void Start()
    {
        // (근접) 뱀 번호 수정
        if (num == 7)
        {
            num = 5;
            Collider_Box();
        }
        else if (num == 10)
        {
            num = 6;
            Collider_Circle();
        }
        else if (num == 11)
        {
            num = 7;
            Collider_Circle();
        }
        else if (num == 12)
        {
            num = 8;
            Collider_Box();
        }
        else if (num == 17 || num == 18)
        {
            num = 9;
            Collider_Circle();
        }
        else if (num == 20) 
        {
            num = 10;
            Collider_Box();
        }
        else
        {
            Collider_Circle();
        }
        anim = GetComponent<Animator>();
        anim.SetInteger("Arrow_num", num);
        // 사이즈
        transform.localScale = new Vector3(parent_monster.GetComponent<Monster2>().arrow_size, parent_monster.GetComponent<Monster2>().arrow_size);
        // 발사방향
        if (move_set == -1)
        {
            way = 1;
            if (num == 10) 
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else if (move_set == 1) 
        {
            way = 2;
            if (num == 10)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = arrow_img[num];
        Invoke("Destory_Monster_Arrow", 1.7f);
        
    }

    void Update()
    {
        // 투사체 움직이게 하기 ( 원거리 공격 )
        if (way == 1)
            {
                transform.Translate(transform.right * -1 * speed * Time.deltaTime);
            }
        else if (way == 2)
            {
                transform.Translate(transform.right * speed * Time.deltaTime);
            }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 데미지 ( 보이지 않는 갑옷 적용 / 미적용 )
            if (GameManager.instance.attack_object[18] == false)
            {
                GameManager.instance.hp += GameManager.instance.def - atk;
            }
            else if (GameManager.instance.attack_object[18] == true)
            {
                int rnd = Random.Range(0, 10);
                if (rnd >= 6)
                {
                    GameManager.instance.hp += GameManager.instance.def - (atk / 2);
                }
                else
                {
                    GameManager.instance.hp += GameManager.instance.def - atk;
                }
            }

            // 원거리 타입 뱀
            if (num == 5)
            {
                collision.gameObject.GetComponent<Player_Move>().Poison(4f, 1f, 2);
            }
            // 원거리 타입 스컬 피쉬
            else if (num == 8) 
            {
                collision.gameObject.GetComponent<Player_Move>().Bleeding(5f, 1.2f, 7);
            }
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(transform.position);
            Destory_Monster_Arrow();
        }
        if (GameManager.instance.attack_object[16] == true && (collision.gameObject.CompareTag("Arrow") || collision.gameObject.CompareTag("Laser_Arrow")))
        {
            Destory_Monster_Arrow();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destory_Monster_Arrow();
        }
    }
    public void Destory_Monster_Arrow() 
    {
        // 터지기 전 소환 후 객체 ( 폭발 만든 것 처럼 )
        if (num == 7)
        {
            int start_Angle = 0, end_Angle = 360, angle_Interval = 60;
            for (int fireAngle = start_Angle; fireAngle < end_Angle; fireAngle += angle_Interval)
            {
                summon_Object[0].GetComponent<Monster_Arrow_Summon>().dmg = atk;
                GameObject tempObject = Instantiate(summon_Object[0], gameObject.transform.position, transform.rotation);
                Vector2 dir = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));

                tempObject.transform.right = dir;
                tempObject.transform.position = transform.position;
            }
        }
        else if (num == 9) 
        {
            summon_Object[1].GetComponent<Lava_Slime_Fire>().atk = atk;
            Instantiate(summon_Object[1], transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    public void Collider_Box()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void Collider_Circle()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
