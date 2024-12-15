using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survival_Mod_Attack : MonoBehaviour
{
    [Header("공격 위치 설정")]
    public float dir_X;
    public float dir_Y;
    public Vector3 attack_Pos;
    public GameObject attack_Pos_Object;
    public GameObject[] s_atk_objects;
    public bool[] s_atk_obj;

    [Header("공격관련")]
    public GameObject arrow;
    public float cool_timer;

    void Update()
    {
        attack_Pos.x = transform.position.x + dir_X;
        attack_Pos.y = transform.position.y + dir_Y;
        attack_Pos.z = -1;
        attack_Pos_Object.transform.position = attack_Pos;

        if (dir_X == 0 && dir_Y == 0)
        {
            attack_Pos_Object.SetActive(false);
        }
        else 
        {
            attack_Pos_Object.SetActive(true);

            if (dir_X == 0 && dir_Y == 1)
            {
                attack_Pos_Object.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (dir_X == 1 && dir_Y == 1)
            {
                attack_Pos_Object.transform.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (dir_X == 1 && dir_Y == 0)
            {
                attack_Pos_Object.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (dir_X == 1 && dir_Y == -1)
            {
                attack_Pos_Object.transform.rotation = Quaternion.Euler(0, 0, -135);
            }
            else if (dir_X == 0 && dir_Y == -1)
            {
                attack_Pos_Object.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (dir_X == -1 && dir_Y == 1)
            {
                attack_Pos_Object.transform.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (dir_X == -1 && dir_Y == 0)
            {
                attack_Pos_Object.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (dir_X == -1 && dir_Y == -1)
            {
                attack_Pos_Object.transform.rotation = Quaternion.Euler(0, 0, 135);
            }
        }

        if (GameManager.instance.atk_cool > cool_timer)
        {
            cool_timer += Time.deltaTime;
        }

        if (GameManager.instance.atk_cool <= cool_timer) 
        {
            GameObject a = Instantiate(arrow, attack_Pos_Object.transform.position, attack_Pos_Object.transform.rotation);
            a.GetComponent<Survival_Mod_Arrow>().arrow_pos.x = dir_X;
            a.GetComponent<Survival_Mod_Arrow>().arrow_pos.y = dir_Y;
            if (dir_X == 0 && dir_Y == 0)
            {
                a.GetComponent<Survival_Mod_Arrow>().arrow_pos.x = gameObject.GetComponent<Survival_Mod_Player_Move>().flip_img;
            }
            cool_timer = 0;
        }
        // 썬더 샷
        if (s_atk_obj[0] == true) 
        {
            s_atk_obj[0] = false;
            GameObject a = Instantiate(s_atk_objects[0], attack_Pos_Object.transform.position, attack_Pos_Object.transform.rotation);
            a.GetComponent<Thunder_Shot>().arrow_pos.x = dir_X;
            a.GetComponent<Thunder_Shot>().arrow_pos.y = dir_Y;
            if (dir_X == 0 && dir_Y == 0)
            {
                a.GetComponent<Thunder_Shot>().arrow_pos.x = gameObject.GetComponent<Survival_Mod_Player_Move>().flip_img;
            }
        }
    }
}
