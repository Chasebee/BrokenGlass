using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Use_Item", menuName = "Scriptable Obejct/ Use_Item")]
public class Use_Item : ScriptableObject
{
    public int item_id;
    public int item_type; // 0 포션 1 주문서 등.
    public string item_name;
    public Sprite item_img; // 아이템 이미지
    public string item_exp; // 아이템 설명
    public int buy_price; // 상점 구매가
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
