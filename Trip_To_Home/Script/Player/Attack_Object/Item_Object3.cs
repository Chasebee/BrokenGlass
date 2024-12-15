using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Item_Object3 : MonoBehaviour
{
    public float cool;
    public float timmer;
    public bool oper;
    public GameObject player;
    SpriteRenderer rend;
    public int cnt;
    public void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rend = GetComponent<SpriteRenderer>();
        
    }
    void Update()
    {
        cool = GameManager.instance.attack_object_cnt[3];
        if (GameManager.instance.attack_object[3] == true && cool >= timmer && oper == false) 
        {
            timmer += Time.deltaTime;
        }
        if(GameManager.instance.attack_object[3] == true && cool <= timmer && oper == false) 
        {
            oper = true;
            rend.enabled = true;
        }

        // 리필해주세요
        if (GameManager.instance.playerdata.Achievements[20] == false && GameManager.instance.achievement_bool == false && cnt >= 10)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>도전과제 달성!</color>\n리필해주세요";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[7];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[20] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.survival_Mod == false)
        {
            if ((collision.gameObject.CompareTag("Monster2") || collision.gameObject.CompareTag("Monster_Arrow") || collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Summon") || collision.gameObject.CompareTag("Summon_Trap"))
                && oper == true)
            {
                // 도전과제 이거 방탄유리야!
                if (GameManager.instance.playerdata.Achievements[10] == false && GameManager.instance.achievement_bool == false)
                {
                    GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                    GameManager.instance.achievement_bool = true;
                    GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>도전과제 달성!</color>\n이거 방탄유리야!";
                    GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Lv_Up_item_Img_n[16];
                    Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                    GameManager.instance.playerdata.Achievements[10] = true;
                    GameManager.instance.Save_PlayerData_ToJson();
                }

                player.layer = 9;
                // 맞아서 투명(무적판정)
                player.GetComponent<Player_Move>().spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                Invoke("infinity_disable", 3f);
                timmer = 0f;
                oper = false;
                rend.enabled = false;
                cnt++;
            }
        }
        else if (GameManager.instance.survival_Mod == true) 
        {
            if (collision.gameObject.CompareTag("S_Monster") && oper == true)
            {
                player.layer = 9;
                player.GetComponent<Survival_Mod_Player_Move>().rend.color = new Color(1, 1, 1, 0.5f);
                Invoke("infinity_disable", 3f);
                timmer = 0f;
                oper = false;
                rend.enabled = false;
            }
        }
    }
    public void infinity_disable() 
    {
        player.layer = 8;
        if (GameManager.instance.survival_Mod == false)
        {
            player.GetComponent<Player_Move>().spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        else if (GameManager.instance.survival_Mod == true)
        {
            player.GetComponent<Survival_Mod_Player_Move>().rend.color = new Color(1, 1, 1, 1);
        }
    }
}
