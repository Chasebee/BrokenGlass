using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenTory_Use
{
    public int item_id;
    public int item_type; // 0 포션 1 주문서 등.
    public string item_name;
    public string item_exp; // 아이템 설명
    public int sell_price; // 상점 판매가
    public int rare; // 아이템 희귀도

    public int hp_recover; // 체력 회복
    public int mp_recover; // 마나 회복
    public string map_name; // 귀환 주문서

    public int reserves;

    public bool cant_Create;
    public string[] create_item;
    public int[] material_count;
}
