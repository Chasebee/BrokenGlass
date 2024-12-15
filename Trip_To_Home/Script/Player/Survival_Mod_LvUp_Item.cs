using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Survival_Mod_LvUp_Item : MonoBehaviour
{
    public GameObject svm;
    public GameObject player;
    Survival_Mod_Manager sm;

    public Image item_img;
    public int option_num, option_rank;
    int cnt, ary_leng;
    public TextMeshProUGUI item_explane;
    public bool trigger;

    public Image[] item_rank_sprite;
    public Sprite[] rank_sprite;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        svm = GameObject.FindWithTag("Manager");
        sm = GameObject.FindWithTag("Manager").GetComponent<Survival_Mod_Manager>();
        cnt = sm.LvUp_Object_List.Count - 1;

    }

    void Update()
    {
        Item_Explane();
        Item_Img();
        sm.paused_now = true;
    }

    public void Item_Explane() 
    {
        // 노말
        if (option_rank == 1)
        {
            if (option_num == 0)
            {
                item_explane.text = "공격력 증가Ⅰ\n공격력을 3 증가시킵니다.";
            }
            if (option_num == 1)
            {
                item_explane.text = "공격속도 증가Ⅰ\n공격속도를 1 증가시킵니다.";
            }
            if (option_num == 2)
            {
                item_explane.text = "체력 회복\n체력을 전부 회복합니다.";
            }
            if (option_num == 3)
            {
                item_explane.text = "이동속도 증가Ⅰ\n이동 속도를 1 증가시킵니다.";
            }
            if (option_num == 4)
            {
                item_explane.text = "최대체력 증가Ⅰ\n최대체력을 3 증가시킵니다.";
            }
            if (option_num == 5)
            {
                item_explane.text = "탄속 증가Ⅰ\n투사체의 속도를 10 증가시킵니다.";
            }
            if (option_num == 6)
            {
                item_explane.text = "공격력 증가Ⅱ\n공격력을 5 증가시킵니다.";
            }
            if (option_num == 7)
            {
                item_explane.text = "공격속도 증가Ⅱ\n공격속도를 3 증가시킵니다.";
            }
            if (option_num == 8)
            {
                item_explane.text = "방어력 증가Ⅰ\n방어력을 1 증가시킵니다.";
            }
            if (option_num == 9)
            {
                item_explane.text = "이동속도 증가Ⅱ\n이동 속도를 3 증가시킵니다.";
            }
            if (option_num == 10)
            {
                item_explane.text = "최대체력 증가Ⅱ\n최대체력을 5 증가시킵니다.";
            }
            if (option_num == 11)
            {
                item_explane.text = "탄속 증가Ⅱ\n투사체의 속도를 15 증가시킵니다.";
            }
            // 회전의 탄막
            if (option_num == 12 && GameManager.instance.attack_object[0] == false)
            {
                item_explane.text = "회전의 마력구체\n13초마다 5초동안 플레이어 캐릭터의 주변을 회전하는 구체를 생성합니다. 중복 선택시 그 구체의 갯수가 늘어납니다.\n<size=14>각 구체는 플레이어 공격력의 75% 만큼의 데미지를 갖습니다.</size>";
            }
            else if (option_num == 12 && GameManager.instance.attack_object[0] == true)
            {
                item_explane.text = "회전의 마력구체\n마력 구체의 숫자를 한개 더 늘립니다. \n현재 마력구체 갯수 : " + GameManager.instance.attack_object_cnt[0];
            }
            // 괴짜 연구자의 안경
            if (option_num == 13 && GameManager.instance.s_attack_object[2] == false)
            {
                item_explane.text = "괴짜 연구자의 안경\n적을 처치시 경험치를 5% 추가로 획득합니다. 쓰고있으니 굉장하지만 말이 안되는 아이디어들이 떠오릅니다!";
            }
            else if (option_num == 13 && GameManager.instance.s_attack_object[2] == true)
            {
                item_explane.text = "괴짜 연구자의 안경+\n적 처치시 얻는 추가 경험치의 계수를 증가시킵니다. 현재 " + GameManager.instance.s_attack_object_cnt[2] + "% => " + (GameManager.instance.s_attack_object_cnt[2] + 5) + "%";
            }
            // 튕기는 마력 구체
            if (option_num == 14 && GameManager.instance.s_attack_object[7] == false)
            {
                item_explane.text = "튕기는 마력 구체\n이리 저리 튕기며 적들에게 피해를 입히는 마력 구체를 25초마다 소환합니다. 마력 구체는 플레이어 공격력의 80% 만큼의 피해를 입히며 10초동안 유지됩니다.";
            }
            else if (option_num == 14 && GameManager.instance.s_attack_object[7] == true)
            {
                item_explane.text = "튕기는 마력 구체+\n소환되는 마력 구체의 갯수를 증가시킵니다. 현재 : " + GameManager.instance.s_attack_object_cnt[7] + "개 => " + (GameManager.instance.s_attack_object_cnt[7] + 1) + "개";
            }
            // 독안개
            if (option_num == 15 && GameManager.instance.s_attack_object[8] == false)
            {
                item_explane.text = "독안개\n18초마다 플레이어의 주변에 7.4초동안 유지되는 독안개지역을 생성합니다. 몬스터가 독안개 지역에있는 경우 0.3초마다 플레이어 공격력의 75% 만큼의 데미지를 입힙니다.";
            }
            else if (option_num == 15 && GameManager.instance.s_attack_object[8] == true)
            {
                item_explane.text = "독안개+\n생성되는 독안개 지역의 수를 1개 독안개의 범위를 10% 데미지를 5% 증가시킵니다." +
                    "\n현재(갯수 / 범위 / 데미지) : " + (GameManager.instance.s_attack_object_cnt[8]*10) + "개 / " + (int)(GameManager.instance.s_attack_object_cnt[8] * 100) + "% / " + (int)(GameManager.instance.s_attack_object_cnt[9] * 100) + "% => " +
                    (int)(((GameManager.instance.s_attack_object_cnt[8] + 0.1) * 10)) + "개 / " + (int)((GameManager.instance.s_attack_object_cnt[8] + 0.1) * 100) +"% / " + (int)((GameManager.instance.s_attack_object_cnt[9] + 0.05) * 100) + "%";
            }
        }

        // 레어
        else if (option_rank == 2)
        {
            // 정화의 부적
            if (option_num == 0)
            {
                item_explane.text = "정화의 부적\n적을" + (int)(GameManager.instance.attack_object_cnt[7] - 1) + "회 처치하면 최대체력을 1~2 만큼 얻습니다. 중복 선택시 목표수량을 1회 감소시킵니다.";
            }

            // 마력의 결정
            if (option_num == 1)
            {
                item_explane.text = "마력의 결정\n모든 능력치를 5 증가 시킵니다. ( 방어력은 2 증가 )";
            }

            // 마법의 반창고
            if (option_num == 2)
            {
                item_explane.text = "마법의 반창고\n최대체력을 40 방어력을 1 증가시킨 후 체력을 전부 회복합니다.\n<size=17><color=#969696>-귀여운 펭귄 캐릭터가 그려져있습니다!-</color></size>";
            }

            // 작은 악마의 뿔
            if (option_num == 3 && GameManager.instance.s_attack_object[1] == false)
            {
                item_explane.text = "작은 악마의 뿔\n작지만 굉장히 강한 기운이 뻗쳐나오는 악마의 뿔 입니다. 적 처치 시 적의 생명력을 25% 확률로 착취하여 최대 체력의" + GameManager.instance.s_attack_object_cnt[1] + "% 만큼 체력을 회복합니다.";
            }
            else if (option_num == 3 && GameManager.instance.s_attack_object[1] == true)
            {
                item_explane.text = "작은 악마의 뿔+\n적의 생명력을 더욱 착취하여 더 많은 체력을 회복합니다. 현재 회복 비율 " + GameManager.instance.s_attack_object_cnt[1] + "% 선택시 비율 " + (GameManager.instance.s_attack_object_cnt[1] + 0.3f) + "%";
            }

            // 치명타 확률 증가
            if (option_num == 4)
            {
                item_explane.text = "치명타 확률 증가Ⅰ\n적의 약점을 포착합니다. 치명타 확률을 5% 증가시킵니다.";
            }

            // 마력 지뢰
            if (option_num == 5 && GameManager.instance.s_attack_object[3] == false)
            {
                item_explane.text = "마력 지뢰\n4.8초마다 플레이어 위치에 자동으로 지뢰가 설치됩니다. 지뢰에 적이 닿을경우 자동으로 폭발하며 공격력의 130% 만큼의 피해를 모든 적에게 줍니다.";
            }
            else if (option_num == 5 && GameManager.instance.s_attack_object[3] == true)
            {
                item_explane.text = "마력 지뢰+\n'마력 지뢰'의 폭발 데미지가 증가합니다. 현재 : " + GameManager.instance.s_attack_object_cnt[3] + "% => " + (GameManager.instance.s_attack_object_cnt[3] + 10) + "%";
            }

            // 마력의 화살
            if (option_num == 6 && GameManager.instance.s_attack_object[4] == false)
            {
                item_explane.text = "마력의 화살\n5초마다 적 하나를 유도 추적하여 공격력의 85% 피해를 주는 마법화살 3개를 소환합니다." +
                    " 마력 화살은 적을 관통 할 수 있으며 목표로 지정된 적이 아닌 다른 적에게는 절반의 피해만 입힙니다.\n적이 없을 경우 마법화살은 생성되지 않습니다.";
            }
            else if (option_num == 6 && GameManager.instance.s_attack_object[4] == true)
            {
                item_explane.text = "마력의 화살+\n생성하는 마력화살의 갯수를 하나 더 추가하며 공격력을 5% 증가시킵니다.\n" +
                    "현재 : " + GameManager.instance.s_attack_object_cnt[5] + "개 / " + (GameManager.instance.s_attack_object_cnt[4] * 100) + "%";
            }

            // 라이트닝 샷
            if (option_num == 7 && GameManager.instance.s_attack_object[6] == false)
            {
                item_explane.text = "라이트닝 샷\n7.4초마다 플레이어의 공격 방향에서 굉장히 빠르게 발사되며 적을 관통하는 라이트닝 샷을 발사합니다." +
                    " 라이트닝 샷은 플레이어의 공격력의 55% 의 피해를 입히며. 적을 관통 할 때 마다 데미지가 10% 씩 증가합니다.\n데미지 증가 중첩수는 최대 5회이며 일정 확률로 적을 마비시켜 이동불가 시킬 수 있습니다.";
            }
            else if (option_num == 7 && GameManager.instance.s_attack_object[6] == true)
            {
                item_explane.text = "라이트닝 샷+\n'라이트닝 샷'의 공격력 계수를 5% 증가시킵니다. 현재 : " + (GameManager.instance.s_attack_object_cnt[6] * 100) + "% => " + (int)((GameManager.instance.s_attack_object_cnt[6] + 0.05) * 100) + "%";
            }

            // 정화
            if (option_num == 8 && GameManager.instance.s_attack_object[10] == false)
            {
                item_explane.text = "정화\n플레이어의 중심에 원이 생성됩니다. 플레이어가 체력을 회복 시 원 내부에 있는 모든 적에게 공격력의 15% + 회복량 X 1.3 만큼의 데미지를 입힙니다.";
            }
            if (option_num == 8 && GameManager.instance.s_attack_object[10] == true)
            {
                item_explane.text = "정화+\n플레이어의 중심의 원의 크기가 5% 증가합니다. 현재 추가 크기 : " + GameManager.instance.s_attack_object_cnt[10] + "% => " + (int)((GameManager.instance.s_attack_object_cnt[10] + 0.05f) * 100)+"%";
            }    
        }

        // 유니크
        else if (option_rank == 3)
        {
            // 일반공격 관통 가능
            if (option_num == 0)
            {
                item_explane.text = "날카로운 마력\n기본공격이 모든 적들을 관통합니다.";
            }

            // 천상의 축복
            if (option_num == 1)
            {
                item_explane.text = "천상의 축복\n최대체력 25 공격력 20 사거리 10 방어력 3 이동속도 3 만큼 증가시킵니다.";
            }

            // (세트아이템 - 치명타 관련 ( 한쪽은 치명타 확률, 다른쪽은 치명타 피해 ) 둘다 먹었을 경우 치명타 데미지 발생시 치명타 데미지 한번 더 적용 / 치명타 피해시 체력 회복
            if (option_num == 2 && GameManager.instance.s_attack_object[11] == false)
            {
                item_explane.text = "불완전한 파괴의 드림캐쳐\n비밀을 가지고있는 듯 한 드림캐쳐입니다. 강력한 힘이 느껴지지만 아직은 뭔가 부족해 보입니다." +
                    "\n치명타 공격력의 계수를 80% 치명타 확률을 45% 증가시킵니다. '불완전한 재생의 드림캐쳐' 를 소지한 경우에 치명타가 발동한 경우 치명타 데미지를 입히면 치명타 데미지의 23% 만큼 체력을 회복하며, 한번더 치명타 피해를 입힙니다.";
            }
            if (option_num == 3 && GameManager.instance.s_attack_object[12] == false) 
            {
                item_explane.text = "불완전한 재생의 드림캐쳐\n비밀을 가지고있는 듯 한 드림캐쳐입니다. 강력한 힘이 느껴지지만 아직은 뭔가 부족해 보입니다.\n3초마다 최대체력의 3.5% 만큼 체력을 회복합니다." +
                    "\n'불완전한 파괴의 드림캐쳐' 를 소지한 경우에 치명타가 발동한 경우 치명타 데미지를 입히면 치명타 데미지의 23% 만큼 체력을 회복하며, 한번더 치명타 피해를 입힙니다.";
            }

            // 보호의 장막
            if ((option_num == 4 && GameManager.instance.attack_object[3] == false) || (option_num == 2 && GameManager.instance.s_attack_object[11] == true))
            {
                item_explane.text = "보호의 장막\n8초마다 적의 공격을 1회 막아주는 보호막을 생성합니다. 공격 받을 시 3초동안 무적효과를 받으며 모든 적을 통과하여 다닐 수 있습니다.";
            }
            else if ((option_num == 4 && GameManager.instance.attack_object[3] == true) || (option_num == 2 && GameManager.instance.s_attack_object[11] == true)) 
            {
                item_explane.text = "보호의 장막+\n보호막의 재생성 주기를 0.5초 감소시킵니다. 최소단축 (3.5초)\n현재 재생성 주기 :" + GameManager.instance.attack_object_cnt[3] + "초";
            }
            
            // 고대의 마서
            if (option_num == 5 || (option_num == 3 && GameManager.instance.s_attack_object[12] == true))
            {
                item_explane.text = "고대의 마서\n고대에 집필된 마도서 입니다 강력한 마법이 기록되어있지만 대가가 있다고 합니다. 공격력을 25 증가시키지만 공격속도가 40 감소합니다. 또한 탄환의 크기가 소폭 증가합니다.";
            }
        }
    }
    public void Item_Select()
    {
        // 노말
        if (option_rank == 1) 
        {
            if (option_num == 0)
            {
                GameManager.instance.atk += 3;
            }
            if (option_num == 1)
            {
                GameManager.instance.atk_cool -= (float)0.01;

            }
            if (option_num == 2)
            {
                int heal = GameManager.instance.maxhp - GameManager.instance.hp;
                GameManager.instance.hp = GameManager.instance.maxhp;
                if (GameManager.instance.s_attack_object[10] == true) 
                {
                    player.GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_val = heal;
                    player.GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_on = true;
                }
            }
            if (option_num == 3)
            {
                GameManager.instance.speed += (float)0.1;

            }
            if (option_num == 4)
            {
                GameManager.instance.maxhp += 3;
            }
            if (option_num == 5)
            {
                GameManager.instance.arrow_speed += (float)0.1;
            }
            if (option_num == 6)
            {
                GameManager.instance.atk += 5;
            }
            if (option_num == 7)
            {
                GameManager.instance.atk_cool -= (float)0.03;
            }
            if (option_num == 8)
            {
                GameManager.instance.def++;
            }
            if (option_num == 9)
            {
                GameManager.instance.speed += (float)0.3;
            }
            if (option_num == 10)
            {
                GameManager.instance.maxhp += 5;
            }
            if (option_num == 11)
            {
                GameManager.instance.arrow_speed += (float)0.15;
            }
            // 회전의 탄막
            if (option_num == 12 && GameManager.instance.attack_object[0] == false)
            {
                GameManager.instance.attack_object[0] = true;
                GameManager.instance.attack_object_cnt[0] = 3;
            }
            else if (option_num == 12 && GameManager.instance.attack_object[0] == true)
            {
                GameManager.instance.attack_object_cnt[0]++;
            }
            // 괴짜 연구자의 안경
            if (option_num == 13 && GameManager.instance.s_attack_object[2] == false)
            {
                GameManager.instance.s_attack_object[2] = true;
                GameManager.instance.s_attack_object_cnt[2] = 5;
            }
            else if (GameManager.instance.s_attack_object[2] == true)
            {
                GameManager.instance.s_attack_object_cnt[2] += 5f;
            }
            // 튕기는 마력 구체
            if (option_num == 14 && GameManager.instance.s_attack_object[7] == false)
            {
                GameManager.instance.s_attack_object[7] = true;
                GameManager.instance.s_attack_object_cnt[7] = 2;
            }
            else if (option_num == 14 && GameManager.instance.s_attack_object[7] == true)
            {

                GameManager.instance.s_attack_object_cnt[7]++;
            }
            // 독안개
            if (option_num == 15 && GameManager.instance.s_attack_object[8] == false)
            {
                GameManager.instance.s_attack_object[8] = true;
                GameManager.instance.s_attack_object_cnt[8] = 0.1f;
                GameManager.instance.s_attack_object_cnt[9] = 0.75f;
            }
            else if (option_num == 15 && GameManager.instance.s_attack_object[8] == true)
            {
                GameManager.instance.s_attack_object_cnt[8] += 0.1f;
                GameManager.instance.s_attack_object_cnt[9] += 0.05f;
            }
        }

        // 레어
        else if (option_rank == 2)
        {
            // 정화의 부적
            if (option_num == 0)
            {
                GameManager.instance.attack_object[7] = true;
                GameManager.instance.attack_object_cnt[7]--;
            }
            // 마력의 결정
            if (option_num == 1)
            {
                GameManager.instance.atk += 5;
                GameManager.instance.maxhp += 5;
                GameManager.instance.speed += 0.5f;
                GameManager.instance.atk_cool -= 0.05f;
                GameManager.instance.arrow_speed += 0.05f;
                GameManager.instance.def += 2;
            }
            // 마법의 반창고
            if (option_num == 2)
            {
                GameManager.instance.maxhp += 40;
                int heal = GameManager.instance.maxhp - GameManager.instance.hp;
                GameManager.instance.hp = GameManager.instance.maxhp;
                GameManager.instance.def += 1;
                if (GameManager.instance.s_attack_object[10] == true)
                {
                    player.GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_val = heal;
                    player.GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_on = true;
                }
            }
            // 작은 악마의 뿔
            if (option_num == 3 && GameManager.instance.s_attack_object[1] == false)
            {
                GameManager.instance.s_attack_object[1] = true;
            }
            else if (option_num == 3 && GameManager.instance.s_attack_object[1] == true)
            {
                GameManager.instance.s_attack_object_cnt[1] += 0.3f;
            }
            // 치확 증가
            if (option_num == 4) 
            {
                GameManager.instance.crit_per += 5f;
            }
            // 마법 지뢰
            if (option_num == 5 && GameManager.instance.s_attack_object[3] == false)
            {
                GameManager.instance.s_attack_object[3] = true;
                GameManager.instance.s_attack_object_cnt[3] = 130;
            }
            else if (option_num == 5 && GameManager.instance.s_attack_object[3] == true)
            {
                GameManager.instance.s_attack_object_cnt[3] += 10;
            }
            // 마력의 화살
            if (option_num == 6 && GameManager.instance.s_attack_object[4] == false)
            {
                GameManager.instance.s_attack_object[4] = true;
                GameManager.instance.s_attack_object_cnt[4] = 0.85f;
                GameManager.instance.s_attack_object_cnt[5] = 3;
            }
            else if (option_num == 6 && GameManager.instance.s_attack_object[4] == true)
            {
                GameManager.instance.s_attack_object_cnt[4] += 0.05f;
                GameManager.instance.s_attack_object_cnt[5]++;
            }
            // 라이트닝 샷
            if (option_num == 7 && GameManager.instance.s_attack_object[6] == false)
            {
                GameManager.instance.s_attack_object[6] = true;
                GameManager.instance.s_attack_object_cnt[6] = 0.55f;
            }
            else if (option_num == 7 && GameManager.instance.s_attack_object[6] == true)
            {
                GameManager.instance.s_attack_object_cnt[6] += 0.05f;
            }
            // 정화
            if (option_num == 8 && GameManager.instance.s_attack_object[10] == false)
            {
                player.GetComponent<Survival_Mod_Player_Move>().heal_area.SetActive(true);
                GameManager.instance.s_attack_object[10] = true;
            }
            else if (option_num == 8 && GameManager.instance.s_attack_object[10] == true)
            {
                GameManager.instance.s_attack_object_cnt[10] += 0.05f;
                float bonus_x = player.GetComponent<Survival_Mod_Player_Move>().heal_area.transform.localScale.x * GameManager.instance.s_attack_object_cnt[10];
                float bonus_y = player.GetComponent<Survival_Mod_Player_Move>().heal_area.transform.localScale.y * GameManager.instance.s_attack_object_cnt[10];
                player.GetComponent<Survival_Mod_Player_Move>().heal_area.transform.localScale =
                    new Vector2(player.GetComponent<Survival_Mod_Player_Move>().heal_area.transform.localScale.x + bonus_x, player.GetComponent<Survival_Mod_Player_Move>().heal_area.transform.localScale.y + bonus_y); 
            }
        }

        // 유니크
        else if (option_rank == 3)
        {
            // 날카로운 마력
            if (option_num == 0)
            {
                GameManager.instance.s_attack_object[0] = true;
            }

            // 천상의 축복
            if (option_num == 1)
            {
                GameManager.instance.maxhp += 25;
                GameManager.instance.atk += 20;
                GameManager.instance.def += 3;
                GameManager.instance.speed += 0.03f;
                GameManager.instance.arrow_speed += 0.1f;
            }

            // 세트아이템 - 드림캐쳐
            if (option_num == 2 && GameManager.instance.s_attack_object[11] == false) 
            {
                GameManager.instance.crit_dmg += 0.8f;
                GameManager.instance.crit_per += 45;
                GameManager.instance.s_attack_object[11] = true;
                trigger = true;
            }
            if (option_num == 3 && GameManager.instance.s_attack_object[12] == false)
            {
                GameManager.instance.s_attack_object[12] = true;
                trigger = true;
            }
            
            // 보호의 장막
            if (((option_num == 4 && GameManager.instance.attack_object[3] == false) || (option_num == 2 && GameManager.instance.s_attack_object[11] == true)) && trigger == false)
            {
                GameManager.instance.attack_object[3] = true;
                GameManager.instance.attack_object_cnt[3] = 8;
            }
            if (((option_num == 4 && GameManager.instance.attack_object[3] == true) || (option_num == 2 && GameManager.instance.s_attack_object[11] == true)) && trigger == false)
            {
                GameManager.instance.attack_object_cnt[3]-= 0.5f;
            }

            // 고대의 마서
            if ((option_num == 5 || (option_num == 3 && GameManager.instance.s_attack_object[12] == true)) && trigger == false)
            {
                GameManager.instance.atk += 25;
                GameManager.instance.atk_cool += 0.4f;
                GameManager.instance.arrow_size += 0.1f;
            }
        }

        // 레벨업 오브젝트 삭제
        for (int i = 0; i < 4; i++)
        {
            Destroy(sm.LvUp_Object_List[cnt]);
            cnt--;
        }
        sm.lvup_cnt--;

        sm.List_Clean();
        sm.Using_Item_Create_List();

        // 레벨업 끝!
        if (sm.lvup_cnt <= 0)
        {
            ary_leng = sm.LvUp_Object_List.Count;
            for (int i = 0; i < ary_leng; i++)
            {
                sm.LvUp_Object_List.RemoveAt(0);
            }
            sm.paused_now = false;
            sm.List_Clean();
            sm.LvUp_Object.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
    public void Item_Img()
    {
        switch (option_rank)
        {
            case 1:
                item_rank_sprite[0].sprite = rank_sprite[0];
                item_rank_sprite[1].sprite = rank_sprite[3];
                item_rank_sprite[2].sprite = rank_sprite[6];
                break;
            case 2:
                item_rank_sprite[0].sprite = rank_sprite[1];
                item_rank_sprite[1].sprite = rank_sprite[4];
                item_rank_sprite[2].sprite = rank_sprite[7];
                break;
            case 3:
                item_rank_sprite[0].sprite = rank_sprite[2];
                item_rank_sprite[1].sprite = rank_sprite[5];
                item_rank_sprite[2].sprite = rank_sprite[8];
                break;
        }
        if (option_rank == 1)
        {
            item_img.sprite = GameManager.instance.s_Lv_Up_item_Img_n[option_num];

            /*if (option_num == 29 && GameManager.instance.attack_object[20] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_n[32];
            }*/
        }

        else if (option_rank == 2)
        {
            item_img.sprite = GameManager.instance.s_Lv_Up_item_Img_r[option_num];
        }

        else if (option_rank == 3)
        {
            item_img.sprite = GameManager.instance.s_Lv_Up_item_Img_u[option_num];
        }
    }
}
