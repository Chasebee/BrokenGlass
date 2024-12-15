using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bonus_Stat_Text : MonoBehaviour
{
    public int num;
    public TextMeshProUGUI stat_text;
    void Update()
    {
        switch (num)
        {
            case 0:
                stat_text.text = GameManager.instance.playerdata.bonus_hp.ToString();
                break;
            case 1:
                stat_text.text = GameManager.instance.playerdata.bonus_atk.ToString();
                break;
            case 2:
                stat_text.text = GameManager.instance.playerdata.bonus_def.ToString();
                break;
            case 3:
                stat_text.text = GameManager.instance.playerdata.bonus_atk_cool.ToString();
                break;
            case 4:
                stat_text.text = GameManager.instance.playerdata.bonus_arrow_speed.ToString();
                break;
            case 5:
                stat_text.text = GameManager.instance.playerdata.bonus_speed.ToString();
                break;
            case 6:
                stat_text.text = GameManager.instance.playerdata.bonus_jump_power.ToString();
                break;
            case 7:
                stat_text.text = GameManager.instance.playerdata.bonus_jump_cnt.ToString();
                break;
            case 8:
                if (GameManager.instance.coin == 0)
                {
                    stat_text.text = "0";
                }
                else
                {
                    stat_text.text = CommaText(GameManager.instance.coin);
                }
                break;
        }
    }

    public string CommaText(int gold)
    {
        return string.Format("{0:#,###}", gold);
    }

}
