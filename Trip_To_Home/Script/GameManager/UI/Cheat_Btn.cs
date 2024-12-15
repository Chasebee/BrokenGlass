using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat_Btn : MonoBehaviour
{
    GameObject btm;
    BattleManager bt;
    public int area_num;
    public int stage_num;
    RectTransform r_pos;
    bool btn_stat;
    void Start()
    {
        r_pos = GetComponent<RectTransform>();
        //btm = GameObject.FindWithTag("Manager");
        //bt = GameObject.FindWithTag("Manager").GetComponent<BattleManager>();
    }

    public void Cheat_Option_Buttons()
    {
        if (btn_stat == false)
        {
            r_pos.anchoredPosition = new Vector2(697, 0);
            btn_stat = true;
        }
        else if (btn_stat == true)
        {
            r_pos.anchoredPosition = new Vector2(937, 0);
            btn_stat = false;
        }
    }

    public void Chapter_Select()
    {
        GameManager.instance.SFX_Play("Portal", GameManager.instance.sfx[3], 1);

        GameManager.instance.area_num = area_num;
        GameManager.instance.stage = 1;
        int num = GameManager.instance.stage;
        Loading_Scene.LoadScene("Stage_1");
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

    public void Stage_Select() 
    {
        GameManager.instance.SFX_Play("Portal", GameManager.instance.sfx[3], 1);

        string stage_name = null;
        switch (stage_num)
        {
            case 1:
                GameManager.instance.stage = 1;
                stage_name = "Stage_1";
                break;
            case 2:
                GameManager.instance.stage = 2;
                stage_name = "Stage_2";
                break;
            case 3:
                GameManager.instance.stage = 3;
                stage_name = "Stage_3";
                break;
            case 4:
                GameManager.instance.stage = 4;
                stage_name = "Stage_4";
                break;
            case 5:
                GameManager.instance.stage = 5;
                stage_name = "Stage_5";
                break;
            case 6:
                GameManager.instance.stage = 6;
                stage_name = "Boss_Stage";
                break;
        }
        if (stage_num == 6)
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
        }
        else
        {

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
        Loading_Scene.LoadScene(stage_name);
    }

    public void HP_Set() 
    {
        GameManager.instance.maxhp = 9999;
        GameManager.instance.hp = 9999;
    }
    public void ATK_Set()
    {
        GameManager.instance.atk = 99999;
    }
    public void LV_up() 
    {
        GameManager.instance.exp = GameManager.instance.maxexp;
    }
}
