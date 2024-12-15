using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenTory_Equipment
{
    public int item_id; // 아이템 넘버
    public string item_name; // 아이템 이름
    public string item_exp; // 아이템 설명
    public int item_lv; // 아이템 장착 레벨제한
    public int type; // 아이템 무기, 방어구 구분
    public int attack_type; // 무기타입
    public float[] base_option; // 아이템 기본 능력치
    public int enforce; // 강화 수치
    public int sell_price; // 상점 판매가
    public int rare; // 아이템 희귀도

    public float[] item_option; // 아이템 획득시 지정될 추가 능력치
    public int[] item_skill_option; // 아이템 획득시 지정될 추가 스킬레벨
}
