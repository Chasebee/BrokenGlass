using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Stack_Magic : MonoBehaviour
{
    public int type; // 일반 몹 판정 / 소환체 판정
    public int cnt;
    public float timmer;
    public GameObject arrow, parent_pos;
    bool confirm;
    void Update()
    {
        if (type == 0)
        {
            cnt = parent_pos.GetComponent<Monster2>().stack_magic_cnt;
            if (cnt >= 6)
            {
                cnt = 5;
            }
            if (timmer <= 3f && confirm == false)
            {
                timmer += Time.deltaTime;
            }
            if (timmer >= 3f && confirm == false)
            {
                confirm = true;
                timmer = 0f;
                StartCoroutine(Summon());
            }
        }

        else if (type == 1)
        {
            cnt = parent_pos.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().stack_magic_cnt;
            if (cnt >= 6)
            {
                cnt = 5;
            }
            if (timmer <= 3f && confirm == false)
            {
                timmer += Time.deltaTime;
            }
            if (timmer >= 3f && confirm == false)
            {
                confirm = true;
                timmer = 0f;
                StartCoroutine(Summon_Type2());
            }
        }
    }
    IEnumerator Summon() 
    {
        for (int i = 0; i < cnt; i++) 
        {
            arrow.GetComponent<Stack_Magic_Summon>().center_obj = parent_pos;
            Instantiate(arrow);
            yield return new WaitForSeconds(0.3f);
        }
        parent_pos.GetComponent<Monster2>().stack_magic = false;
        parent_pos.GetComponent<Monster2>().stack_magic_cnt = 1;
        Destroy(gameObject);
    }
    IEnumerator Summon_Type2()
    {
        for (int i = 0; i < cnt; i++)
        {
            arrow.GetComponent<Stack_Magic_Summon>().center_obj = parent_pos;
            Instantiate(arrow);
            yield return new WaitForSeconds(0.3f);
        }
        parent_pos.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().stack_magic = false;
        parent_pos.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().stack_magic_cnt = 1;
        Destroy(gameObject);
    }
}
