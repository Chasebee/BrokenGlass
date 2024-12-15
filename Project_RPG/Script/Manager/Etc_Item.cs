using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Etc_Item", menuName = "Scriptable Obejct/ Etc_Item")]
public class Etc_Item : ScriptableObject
{
    public int item_id;
    public string item_name;
    public Sprite item_img; // ������ �̹���
    public string item_exp; // ������ ����
    public int buy_price;
    public int sell_price; // ���� �ǸŰ�
    public int rare; // ������ ��͵�

    public bool cant_Create;
    public string[] create_item;
    public int[] material_count;
}
