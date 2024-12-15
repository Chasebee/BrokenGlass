using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Equip_Item", menuName = "Scriptable Obejct/ Equip_Item")]
public class Equip_Item : ScriptableObject
{
    // 인스펙터에서 미리 작성할 목록

    public int item_id; // 아이템 넘버
    public string item_name; // 아이템 이름
    public Sprite[] item_img; // 아이템 이미지
    public string item_exp; // 아이템 설명
    public int item_lv; // 아이템 장착 레벨제한
    public int type; // 아이템 무기, 방어구 구분
    public int attack_type; // 무기타입
    public float[] base_option; // 아이템 기본 능력치
    public int enforce; // 강화 수치
    public int buy_price;
    public int sell_price; // 상점 판매가

    /* type 작성 규칙
    1- 무기
    2- 머리
    3- 몸통
    4- 신발
     */
    /* attack_type 작성 규칙
     1 - 검
     2- 활
     3 - 마법
     4 - 쌍검
     */
    public int rare; // 아이템 희귀도

    // 아이템 획득시 랜덤으로 지정 배열의 크기는 인스펙터에서 따로 지정한다.
    public float[] item_option_min;
    public float[] item_option_max;
    /*
        추가옵션은
        검 [0] = 공 [1] = 공격속도 [2]  = 치명타확률
        활 [0] = 공 [1] = 마 [2] = 치명타확률
        마법 [0] = 마 [1] = 마나 [2] = 치명타확률
        그 이외에는 더 생각해보자 
     */
    public float[] item_option; // 아이템 획득시 지정될 추가 능력치
    public int[] item_skill_option; // 아이템 획득시 지정될 추가 스킬레벨

    public bool cant_Create;
    public string[] create_Item; // 제작필요아이템
    public string[] disassemble_item; // 분해시 획득아이템
    public int[] material_count; // 제작 요구수량
    public int[] disassemble_material_count; // 분해시 최대 획득량
}

