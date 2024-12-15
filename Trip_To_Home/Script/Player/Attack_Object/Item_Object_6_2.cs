using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Item_Object_6_2 : MonoBehaviour
{
    public int dmg, type, cnt;
    public float timmer;
    void Start()
    {
        // type = 0 점착탄 아이템 type = 1 폭발탄창 데미지
        if (type == 0)
        {
            dmg = (int)(GameManager.instance.atk * (GameManager.instance.attack_object_cnt[5] / 100));
        }
        else if (type == 1) 
        {
            dmg = (int)(GameManager.instance.atk * GameManager.instance.attack_object_cnt[8] / 100);
        }
        // 애니메이션 오브젝트 소환할것
    }
    void Update()
    {
        if (cnt >= 3) 
        {
            // 업적
            if (GameManager.instance.playerdata.Achievements[14] == false && GameManager.instance.achievement_bool == false)
            {
                GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                GameManager.instance.achievement_bool = true;
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>도전과제 달성!</color>\n예술?";
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Lv_Up_item_Img_r[3];
                Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                GameManager.instance.playerdata.Achievements[14] = true;
                GameManager.instance.Save_PlayerData_ToJson();
            }
        }
        timmer += Time.deltaTime;
        if (timmer >= 1.2f) 
        {
            Destroy(gameObject);
        }
    }
}
