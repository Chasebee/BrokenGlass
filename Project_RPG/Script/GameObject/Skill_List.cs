using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Obejct/ Skill")]
public class Skill_List : ScriptableObject
{
    public int skill_type;
    public int skill_class;
    public int skill_num;
    public string skill_name;
    public Sprite skill_img;
    public int use_mp;
    public float charge_time;
    public string skill_text;
    public float[] skill_var;
    public float[] skill_var_2;
    public int max_lv;
    public int pre_skill;
    public int[] sub_skill;
    public bool passive;
    public bool explane_type; // true시 % 타입 false 시 일반 타입 텍스트가 붙는다
    public bool double_stat;
}
