using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class Active_Item_Option : MonoBehaviour
{
    public Image cooltime_img;
    public GameObject player;
    public GameObject[] active_Object;
    public GameObject btm;
    public int use_cnt;
    BattleManager bt;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        btm = GameObject.FindWithTag("Manager");
        bt = btm.GetComponent<BattleManager>();
    }

    void Update()
    {
        // 액티브 발동
        if (Input.GetKeyDown(KeySet.keys[Key_Action.Active]) && (GameManager.instance.active_timmer >= GameManager.instance.active_item_cool))
        {
            Active_Effect(GameManager.instance.active_item_name);
            GameManager.instance.active_timmer = 0f;
            cooltime_img.gameObject.SetActive(true);
        }

        // 쿨타임 관련
        if (GameManager.instance.active_timmer <= GameManager.instance.active_item_cool)
        {
            cooltime_img.fillAmount = GameManager.instance.active_timmer / GameManager.instance.active_item_cool;
            GameManager.instance.active_timmer += Time.deltaTime;
            cooltime_img.gameObject.SetActive(true);
        }
        else if (GameManager.instance.active_timmer >= GameManager.instance.active_item_cool)
        {
            cooltime_img.gameObject.SetActive(false);
        }

        // 이미지
        gameObject.GetComponent<Image>().sprite = GameManager.instance.active_item_img;
    }

    // 옵션 적용 부분 버프 부분은 buff 옵션 [0 체 1 공 2 방 3 공속 4 이속 5 점프]
    public void Active_Effect(string name)
    {
        if (name == "성령의 십자가")
        {
            Vector3 pos = GameObject.FindWithTag("Player").transform.position;
            pos = new Vector3(pos.x, pos.y + 2.65f, 1);
            Instantiate(GameManager.instance.summon_Object[0], pos, transform.rotation);
        }

        if (name == "숲의 팬던트" && use_cnt <= 4)
        {
            GameManager.instance.hp += 15;
            bt.heal_Alert.text = "15";
            Instantiate(bt.heal_Alert);
            if (GameManager.instance.hp >= GameManager.instance.maxhp)
            {
                GameManager.instance.hp = GameManager.instance.maxhp;
            }
            use_cnt++;
        }

        if (name == "부패의 팬던트")
        {
            Instantiate(active_Object[0]);
        }

        if (name == "폭주의 가면")
        {
            GameManager.instance.infinity = true;
            Infinity();
            Invoke("Infinity_false", 7f);
        }

        if (name == "방출의 마력구")
        {
            StartCoroutine(Fast_Bullet());
        }
        // 탄환 가속기
        IEnumerator Fast_Bullet()
        {
            for (int i = 0; i < 15; i++)
            {
                Instantiate(player.GetComponent<Attack>().arrow, player.GetComponent<Attack>().pos.position, transform.rotation);
                yield return new WaitForSeconds(0.03f);
            }
            GameObject.FindWithTag("Player").GetComponent<Attack>().Attack_Denail_Setting(5f);
        }

        if (name == "유체화")
        {
            player.layer = 9;
            player.GetComponent<Player_Move>().spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            Invoke("Infinity_false", 10f);
        }

        if (name == "이중성의 수정")
        {
            // 그림자
            if (GameManager.instance.active_buff[1] == false)
            {
                GameManager.instance.active_buff[1] = true;
            }
            // 빛
            else if (GameManager.instance.active_buff[1] == true)
            {
                GameManager.instance.active_buff[1] = false;
            }
        }

        if (name == "블랙카드" && GameManager.instance.coin >= 25)
        {
            // 위치값을 전달해줄걸 만들어주자!
            Vector2 player_pos = GameObject.FindWithTag("Player").transform.position;
            Instantiate(bt.Shop_Keeper, player_pos, Quaternion.Euler(0, 0, 0));
        }

        if (name == "머니 건")
        {
            GameManager.instance.active_buff[2] = true;
            Invoke("Money_Gun_Buff_Disable", 10f);
        }

        if (name == "바베큐 파티!")
        {
            Vector3 pos = GameObject.FindWithTag("Player").transform.position;
            pos = new Vector3(pos.x, pos.y + 0.2f, 1);
            Instantiate(GameManager.instance.summon_Object[1], pos, transform.rotation);
        }

        if (name == "썬더볼트") 
        {
            StartCoroutine(Thunder_Volt());
        }
        IEnumerator Thunder_Volt() 
        {
            float pos = 0.3f;
            float pos_x = GameObject.FindWithTag("Player").transform.position.x;
            float pos_y = GameObject.FindWithTag("Player").transform.position.y + 1.3f;
            for (int i = 0; i < 4; i++)
            {
                Instantiate(GameManager.instance.summon_Object[2], new Vector2(pos_x + pos, pos_y), Quaternion.Euler(0, 0, 0));
                Instantiate(GameManager.instance.summon_Object[2], new Vector2(pos_x - pos, pos_y), Quaternion.Euler(0, 0, 0));
                pos += 2f;
                yield return new WaitForSeconds(0.4f);
            }
        }
    }

    // 무적
    public void Infinity()
    {
        player.layer = 9;
        player.GetComponent<Player_Move>().spriteRenderer.color = new Color(1, 0.92f, 0.016f, 0.7f);
    }
    // 무적 해제
    public void Infinity_false()
    {
        player.layer = 8;
        player.GetComponent<Player_Move>().spriteRenderer.color = new Color(1, 1, 1, 1);
        GameManager.instance.infinity = false;
    }
    //머니건
    public void Money_Gun_Buff_Disable() 
    {
        GameManager.instance.active_buff[2] = false;
    }
    // 버프관련
    public void Buff_Input(int hp, int atk, int def, float atk_cool, float speed, float jump_power) 
    {
        // 공업
        GameManager.instance.atk -= (int)GameManager.instance.buff[1];
        GameManager.instance.buff[1] += atk;
        GameManager.instance.atk += (int)GameManager.instance.buff[1];
        

        // 방업
        GameManager.instance.def -= (int)GameManager.instance.buff[2];
        GameManager.instance.buff[2] += def;
        GameManager.instance.def += (int)GameManager.instance.buff[2];

        // 공속업
        GameManager.instance.atk_cool += GameManager.instance.buff[3];
        GameManager.instance.buff[3] += atk_cool;
        GameManager.instance.atk_cool -= GameManager.instance.buff[3];

        // 이속업
        GameManager.instance.speed -= GameManager.instance.buff[4];
        GameManager.instance.buff[4] += speed;
        GameManager.instance.speed += GameManager.instance.buff[4];

        // 점프업
        GameManager.instance.jump_power -= GameManager.instance.buff[5];
        GameManager.instance.buff[5] += jump_power;
        GameManager.instance.jump_power += GameManager.instance.buff[5];
    }
    public void Buff_OutPut(int hp, int atk, int def, float atk_cool, float speed, float jump_power)
    {
        // 공업
        GameManager.instance.atk -= (int)GameManager.instance.buff[1];
        GameManager.instance.buff[1] -= atk;
        GameManager.instance.atk += (int)GameManager.instance.buff[1];

        // 방업
        GameManager.instance.def -= (int)GameManager.instance.buff[2];
        GameManager.instance.buff[2] -= def;
        GameManager.instance.def += (int)GameManager.instance.buff[2];

        // 공속업
        GameManager.instance.atk_cool += GameManager.instance.buff[3];
        GameManager.instance.buff[3] -= atk_cool;
        GameManager.instance.atk_cool -= GameManager.instance.buff[3];

        // 이속업
        GameManager.instance.speed -= GameManager.instance.buff[4];
        GameManager.instance.buff[4] -= speed;
        GameManager.instance.speed += GameManager.instance.buff[4];

        // 점프업
        GameManager.instance.jump_power -= GameManager.instance.buff[5];
        GameManager.instance.buff[5] -= jump_power;
        GameManager.instance.jump_power += GameManager.instance.buff[5];
    }
}
