using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Key_Setting_Btn : MonoBehaviour
{
    public int key = -1;
    public int num;
    public TextMeshProUGUI self_text;
    bool setting_mod = false;

    void Update()
    {
        self_text.text = KeySet.keys[(Key_Action)num].ToString();
        if (self_text.text == "UpArrow")
        {
            self_text.text = "↑";
        }
        else if (self_text.text == "DownArrow")
        {
            self_text.text = "↓";
        }
        else if (self_text.text == "LeftArrow")
        {
            self_text.text = "←";
        }
        else if (self_text.text == "RightArrow")
        {
            self_text.text = "→";
        }
    }
    // 키 변경 설정
    private void OnGUI()
    {
        Event key_Evenet = Event.current;
        if (key_Evenet.isKey && setting_mod == true)
        {
            KeySet.keys[(Key_Action)key] = key_Evenet.keyCode;
            GameManager.instance.playerdata.save_Key[key] = key_Evenet.keyCode;
            key = -1;
            setting_mod = false;
        }
    }

    public void Change_Key(int set)
    {
        key = set;
        setting_mod = true;
    }
}
