using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Btn : MonoBehaviour
{
    public GameObject[] bt_object;
    public AudioClip[] clip;
    GameObject btm;
    BattleManager bt;
    int rnd;
    string scene_Name;
    bool calculator = false;
    void Start()
    {
        btm = GameObject.FindWithTag("Manager");
        bt = GameObject.FindWithTag("Manager").GetComponent<BattleManager>();
    }
    // 포탈 관련
    public void Next_Stage()
    {
        GameManager.instance.stage++;
        // 일반 방
        if (GameManager.instance.stage <= 5)
        {
            int num = GameManager.instance.stage;
            switch (num)
            {
                case 1:
                    scene_Name = "Stage_1";
                    break;

                case 2:
                    scene_Name = "Stage_2";
                    break;

                case 3:
                    scene_Name = "Stage_3";
                    break;

                case 4:
                    scene_Name = "Stage_4";
                    break;

                case 5:
                    scene_Name = "Stage_5";
                    break;
            }
            Loading_Scene.LoadScene(scene_Name);
        }
        // 보스 방
        else if (GameManager.instance.stage == 6)
        {
            switch (GameManager.instance.area_num)
            {
                case 1:
                    GameManager.instance.BGM_Play(GameManager.instance.bgm[2]);
                    break;
                case 2:
                    GameManager.instance.BGM_Play(GameManager.instance.bgm[4]);
                    break;
                case 3:
                    GameManager.instance.BGM_Play(GameManager.instance.bgm[6]);
                    break;
                case 4:
                    GameManager.instance.BGM_Play(GameManager.instance.bgm[8]);
                    break;
            }
            Loading_Scene.LoadScene("Boss_Stage");
        }
        // 보스 클리어 후
        else if (GameManager.instance.stage == 7)
        {
            GameManager.instance.area_num++;
            GameManager.instance.stage = 1;
            int num = GameManager.instance.stage;
            switch (num)
            {
                case 1:
                    scene_Name = "Stage_1";
                    break;

                case 2:
                    scene_Name = "Stage_2";
                    break;

                case 3:
                    scene_Name = "Stage_3";
                    break;

                case 4:
                    scene_Name = "Stage_4";
                    break;

                case 5:
                    scene_Name = "Stage_5";
                    break;
            }
            Loading_Scene.LoadScene(scene_Name);
            switch (GameManager.instance.area_num)
            {
                case 1:
                    GameManager.instance.BGM_Play(GameManager.instance.bgm[1]);
                    break;
                case 2:
                    GameManager.instance.BGM_Play(GameManager.instance.bgm[3]);
                    break;
                case 3:
                    GameManager.instance.BGM_Play(GameManager.instance.bgm[5]);
                    break;
                case 4:
                    GameManager.instance.BGM_Play(GameManager.instance.bgm[7]);
                    break;
            }
        }
        Time.timeScale = 1.0f;
    }

    // 상점 관련
    public void Shop_Dialouge_Close() 
    {
        bt.dialogue[1].SetActive(false);
        for (int i = 0; i < bt.shop_items.Count; i++)
        {
            Destroy(bt.shop_items[i]);
        }
        int len = bt.shop_items.Count;
        for (int i = 0; i < len; i++)
        {
            bt.shop_items.RemoveAt(0);
        }
        Destroy(GameObject.FindWithTag("Shop"));
        Time.timeScale = 1.0f;
    }
    public void Shop_Open() 
    {
        bt.shop.SetActive(true);
    }
    public void Shop_Close()
    {
        bt.shop.SetActive(false);
    }
    public void Buy_Item() 
    {
        if (bt.buy_cost <= GameManager.instance.coin)
        {
            // 좋은 거래였다.
            if (GameManager.instance.playerdata.Achievements[17] == false && GameManager.instance.achievement_bool == false)
            {
                GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                GameManager.instance.achievement_bool = true;
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>도전과제 달성!</color>\n좋은 거래였다.";
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[4];
                Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                GameManager.instance.playerdata.Achievements[17] = true;
                GameManager.instance.Save_PlayerData_ToJson();
            }
            GameManager.instance.coin -= bt.buy_cost;
            // 음식 아이템
            if (bt.buy_type == 0)
            {
                if (bt.buy_number == 0)
                {
                    GameManager.instance.hp += 10;
                }
                else if (bt.buy_number == 1)
                {
                    GameManager.instance.hp += 20;
                }
                else if (bt.buy_number == 2)
                {
                    GameManager.instance.hp += 25;
                }
                else if (bt.buy_number == 3)
                {
                    GameManager.instance.hp += 25;
                    GameManager.instance.atk++;
                    calculator = true;
                }
                else if (bt.buy_number == 4)
                {
                    GameManager.instance.hp += 20;
                    GameManager.instance.atk += 2;
                    GameManager.instance.speed += 0.01f;
                    calculator = true;
                }
                else if (bt.buy_number == 5)
                {
                    GameManager.instance.hp += 30;
                    GameManager.instance.atk += 2;
                    GameManager.instance.atk_cool -= 0.02f;
                    calculator = true;
                }
                else if (bt.buy_number == 6)
                {
                    GameManager.instance.hp += 5;
                }
                else if (bt.buy_number == 7)
                {
                    GameManager.instance.hp += 20;
                    GameManager.instance.speed += 0.02f;
                }
                else if (bt.buy_number == 8)
                {
                    GameManager.instance.hp += 1;
                }
            }
            
            // 옵션 아이템
            else if (bt.buy_type == 1)
            {
                // 독성마법
                if (bt.buy_number == 0 && GameManager.instance.attack_object[6] == false)
                {
                    GameManager.instance.attack_object[6] = true;
                    GameManager.instance.attack_object_cnt[6] = 0.03f;
                }
                else if (bt.buy_number == 0 && GameManager.instance.attack_object[6] == true)
                {
                    GameManager.instance.attack_object_cnt[6] += 0.01f;
                }

                // 폭발마법
                if (bt.buy_number == 1 && GameManager.instance.attack_object[8] == false)
                {
                    GameManager.instance.attack_object[8] = true;
                }
                else if (bt.buy_number == 1 && GameManager.instance.attack_object[8] == true)
                {
                    GameManager.instance.attack_object_cnt[8] += 0.1f;
                }

                // 라이트닝!
                if (bt.buy_number == 2 && GameManager.instance.attack_object[25] == false)
                {
                    GameManager.instance.attack_object[25] = true;
                    Instantiate(bt.attack_objects[3]);
                }
                else if (bt.buy_number == 2 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] <= 0)
                {
                    Destroy(GameObject.FindWithTag("Lightning"));
                    GameManager.instance.attack_object_cnt[25] = 1;
                    if (GameManager.instance.arrow_size <= 0.8f)
                    {
                        GameManager.instance.arrow_size = 0.8f;
                    }
                    else
                    {
                        GameManager.instance.arrow_size += 2f;
                    }
                }
                else if (bt.buy_number == 2 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] >= 1)
                {
                    GameManager.instance.attack_object_cnt[25]++;
                }

                // 회전구체
                if (bt.buy_number == 3 && GameManager.instance.attack_object[0] == false)
                {
                    GameManager.instance.attack_object[0] = true;
                    GameManager.instance.attack_object_cnt[0] = 3;
                }
                else if (bt.buy_number == 3 && GameManager.instance.attack_object[0] == true)
                {
                    GameManager.instance.attack_object_cnt[0]++;
                }

                // 보호의 장막
                if (bt.buy_number == 4)
                {
                    GameManager.instance.attack_object[3] = true;
                    if (GameManager.instance.attack_object_cnt[3] > 5)
                    {
                        GameManager.instance.attack_object_cnt[3]--;
                    }
                }

                // 신비한 돌
                if (bt.buy_number == 5) 
                {
                    int faill = 100 - (int)GameManager.instance.attack_object_cnt[14];
                    int total = 100;
                    int pivot = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));
                    int rock = Random.Range(0, 2);
                    // 성공
                    if (pivot >= faill)
                    {
                        switch (rock)
                        {
                            case 0:
                                GameManager.instance.atk += 5;
                                calculator = true;
                                break;
                            case 1:
                                GameManager.instance.atk_cool -= 0.05f;
                                break;
                            case 2:
                                GameManager.instance.speed += 0.05f;
                                break;
                        }
                        GameManager.instance.attack_object_cnt[14] -= 10;
                        if (GameManager.instance.attack_object_cnt[14] <= 25)
                        {
                            GameManager.instance.attack_object_cnt[14] = 25;
                        }
                    }

                    // 실패
                    else if (pivot < faill)
                    {
                        GameManager.instance.attack_object_cnt[14] += 10;
                        if (GameManager.instance.attack_object_cnt[14] >= 75)
                        {
                            GameManager.instance.attack_object_cnt[14] = 75;
                        }
                    }
                }

                // 중첩마력
                if (bt.buy_number == 6 && GameManager.instance.attack_object[24] == false)
                {
                    GameManager.instance.attack_object[24] = true;
                    GameManager.instance.attack_object_cnt[24] = 1.15f;
                }
                else if (bt.buy_number == 6 && GameManager.instance.attack_object[24] == true)
                {
                    GameManager.instance.attack_object_cnt[24] += 0.05f;
                }
            }

            // 질보다 양 먹은경우
            if (GameManager.instance.attack_object[26] == true && calculator == true)
            {
                int cal = GameManager.instance.real_atk - GameManager.instance.atk;
                cal = GameManager.instance.calcul - cal;
                GameManager.instance.real_atk += cal;
                GameManager.instance.atk = GameManager.instance.real_atk - (int)(GameManager.instance.real_atk * 0.85);
                GameManager.instance.calcul = GameManager.instance.real_atk - GameManager.instance.atk;

                GameManager.instance.atk_cool = 0.13f;
            }
            
            bt.buy_alert.SetActive(false);
        }
        else if (bt.buy_cost > GameManager.instance.coin) 
        {
            bt_object[0].SetActive(false);
            bt_object[1].SetActive(false);
            bt_object[2].SetActive(true);
            bt_object[3].GetComponent<TextMeshProUGUI>().text = "코인이 부족합니다.";
        }
    }
    public void Buy_Cancel() 
    {
        bt_object[0].SetActive(true);
        bt_object[1].SetActive(true);
        bt_object[2].SetActive(false);

        bt.buy_alert.SetActive(false);
    }
}
