using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Holy_Magic : MonoBehaviour
{
    public int type; // 일반 몹 판정 / 소환체 판정
    public int cnt;
    public float timmer;
    public GameObject parent_pos;
    public GameObject[] objects;
    bool confirm;

    void Update()
    {
        if (type == 0)
        {
            cnt = parent_pos.GetComponent<Monster2>().light_magic_cnt;
            if (timmer <= 3.7f && confirm == false)
            {
                timmer += Time.deltaTime;
            }
            if (timmer >= 3.7f && confirm == false)
            {
                confirm = true;
                timmer = 0f;
                StartCoroutine(Summon());
            }
        }

        else if (type == 1) 
        {
            cnt = parent_pos.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().light_magic_cnt;
            if (timmer <= 3.7f && confirm == false)
            {
                timmer += Time.deltaTime;
            }
            if (timmer >= 3.7f && confirm == false)
            {
                confirm = true;
                timmer = 0f;
                StartCoroutine(Summon2());
            }

        }
    }

    IEnumerator Summon() 
    {
        yield return new WaitForSeconds(0f);
        // 2스택 이하
        if (cnt <= 2)
        {
            objects[0].GetComponent<Holy_Effect_Explosion>().cnt = cnt;
            Instantiate(objects[0], parent_pos.transform.position, Quaternion.Euler(0, 0, 0));
        }

        // 3스택 이상 4스택 이하
        else if ((cnt >= 3 && cnt <= 4))
        {
            objects[1].GetComponent<Holy_Sword1>().cnt = cnt;
            objects[1].GetComponent<Holy_Sword1>().number = 3;
            GameObject sword = Instantiate(objects[1], new Vector2(parent_pos.transform.position.x, parent_pos.transform.position.y + 3f), Quaternion.Euler(0, 0, 0));
            sword.transform.localScale = new Vector3(1, 1, 1);
            sword.GetComponent<Holy_Sword1>().target_name = parent_pos.name;
            sword.GetComponent<Holy_Sword1>().pos = parent_pos;
            sword.transform.parent = parent_pos.transform;
        }

        // 5스택 이상
        else if (cnt >= 5)
        {
            objects[0].GetComponent<Holy_Effect_Explosion>().cnt = cnt;
            Instantiate(objects[0], parent_pos.transform.position, Quaternion.Euler(0, 0, 0));
            for(int i = 0; i < 3; i++)
            {
                objects[1].GetComponent<Holy_Sword1>().number = i;
                objects[1].GetComponent<Holy_Sword1>().cnt = cnt;
                GameObject sword = Instantiate(objects[1]);
                sword.GetComponent<Holy_Sword1>().target_name = parent_pos.name;
                sword.transform.parent = parent_pos.transform;
                sword.GetComponent<Holy_Sword1>().pos = parent_pos;
                if (i == 0)
                {
                    sword.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                    sword.transform.position = new Vector2(parent_pos.transform.position.x - 3, parent_pos.transform.position.y + 2);
                    sword.transform.rotation = Quaternion.Euler(0, 0, 45f);
                }
                else if (i == 1)
                {
                    sword.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                    sword.transform.position = new Vector2(parent_pos.transform.position.x, parent_pos.transform.position.y + 3);
                }
                else if (i == 2)
                {
                    sword.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                    sword.transform.position = new Vector2(parent_pos.transform.position.x + 3, parent_pos.transform.position.y + 2);
                    sword.transform.rotation = Quaternion.Euler(0, 0, -45f);
                }
                yield return new WaitForSeconds(0.2f);
            }
        }

        parent_pos.GetComponent<Monster2>().magic_type_light = false;
        parent_pos.GetComponent<Monster2>().light_magic_cnt = 1;
        Destroy(gameObject);
    }
    IEnumerator Summon2()
    {
        yield return new WaitForSeconds(0f);
        // 2스택 이하
        if (cnt <= 2)
        {
            objects[0].GetComponent<Holy_Effect_Explosion>().cnt = cnt;
            Instantiate(objects[0], parent_pos.transform.position, Quaternion.Euler(0, 0, 0));
        }

        // 3스택 이상 4스택 이하
        else if ((cnt >= 3 && cnt <= 4))
        {
            objects[1].GetComponent<Holy_Sword1>().cnt = cnt;
            objects[1].GetComponent<Holy_Sword1>().number = 3;
            GameObject sword = Instantiate(objects[1], new Vector2(parent_pos.transform.position.x, parent_pos.transform.position.y + 3f), Quaternion.Euler(0, 0, 0));
            sword.transform.localScale = new Vector3(1, 1, 1);
            sword.GetComponent<Holy_Sword1>().target_name = parent_pos.name;
            sword.GetComponent<Holy_Sword1>().pos = parent_pos;
            sword.transform.parent = parent_pos.transform;
        }

        // 5스택 이상
        else if (cnt >= 5)
        {
            objects[0].GetComponent<Holy_Effect_Explosion>().cnt = cnt;
            Instantiate(objects[0], parent_pos.transform.position, Quaternion.Euler(0, 0, 0));
            for (int i = 0; i < 3; i++)
            {
                objects[1].GetComponent<Holy_Sword1>().number = i;
                objects[1].GetComponent<Holy_Sword1>().cnt = cnt;
                GameObject sword = Instantiate(objects[1]);
                sword.GetComponent<Holy_Sword1>().target_name = parent_pos.name;
                sword.transform.parent = parent_pos.transform;
                sword.GetComponent<Holy_Sword1>().pos = parent_pos;
                if (i == 0)
                {
                    sword.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                    sword.transform.position = new Vector2(parent_pos.transform.position.x - 3, parent_pos.transform.position.y + 2);
                    sword.transform.rotation = Quaternion.Euler(0, 0, 45f);
                }
                else if (i == 1)
                {
                    sword.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                    sword.transform.position = new Vector2(parent_pos.transform.position.x, parent_pos.transform.position.y + 3);
                }
                else if (i == 2)
                {
                    sword.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                    sword.transform.position = new Vector2(parent_pos.transform.position.x + 3, parent_pos.transform.position.y + 2);
                    sword.transform.rotation = Quaternion.Euler(0, 0, -45f);
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
        
        parent_pos.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().magic_type_light = false;
        parent_pos.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().light_magic_cnt = 1;
        Destroy(gameObject);
    }
}
