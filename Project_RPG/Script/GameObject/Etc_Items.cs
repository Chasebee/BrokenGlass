using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etc_Items
{
    public int item_id;
    public string item_name;
    public Sprite item_img; // ������ �̹���
    public string item_exp; // ������ ����
    public int sell_price; // ���� �ǸŰ�
    public int rare; // ������ ��͵�

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
