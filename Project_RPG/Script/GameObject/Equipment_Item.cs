using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Item
{
    // 인스펙터에서 미리 작성할 목록

    public int item_id; // 아이템 넘버
    public string item_name; // 아이템 이름
    public string item_exp; // 아이템 설명
    public int item_lv; // 아이템 장착 레벨제한
    public int type; // 아이템 무기, 방어구 구분
    public int attack_type; // 무기타입
    public float[] base_option; // 아이템 기본 능력치
    public int enforce; // 강화 수치
    public int sell_price; // 상점 판매가

    /* base_option 작성 규칙
    활인 경우의 순서 - 공격력, 마력, 공격속도, 치명타 확률
    검인 경우 - 공격력, 공격속도, 치명타 확률
    수정구인 경우 - 마력, 공격속도, 치명타 확률
    
    머리, 몸통, 신발 등과 같은 방어구의 순서는
    체력, 방어력, 회피율 으로 처리된다
     */

    public int rare; // 아이템 희귀도

    // 아이템 획득시 랜덤으로 지정 배열의 크기는 인스펙터에서 따로 지정한다.
    public int[] item_option_min;
    public int[] item_option_max;

    public float[] item_option; // 아이템 획득시 지정될 추가 능력치
    public int[] item_skill_option; // 아이템 획득시 지정될 추가 스킬레벨

    public Equipment_Item(Equip_Item item) 
    {
        this.item_id = item.item_id;
        this.item_name = item.item_name;
        this.item_exp = item.item_exp;
        this.item_lv = item.item_lv;
        this.type = item.type;
        this.attack_type = item.attack_type;
        this.base_option = item.base_option;
        this.rare = item.rare;

        this.item_option = item.item_option;
        this.item_skill_option = item.item_skill_option;
    }
}
