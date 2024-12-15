using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Etc_Item", menuName = "Scriptable Obejct/ Etc_Item")]
public class Etc_Item : ScriptableObject
{
    public int item_id;
    public string item_name;
    public Sprite item_img; // 아이템 이미지
    public string item_exp; // 아이템 설명
    public int buy_price;
    public int sell_price; // 상점 판매가
    public int rare; // 아이템 희귀도

    public bool cant_Create;
    public string[] create_item;
    public int[] material_count;
}
