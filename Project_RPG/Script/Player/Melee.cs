using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public int atk, num;
    public bool piercing;
    public float dmg, dmg_reset, speed, skill_var;
    float timer;
    public bool ciritical;
    public HashSet<GameObject> piercing_object = new HashSet<GameObject>();
    Skill_List skill;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.SetInteger("Num", num);
    }
    public void Attack_Start() 
    {
        dmg = Random.Range(GameManager.instance.atk * 0.85f, GameManager.instance.atk * 1.15f);
        Dmg_Calcul();
    }
    public void Skill_Start(int num) 
    {
        Debug.Log(num);
        for (int i = 0; i < GameManager.instance.allSkills.Length; i++) 
        {
            if (GameManager.instance.allSkills[i].skill_type == 1) 
            {
                if (GameManager.instance.allSkills[i].skill_num == num) 
                {
                    skill = GameManager.instance.allSkills[i];
                    break;
                }
            }
        }
        int sk_var = GameManager.instance.sword_skill[num];
        float multiple = 0;
        if (sk_var >= 1 && sk_var <= 3)
        {
            multiple = skill.skill_var[1];
        }
        else if (sk_var >= 4 && sk_var <= 7)
        {
            multiple = skill.skill_var[2];
        }
        else if (sk_var >= 8 && sk_var <= 11)
        {
            multiple = skill.skill_var[3];
        }
        else if (sk_var >= 12 && sk_var <= 15)
        {
            multiple = skill.skill_var[4];
        }
        else if (sk_var >= 16 && sk_var <= 19)
        {
            multiple = skill.skill_var[5];
        }
        else if (sk_var == 20)
        {
            multiple = skill.skill_var[6];
        }

        dmg = (int)(multiple * Random.Range(GameManager.instance.atk, GameManager.instance.atk * 1.15f));
        Dmg_Calcul();
    }
    void Dmg_Calcul() 
    {
        float crit = Random.Range(0.0f, 1.0f);

        // 치명타 처리
        if (GameManager.instance.critical >= crit)
        {
            dmg = dmg * 1.3f;
            ciritical = true;
        }
        else
        {
            ciritical = false;
        }
        atk = (int)dmg;
    }
}
