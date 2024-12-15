using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Btn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject btm;
    public GameObject object_1;
    public GameObject option_screen;
    public GameObject[] btns;
    public int btn_num; // 버튼 자체 넘버 30, 31, 32, 33은 타이틀 화면 전용 버튼들
    public TextMeshProUGUI mesh_text;
    int num;
    string scene_Name;

    void Start() 
    {
        if (btn_num == 34 && GameManager.instance.playerdata.story_Mod_Clear == true) 
        {
            gameObject.GetComponent<Button>().interactable = true;
            mesh_text.text = "생존 모드";
        }
    }

    // 처음부터 시작
    public void GameStart()
    {
        GameManager.instance.survival_Mod = false;
        GameManager.instance.SFX_Play("Button", GameManager.instance.sfx[0], 1);
        GameManager.instance.Setting();
        num = GameManager.instance.stage;
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
        GameManager.instance.area_num = 1;

        // 음악 바꾸기
        GameManager.instance.BGM_Play(GameManager.instance.bgm[1]);
    }
    public void Exit_Game()
    {
        GameManager.instance.SFX_Play("Button", GameManager.instance.sfx[0], 1);
        Application.Quit();
    }
    public void Shop_Open()
    {
        GameManager.instance.SFX_Play("Button", GameManager.instance.sfx[0], 1);
        option_screen.SetActive(true);
    }
    public void Shop_Close()
    {
        GameManager.instance.SFX_Play("Button", GameManager.instance.sfx[0], 1);
        object_1.SetActive(false);
    }
    public void Shop_Alert_Close()
    {
        GameManager.instance.SFX_Play("Button", GameManager.instance.sfx[0], 1);
        object_1.SetActive(false);
        btns[0].SetActive(true);
        btns[1].SetActive(true);
        btns[2].SetActive(false);
    }
    public void Arena_Mod_Start() 
    {
        GameManager.instance.survival_Mod = true;
        GameManager.instance.SFX_Play("Button", GameManager.instance.sfx[0], 1);
        GameManager.instance.Setting();
        GameManager.instance.attack_object_cnt[7] = 56;
        scene_Name = "Arena_Stage";
        Loading_Scene.LoadScene(scene_Name);

        // 음악 체인지
        GameManager.instance.BGM_Play(GameManager.instance.bgm[9]);
    }

    // 셋팅관련
    public void Setting_Option_Open()
    {
        GameManager.instance.Save_PlayerData_ToJson();
        GameManager.instance.SFX_Play("Button", GameManager.instance.sfx[0], 1);
        option_screen.SetActive(true);
        GameObject.Find("Frame_Setting_Bar").GetComponent<Slider>().value = GameManager.instance.frame_set;
        GameObject.Find("BGM_Slider").GetComponent<Slider>().value = GameManager.instance.bgm_value;
        GameObject.Find("SFX_Slider").GetComponent<Slider>().value = GameManager.instance.sfx_value;

        GameObject.Find("Frame_Texts").GetComponent<TextMeshProUGUI>().text = "프레임\n<size=20>(" + GameManager.instance.frame_set + ")</size>";
    }
    public void Setting_Option_Close()
    {
        GameManager.instance.Save_PlayerData_ToJson();
        GameManager.instance.SFX_Play("Button", GameManager.instance.sfx[0], 1);
        option_screen.SetActive(false);
    }
    public void BGM_Volume(float val)
    {
        GameManager.instance.mixer.SetFloat("BGM", Mathf.Log10(val) * 20);
        GameManager.instance.bgm_value = val;
    }
    public void SFX_Volume(float val)
    {
        GameManager.instance.mixer.SetFloat("SFX", Mathf.Log10(val) * 20);
        GameManager.instance.sfx_value = val;
    }
    public void Frame_Setting() 
    {
        int frame = Mathf.RoundToInt(gameObject.GetComponent<Slider>().value);
        GameManager.instance.frame_set = frame;
        Application.targetFrameRate = frame;
        GameObject.Find("Frame_Texts").GetComponent<TextMeshProUGUI>().text = "프레임\n<size=20>(" + GameManager.instance.frame_set + ")</size>";
    }

    // 게임 시간 정지
    public void Time_Stop()
    {
        Time.timeScale = 0.0f;
    }

    // 게임 시간 활성화
    public void Time_Run()
    {
        Time.timeScale = 1.0f;
    }

    // 타이틀로
    public void Goto_Title()
    {
        GameManager.instance.SFX_Play("Button", GameManager.instance.sfx[0], 1);
        GameManager.instance.playerdata.coin_now = GameManager.instance.coin;
        GameManager.instance.Setting();
        object_1.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.instance.area_num = 0;
        GameManager.instance.stage_num = 0;
        GameManager.instance.BGM_Play(GameManager.instance.bgm[0]);
        Loading_Scene.LoadScene("Title");
    }

    public void Bonus_Shop_Buy() 
    {
        if (GameManager.instance.coin >= GameManager.instance.bonus_shop_cost)
        {
            GameManager.instance.SFX_Play("buy", GameManager.instance.sfx[1], 1);
            // 첫 구매
            if (GameManager.instance.playerdata.Achievements[19] == false && GameManager.instance.achievement_bool == false)
            {
                GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                GameManager.instance.achievement_bool = true;
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>도전과제 달성!</color>\n사전준비";
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[6];
                Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                GameManager.instance.playerdata.Achievements[19] = true;
                GameManager.instance.Save_PlayerData_ToJson();
            }
            GameManager.instance.coin -= GameManager.instance.bonus_shop_cost;
            GameManager.instance.playerdata.coin_now = GameManager.instance.coin;
            // 체력
            if (GameManager.instance.bonus_shop_value == 0)
            {
                if (GameManager.instance.playerdata.bonus_lv[0] == 0)
                {
                    GameManager.instance.playerdata.bonus_lv[0]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[0] == 1)
                {
                    GameManager.instance.playerdata.bonus_lv[0]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[0] == 2)
                {
                    GameManager.instance.playerdata.bonus_lv[0]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[0] == 3)
                {
                    GameManager.instance.playerdata.bonus_lv[0]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[0] == 4)
                {
                    GameManager.instance.playerdata.bonus_lv[0]++;
                }
            }
            // 공격력
            else if (GameManager.instance.bonus_shop_value == 1)
            {
                if (GameManager.instance.playerdata.bonus_lv[1] == 0)
                {
                    GameManager.instance.playerdata.bonus_lv[1]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[1] == 1)
                {
                    GameManager.instance.playerdata.bonus_lv[1]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[1] == 2)
                {
                    GameManager.instance.playerdata.bonus_lv[1]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[1] == 3)
                {
                    GameManager.instance.playerdata.bonus_lv[1]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[1] == 4)
                {
                    GameManager.instance.playerdata.bonus_lv[1]++;
                }
            }
            // 방어력
            else if (GameManager.instance.bonus_shop_value == 2)
            {
                if (GameManager.instance.playerdata.bonus_lv[2] == 0)
                {
                    GameManager.instance.playerdata.bonus_lv[2]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[2] == 1)
                {
                    GameManager.instance.playerdata.bonus_lv[2]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[2] == 2)
                {
                    GameManager.instance.playerdata.bonus_lv[2]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[2] == 3)
                {
                    GameManager.instance.playerdata.bonus_lv[2]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[2] == 4)
                {
                    GameManager.instance.playerdata.bonus_lv[2]++;
                }
            }
            // 공격속도
            else if (GameManager.instance.bonus_shop_value == 3)
            {
                if (GameManager.instance.playerdata.bonus_lv[3] == 0)
                {
                    GameManager.instance.playerdata.bonus_lv[3]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[3] == 1)
                {
                    GameManager.instance.playerdata.bonus_lv[3]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[3] == 2)
                {
                    GameManager.instance.playerdata.bonus_lv[3]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[3] == 3)
                {
                    GameManager.instance.playerdata.bonus_lv[3]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[3] == 4)
                {
                    GameManager.instance.playerdata.bonus_lv[3]++;
                }
            }
            // 탄속
            else if (GameManager.instance.bonus_shop_value == 4)
            {
                if (GameManager.instance.playerdata.bonus_lv[4] == 0)
                {
                    GameManager.instance.playerdata.bonus_lv[4]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[4] == 1)
                {
                    GameManager.instance.playerdata.bonus_lv[4]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[4] == 2)
                {
                    GameManager.instance.playerdata.bonus_lv[4]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[4] == 3)
                {
                    GameManager.instance.playerdata.bonus_lv[4]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[4] == 4)
                {
                    GameManager.instance.playerdata.bonus_lv[4]++;
                }
            }
            // 스피드
            else if (GameManager.instance.bonus_shop_value == 5)
            {
                if (GameManager.instance.playerdata.bonus_lv[5] == 0)
                {
                    GameManager.instance.playerdata.bonus_lv[5]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[5] == 1)
                {
                    GameManager.instance.playerdata.bonus_lv[5]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[5] == 2)
                {
                    GameManager.instance.playerdata.bonus_lv[5]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[5] == 3)
                {
                    GameManager.instance.playerdata.bonus_lv[5]++;
                }
                else if (GameManager.instance.playerdata.bonus_lv[5] == 4)
                {
                    GameManager.instance.playerdata.bonus_lv[5]++;
                }
            }
            // 점프력
            else if (GameManager.instance.bonus_shop_value == 6)
            {
                if (GameManager.instance.playerdata.bonus_lv[6] == 0)
                {
                    GameManager.instance.playerdata.bonus_lv[6]++;
                    GameManager.instance.playerdata.bonus_jump_power += 0.03f;
                }
                else if (GameManager.instance.playerdata.bonus_lv[6] == 1)
                {
                    GameManager.instance.playerdata.bonus_lv[6]++;
                    GameManager.instance.playerdata.bonus_jump_power += 0.03f;
                }
                else if (GameManager.instance.playerdata.bonus_lv[6] == 2)
                {
                    GameManager.instance.playerdata.bonus_lv[6]++;
                    GameManager.instance.playerdata.bonus_jump_power += 0.05f;
                }
                else if (GameManager.instance.playerdata.bonus_lv[6] == 3)
                {
                    GameManager.instance.playerdata.bonus_lv[6]++;
                    GameManager.instance.playerdata.bonus_jump_power += 0.05f;
                }
                else if (GameManager.instance.playerdata.bonus_lv[6] == 4)
                {
                    GameManager.instance.playerdata.bonus_lv[6]++;
                    GameManager.instance.playerdata.bonus_jump_power += 0.07f;
                }
            }
            // 점프 횟수
            else if (GameManager.instance.bonus_shop_value == 7)
            {
                if (GameManager.instance.playerdata.bonus_lv[7] == 0)
                {
                    GameManager.instance.playerdata.bonus_lv[7]++;
                    GameManager.instance.playerdata.bonus_jump_cnt++;
                }
            }
            object_1.SetActive(false);
            GameManager.instance.Setting();
        }
        else if (GameManager.instance.coin < GameManager.instance.bonus_shop_cost)
        {
            GameManager.instance.SFX_Play("buy", GameManager.instance.sfx[2], 1);
            option_screen.GetComponent<TextMeshProUGUI>().text = "코인이 부족합니다!";
            btns[0].SetActive(false);
            btns[1].SetActive(false);
            btns[2].SetActive(true);
        }
    }

    // 업적 관련 버튼들
    public void Achievements_Room_Open()
    {
        GameManager.instance.SFX_Play("Button", GameManager.instance.sfx[0], 1);
        object_1.SetActive(true);
    }

    public void Achievements_Room_Close()
    {
        GameManager.instance.SFX_Play("Button", GameManager.instance.sfx[0], 1);
        object_1.SetActive(false);
    }

    public void Achievements_Close()
    {
        GameManager.instance.SFX_Play("Button", GameManager.instance.sfx[0], 1);
        object_1.SetActive(false);
    }

    // 메인화면 컴퓨터 이미지 효과
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (btn_num == 30)
        {
            object_1.GetComponent<Computer>().number = 0;
            object_1.GetComponent<Computer>().anim.SetBool("Trans", true);
        }
        else if (btn_num == 31)
        {
            object_1.GetComponent<Computer>().number = 1;
            object_1.GetComponent<Computer>().anim.SetBool("Trans", true);
        }
        else if (btn_num == 32)
        {
            option_screen.GetComponent<Computer>().number = 2;
            option_screen.GetComponent<Computer>().anim.SetBool("Trans", true);
        }
        else if (btn_num == 33)
        {
            object_1.GetComponent<Computer>().number = 3;
            object_1.GetComponent<Computer>().anim.SetBool("Trans", true);
        }
        else if (btn_num == 34 && GameManager.instance.playerdata.story_Mod_Clear == true)
        {
            object_1.GetComponent<Computer>().number = 4;
            object_1.GetComponent<Computer>().anim.SetBool("Trans", true);
        }
        else if (btn_num == 35)
        {
            object_1.GetComponent<Computer>().number = 5;
            object_1.GetComponent<Computer>().anim.SetBool("Trans", true);
        }
    }
    public void OnPointerExit(PointerEventData eventData) 
    {
        if (btn_num == 30)
        {
            object_1.GetComponent<Computer>().anim.SetBool("Trans", false);
            object_1.GetComponent<Computer>().anim.SetBool("Load", false);
        }
        else if (btn_num == 31)
        {
            object_1.GetComponent<Computer>().anim.SetBool("Trans", false);
            object_1.GetComponent<Computer>().anim.SetBool("Load", false);
        }
        else if (btn_num == 32)
        {
            option_screen.GetComponent<Computer>().anim.SetBool("Trans", false);
            option_screen.GetComponent<Computer>().anim.SetBool("Load", false);
        }
        else if (btn_num == 33)
        {
            object_1.GetComponent<Computer>().anim.SetBool("Trans", false);
            object_1.GetComponent<Computer>().anim.SetBool("Load", false);
        }
        else if (btn_num == 34 && GameManager.instance.playerdata.story_Mod_Clear == true)
        {
            object_1.GetComponent<Computer>().anim.SetBool("Trans", false);
            object_1.GetComponent<Computer>().anim.SetBool("Load", false);
        }
        else if (btn_num == 35)
        {
            object_1.GetComponent<Computer>().anim.SetBool("Trans", false);
            object_1.GetComponent<Computer>().anim.SetBool("Load", false);
        }
    }

    
}