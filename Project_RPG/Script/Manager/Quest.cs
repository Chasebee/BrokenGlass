using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Obejct/ Quest")]
public class Quest : ScriptableObject
{
    public int quest_value; // 0 - ���� 1- ����
    public int quest_type; // 0 - ��� Ŭ���� 1 - ��ȭ�� 2 - ���� óġ 3 - ������ ���� 4 - ����óġ, ����
    public int quest_num;
    public string quest_name;
    public string quest_explane;

    public string object_npc;
    public string[] object_item_name;
    public int[] object_item_cnt;

    public string[] object_mob;
    public int[] object_mob_cnt;

    public int reward_exp;
    public string[] reward_item_name;
    public int[] reward_item_cnt;
    public bool cleard;
    public bool now;

    // NPC ��ũ��Ʈ�� ����Ʈ�� �����ϴ� NPC�� �� ��ũ���Ӹ� ������Ʈ�� ����
}

// ���� �����̵Ǵ� ����Ʈ ��
public class User_Quest_List
{
    public int quest_value;
    public int quest_type;
    public int quest_num;
    public string quest_name;
    public string quest_explane;

    public string object_npc;
    public string[] object_item_name;
    public int[] p_object_item_cnt;
    public int[] object_item_cnt;

    public string[] object_mob;
    public int[] object_mob_cnt;
    public int[] p_object_mob_cnt;

    public int reward_exp;
    public string[] reward_item_name;
    public int[] reward_item_cnt;

    public bool cleard;
    public bool now;
}
