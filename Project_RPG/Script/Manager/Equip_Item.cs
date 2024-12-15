using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Equip_Item", menuName = "Scriptable Obejct/ Equip_Item")]
public class Equip_Item : ScriptableObject
{
    // �ν����Ϳ��� �̸� �ۼ��� ���

    public int item_id; // ������ �ѹ�
    public string item_name; // ������ �̸�
    public Sprite[] item_img; // ������ �̹���
    public string item_exp; // ������ ����
    public int item_lv; // ������ ���� ��������
    public int type; // ������ ����, �� ����
    public int attack_type; // ����Ÿ��
    public float[] base_option; // ������ �⺻ �ɷ�ġ
    public int enforce; // ��ȭ ��ġ
    public int buy_price;
    public int sell_price; // ���� �ǸŰ�

    /* type �ۼ� ��Ģ
    1- ����
    2- �Ӹ�
    3- ����
    4- �Ź�
     */
    /* attack_type �ۼ� ��Ģ
     1 - ��
     2- Ȱ
     3 - ����
     4 - �ְ�
     */
    public int rare; // ������ ��͵�

    // ������ ȹ��� �������� ���� �迭�� ũ��� �ν����Ϳ��� ���� �����Ѵ�.
    public float[] item_option_min;
    public float[] item_option_max;
    /*
        �߰��ɼ���
        �� [0] = �� [1] = ���ݼӵ� [2]  = ġ��ŸȮ��
        Ȱ [0] = �� [1] = �� [2] = ġ��ŸȮ��
        ���� [0] = �� [1] = ���� [2] = ġ��ŸȮ��
        �� �̿ܿ��� �� �����غ��� 
     */
    public float[] item_option; // ������ ȹ��� ������ �߰� �ɷ�ġ
    public int[] item_skill_option; // ������ ȹ��� ������ �߰� ��ų����

    public bool cant_Create;
    public string[] create_Item; // �����ʿ������
    public string[] disassemble_item; // ���ؽ� ȹ�������
    public int[] material_count; // ���� �䱸����
    public int[] disassemble_material_count; // ���ؽ� �ִ� ȹ�淮
}

