using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class arrow : MonoBehaviour
{
    public float speed;
    int way;
    public GameObject arrow_pos;
    public RectTransform arrow_Rect_pos;
    public GameObject[] arrow_Create;
    public float distance;

    public GameObject induce;
    public Transform target_pos;
    public bool induce_target;
    public LayerMask isLayer;

    RaycastHit2D col;

    void Start()
    {
        arrow_pos = GameObject.FindWithTag("arrowpos");
        arrow_Rect_pos = arrow_pos.GetComponent<RectTransform>();
        Invoke("DestoryArrow", 0.5f);
        // 사이즈 설정
        Vector3 scale = new Vector3(GameManager.instance.arrow_size, GameManager.instance.arrow_size);
        // 실제 사이즈 적용
        transform.localScale = scale;
        // 발사 방향
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
        // 사이즈 설정
        Vector3 scale = new Vector3(GameManager.instance.arrow_size, GameManager.instance.arrow_size);
        // 레이캐스트 - 박스캐스트
        col = Physics2D.BoxCast(gameObject.transform.position, scale, 0, Vector2.left, 0, isLayer);

        // 실제 사이즈 적용
        transform.localScale = scale;
        // 속도
        speed = GameManager.instance.arrow_speed;

        // 투사체 움직이게 하기
        if (way == 1 && GameManager.instance.luna == false)
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
        else if (way == 2 && GameManager.instance.luna == false)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }

        if (way == 1 && GameManager.instance.luna == true)
        {
            if (induce_target == false)
            {
                transform.Translate(transform.right * -1 * speed * Time.deltaTime);
            }
            // 유도
            else if (induce_target == true)
            {
                Vector3 direction = target_pos.position - transform.position;
                direction.Normalize();
                transform.position += direction * speed * Time.deltaTime;
            }
        }
        else if (way == 2 && GameManager.instance.luna == true)
        {
            if (induce_target == false)
            {
                transform.Translate(transform.right * speed * Time.deltaTime);
            }
            // 유도
            else if (induce_target == true)
            {
                Vector3 direction = target_pos.position - transform.position;
                direction.Normalize();
                transform.position += direction * speed * Time.deltaTime;
            }
        }

        // 충돌 판정
        if (col.collider != null)
        {
            // 기본 판정
            if (col.collider.CompareTag("Monster2"))
            {
                int dmg = col.collider.GetComponent<Monster2>().mdef - GameManager.instance.atk;

                if (dmg >= 0)
                {
                    dmg = -1;
                }
                col.collider.GetComponent<Monster2>().mhp += dmg;
                dmg *= -1;
                col.collider.GetComponent<Monster2>().hit = true;
                col.collider.GetComponent<Monster2>().dmg = dmg;
                // 독성 탄창
                if (GameManager.instance.attack_object[6] == true)
                {
                    col.collider.GetComponent<Monster2>().mezz[1] = true;
                    col.collider.GetComponent<Monster2>().mezz_time[1] = 5f;
                }
                // 폭발 탄창
                if (GameManager.instance.attack_object[8] == true)
                {
                    GameObject par = col.collider.gameObject;
                    Transform par_pos = par.GetComponent<Transform>();
                    arrow_Create[0].GetComponent<Item_Object_6_1>().type = 1;
                    Instantiate(arrow_Create[0], par_pos);
                }
                // 중첩 마력
                if (GameManager.instance.attack_object[24] == true) 
                {
                    GameObject par = col.collider.gameObject;
                    Transform par_pos = par.GetComponent<Transform>();
                    // 생성 혹은 스택 증가
                    if (col.collider.gameObject.GetComponent<Monster2>().stack_magic == false)
                    {
                        col.collider.gameObject.GetComponent<Monster2>().stack_magic = true;
                        arrow_Create[1].GetComponent<Stack_Magic>().type = 0;
                        arrow_Create[1].GetComponent<Stack_Magic>().parent_pos = col.collider.gameObject;
                        Instantiate(arrow_Create[1], par_pos);
                    }
                    else if(col.collider.gameObject.GetComponent<Monster2>().stack_magic == true) 
                    {
                        col.collider.gameObject.GetComponent<Monster2>().stack_magic_cnt++;
                    }
                }
                // 마력부여 : 빛
                if (GameManager.instance.attack_object[27] == true)
                {
                    GameObject par = col.collider.gameObject;
                    Transform par_pos = par.GetComponent<Transform>();
                    // 생성 혹은 스택 증가
                    if (col.collider.gameObject.GetComponent<Monster2>().magic_type_light == false)
                    {
                        col.collider.gameObject.GetComponent<Monster2>().magic_type_light = true;
                        arrow_Create[2].GetComponent<Holy_Magic>().type = 0;
                        arrow_Create[2].GetComponent<Holy_Magic>().parent_pos = col.collider.gameObject;
                        Instantiate(arrow_Create[2], par_pos);
                    }
                    else if (col.collider.gameObject.GetComponent<Monster2>().magic_type_light == true)
                    {
                        col.collider.gameObject.GetComponent<Monster2>().light_magic_cnt++;
                    }
                }
                DestoryArrow();

            }

            // 소환물 판정 ( ex 촉수 )
            if (col.collider.CompareTag("Summon"))
            {
                int dmg = col.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mdef - GameManager.instance.atk;

                if (dmg >= 0)
                {
                    dmg = -1;
                }
                col.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp += dmg;
                dmg *= -1;
                col.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;
                col.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = dmg;
                // 독성 탄창
                if (GameManager.instance.attack_object[6] == true)
                {
                    col.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mezz[1] = true;
                    col.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mezz_time[1] = 5f;
                }
                // 폭발 탄창
                if (GameManager.instance.attack_object[8] == true)
                {
                    GameObject par = col.collider.gameObject;
                    Transform par_pos = par.GetComponent<Transform>();
                    arrow_Create[0].GetComponent<Item_Object_6_1>().type = 1;
                    Instantiate(arrow_Create[0], par_pos);
                }
                // 중첩 마력
                if (GameManager.instance.attack_object[24] == true)
                {
                    GameObject par = col.collider.gameObject;
                    Transform par_pos = par.GetComponent<Transform>();
                    // 생성 혹은 스택 증가
                    if (col.collider.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().stack_magic == false)
                    {
                        col.collider.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().stack_magic = true;
                        arrow_Create[1].GetComponent<Stack_Magic>().parent_pos = col.collider.gameObject;
                        arrow_Create[1].GetComponent<Stack_Magic>().type = 1;
                        Instantiate(arrow_Create[1], par_pos);
                    }
                    else if (col.collider.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().stack_magic == true)
                    {
                        col.collider.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().stack_magic_cnt++;
                    }
                }
                // 마력부여 : 빛
                if (GameManager.instance.attack_object[27] == true)
                {
                    GameObject par = col.collider.gameObject;
                    Transform par_pos = par.GetComponent<Transform>();
                    // 생성 혹은 스택 증가
                    if (col.collider.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().magic_type_light == false)
                    {
                        col.collider.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().magic_type_light = true;
                        arrow_Create[2].GetComponent<Holy_Magic>().type = 1;
                        arrow_Create[2].GetComponent<Holy_Magic>().parent_pos = col.collider.gameObject;
                        Instantiate(arrow_Create[2], par_pos);
                    }
                    else if (col.collider.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().magic_type_light == true)
                    {
                        col.collider.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().light_magic_cnt++;
                    }
                }
                DestoryArrow();

            }
            
            // 지형 관통 불가 판정
            if (col.collider.CompareTag("Ground") && GameManager.instance.luna == false)
            {
                DestoryArrow();
            }
        }
    }
    public void DestoryArrow()
    {
        Destroy(gameObject);
    }

}
