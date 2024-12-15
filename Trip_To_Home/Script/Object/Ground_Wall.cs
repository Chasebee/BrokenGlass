using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Wall : MonoBehaviour
{
    public Vector3[] position;
    public bool on_off;
    public bool summon;
    public GameObject[] monster;
    Vector3 vel = Vector3.zero;
    public Transform pos;
    void Update()
    {
        // 챕터 1 3스테이지 방
        if (GameManager.instance.area_num == 1 && GameManager.instance.stage_num == 4)
        {
            if (on_off == true)
            {
                transform.position = Vector3.SmoothDamp(gameObject.transform.position, position[1], ref vel, 0.5f);
                if (summon == false)
                {
                    summon = true;
                    StartCoroutine(Mob_Spawn(2, 0));
                    StartCoroutine(Mob_Spawn2(2, 2));
                }
            }
            else if (on_off == false)
            {
                transform.position = Vector3.SmoothDamp(gameObject.transform.position, position[0], ref vel, 0.5f);
            }
        }
        // 챕터 2 3스테이지 방
        else if (GameManager.instance.area_num == 2 && GameManager.instance.stage_num == 4)
        {
            if (on_off == true)
            {
                transform.position = Vector3.SmoothDamp(gameObject.transform.position, position[1], ref vel, 0.5f);
                if (summon == false)
                {
                    summon = true;
                    StartCoroutine(Mob_Spawn(1, 0));
                    StartCoroutine(Mob_Spawn2(2, 2));
                    StartCoroutine(Mob_Spawn2(1, 1));
                }
            }
            else if (on_off == false)
            {
                transform.position = Vector3.SmoothDamp(gameObject.transform.position, position[0], ref vel, 0.5f);
            }
        }
        // 챕터 4 1스테이지 방
        else if (GameManager.instance.area_num == 4 && GameManager.instance.stage_num == 2)
        {
            if (on_off == true)
            {
                transform.position = Vector3.SmoothDamp(gameObject.transform.position, position[1], ref vel, 0.5f);
                if (summon == false)
                {
                    summon = true;
                }
            }
            else if (on_off == false)
            {
                transform.position = Vector3.SmoothDamp(gameObject.transform.position, position[0], ref vel, 0.5f);
            }
        }
        // 챕터 4 2스테이지 방
        else if (GameManager.instance.area_num == 4 && GameManager.instance.stage_num == 3)
        {
            if (on_off == true)
            {
                transform.position = Vector3.SmoothDamp(gameObject.transform.position, position[1], ref vel, 0.5f);
                if (summon == false) 
                {
                    summon = true;
                    Mob_Spawn(2, 0);
                    Mob_Spawn2(2, 1);
                }
            }
            else if (on_off == false)
            {
                transform.position = Vector3.SmoothDamp(gameObject.transform.position, position[0], ref vel, 0.5f);
            }
        }
        // 챕터 4 5스테이지 방
        else if (GameManager.instance.area_num == 4 && GameManager.instance.stage_num == 6)
        {
            if (on_off == true)
            {
                transform.position = Vector3.SmoothDamp(gameObject.transform.position, position[1], ref vel, 0.5f);
            }
            else if (on_off == false)
            {
                transform.position = Vector3.SmoothDamp(gameObject.transform.position, position[0], ref vel, 0.5f);
            }
        }
    }



    /* -------- 몹 소환 트리거 -------- */
    IEnumerator Mob_Spawn(int cnt, int type) 
    {
        for(int i = 0; i < cnt; i++) 
        {
            Instantiate(monster[type], pos.transform.position, transform.rotation);
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator Mob_Spawn2(int cnt, int type)
    {
        for (int i = 0; i < cnt; i++)
        {
            yield return new WaitForSeconds(1.5f);
            Instantiate(monster[type], pos.transform.position, transform.rotation);
        }
    }
}
