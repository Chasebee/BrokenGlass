using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenTory_Equipment
{
    public int item_id; // ������ �ѹ�
    public string item_name; // ������ �̸�
    public string item_exp; // ������ ����
    public int item_lv; // ������ ���� ��������
    public int type; // ������ ����, �� ����
    public int attack_type; // ����Ÿ��
    public float[] base_option; // ������ �⺻ �ɷ�ġ
    public int enforce; // ��ȭ ��ġ
    public int sell_price; // ���� �ǸŰ�
    public int rare; // ������ ��͵�

    public float[] item_option; // ������ ȹ��� ������ �߰� �ɷ�ġ
    public int[] item_skill_option; // ������ ȹ��� ������ �߰� ��ų����
}
