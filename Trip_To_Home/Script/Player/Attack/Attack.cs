using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Attack : MonoBehaviour
{
    public GameObject arrow, charge_Shot;
    public Transform pos;
    // 0.4초 = 최대 5개
    public float cooltime;
    public float cool;
    public GameObject charge_bar, charge_bar_gage;
    public GameObject[] attack_object_arrow;
    public bool charge, combo_sw;
    public bool[] combo;
    public float charge_Shot_timmer, combo_timmer;
    public bool attack_denail;
    
    private int duality;

    void Start()
    {
        charge_bar = GameObject.Find("Charge_Bar");
        charge_bar_gage = GameObject.Find("Charge_Gage");
    }

    void Update()
    {
        // 공속 설정
        cooltime = GameManager.instance.atk_cool;

        if (attack_denail == false)
        {
            // 기본 공격
            if (GameManager.instance.attack_object[25] == false && Input.GetKey(KeySet.keys[Key_Action.Attack]))
            {
                if (cool <= 0)
                {
                    // 통상공격
                    if (GameManager.instance.double_shot == false && GameManager.instance.tripple_shpt == false)
                    {
                        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[0], 0.7f);
                        Instantiate(arrow, pos.position, transform.rotation);
                    }
                    // 더블샷만 먹은 경우
                    else if (GameManager.instance.double_shot == true && GameManager.instance.tripple_shpt == false)
                    {
                        StartCoroutine(Double_Attack_Delay());
                    }
                    // 트리플 샷만 먹은 경우
                    else if (GameManager.instance.tripple_shpt == true && GameManager.instance.double_shot == false)
                    {
                        StartCoroutine(Tripple_Attack_Delay());
                    }
                    // 더블, 트리플샷을 모두 먹은 경우
                    else if (GameManager.instance.tripple_shpt == true && GameManager.instance.double_shot == true)
                    {
                        StartCoroutine(Quadra_Attack_Delay());
                    }

                    Sub_Attack();
                    GameManager.instance.playerdata.atk_cnt++;

                    // 사격 딜레이
                    cool = cooltime;
                }
            }

            // 라이트닝 차지샷 O
            else if (GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] >= 1)
            {
                if (Input.GetKeyDown(KeySet.keys[Key_Action.Attack]))
                {
                    charge = true;
                }
                else if (Input.GetKeyUp(KeySet.keys[Key_Action.Attack]) && charge_Shot_timmer >= cooltime)
                {
                    // 통상공격
                    if (GameManager.instance.double_shot == false && GameManager.instance.tripple_shpt == false)
                    {
                        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[1], 0.7f);
                        Instantiate(charge_Shot, pos.position, transform.rotation);
                    }
                    // 더블샷만 먹은 경우
                    else if (GameManager.instance.double_shot == true && GameManager.instance.tripple_shpt == false)
                    {
                        StartCoroutine(Double_Laser_Delay());
                    }
                    // 트리플 샷만 먹은 경우
                    else if (GameManager.instance.double_shot == false && GameManager.instance.tripple_shpt == true)
                    {
                        StartCoroutine(Tripple_Laser_Delay());
                    }
                    // 쿼드라 샷
                    else if (GameManager.instance.double_shot == true && GameManager.instance.tripple_shpt == true)
                    {
                        StartCoroutine(Quadra_Laser_Delay());
                    }

                    Sub_Attack();
                    GameManager.instance.playerdata.atk_cnt++;

                    charge_Shot_timmer = 0f;
                    charge_bar.SetActive(false);
                    charge = false;
                }
                else if (Input.GetKeyUp(KeySet.keys[Key_Action.Attack]) && charge_Shot_timmer < cooltime)
                {
                    charge_Shot_timmer = 0f;
                    charge_bar.SetActive(false);
                    charge = false;
                }
            }

            // 충전
            if (charge == true)
            {
                charge_Shot_timmer += Time.deltaTime;
                charge_bar.SetActive(true);
                charge_bar.transform.position = Camera.main.WorldToScreenPoint(gameObject.GetComponent<Player_Move>().charge_bar_pos.transform.position);
                charge_bar_gage.GetComponent<Image>().fillAmount = charge_Shot_timmer / cooltime;
            }

            cool -= Time.deltaTime;
        }
    }

    IEnumerator Double_Attack_Delay()
    {
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[0], 0.7f);
        Instantiate(arrow, new Vector2(pos.position.x, pos.position.y - (float)0.2), transform.rotation);
        yield return new WaitForSeconds(0.04f);
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[0], 0.7f);
        Instantiate(arrow, new Vector2(pos.position.x, pos.position.y + (float)0.2), transform.rotation);
    }
    IEnumerator Tripple_Attack_Delay()
    {
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[0], 0.7f);
        Instantiate(arrow, new Vector2(pos.position.x, pos.position.y + (float)0.28), transform.rotation);
        yield return new WaitForSeconds(0.04f);
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[0], 0.7f);
        Instantiate(arrow, new Vector2(pos.position.x, pos.position.y + (float)0.14f), transform.rotation);
        yield return new WaitForSeconds(0.03f);
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[0], 0.7f);
        Instantiate(arrow, new Vector2(pos.position.x, pos.position.y - (float)0.1), transform.rotation);
    }
    IEnumerator Quadra_Attack_Delay()
    {
        for (int i = 0; i < 2; i++)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[0], 0.7f);
            Instantiate(arrow, new Vector2(pos.position.x, pos.position.y - (float)0.2), transform.rotation);
            yield return new WaitForSeconds(0.03f);
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[0], 0.7f);
            Instantiate(arrow, new Vector2(pos.position.x, pos.position.y + (float)0.2), transform.rotation);
            yield return new WaitForSeconds(0.07f);
        }
    }

    // 라이트닝 샷
    IEnumerator Double_Laser_Delay()
    {
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[1], 1);
        Instantiate(charge_Shot, new Vector2(pos.position.x, pos.position.y - (float)0.2), transform.rotation);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[1], 1);
        Instantiate(charge_Shot, new Vector2(pos.position.x, pos.position.y + (float)0.2), transform.rotation);
    }
    IEnumerator Tripple_Laser_Delay()
    {
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[1], 0.7f);
        Instantiate(charge_Shot, pos.position, transform.rotation);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[1], 0.7f);
        Instantiate(charge_Shot, new Vector2(pos.position.x, pos.position.y - (float)0.3), transform.rotation);
        yield return new WaitForSeconds(0.3f);
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[1], 0.7f);
        Instantiate(charge_Shot, new Vector2(pos.position.x, pos.position.y + (float)0.3), transform.rotation);
    }
    IEnumerator Quadra_Laser_Delay()
    {
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[1], 0.7f);
        Instantiate(charge_Shot, new Vector2(pos.position.x, pos.position.y + (float)0.3), transform.rotation);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[1], 0.7f);
        Instantiate(charge_Shot, new Vector2(pos.position.x, pos.position.y - (float)0.3), transform.rotation);
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[1], 0.7f);
        Instantiate(charge_Shot, new Vector2(pos.position.x, pos.position.y + (float)0.3), transform.rotation);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.attack_Clip[1], 0.7f);
        Instantiate(charge_Shot, new Vector2(pos.position.x, pos.position.y - (float)0.3), transform.rotation);
    }


    public void Sub_Attack() 
    {
        // 럭키 샷
        if (GameManager.instance.attack_object[1] == true)
        {
            int random = Random.Range(0, 10);
            if (random <= 3)
            {
                int coeffcient = (int)GameManager.instance.attack_object_cnt[1] / 100;
                int dmg = GameManager.instance.atk + (GameManager.instance.atk * coeffcient);
                dmg *= -1;
                attack_object_arrow[0].GetComponent<Item_Object_2>().dmg = dmg;
                Instantiate(attack_object_arrow[0], pos.position, transform.rotation);
            }
        }

        // 마비탄
        if (GameManager.instance.attack_object[4] == true)
        {
            int random = Random.Range(0, 10);
            if (random <= 3)
            {
                int dmg = (int)(GameManager.instance.atk * 0.65);
                dmg *= -1;
                Instantiate(attack_object_arrow[1], pos.position, transform.rotation);
            }
        }

        // 점착탄
        if (GameManager.instance.attack_object[5] == true)
        {
            int random = Random.Range(0, 10);
            if (random <= 3)
            {
                int dmg = GameManager.instance.atk;
                dmg *= -1;
                Instantiate(attack_object_arrow[2], pos.position, transform.rotation);
            }
        }

        // 이중성의 수정
        if (GameManager.instance.active_item_name == "이중성의 수정")
        {
            duality++;
            if (duality >= 3)
            {
                // 그림자
                if (GameManager.instance.active_buff[1] == true)
                {
                    int dmg = (int)(GameManager.instance.maxhp * 0.015);
                    if (dmg <= 1)
                    {
                        dmg = 1;
                    }
                    GameManager.instance.hp -= dmg;
                    Instantiate(attack_object_arrow[3], pos.position, transform.rotation);
                }
                // 빛
                else if (GameManager.instance.active_buff[1] == false)
                {
                    Instantiate(attack_object_arrow[3], pos.position, transform.rotation);
                }
                duality = 0;
            }
        }

        // 머니 건 효과
        if (GameManager.instance.active_buff[2] == true && GameManager.instance.coin >= 1) 
        {
            GameManager.instance.coin--;
            int dmg = GameManager.instance.atk * -1;
            attack_object_arrow[4].GetComponent<Coin_Arrow>().dmg = dmg;
            Instantiate(attack_object_arrow[4], pos.position, transform.rotation);
        }
    }

    public void End_Combo() 
    {
        for(int i = 0; i < combo.Length; i++) 
        {
            combo[i] = false;
        }
        combo_timmer = 0f;
        combo_sw = false;
    }

    public void Attack_Denail_Setting(float times) 
    {
        attack_denail = true;
        Invoke("Attack_Denail_Disable", times);
        
    }
    public void Attack_Denail_Disable() 
    {
        attack_denail = false;
    }
}
