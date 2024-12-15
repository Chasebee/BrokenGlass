using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge_Shot_Arrow : MonoBehaviour
{
    Animator anim;
    GameObject player;
    public GameObject[] arrow_Create;
    public int cnt;

    float direct;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        direct = player.GetComponent<Player_Move>().direction;

        transform.localScale = new Vector3(GameManager.instance.arrow_size, 1.6f, GameManager.instance.arrow_size);

        //방향
        if (direct == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direct == -1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        Invoke("Self_Destroy", 0.233f);
    }
    void Update() 
    {
        if (GameManager.instance.playerdata.Achievements[9] == false && GameManager.instance.achievement_bool == false && cnt >= 3)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>도전과제 달성!</color>\n내 레이저는 모든걸 뚫는 레이저다!";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Lv_Up_item_Img_r[19];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[9] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster2"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            int dmg = collision.gameObject.GetComponent<Monster2>().mdef - GameManager.instance.atk;
            if (dmg >= 0)
            {
                dmg = -1;
            }
            collision.gameObject.GetComponent<Monster2>().mhp += dmg;
            dmg *= -1;
            collision.gameObject.GetComponent<Monster2>().dmg = dmg;
            collision.gameObject.GetComponent<Monster2>().hit = true;


            // 독성 탄창
            if (GameManager.instance.attack_object[6] == true)
            {
                collision.gameObject.GetComponent<Monster2>().mezz[1] = true;
                collision.gameObject.GetComponent<Monster2>().mezz_time[1] = 5f;
            }

            // 폭발 탄창
            if (GameManager.instance.attack_object[8] == true)
            {
                GameObject par = collision.gameObject;
                Transform par_pos = par.GetComponent<Transform>();
                arrow_Create[0].GetComponent<Item_Object_6_1>().type = 1;
                Instantiate(arrow_Create[0], par_pos);
            }

            // 중첩 마력
            if (GameManager.instance.attack_object[24] == true)
            {
                GameObject par = collision.gameObject;
                Transform par_pos = par.GetComponent<Transform>();
                // 생성 혹은 스택 증가
                if (collision.gameObject.GetComponent<Monster2>().stack_magic == false)
                {
                    collision.gameObject.GetComponent<Monster2>().stack_magic = true;
                    arrow_Create[1].GetComponent<Stack_Magic>().type = 0;
                    arrow_Create[1].GetComponent<Stack_Magic>().parent_pos = collision.gameObject;
                    Instantiate(arrow_Create[1], par_pos);
                }
                else if (collision.gameObject.GetComponent<Monster2>().stack_magic == true)
                {
                    collision.gameObject.GetComponent<Monster2>().stack_magic_cnt++;
                }
            }

            // 마력부여 : 빛
            if (GameManager.instance.attack_object[27] == true)
            {
                GameObject par = collision.gameObject;
                Transform par_pos = par.GetComponent<Transform>();
                // 생성 혹은 스택 증가
                if (collision.gameObject.GetComponent<Monster2>().magic_type_light == false)
                {
                    collision.gameObject.GetComponent<Monster2>().magic_type_light = true;
                    arrow_Create[3].GetComponent<Holy_Magic>().type = 0;
                    arrow_Create[3].GetComponent<Holy_Magic>().parent_pos = collision.gameObject;
                    Instantiate(arrow_Create[3], par_pos);
                }
                else if (collision.gameObject.GetComponent<Monster2>().magic_type_light == true)
                {
                    collision.gameObject.GetComponent<Monster2>().light_magic_cnt++;
                }
            }

            cnt++;
        }

        if (collision.gameObject.CompareTag("Summon"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            int dmg = collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mdef - GameManager.instance.atk;
            if (dmg >= 0)
            {
                dmg = -1;
            }

            collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp += dmg;
            dmg *= -1;
            collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = dmg;
            collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;


            // 독성 탄창
            if (GameManager.instance.attack_object[6] == true)
            {
                collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mezz[1] = true;
                collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mezz_time[1] = 5f;
            }

            // 폭발 탄창
            if (GameManager.instance.attack_object[8] == true)
            {
                GameObject par = collision.gameObject;
                Transform par_pos = par.GetComponent<Transform>();
                arrow_Create[0].GetComponent<Item_Object_6_1>().type = 1;
                Instantiate(arrow_Create[0], par_pos);
            }

            // 중첩 마력
            if (GameManager.instance.attack_object[24] == true)
            {
                GameObject par = collision.gameObject;
                Transform par_pos = par.GetComponent<Transform>();
                // 생성 혹은 스택 증가
                if (collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().stack_magic == false)
                {
                    collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().stack_magic = true;
                    arrow_Create[1].GetComponent<Stack_Magic>().type = 1;
                    arrow_Create[1].GetComponent<Stack_Magic>().parent_pos = collision.gameObject;
                    Instantiate(arrow_Create[1], par_pos);
                }
                else if (collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().stack_magic == true)
                {
                    collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().stack_magic_cnt++;
                }
            }

            // 마력부여 : 빛
            if (GameManager.instance.attack_object[27] == true)
            {
                GameObject par = collision.gameObject;
                Transform par_pos = par.GetComponent<Transform>();
                // 생성 혹은 스택 증가
                if (collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().magic_type_light == false)
                {
                    collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().magic_type_light = true;
                    arrow_Create[3].GetComponent<Holy_Magic>().type = 1;
                    arrow_Create[3].GetComponent<Holy_Magic>().parent_pos = collision.gameObject;
                    Instantiate(arrow_Create[3], par_pos);
                }
                else if (collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().magic_type_light == true)
                {
                    collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().light_magic_cnt++;
                }
            }
        }
    }

    public void Self_Destroy() 
    {
        Destroy(gameObject);
    }
}
