using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Use_Item", menuName = "Scriptable Obejct/ Use_Item")]
public class Use_Item : ScriptableObject
{
    public int item_id;
    public int item_type; // 0 ���� 1 �ֹ��� ��.
    public string item_name;
    public Sprite item_img; // ������ �̹���
    public string item_exp; // ������ ����
    public int buy_price; // ���� ���Ű�
    public int sell_price; // ���� �ǸŰ�
    public int rare; // ������ ��͵�

    public int hp_recover; // ü�� ȸ��
    public int mp_recover; // ���� ȸ��
    public string map_name; // ��ȯ �ֹ���

    public int reserves;

    public bool cant_Create;
    public string[] create_item;
    public int[] material_count;
}
