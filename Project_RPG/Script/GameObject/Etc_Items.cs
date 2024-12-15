using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etc_Items
{
    public int item_id;
    public string item_name;
    public Sprite item_img; // 아이템 이미지
    public string item_exp; // 아이템 설명
    public int sell_price; // 상점 판매가
    public int rare; // 아이템 희귀도

    public bool cant_Create;
    public string[] create_item;
    public int[] material_count;

    public Etc_Items(Etc_Item item) 
    {
        this.item_id= item.item_id;
        this.item_name= item.item_name;
        this.item_exp= item.item_exp;
        this.sell_price= item.sell_price;
        this.rare = item.rare;
        
    }
}
