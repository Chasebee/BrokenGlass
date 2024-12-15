using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Item : MonoBehaviour
{
    public int item_num, cost, type, rank;
    public TextMeshProUGUI item_explane;
    public Image img;
    public string it_name;

    GameObject btm;
    BattleManager bt;
    
    void Start()
    {
        btm = GameObject.FindWithTag("Manager");
        bt = GameObject.FindWithTag("Manager").GetComponent<BattleManager>();
    }

    void Update()
    {
        Item_option();
    }

    public void Item_option() 
    {
        // type 이 0 이면 음식 , 1이면 레벨업 아이템
        if (type == 0)
        {
            if (item_num == 0)
            {
                item_explane.text = "따뜻한 우유 [10 코인]\n 체력을 10만큼 회복시켜줍니다. 따뜻한 느낌이 포근하게 느껴집니다.";
                img.sprite = GameManager.instance.shop_Food_img[0];
                cost = 10;
            }
            else if (item_num == 1)
            {
                item_explane.text = "호밀빵 [15 코인]\n체력을 20만큼 회복 시킵니다. 이 세계에서도 흔히 구할 수 있는 빵입니다.";
                img.sprite = GameManager.instance.shop_Food_img[1];
                cost = 15;
            }
            else if (item_num == 2)
            {
                item_explane.text = "마카롱 [20 코인]\n체력을 25만큼 회복시킵니다. 달달한 맛이 느껴집니다!";
                img.sprite = GameManager.instance.shop_Food_img[2];
                cost = 20;
            }
            else if (item_num == 3)
            {
                item_explane.text = "감자튀김 [25 코인]\n체력을 25만큼 회복하고 공격력을 1 증가시킵니다. 굉장히 바삭하고 고소합니다!\n<size=17><color=#969696>??? : 또 올테니 긴장해~</color></size>";
                img.sprite = GameManager.instance.shop_Food_img[3];
                cost = 25;
            }
            else if (item_num == 4)
            {
                item_explane.text = "초콜릿 도넛 [30 코인]\n체력을 20만큼 회복하고 공격력 2 이동속도를 1 증가시킵니다. 굉장히 달콤한 맛이 나는 도넛입니다!";
                img.sprite = GameManager.instance.shop_Food_img[4];
                cost = 30;
            }
            else if (item_num == 5)
            {
                item_explane.text = "피자 [40 코인]\n체력을 30만큼 회복하고 공격력 2 공격속도 2 증가시킵니다.";
                img.sprite = GameManager.instance.shop_Food_img[5];
                cost = 40;
            }
            else if (item_num == 6)
            {
                item_explane.text = "삶은 계란 [5 코인]\n체력을 5만큼 회복합니다. 고소한 맛이 일품이지만 마실 것이 없으면 목이 굉장히 막혀옵니다.";
                img.sprite = GameManager.instance.shop_Food_img[6];
                cost = 5;
            }
            else if (item_num == 7)
            {
                item_explane.text = "소세지 [20 코인]\n체력을 20만큼 회복하고 이동속도를 2 증가시킵니다. 오동통하며 노릇노릇하고 맛있는 냄새가 납니다.";
                img.sprite = GameManager.instance.shop_Food_img[7];
                cost = 20;
            }
            else if (item_num == 8)
            {
                item_explane.text = "민트초코 [1 코인]\n체력을 1만큼 회복합니다. 굉장히 이상한 식감이 느껴집니다.";
                img.sprite = GameManager.instance.shop_Food_img[8];
                cost = 1;
            }
        }
        else if(type == 1)
        {
            // 독성 마법
            if (item_num == 0 && GameManager.instance.attack_object[6] == false)
            {
                item_explane.text = "마법부여 : 독성마법 [60 코인]\n적에게 기본공격 적중시마다 0.7초마다 적에게 공격력의" + (int)((GameManager.instance.attack_object_cnt[6]) * 100) + "% 고정피해를 입히는 '중독' 상태이상을 부여합니다. '중독' 상태이상은 5초동안 유지됩니다.";
                img.sprite = GameManager.instance.shop_Item_img[0];
                cost = 60;
            }
            else if (item_num == 0 && GameManager.instance.attack_object[6] == true)
            {
                item_explane.text = "마법부여 : 독성마법+ [60 코인]\n'중독'상태이상의 공격력을 1% 증가시킵니다.";
                img.sprite = GameManager.instance.shop_Item_img[0];
                cost = 60;
            }

            // 폭발 마법
            if (item_num == 1 && GameManager.instance.attack_object[8] == false)
            {
                item_explane.text = "마법부여 : 폭발마법 [65 코인]\n적에게 가하는 기본공격에 폭발마법을 부여합니다. 적에게 기본공격이 적중시 마법 폭발을 일으키는 구체를 적에게 부착시킵니다\n폭발 데미지는 공격력의 165% 에 해당합니다.";
                img.sprite = GameManager.instance.shop_Item_img[1];
                cost = 65;
            }
            if (item_num == 1 && GameManager.instance.attack_object[8] == true)
            {
                item_explane.text = "마법부여 : 폭발마법+\n폭발마법의 폭발 데미지를 10% 증가시킵니다.";
                img.sprite = GameManager.instance.shop_Item_img[1];
                cost = 65;
            }

            // 라이트닝
            if (item_num == 2 && GameManager.instance.attack_object[25] == false)
            {
                item_explane.text = "라이트닝! [75 코인]\n<size=18>기본 공격이 상시로 일직선 형태의 전기를 전방으로 방출하는 형태로 변화하며 공격력이 10 증가합니다.</size>\n<size=17>지형과 적을 관통할 수 있지만 기존의 기본공격 강화 효과는 받을 수 없습니다.</size>";
                img.sprite = GameManager.instance.shop_Item_img[2];
                cost = 75;
            }
            else if (item_num == 2 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] == 0)
            {
                item_explane.text = "라이트닝 샷 [75 코인]\n기본공격을 충전 형식으로 변경합니다. 충전이 완료된 상태에서 공격시 레이저 형태의 전격을 전방에 발사합니다. 지형과 적을 관통 할 수 있습니다.\n" +
                    "'폭발마법'같은 기본공격 강화 효과가 적용 가능합니다.";
                img.sprite = GameManager.instance.shop_Item_img[2];
                cost = 75;
            }
            else if (item_num == 2 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] == 1)
            {
                item_explane.text = "전류방출 [75 코인]\n20% 확률로 '라이트닝 샷'을 피격한 적에게 '방전' 효과를 발생시킵니다." +
                    "\n<size='16'>방전 : 피격당한 대상을 중심으로 강력한 전류가 흐르는 번개의 구가 생겨 닿는 모든 적에게 공격력의 100%에 달하는 피해를 주며 45% 확률로 마비시킵니다.</size>";
                img.sprite = GameManager.instance.shop_Item_img[2];
                cost = 75;
            }
            else if (item_num == 2 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] >= 2)
            {
                item_explane.text = "전류방출+[75 코인]\n'방전'효과의 공격력을 5% 증가시킵니다. 현재 추가 공격력" + GameManager.instance.attack_object_cnt[25] * 5+"%";
                img.sprite = GameManager.instance.shop_Item_img[2];
                cost = 75;
            }

            // 회전구체
            if (item_num == 3 && GameManager.instance.attack_object[0] == false)
            {
                item_explane.text = "회전의 마력구체 [55 코인]\n13초마다 5초동안 플레이어 캐릭터의 주변을 회전하는 구체를 생성합니다. 중복 선택시 그 구체의 갯수가 늘어납니다.\n<size=14>플레이어 공격력의 75% 만큼의 데미지.</size>";
                img.sprite = GameManager.instance.shop_Item_img[3];
                cost = 55;
            }
            if (item_num == 3 && GameManager.instance.attack_object[0] == true)
            {
                item_explane.text = "회전의 마력구체 [55 코인]\n마력 구체의 숫자를 한개 더 늘립니다. \n현재 마력구체 갯수 : " + GameManager.instance.attack_object_cnt[0];
                img.sprite = GameManager.instance.shop_Item_img[3];
                cost = 55;
            }

            // 보호의 장막
            if (item_num == 4)
            {
                int an = (int)GameManager.instance.attack_object_cnt[3] - 1;
                if (an < 5)
                {
                    an = 5;
                }
                item_explane.text = "보호의 장막[70 코인]\n" + an + "초마다 적의 공격을 1회 막아주는 보호막을 생성합니다.";
                img.sprite = GameManager.instance.shop_Item_img[4];
                cost = 70;
            }

            // 신비한 돌
            if (item_num == 5)
            {
                item_explane.text = "신비한 돌 [25 코인]\n" + GameManager.instance.attack_object_cnt[14] + "%확률로 공격력, 공격속도, 이동속도 3가지중 하나를 5 증가시킵니다. 성공시 성공확률이 10% 차감되고 실패시 10% 증가하고 어떠한 효과도 받을 수 없습니다. \n(최대 75% 최저 25%)"
                    + "<size=17><color=#969696>??? : 이게... 확률이 좀 이상해!</color></size>";
                img.sprite = GameManager.instance.shop_Item_img[5];
                cost = 25;
            }

            // 중첩 마력
            if (item_num == 6 && GameManager.instance.attack_object[24] == false)
            {
                item_explane.text = "중첩의 마력 [65 코인]\n기본공격 발사시 탄알에 담겨있는 마력을 적에게 주입시켜 중첩시킵니다. 3초 후 중첩된 마력은 적 주변에 공격력의 115%의 마법구체가 중첩 된 수 만큼 생성되어 적을 향해 타격합니다." +
                        " (최대 중첩 수 5회)";
                img.sprite = GameManager.instance.shop_Item_img[6];
                cost = 65;
            }
            else if (item_num == 6 && GameManager.instance.attack_object[24] == true)
            {
                item_explane.text = "중첩의 마력+ [65 코인]\n중첩의 마력 공격력 계수가 5% 증가합니다.\n현재계수 : " + GameManager.instance.attack_object_cnt[24] * 100 + "%";
                img.sprite = GameManager.instance.shop_Item_img[6];
                cost = 65;
            }

        }
        
    }

    public void Buy() 
    {
        bt.buy_alert.SetActive(true);
        bt.buy_number = item_num;
        bt.buy_cost = cost;
        bt.buy_type = type;
        bt.buy_alery_text.text = "정말로 구매하시겠습니까?\n" + cost + "코인이 소모 됩니다.";
    }
}
