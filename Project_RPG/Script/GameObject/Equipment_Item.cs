using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Item
{
    // �ν����Ϳ��� �̸� �ۼ��� ���

    public int item_id; // ������ �ѹ�
    public string item_name; // ������ �̸�
    public string item_exp; // ������ ����
    public int item_lv; // ������ ���� ��������
    public int type; // ������ ����, �� ����
    public int attack_type; // ����Ÿ��
    public float[] base_option; // ������ �⺻ �ɷ�ġ
    public int enforce; // ��ȭ ��ġ
    public int sell_price; // ���� �ǸŰ�

    /* base_option �ۼ� ��Ģ
    Ȱ�� ����� ���� - ���ݷ�, ����, ���ݼӵ�, ġ��Ÿ Ȯ��
    ���� ��� - ���ݷ�, ���ݼӵ�, ġ��Ÿ Ȯ��
    �������� ��� - ����, ���ݼӵ�, ġ��Ÿ Ȯ��
    
    �Ӹ�, ����, �Ź� ��� ���� ���� ������
    ü��, ����, ȸ���� ���� ó���ȴ�
     */

    public int rare; // ������ ��͵�

    // ������ ȹ��� �������� ���� �迭�� ũ��� �ν����Ϳ��� ���� �����Ѵ�.
    public int[] item_option_min;
    public int[] item_option_max;

    public float[] item_option; // ������ ȹ��� ������ �߰� �ɷ�ġ
    public int[] item_skill_option; // ������ ȹ��� ������ �߰� ��ų����

    public Equipment_Item(Equip_Item item) 
    {
        this.item_id = item.item_id;
        this.item_name = item.item_name;
        this.item_exp = item.item_exp;
        this.item_lv = item.item_lv;
        this.type = item.type;
        this.attack_type = item.attack_type;
        this.base_option = item.base_option;
        this.rare = item.rare;

        this.item_option = item.item_option;
        this.item_skill_option = item.item_skill_option;
    }
}
