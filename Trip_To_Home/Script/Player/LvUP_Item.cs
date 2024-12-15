using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LvUP_Item : MonoBehaviour
{
    public GameObject btm;
    BattleManager bt;

    public GameObject chr;

    public Image item_img;
    public int option_num, option_rank;
    public TextMeshProUGUI item_explane;
    int cnt, ary_leng, rock;
    bool calculator = false;

    public Image[] item_rank_sprite;
    public Sprite[] rank_sprite;
    Image self_img;
    // rock = 공격력 공속 이속

    void Start()
    {
        self_img = GetComponent<Image>();
        btm = GameObject.FindWithTag("Manager");
        bt = GameObject.FindWithTag("Manager").GetComponent<BattleManager>();
        chr = GameObject.FindWithTag("Player");
        cnt = bt.LvUp_Object_List.Count - 1;
        rock = Random.Range(0, 3);
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
        Item_Img();
    }
    void Update()
    {
        item_Option_explane();
        Item_Img();
        bt.paused_now = true;
    }

    public void item_Option_explane()
    {
        // 스탯 옵션 비율 ( 공속, 점프 이속 ) 0.01 = 1%

        /*------ 노말 등급 ------*/
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
            if (option_num == 12)
            {
                item_explane.text = "점프 횟수 증가\n점프 가능 횟수를 1 증가시킵니다.";
            }

            // 회전의 탄막
            if (option_num == 13 && GameManager.instance.attack_object[0] == false)
            {
                item_explane.text = "회전의 마력구체\n13초마다 5초동안 플레이어 캐릭터의 주변을 회전하는 구체를 생성합니다. 중복 선택시 그 구체의 갯수가 늘어납니다.\n<size=14>플레이어 공격력의 75% 만큼의 데미지.</size>";
            }
            else if (option_num == 13 && GameManager.instance.attack_object[0] == true)
            {
                item_explane.text = "회전의 마력구체\n마력 구체의 숫자를 한개 더 늘립니다. \n현재 마력구체 갯수 : " + GameManager.instance.attack_object_cnt[0];
            }

            // 행운의 한발
            if (option_num == 14)
            {
                item_explane.text = "행운의 한발\n공격시 40% 확률로 공격력의" + (int)(GameManager.instance.attack_object_cnt[1] + 15) + "% 만큼의 고정데미지를 주는 탄환을 추가로 하나 더 발사합니다.";
            }

            // 성령의 십자가 ( 액티브 )
            if (option_num == 15)
            {
                item_explane.text = "(액티브) 성령의 십자가\n성령의 기운이 서려있는 신성한 십자가를 소환하여 설치합니다. 십자가는 10초간 유지 되며 3초마다 십자가 주위의 플레이어는 최대체력의 7% 만큼 회복되고" +
                    "\n주변모든 적에게는 10의 고정피해를 줍니다." +
                    "<size=16><color=#969696>??? : 저놈의 몸에 생기가 돌아온다!</color></size>";
            }

            // 보호의 장막
            if (option_num == 16)
            {
                int an = (int)GameManager.instance.attack_object_cnt[3] - 1;
                if (an < 5)
                {
                    an = 5;
                }
                item_explane.text = "보호의 장막\n" + an + "초마다 적의 공격을 1회 막아주는 보호막을 생성합니다.";
            }

            // 신경독
            if (option_num == 17)
            {
                item_explane.text = "신경독\n공격시 40% 확률로 공격력의 65% 만큼의 피해를 주는 신경독탄을 발사합니다.\n피격 받은 몬스터는" + (float)(GameManager.instance.attack_object_cnt[4] + 0.2) + " 초 동안 이동불능상태가 됩니다."
                    + "\n<size=17><color=#969696>??? : 슈뢰딩거의 고양이 이야기가 나올 때면, 나는 내 총을 꺼낸다.</color></size>";
            }

            // 숲의 팬던트(액티브)
            if (option_num == 18)
            {
                item_explane.text = "숲의 팬던트(액티브)\n사용시 플레이어의 체력을 15 회복시킵니다.\n한 스테이지당 5회 이상 사용시 숲의기운이 부족해 사용이 일시적으로 불가능합니다";
            }

            // 부패의 팬던트(액티브)
            if (option_num == 19)
            {
                item_explane.text = "부패의 팬던트(액티브)\n사용시 플레이어를 기준으로 주변 적들에게 공격력의 25% 피해를 입힙니다.";
            }

            // 점착탄
            if (option_num == 20)
            {
                item_explane.text = "점착탄\n공격시 30% 확률로 적에게 달라붙는 탄환을 발사해 피격시 공격력의 100% 피해를 입힌 후, 달라붙은 탄환이 3초후 폭발하여" +
                    " 공격력의" + (int)(GameManager.instance.attack_object_cnt[5] + 10) + "% 만큼피해를 줍니다\n<size=17><color=#969696>??? : 예술은 폭발이다!</color></size>";
            }

            // 정화의 부적
            if (option_num == 21)
            {
                item_explane.text = "정화의 부적\n적을" + (int)(GameManager.instance.attack_object_cnt[7] - 1) + "회 처치하면 최대체력을 3~5 만큼 얻습니다. 중복 선택시 목표수량을 1회 감소시킵니다.";
            }

            // 달의 징표1
            if (option_num == 22 && GameManager.instance.attack_object[9] == false)
            {
                item_explane.text = "달의 징표Ⅰ\n무언가 효과가 있을것같지만 아직 아무도 모릅니다.";
            }
            if (option_num == 22 && GameManager.instance.attack_object[9] == true)
            {
                item_explane.text = "달의 징표Ⅰ - 생명\n 최대체력을 15 증가시킵니다.";
            }

            // 달의 징표2
            if (option_num == 23 && GameManager.instance.attack_object[10] == false)
            {
                item_explane.text = "달의 징표Ⅱ\n무언가 효과가 있을것같지만 아직 아무도 모릅니다.";
            }
            if (option_num == 23 && GameManager.instance.attack_object[10] == true)
            {
                item_explane.text = "달의 징표Ⅱ - 힘\n공격력을 10 증가시킵니다.";
            }

            // 달의 징표3
            if (option_num == 24 && GameManager.instance.attack_object[11] == false)
            {
                item_explane.text = "달의 징표Ⅲ\n무언가 효과가 있을것같지만 아직 아무도 모릅니다.";
            }
            if (option_num == 24 && GameManager.instance.attack_object[11] == true)
            {
                item_explane.text = "달의 징표Ⅲ - 속도\n공격속도를 5 투사체 속도를 2 증가시킵니다.";
            }

            // 수상한 알약Ⅰ
            if (option_num == 25)
            {
                item_explane.text = "수상한 알약Ⅰ\n플레이어의 크기가 커집니다. 그게 답니다.\n<size=17><color=#969696>일정 크기를 달성한다면 무슨 일이 일어 날 지도...?</color></size>";
            }

            // 수상한 알약Ⅱ
            if (option_num == 26)
            {
                item_explane.text = "수상한 알약Ⅱ\n플레이어의 크기가 작아집니다. 그게 답니다.\n<size=17><color=#969696>일정 크기를 달성한다면 무슨 일이 일어 날 지도...?</color></size>";
            }

            // 항중력 마법서
            if (option_num == 27 && GameManager.instance.attack_object[13] == false)
            {
                item_explane.text = "항중력 마법서\n적의 중력을 뒤틀어 버리는 마법을 배웁니다. 넉백 판정을 반대로 작용시킵니다.\n<size=17><color=#969696>??? : 지평 좌표계를 어떻게 적용시킨 겁니까?</color></size>";
            }

            // 초콜릿
            if (option_num == 27 && GameManager.instance.attack_object[13] == true)
            {
                item_explane.text = "초콜릿\n체력을 15 회복하고 공격력이 1 증가합니다. 왠지 모르게 세상 모든게 아름다워 보입니다!\n<size=17><color=#969696>?? : 사아아아랑해요~</color></size>";
            }

            // 777 잭팟
            if (option_num == 28 && GameManager.instance.attack_object[19] == false)
            {
                item_explane.text = "777\n코인을 얻을시 2배로 얻습니다.";
            }
            if (option_num == 28 && GameManager.instance.attack_object[19] == true)
            {
                item_explane.text = "뜻밖의 행운\n25 코인을 획득합니다.";
            }

            // 운수 좋은날
            if (option_num == 29 && GameManager.instance.attack_object[20] == false)
            {
                item_explane.text = "운수 좋은날\n코인의 드랍 확률이 15% 증가합니다.";
            }

            // 블랙카드(액티브)
            if (option_num == 29 && GameManager.instance.attack_object[20] == true)
            {
                item_explane.text = "블랙카드\n25코인을 소모하여 떠돌이 상인을 소환합니다.\n<size=17><color=#969696>여기 어디야. - 자고 있다 소환된 떠돌이 상인 -</color></size>";
            }

            // 신비한 돌
            if (option_num == 30)
            {
                item_explane.text = "신비한 돌\n" + GameManager.instance.attack_object_cnt[14] + "%확률로 공격력, 공격속도, 이동속도 3가지중 하나를 5 증가시킵니다. 성공시 성공확률이 10% 차감되고 실패시 10% 증가하고 어떠한 효과도 받을 수 없습니다. \n(최대 75% 최저 25%)"
                    + "<size=17><color=#969696>??? : 이게... 확률이 좀 이상해!</color></size>";
            }

            // 이중성의 수정(액티브)
            if (option_num == 31)
            {
                item_explane.text = "<size=16>이중성의 수정(액티브)\n액티브 아이템 사용키로 '이중성 - 그림자', '이중성 - 빛'전환이 가능합니다. '그림자' 상태일 경우 공격 3타마다 최대체력의 1.5% 사용하여 공격력의 235% 고정피해를 주는 탄을 발사합니다." +
                    "\n'빛' 상태일 경우 3타마다 적중시 최대체력의 1.5% 를 회복하며 공격력의 100% 데미지를 주는 탄을 발사합니다.</size>";
            } 

            // 마력질주
            if (option_num == 32 && GameManager.instance.attack_object[29] == false) 
            {
                item_explane.text = "마력질주\n원하는 방향의 방향키를 빠르게 2번 입력시 마력의 기운을 사용하여 이동속도의" + GameManager.instance.attack_object_cnt[29]*100 + "% 의 속도로 돌진합니다." +
                    "\n'마력질주'는 2초의 재사용 대기시간이 있습니다." +
                    "\n<size=17><color=#969696>??? : 느려.</color></size>";
            }
            if (option_num == 32 && GameManager.instance.attack_object[29] == true)
            {
                item_explane.text = "마력질주+\n'마력질주'의 이동속도 계수를 5% 증가시킵니다 현재 계수 : " + GameManager.instance.attack_object_cnt[29] * 100 +"%";
            }
        }

        /*------ 레어 등급 ------*/
        if (option_rank == 2)
        {
            // 더블샷 미습득
            if (option_num == 0 && GameManager.instance.double_shot == false)
            {
                item_explane.text = "더블샷\n공격시 발사되는 탄환이 2개로 변경됩니다. \n<size=17><color=#969696>-??? : 총알이 두개지요?!-</color></size>";
            }

            // 더블샷 습득
            if (option_num == 0 && GameManager.instance.double_shot == true)
            {
                item_explane.text = "새우튀김\n바삭하고 고소한 맜있는 새우튀김입니다! 공격력, 공격속도가 2 증가합니다.\n<size=17><color=#969696>제가 제일 좋아합니다.</color></size>";
            }

            // 마력의 결정
            if (option_num == 1)
            {
                item_explane.text = "마력의 결정\n모든 능력치를 5 증가 시킵니다. ( 방어력과 점프력은 2 증가 )";
            }

            // 독성마법
            if (option_num == 2 && GameManager.instance.attack_object[6] == false)
            {
                item_explane.text = "마법부여 : 독성마법\n적에게 기본공격 적중시마다 0.7초마다 적에게 공격력의" + (int)((GameManager.instance.attack_object_cnt[6]) * 100) + "% 고정피해를 입히는 '중독' 상태이상을 부여합니다. '중독' 상태이상은 5초동안 유지됩니다.";
            }

            // 독성마법 강화
            if (option_num == 2 && GameManager.instance.attack_object[6] == true)
            {
                item_explane.text = "마법부여 : 독성마법+\n'중독'상태이상의 공격력을 1% 증가시킵니다.";
            }

            // 폭발마법
            if (option_num == 3 && GameManager.instance.attack_object[8] == false)
            {
                item_explane.text = "마법부여 : 폭발마법\n적에게 가하는 기본공격에 폭발마법을 부여합니다. 적에게 기본공격이 적중시 마법 폭발을 일으키는 구체를 적에게 부착시킵니다\n폭발 데미지는 공격력의 140% 에 해당합니다.";
            }

            // 폭발마법 강화
            if (option_num == 3 && GameManager.instance.attack_object[8] == true)
            {
                item_explane.text = "마법부여 : 폭발마법+\n폭발마법의 폭발 데미지를 10% 증가시킵니다.";
            }

            // 마법의 반창고
            if (option_num == 4)
            {
                item_explane.text = "마법의 반창고\n최대체력을 40 방어력을 1 증가시킨 후 체력을 전부 회복합니다.\n<size=17><color=#969696>-귀여운 펭귄 캐릭터가 그려져있습니다!-</color></size>";
            }

            // 잘 익은 토스트
            if (option_num == 5)
            {
                item_explane.text = "잘 익은 토스트\n이동속도를 2  점프력을 2 증가시킵니다.\n<size=17><color=#969696>-지각이야! 지각!-</color></size>";
            }

            // 점프 교본
            if (option_num == 6)
            {
                item_explane.text = "점프 교본\n점프력을 1 최대점프 횟수를 1 증가시킵니다.\n<size=17><color=#969696>-??? : -</color></size>";
            }

            // 수호의 징표
            if (option_num == 7)
            {
                item_explane.text = "수호의 징표\n최대체력이 10 방어력이 5 상승합니다.";
            }

            // 권투 글러브
            if (option_num == 8)
            {
                item_explane.text = "권투 글러브\n공격력을 3 넉백을 2 증가시킵니다.\n<size=17><color=#969696>-??? : 간다! 인천의 비기!-</color></size>";
            }

            // 달의 징표4
            if (option_num == 9 && GameManager.instance.attack_object[15] == false)
            {
                item_explane.text = "달의 징표Ⅳ\n무언가 효과가 있을것같지만 아직 아무도 모릅니다.";
            }
            if (option_num == 9 && GameManager.instance.attack_object[15] == true)
            {
                item_explane.text = "달의 징표Ⅳ - 유연\n점프력이 3 점프횟수가 1 증가합니다.";
            }

            // 붉은 수정(하트모양)
            if (option_num == 10 && GameManager.instance.attack_object[17] == false)
            {
                item_explane.text = "붉은 수정\n수정의 기운으로 " + (GameManager.instance.attack_object_cnt[17] - 0.1f) + "초마다 최대체력의 3% 를 회복합니다.";
            }
            if (option_num == 10 && GameManager.instance.attack_object[17] == true)
            {
                item_explane.text = "햄버거\n체력을 30 회복합니다. 공격력을 1 증가시킵니다.\n<size=17><color=#969696>참깨빵 위에 순쇠고기....</color></size>";
            }

            // 맞대응
            if (option_num == 11 && GameManager.instance.attack_object[16] == false)
            {
                item_explane.text = "맞대응\n내 기본공격에 적의 탄환에 상쇄되는 마력을 주입하여 적의 투사체 공격을 상쇄하여 사라지게 만듭니다.";
            }
            if (option_num == 11 && GameManager.instance.attack_object[16] == true)
            {
                item_explane.text = "바삭한 치킨\n체력을 30 회복하고 공격력이 2 증가합니다.";
            }

            // 작은 친구 네로
            if (option_num == 12 && GameManager.instance.attack_object[21] == false)
            {
                item_explane.text = "작은친구 네로\n'보은'효과 를 발동하는 패밀리어 마법 고양이 네로와 계약합니다. \n<size=17>보은 : 일정거리내에 적이 접근시 자동 ㅂ라 탄알을 11.7초마다 생성합니다. 탄알의 공격력은 플레이어 공격력의 1.2배 입니다.</size>";
            }
            if (option_num == 12 && GameManager.instance.attack_object[21] == true)
            {
                item_explane.text = "작은친구 네로+\n탄알 생성 시간이 0.4초 감소합니다.";
            }

            // 붉은전기의 위습
            if (option_num == 13 && GameManager.instance.attack_object[22] == false)
            {
                item_explane.text = "붉은전기의 위습\n10초마다 플레이어 공격력의 135% 데미지를 주는 굉장히 길지만 폭이 좁은 레이저를 발사하는 강력한 패밀리어인 전기위습과 계약합니다.";
            }
            if (option_num == 13 && GameManager.instance.attack_object[22] == true)
            {
                item_explane.text = "붉은전기의 위습\n위습의 레이저 공격력을 10% 상승시킵니다. 레이저의 폭이 소량 증가됩니다.";
            }

            // 완벽한 파트너 
            if (option_num == 14 && GameManager.instance.attack_object[23] == false)
            {
                item_explane.text = "영혼의 파트너\n패밀리어 계열 공격의 공격력을 2배 증가시킵니다.";
            }
            if (option_num == 14 && GameManager.instance.attack_object[23] == true)
            {
                item_explane.text = "패밀리어의 축복\n점프 횟수를 제외한 모든 스탯이 3 증가합니다. (체력은 5, 방어력은 1 증가)";
            }

            // 중첩마력
            if (option_num == 15 && GameManager.instance.attack_object[24] == false)
            {
                item_explane.text = "중첩의 마력\n기본공격 발사시 탄알에 담겨있는 마력을 적에게 주입시켜 중첩시킵니다. 3초 후 중첩된 마력은 적 주변에 공격력의 115%의 마법구체가 중첩 된 수 만큼 생성되어 적을 향해 타격합니다." +
                    " (최대 중첩 수 5회)";
            }
            else if (option_num == 15 && GameManager.instance.attack_object[24] == true)
            {
                item_explane.text = "중첩의 마력+\n중첩의 마력 공격력 계수가 5% 증가합니다.\n현재계수 : " + GameManager.instance.attack_object_cnt[24] * 100 + "%";
            }

            // 복실복실한 친구 몽몽
            if (option_num == 16 && GameManager.instance.attack_object[12] == false)
            {
                item_explane.text = "복실복실한 친구 몽몽\n언제나 복실복실하고 귀엽지만 용맹한 페밀리어 몽몽이와 계약합니다. 몽몽이는 적 발견시 달려들어 플레이어 공격력 85% 데미지를 입히며 공격합니다.";
            }
            else if (option_num == 16 && GameManager.instance.attack_object[12] == true) 
            {
                item_explane.text = "복실복실한 친구 몽몽+\n몽몽이의 공격력계수를 10% 증가시킵니다. 현재 계수 : " + GameManager.instance.attack_object_cnt[12] * 100 + "%";
            }

            // 폭주의 가면(액티브)
            if (option_num == 17)
            {
                item_explane.text = "폭주의 가면(액티브)\n사용시 7초동안 무적이 되며, 적들과 부딪 힐 경우 공격력의 100% 만큼의 고정 데미지를 입힙니다.";
            }

            // 유체화(액티브)
            if (option_num == 18)
            {
                item_explane.text = "유체화(액티브)\n몸을 일시적으로 유체화 시켜, 적의 모든 공격으로 부터 10초간 자유로워집니다.";
            }

            // 기본공격 변경( 라이트닝! )
            if (option_num == 19 && GameManager.instance.attack_object[25] == false)
            {
                item_explane.text = "라이트닝!\n<size=18>기본 공격이 상시로 일직선 형태의 전기를 전방으로 방출하는 형태로 변화하며 공격력이 10 증가합니다.</size>\n<size=17>지형과 적을 관통할 수 있지만 기존의 기본공격 강화 효과는 받을 수 없습니다.</size>";
            }
            else if (option_num == 19 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] == 0)
            {
                item_explane.text = "라이트닝 샷\n기본공격을 충전 형식으로 변경합니다. 충전이 완료된 상태에서 공격시 레이저 형태의 전격을 전방에 발사합니다. 지형과 적을 관통 할 수 있습니다.\n" +
                    "'폭발마법'같은 기본공격 강화 효과가 적용 가능합니다.";
            }
            else if (option_num == 19 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] == 1)
            {
                item_explane.text = "전류방출\n20% 확률로 '라이트닝 샷'을 피격한 적에게 '방전' 효과를 발생시킵니다." +
                    "\n<size='16'>방전 : 피격당한 대상을 중심으로 강력한 전류가 흐르는 번개의 구가 생겨 닿는 모든 적에게 공격력의 100%에 달하는 피해를 주며 45% 확률로 마비시킵니다.</size>";
            }
            else if (option_num == 19 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] >= 2)
            {
                item_explane.text = "전류방출+\n'방전'효과의 공격력을 5% 증가시킵니다. 현재 추가 공격력" + GameManager.instance.attack_object_cnt[25] * 5+"%";
            }
            // 보이지 않는 갑옷
            if (option_num == 20 && GameManager.instance.attack_object[18] == false)
            {
                item_explane.text = "보이지 않는 갑옷\n분명 갑옷이 있습니다! 보이지 않을 뿐이죠. 최대체력 15을 증가시키고 방어력을 3 증가시킵니다.\n40% 확률로 피해를 받을시 받는 피해를 50% 감소합니다.";
            }
            if (option_num == 20 && GameManager.instance.attack_object[18] == true)
            {
                item_explane.text = "찐 감자\n포슬포슬하고 잘 익은 찐 감자입니다. 체력을 25 회복합니다.\n<size=17><color=#969696>??? : 니 감자는 무봤나?</color></size>";
            }
            // 저주의 고서
            if (option_num == 21) 
            {
                item_explane.text = "저주의 고서\n최대체력과 방어력이 절반으로 감소합니다. 대신 공격력이 1.5배증가합니다.\n<size=17><color=#969696>대가는 당신의 .... 지워져서 읽을 수 없다.</color></size>";
            }
            // 머니 건(액티브)
            if (option_num == 22) 
            {
                item_explane.text = "머니 건(액티브)\n10초동안 기본 공격 시 1 코인을 소모하여 코인 탄환을 추가로 발사합니다. 코인 탄환은 공격력의 100% 피해를 입히며\n탄속이 좀더 빠릅니다. " +
                    "코인 탄환으로 처치시 10% 확률로 코인을 드랍합니다.";
            }
            // 바베큐 파티!(액티브)
            if (option_num == 23)
            {
                item_explane.text = "바베큐 파티!(액티브)\n굉장히 맛있어보이는 바베큐를 설치합니다. 바베큐를 먹으면 체력을 30 + " + (int)(GameManager.instance.maxhp * 0.2)+ "최대체력의 20% 를 회복합니다.\n" +
                    "바베큐는 완전히 익기 전 까지는 먹을 수 없으며 적의 공격에 피격시 파괴됩니다.";
            }
            // 썬더볼트(액티브)
            if (option_num == 24)
            {
                item_explane.text = "썬더볼트(액티브)\n캐릭터를 중심으로 앞 뒤로 번개를 4번 떨어뜨립니다. 이 번개는 공격력의 120% 만큼의 피해를 주고 30% 확률로 기절 상태이상을 부여합니다.";
            }
            // 푸른 전기의 위습
            if (option_num == 25 && GameManager.instance.attack_object[30] == false) 
            {
                item_explane.text = "푸른 전기의 위습\n8초마다 플레이어 공격력의 100% 데미지를 입히는 탄을 발사합니다. 적에게 피격시 '전류방출' 옵션을 발동합니다. \n" +
                    "'푸른 전기의 위습의' '전류방출' 공격력은 플레이어 공격력의 135%만큼의 데미지를 입힙니다.";
            }
            else if (option_num == 25 && GameManager.instance.attack_object[30] == true)
            {
                item_explane.text = "푸른 전기의 위습+\n;푸른 전기의 위습'의 '전류방출' 옵션의 공격력을 10% 증가시킵니다. 현재 공격력" + GameManager.instance.attack_object_cnt[30] * 100 +"%";
            }
        }

        /*------ 유니크 등급 ------*/
        if (option_rank == 3)
        {
            // 트리플 샷
            if (option_num == 0 && GameManager.instance.tripple_shpt == false)
            {
                item_explane.text = "트리플 샷\n공격시 발사되는 탄환의 수가 3개로 변경됩니다.";
            }
            // 트리플 샷
            if (option_num == 0 && GameManager.instance.tripple_shpt == true)
            {
                item_explane.text = "딸기 도넛\n공격력을 10 최대체력을 50 증가시킨 후 체력을 회복합니다.";
            }

            // 고대의 마서
            if (option_num == 1)
            {
                item_explane.text = "고대의 마서\n고대에 집필된 마도서 입니다 강력한 마법이 기록되어있지만 대가가 있다고 합니다. 공격력을 25 증가시키지만 공격속도가 40 감소합니다. 또한 탄환의 크기가 소폭 증가합니다.";
            }

            // 방출의 마력구
            if (option_num == 2)
            {
                item_explane.text = "방출의 마력구(액티브)\n사용시 현재 공격력의 100% 만큼의 피해를 주는 탄환을 빠른속도로 15발 난사합니다. 난사 후 5초동안 공격이 불가능 합니다.";
            }

            // 질보다 양
            if (option_num == 3 && GameManager.instance.attack_object[26] == false)
            {
                item_explane.text = "질보다 양\n공격속도가 최대치로 고정되며 탄알의 사이즈가 대폭 작아집니다! 대신 공격력이 70% 감소합니다.";
            }
            // 천상의 축복
            else if ((option_num == 3 && GameManager.instance.attack_object[26] == true) || option_num == 4) 
            {
                item_explane.text = "천상의 축복\n최대체력 20 공격력 15 사거리 15 방어력 3 이동속도 3 점프횟수 1 을 증가시킵니다.";
            }

            // 마법부여 : 빛
            if (option_num == 5 && GameManager.instance.attack_object[27] == false)
            {
                item_explane.text = "마법부여 : 빛\n기본공격이 적에게 적중시 적에게 빛의 기운을 중첩 시킵니다. 3초 후 중첩에 비례하여 효과가 발동합니다.\n" +
                    "<size=17>2 중첩 이하 : 빛의 폭발을 일으켜 공격력의 145% 만큼의 피해를입힙니다.\n3 - 4 중첩 : 빛의 검을 소환하여 공격력의 185% 만큼의 피해를 입힙니다." +
                    "\n5 중첩 이상 :작은 빛의 검 3개를 소환하여 각각 75% 만큼의 피해를 입힌후 빛의 폭발을 일으켜 공격력의 215% 만큼의 피해를 줍니다." +
                    "또한 이 효과로 피해를 준 경우 입힌 피해의 25% 만큼 체력을 회복합니다.</size>";
            }
            else if (option_num == 5 && GameManager.instance.attack_object[27] == true) 
            {
                item_explane.text = "마법부여 : 빛+\n'마법부여 : 빛' 의 모든 공격 계수들을 15% 증가시킵니다";
            }
        }
    }

    public void Item_Select()
    {
        // 노말 ( 33개 )
        if (option_rank == 1)
        {
            if (option_num == 0)
            {
                //  게임 일시정지해제! 능력적용!
                GameManager.instance.atk += 3;
                calculator = true;
            }
            if (option_num == 1)
            {
                GameManager.instance.atk_cool -= (float)0.01;

            }
            if (option_num == 2)
            {
                GameManager.instance.hp = GameManager.instance.maxhp;
                bt.heal_Alert.text = GameManager.instance.maxhp.ToString();
                Instantiate(bt.heal_Alert);

            }
            if (option_num == 3)
            {
                GameManager.instance.speed += (float)0.1;

            }
            if (option_num == 4)
            {
                GameManager.instance.maxhp += 3;
                bt.heal_Alert.text = "+3";
                Instantiate(bt.heal_Alert);
            }
            if (option_num == 5)
            {
                GameManager.instance.arrow_speed += (float)0.1;

            }
            if (option_num == 6)
            {
                GameManager.instance.atk += 5;
                calculator = true;
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
                bt.heal_Alert.text = "+5";
                Instantiate(bt.heal_Alert);
            }
            if (option_num == 11)
            {
                GameManager.instance.arrow_speed += (float)0.15;

            }
            if (option_num == 12)
            {
                GameManager.instance.jump_cnt++;

            }
            // 회전의 탄막
            if (option_num == 13 && GameManager.instance.attack_object[0] == false)
            {
                GameManager.instance.attack_object[0] = true;
                GameManager.instance.attack_object_cnt[0] = 3;
            }
            else if (option_num == 13 && GameManager.instance.attack_object[0] == true)
            {
                GameManager.instance.attack_object_cnt[0]++;
            }
            // 행운의 한발
            if (option_num == 14)
            {
                GameManager.instance.attack_object[1] = true;
                GameManager.instance.attack_object_cnt[1] += 15;

            }
            // 성령의 십자가
            if (option_num == 15)
            {
                Active_Item_Setting("성령의 십자가", 15);
            }
            // 보호의 장막
            if (option_num == 16)
            {
                GameManager.instance.attack_object[3] = true;
                if (GameManager.instance.attack_object_cnt[3] > 5)
                {
                    GameManager.instance.attack_object_cnt[3]--;
                }
            }
            // 신경독
            if (option_num == 17)
            {
                GameManager.instance.attack_object[4] = true;
                GameManager.instance.attack_object_cnt[4] += 0.2f;

            }
            // 숲의 팬던트 ( 액티브 )
            if (option_num == 18)
            {
                Active_Item_Setting("숲의 팬던트", 18);
            }
            // 부패의 팬던트 ( 액티브 )
            if (option_num == 19)
            {
                Active_Item_Setting("부패의 팬던트", 19);

            }
            // 점착탄
            if (option_num == 20)
            {
                GameManager.instance.attack_object[5] = true;
                GameManager.instance.attack_object_cnt[5] += 10;

            }
            // 정화의 부적
            if (option_num == 21)
            {
                GameManager.instance.attack_object[7] = true;
                GameManager.instance.attack_object_cnt[7]--;
            }
            // 달의 징표 1
            if (option_num == 22 && GameManager.instance.attack_object[9] == false)
            {
                GameManager.instance.attack_object[9] = true;
                if (GameManager.instance.attack_object[15] == true && GameManager.instance.attack_object[9] == true && GameManager.instance.attack_object[10] == true && GameManager.instance.attack_object[11] == true)
                {
                    bt.player_Alert.GetComponent<TextMeshPro>().text = "달의 징표를 모두 모았습니다!";
                    GameManager.instance.luna = true;
                    GameManager.instance.arrow_size *= 4;
                    Instantiate(bt.player_Alert);
                }
            }
            else if (option_num == 22 && GameManager.instance.attack_object[9] == true)
            {
                GameManager.instance.maxhp += 15;
                bt.heal_Alert.text = "+15";
                Instantiate(bt.heal_Alert);
            }
            // 달의 징표 2
            if (option_num == 23 && GameManager.instance.attack_object[10] == false)
            {
                GameManager.instance.attack_object[10] = true;
                if (GameManager.instance.attack_object[15] == true && GameManager.instance.attack_object[9] == true && GameManager.instance.attack_object[10] == true && GameManager.instance.attack_object[11] == true)
                {
                    bt.player_Alert.GetComponent<TextMeshPro>().text = "달의 징표를 모두 모았습니다!";
                    GameManager.instance.luna = true;
                    GameManager.instance.arrow_size *= 4;
                    Instantiate(bt.player_Alert);
                }
            }
            else if (option_num == 23 && GameManager.instance.attack_object[10] == true)
            {
                GameManager.instance.atk += 10;
                calculator = true;
            }
            // 달의 징표 3
            if (option_num == 24 && GameManager.instance.attack_object[11] == false)
            {
                GameManager.instance.attack_object[11] = true;
                if (GameManager.instance.attack_object[15] == true && GameManager.instance.attack_object[9] == true && GameManager.instance.attack_object[10] == true && GameManager.instance.attack_object[11] == true)
                {
                    bt.player_Alert.GetComponent<TextMeshPro>().text = "달의 징표를 모두 모았습니다!";
                    GameManager.instance.luna = true;
                    GameManager.instance.arrow_size *= 4;
                    Instantiate(bt.player_Alert);
                }
            }
            else if (option_num == 24 && GameManager.instance.attack_object[11] == true)
            {
                GameManager.instance.arrow_speed += 2f;
                GameManager.instance.atk_cool -= 0.05f;

            }
            // 알약 1
            if (option_num == 25)
            {
                GameManager.instance.player_size += 0.1f;
                if (GameManager.instance.player_size >= 1.6f)
                {
                    // 알약 효과
                    if (GameManager.instance.giant == false)
                    {
                        GameManager.instance.maxhp += 50;
                        GameManager.instance.atk += 20;
                        GameManager.instance.arrow_size = 0.625f;
                        GameManager.instance.giant = true;
                    }
                    GameManager.instance.player_size = 1.6f;
                    // 사이즈 업적
                    if (GameManager.instance.playerdata.Achievements[12] == false && GameManager.instance.achievement_bool == false)
                    {
                        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                        GameManager.instance.achievement_bool = true;
                        GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>도전과제 달성!</color>\n거대화!";
                        GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Lv_Up_item_Img_n[25];
                        Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                        GameManager.instance.playerdata.Achievements[12] = true;
                        GameManager.instance.Save_PlayerData_ToJson();
                    }
                }

            }
            // 알약 2
            if (option_num == 26)
            {
                GameManager.instance.player_size -= 0.1f;
                if (GameManager.instance.player_size <= 1f)
                {
                    // 알약 효과
                    if (GameManager.instance.sweep == false)
                    {
                        GameManager.instance.speed += 3.5f;
                        GameManager.instance.arrow_speed += 4.5f;
                        GameManager.instance.atk_cool -= 0.2f;
                        GameManager.instance.arrow_size -= 2f;
                        GameManager.instance.sweep = true;
                    }
                    GameManager.instance.player_size = 1f;
                    // 사이즈 업적
                    if (GameManager.instance.playerdata.Achievements[13] == false && GameManager.instance.achievement_bool == false)
                    {
                        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                        GameManager.instance.achievement_bool = true;
                        GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>도전과제 달성!</color>\n축소화!";
                        GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Lv_Up_item_Img_n[26];
                        Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                        GameManager.instance.playerdata.Achievements[13] = true;
                        GameManager.instance.Save_PlayerData_ToJson();
                    }
                }

            }
            // 항중력 마법서
            if (option_num == 27 && GameManager.instance.attack_object[13] == false)
            {
                GameManager.instance.attack_object[13] = true;
            }
            // 초콜릿
            if (option_num == 27 && GameManager.instance.attack_object[13] == true)
            {
                GameManager.instance.atk += 3;
            }
            /// 777 잭팟
            if (option_num == 28 && GameManager.instance.attack_object[19] == false)
            {
                GameManager.instance.attack_object[19] = true;
            }
            else if (option_num == 28 && GameManager.instance.attack_object[19] == true)
            {
                GameManager.instance.coin += 25;
            }
            // 운수 좋은날
            if (option_num == 29 && GameManager.instance.attack_object[20] == false)
            {
                GameManager.instance.attack_object[20] = true;
            }
            // 블랙카드(액티브)
            else if (option_num == 29 && GameManager.instance.attack_object[20] == true)
            {
                Active_Item_Setting("블랙카드", 29);
            }
            // 신비한 돌
            if (option_num == 30)
            {
                int faill = 100 - (int)GameManager.instance.attack_object_cnt[14];
                int total = 100;
                int pivot = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

                // 성공
                if (pivot >= faill)
                {
                    switch (rock)
                    {
                        case 0:
                            GameManager.instance.atk += 5;
                            calculator = true;
                            break;
                        case 1:
                            GameManager.instance.atk_cool -= 0.05f;
                            break;
                        case 2:
                            GameManager.instance.speed += 0.05f;
                            break;
                    }
                    GameManager.instance.attack_object_cnt[14] -= 10;
                    if (GameManager.instance.attack_object_cnt[14] <= 25)
                    {
                        GameManager.instance.attack_object_cnt[14] = 25;
                    }
                }

                // 실패
                else if (pivot < faill)
                {
                    GameManager.instance.attack_object_cnt[14] += 10;
                    if (GameManager.instance.attack_object_cnt[14] >= 75)
                    {
                        GameManager.instance.attack_object_cnt[14] = 75;
                    }
                }
            }
            // 이중성의 수정 ( 액티브 )
            if (option_num == 31)
            {
                Active_Item_Setting("이중성의 수정", 31);
            }
            // 마력질주
            if (option_num == 32 && GameManager.instance.attack_object[29] == false)
            {
                GameManager.instance.attack_object[29] = true;
            }
            else if (option_num == 32 && GameManager.instance.attack_object[29] == true) 
            {
                GameManager.instance.attack_object_cnt[29] += 0.05f;
            }
        }
        // 레어 ( 25개 )
        if (option_rank == 2)
        {
            // 더블샷 미습득
            if (option_num == 0 && GameManager.instance.double_shot == false)
            {
                GameManager.instance.double_shot = true;
            }
            // 더블샷 습득시
            else if (option_num == 0 && GameManager.instance.double_shot == true)
            {
                GameManager.instance.atk += 2;
                GameManager.instance.atk_cool -= (float)0.02;
                calculator = true;
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
                GameManager.instance.jump_power += 0.2f;
                calculator = true;
            }
            // 독성마법
            if (option_num == 2 && GameManager.instance.attack_object[6] == false)
            {
                GameManager.instance.attack_object[6] = true;
                GameManager.instance.attack_object_cnt[6] = 0.03f;
            }
            // 독성마법 강화
            else if (option_num == 2 && GameManager.instance.attack_object[6] == true)
            {
                GameManager.instance.attack_object_cnt[6] += 0.01f;
            }
            // 폭발마법
            if (option_num == 3 && GameManager.instance.attack_object[8] == false)
            {
                GameManager.instance.attack_object[8] = true;
            }
            // 폭발마법 강화
            else if (option_num == 3 && GameManager.instance.attack_object[8] == true)
            {
                GameManager.instance.attack_object_cnt[8] += 0.1f;
            }
            // 마법의 반창고
            if (option_num == 4)
            {
                GameManager.instance.maxhp += 40;
                GameManager.instance.hp = GameManager.instance.maxhp;
                GameManager.instance.def += 1;
                // 최대체력 증가
                bt.heal_Alert.text = "+40";
                Instantiate(bt.heal_Alert);
                // 회복
                bt.heal_Alert.text = GameManager.instance.maxhp.ToString();
                Instantiate(bt.heal_Alert);
            }
            // 잘 익은 토스트
            if (option_num == 5)
            {
                GameManager.instance.speed += 0.2f;
                GameManager.instance.jump_power += 0.2f;
            }
            // 점프 교본
            if (option_num == 6)
            {
                GameManager.instance.jump_power += 0.1f;
                GameManager.instance.jump_cnt++;
            }
            // 수호의 징표
            if (option_num == 7)
            {
                GameManager.instance.maxhp += 10;
                GameManager.instance.def += 5;
            }
            // 권투 글러브
            if (option_num == 8)
            {
                GameManager.instance.atk += 3;
                GameManager.instance.mob_knockback += 0.2f;
                calculator = true;
            }
            // 달의 징표 4
            if (option_num == 9 && GameManager.instance.attack_object[15] == false)
            {
                GameManager.instance.attack_object[15] = true;
                if (GameManager.instance.attack_object[15] == true && GameManager.instance.attack_object[9] == true && GameManager.instance.attack_object[10] == true && GameManager.instance.attack_object[11] == true)
                {
                    bt.player_Alert.GetComponent<TextMeshPro>().text = "달의 징표를 모두 모았습니다!";
                    GameManager.instance.luna = true;
                    GameManager.instance.arrow_size *= 4;
                    Instantiate(bt.player_Alert);
                }
            }
            else if (option_num == 9 && GameManager.instance.attack_object[15] == true)
            {
                GameManager.instance.jump_power += 0.03f;
                GameManager.instance.jump_cnt++;
            }
            // 붉은 수정
            if (option_num == 10 && GameManager.instance.attack_object[17] == false)
            {
                GameManager.instance.attack_object[17] = true;
                GameManager.instance.attack_object_cnt[17] -= 0.1f;
            }
            else if (option_num == 10 && GameManager.instance.attack_object[17] == true)
            {
                GameManager.instance.hp += 30;
                GameManager.instance.atk++;
                bt.heal_Alert.text = "30";
                Instantiate(bt.heal_Alert);
                calculator = true;
            }
            // 맞대응
            if (option_num == 11 && GameManager.instance.attack_object[16] == false)
            {
                GameManager.instance.attack_object[16] = true;
            }
            else if (option_num == 11 && GameManager.instance.attack_object[16] == true)
            {
                GameManager.instance.hp += 30;
                GameManager.instance.atk += 2;
                bt.heal_Alert.text = "30";
                Instantiate(bt.heal_Alert);
                calculator = true;
            }
            // 작은 친구 네로
            if (option_num == 12 && GameManager.instance.attack_object[21] == false)
            {
                Instantiate(bt.attack_objects[1]);
                GameManager.instance.attack_object[21] = true;
                GameManager.instance.attack_object_cnt[21] = 11.7f;
            }
            else if (option_num == 12 && GameManager.instance.attack_object[21] == true)
            {
                GameManager.instance.attack_object_cnt[21] -= 0.4f;
            }
            // 붉은 전기의 위습
            if (option_num == 13 && GameManager.instance.attack_object[22] == false)
            {
                Instantiate(bt.attack_objects[2]);
                GameManager.instance.attack_object[22] = true;
                GameManager.instance.attack_object_cnt[22] = 1.35f;
            }
            else if (option_num == 13 && GameManager.instance.attack_object[22] == true)
            {
                GameManager.instance.attack_object_cnt[22] += 0.1f;
            }
            // 완벽한 파트너
            if (option_num == 14 && GameManager.instance.attack_object[23] == false)
            {
                GameManager.instance.attack_object[23] = true;
            }
            else if (option_num == 14 && GameManager.instance.attack_object[23] == true)
            {
                GameManager.instance.atk += 3;
                GameManager.instance.atk_cool -= 0.03f;
                GameManager.instance.arrow_speed += 0.03f;
                GameManager.instance.speed += 0.03f;
                GameManager.instance.jump_power += 0.03f;
                GameManager.instance.maxhp += 5;
                GameManager.instance.hp += 5;
                GameManager.instance.def += 1;
                calculator = true;
            }
            // 중첩의 마력
            if (option_num == 15 && GameManager.instance.attack_object[24] == false)
            {
                GameManager.instance.attack_object[24] = true;
                GameManager.instance.attack_object_cnt[24] = 1.15f;
            }
            else if (option_num == 15 && GameManager.instance.attack_object[24] == true)
            {
                GameManager.instance.attack_object_cnt[24] += 0.05f;
            }
            // 몽몽이
            if (option_num == 16 && GameManager.instance.attack_object[12] == false)
            {
                Instantiate(bt.attack_objects[4]);
                GameManager.instance.attack_object[12] = true;
                GameManager.instance.attack_object_cnt[12] = 0.85f;
            }
            else if (option_num == 16 && GameManager.instance.attack_object[12] == true) 
            {
                GameManager.instance.attack_object_cnt[12] += 0.1f;
            }
            // 폭주의 가면 ( 액티브 )
            if (option_num == 17)
            {
                Active_Item_Setting("폭주의 가면", 17);
            }
            // 유체화 ( 액티브 )
            if (option_num == 18)
            {
                Active_Item_Setting("유체화", 18);
            }
            // 라이트닝
            if (option_num == 19 && GameManager.instance.attack_object[25] == false)
            {
                GameManager.instance.attack_object[25] = true;
                Instantiate(bt.attack_objects[3]);
            }
            else if (option_num == 19 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] <= 0)
            {
                Destroy(GameObject.FindWithTag("Lightning"));
                GameManager.instance.attack_object_cnt[25] = 1;
                if (GameManager.instance.arrow_size <= 0.8f)
                {
                    GameManager.instance.arrow_size = 0.8f;
                }
                else
                {
                    GameManager.instance.arrow_size += 2f;
                }
            }
            else if (option_num == 19 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] >= 1)
            {
                GameManager.instance.attack_object_cnt[25]++;
            }
            // 보이지 않는 갑옷
            if (option_num == 20 && GameManager.instance.attack_object[18] == false)
            {
                GameManager.instance.attack_object[18] = true;
                GameManager.instance.maxhp += 15;
                GameManager.instance.hp += 15;
                GameManager.instance.def += 3;
                bt.heal_Alert.text = "+15";
                Instantiate(bt.heal_Alert);
            }
            else if (option_num == 20 && GameManager.instance.attack_object[18] == true)
            {
                GameManager.instance.hp += 25;
                bt.heal_Alert.text = "25";
                Instantiate(bt.heal_Alert);
            }
            // 저주의 고서
            if (option_num == 21) 
            {
                GameManager.instance.maxhp /= 2;
                GameManager.instance.def /= 2;
                GameManager.instance.atk = (int)(GameManager.instance.atk * 1.5f);
                calculator = true;
            }
            // 머니 건
            if (option_num == 22) 
            {
                Active_Item_Setting("머니 건", 26);
            }
            // 바베큐 파티!
            if (option_num == 23)
            {
                Active_Item_Setting("바베큐 파티!", 27);
            }
            // 썬더볼트
            if (option_num == 24)
            {
                Active_Item_Setting("썬더볼트", 28);
            }
            // 푸른 전기의 위습
            if (option_num == 25 && GameManager.instance.attack_object[30] == false)
            {
                Instantiate(GameManager.instance.summon_Object[3]);
                GameManager.instance.attack_object[30] = true;
                GameManager.instance.attack_object_cnt[30] = 1.35f;
            }
            else if (option_num == 25 && GameManager.instance.attack_object[30] == true)
            {
                GameManager.instance.attack_object_cnt[30] += 0.1f;
            }

        }
        // 유니크 ( 6개 )
        if (option_rank == 3)
        {
            // 트리플 샷
            if (option_num == 0 && GameManager.instance.tripple_shpt == false)
            {
                GameManager.instance.tripple_shpt = true;

            }
            else if (option_num == 0 && GameManager.instance.tripple_shpt == true)
            {
                GameManager.instance.atk += 10;
                GameManager.instance.maxhp += 50;
                GameManager.instance.hp = GameManager.instance.maxhp;
                calculator = true;
            }
            // 고대의 마서
            if (option_num == 1)
            {
                GameManager.instance.atk += 25;
                GameManager.instance.atk_cool += 0.4f;
                GameManager.instance.arrow_size += 0.1f;
                calculator = true;
            }
            // 방출의 마력구
            if (option_num == 2)
            {
                Active_Item_Setting("방출의 마력구", 2);
            }
            // 질보다 양
            if (option_num == 3 && GameManager.instance.attack_object[26] == false)
            {
                GameManager.instance.attack_object[26] = true;
                GameManager.instance.arrow_size -= 0.25f;
                GameManager.instance.real_atk = GameManager.instance.atk;
                GameManager.instance.atk -= (int)(GameManager.instance.atk * 0.7);
                GameManager.instance.calcul = GameManager.instance.real_atk - GameManager.instance.atk;
                GameManager.instance.atk_cool = 0.13f;
            }
            // 천상의 축복
            else if ((option_num == 3 && GameManager.instance.attack_object[26] == true) || option_num == 4) 
            {
                GameManager.instance.maxhp += 20;
                GameManager.instance.atk += 15;
                GameManager.instance.def += 3;
                GameManager.instance.speed += 0.03f;
                GameManager.instance.jump_cnt++;
                GameManager.instance.arrow_speed += 0.15f;
                calculator = true;
            }
            // 마법부여 : 빛
            if (option_num == 5 && GameManager.instance.attack_object[27] == false)
            {
                GameManager.instance.attack_object[27] = true;
                GameManager.instance.attack_object_cnt[27] = 1;
            }
            else if (option_num == 5 && GameManager.instance.attack_object[27] == true)
            {
                GameManager.instance.attack_object_cnt[27]++;
            }
        }

        // 질보다 양 먹은경우
        if (GameManager.instance.attack_object[26] == true && calculator == true) 
        {
            int cal = GameManager.instance.real_atk - GameManager.instance.atk;
            cal = GameManager.instance.calcul - cal;
            GameManager.instance.real_atk += cal;
            GameManager.instance.atk = GameManager.instance.real_atk - (int)(GameManager.instance.real_atk * 0.7);
            GameManager.instance.calcul = GameManager.instance.real_atk - GameManager.instance.atk;

            GameManager.instance.atk_cool = 0.13f;
        }

        // 라이트닝 차지샷 사이즈 조절
        if (GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] >= 1) 
        {
            GameManager.instance.arrow_size = 0.8f;
        }

        // 레벨업 오브젝트 삭제
        for (int i = 0; i < 4; i++)
        {
            Destroy(bt.LvUp_Object_List[cnt]);
            cnt--;
        }
        bt.lvup_cnt--;

        bt.List_Clean();
        bt.Using_Item_Create_List();

        // 레벨업 끝!
        if (bt.lvup_cnt <= 0)
        {
            ary_leng = bt.LvUp_Object_List.Count;
            for (int i = 0; i < ary_leng; i++)
            {
                bt.LvUp_Object_List.RemoveAt(0);
            }
            bt.paused_now = false;
            bt.List_Clean();
            bt.LvUp_Object.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void Item_Img()
    {
        if (option_rank == 1)
        {
            item_img.sprite = GameManager.instance.Lv_Up_item_Img_n[option_num];

            /* 대체아이템 이미지 지정 */
            // 블랙카드
            if (option_num == 29 && GameManager.instance.attack_object[20] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_n[32];
            }
            // 초콜릿
            if (option_num == 27 && GameManager.instance.attack_object[13] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_n[33];
            }
            // 뜻밖의 행운
            if (option_num == 28 && GameManager.instance.attack_object[19] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_n[34];
            }
            if (option_num == 32)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_n[35];
            }
        }

        else if (option_rank == 2)
        {
            item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[option_num];

            if (option_num == 10 && GameManager.instance.attack_object[17] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[21];
            }
            else if (option_num == 11 && GameManager.instance.attack_object[16] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[22];
            }
            else if (option_num == 20 && GameManager.instance.attack_object[18] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[23];
            }
            else if (option_num == 0 && GameManager.instance.double_shot == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[24];
            }
            else if (option_num == 21)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[25];
            }
            else if (option_num == 22)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[26];
            }
            else if (option_num == 23)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[27];
            }
            else if (option_num == 24)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[28];
            }
            else if (option_num == 25)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[29];
            }
        }

        else if (option_rank == 3)
        {
            item_img.sprite = GameManager.instance.Lv_Up_item_Img_u[option_num];
            if (option_num == 3 && GameManager.instance.attack_object[26] == true) 
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_u[4];
            }
            // 도넛
            if (option_num == 0 && GameManager.instance.tripple_shpt == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_u[5];
            }
        }
    }

    public void Active_Item_Setting(string name, int num)
    {
        if (option_rank == 1)
        {
            GameManager.instance.active_item_img = GameManager.instance.Lv_Up_item_Img_n[num];
            // 블랙카드
            if (option_num == 29 && GameManager.instance.attack_object[20] == true)
            {
                GameManager.instance.active_item_img = GameManager.instance.Lv_Up_item_Img_n[32];
            }
        }
        else if (option_rank == 2)
        {
            GameManager.instance.active_item_img = GameManager.instance.Lv_Up_item_Img_r[num];
        }
        else if (option_rank == 3)
        {
            GameManager.instance.active_item_img = GameManager.instance.Lv_Up_item_Img_u[num];
        }

        if (name == "성령의 십자가")
        {
            GameManager.instance.active_item_cool = 50f;
            GameManager.instance.active_timmer = 50f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "성령의 십자가";
            bt.active_Item.SetActive(true);
        }
        if (name == "숲의 팬던트")
        {
            GameManager.instance.active_item_cool = 40f;
            GameManager.instance.active_timmer = 40f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "숲의 팬던트";
            bt.active_Item.SetActive(true);
        }
        if (name == "부패의 팬던트")
        {
            GameManager.instance.active_item_cool = 25f;
            GameManager.instance.active_timmer = 25f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "부패의 팬던트";
            bt.active_Item.SetActive(true);
        }
        if (name == "방출의 마력구")
        {
            GameManager.instance.active_item_cool = 20f;
            GameManager.instance.active_timmer = 20f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "방출의 마력구";
            bt.active_Item.SetActive(true);
        }
        if (name == "폭주의 가면")
        {
            GameManager.instance.active_item_cool = 35f;
            GameManager.instance.active_timmer = 35f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "폭주의 가면";
            bt.active_Item.SetActive(true);
        }
        if (name == "유체화")
        {
            GameManager.instance.active_item_cool = 25f;
            GameManager.instance.active_timmer = 25f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "유체화";
            bt.active_Item.SetActive(true);
        }
        if (name == "이중성의 수정")
        {
            GameManager.instance.active_item_cool = 2.5f;
            GameManager.instance.active_timmer = 2.5f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "이중성의 수정";
            bt.active_Item.SetActive(true);
        }
        if (name == "블랙카드")
        {
            GameManager.instance.active_item_cool = 10f;
            GameManager.instance.active_timmer = 10f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "블랙카드";
            bt.active_Item.SetActive(true);
        }
        if (name == "머니 건")
        {
            GameManager.instance.active_item_cool = 15f;
            GameManager.instance.active_timmer = 15f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "머니 건";
            bt.active_Item.SetActive(true);
        }
        if (name == "바베큐 파티!")
        {
            GameManager.instance.active_item_cool = 97f;
            GameManager.instance.active_timmer = 97f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "바베큐 파티!";
            bt.active_Item.SetActive(true);
        }
        if (name == "썬더볼트")
        {
            GameManager.instance.active_item_cool = 23f;
            GameManager.instance.active_timmer = 23f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "썬더볼트";
            bt.active_Item.SetActive(true);
        }
    }
}
