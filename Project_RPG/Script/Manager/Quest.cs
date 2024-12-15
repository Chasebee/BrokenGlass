using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Obejct/ Quest")]
public class Quest : ScriptableObject
{
    public int quest_value; // 0 - 메인 1- 서브
    public int quest_type; // 0 - 즉시 클리어 1 - 대화형 2 - 몬스터 처치 3 - 아이템 조달 4 - 몬스터처치, 조달
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

    // NPC 스크립트에 퀘스트가 존재하는 NPC면 이 스크립텁르 오브젝트를 주자
}

// 실제 저장이되는 퀘스트 값
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
