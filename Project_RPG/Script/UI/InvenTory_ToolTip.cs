using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvenTory_ToolTip : MonoBehaviour
{
    public int tool_type;
    public InvenTory_Equipment equip_item;
    public InvenTory_Use use_item;
    public InvenTory_Etc etc_item;
    public TextMeshProUGUI[] stats;

    public float halfwidth, halfheight, pos_x, pos_y;
    RectTransform rect;

    void Awake()
    {
        halfwidth = GetComponentInParent<CanvasScaler>().referenceResolution.x * 0.5f;
        halfheight = GetComponentInParent<CanvasScaler>().referenceResolution.y * 0.5f;
        rect = GetComponent<RectTransform>();
    }

    void Update() 
    {
        FollowMouse();
    }

    public void FollowMouse()
    {
        // 마우스 위치 가져오기
        Vector3 mousePosition = Input.mousePosition;

        Vector3 tooltipPosition = mousePosition + new Vector3(pos_x, pos_y, 0); 
        transform.position = tooltipPosition;

        // 툴팁이 화면을 벗어나지 않도록 처리
        if (rect.anchoredPosition.x + rect.sizeDelta.x > halfwidth)
        {
            rect.pivot = new Vector2(1, 1);
            if (rect.anchoredPosition.y + rect.sizeDelta.y < halfheight)
            {
                rect.pivot = new Vector2(1, 0);
            }
        }
        else
        {
            rect.pivot = new Vector2(0, 1);
            if (rect.anchoredPosition.y + rect.sizeDelta.y < halfheight)
            {
                rect.pivot = new Vector2(0, 0);
            }
        }
    }


    public void ToolTip_Stat()
    {
        // 장비
        if (tool_type == 0)
        {
            string rare = null;
            switch (equip_item.rare)
            {
                case 0:
                    rare = "노말";
                    break;
                case 1:
                    rare = "언커먼";
                    break;
                case 2:
                    rare = "레어";
                    break;
                case 3:
                    rare = "에픽";
                    break;
                case 4:
                    rare = "레전더리";
                    break;
            }

            string equipment_type = null;
            switch (equip_item.attack_type)
            {
                case 1:
                    equipment_type = "검/쌍검";
                    break;
                case 2:
                    equipment_type = "활";
                    break;
                case 3:
                    equipment_type = "아카식 레코드";
                    break;
                case 4:
                    equipment_type = "모자/투구";
                    break;
                case 5:
                    equipment_type = "상의";
                    break;
                case 6:
                    equipment_type = "하의";
                    break;
            }

            stats[0].text = equip_item.item_name;
            stats[1].text = equipment_type + " Lv." + equip_item.item_lv;
            stats[2].text = rare;
            // True 처리후 다시 False 처리
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i].gameObject.SetActive(true);
            }

            // 기본스텟
            for (int i = 0; i < equip_item.base_option.Length; i++)
            {
                if (equip_item.base_option[i] == 0)
                {
                    int a = (3 + i);
                    stats[a].gameObject.SetActive(false);
                }
                else
                {
                    if (i == 0)
                    {
                        stats[3].text = "공격력 +" + equip_item.base_option[i];
                    }
                    else if (i == 1)
                    {
                        stats[4].text = "마력 +" + equip_item.base_option[i];
                    }
                    else if (i == 2)
                    {
                        stats[5].text = "공격속도 " + Mathf.Round(equip_item.base_option[i] * 100) + "%";
                    }
                    else if (i == 3)
                    {
                        stats[6].text = "최대체력 +" + equip_item.base_option[i];
                    }
                    else if (i == 4)
                    {
                        stats[7].text = "최대마나 +" + equip_item.base_option[i];
                    }
                    else if (i == 5)
                    {
                        stats[8].text = "방어력 +" + equip_item.base_option[i];
                    }
                    else if (i == 6)
                    {
                        stats[9].text = "이동속도 +" + (equip_item.base_option[i] / 5.6f) * 100 + "%";
                    }
                }
            }

            // 옵션이 있는지 없는지 체크용
            int option_num = 0;
            int skill_option_num = 0;

            // 추가스텟
            for (int i = 0; i < equip_item.item_option.Length; i++)
            {
                if (equip_item.item_option[i] > 0)
                {
                    if (i == 0)
                    {
                        if (equip_item.type == 1)
                        {
                            if (equip_item.attack_type == 1)
                            {
                                stats[10].text = "<color=#34495e>추가옵션</color>\n공격력 +" + equip_item.item_option[i];
                            }
                            else if (equip_item.attack_type == 2)
                            {
                                stats[10].text = "<color=#34495e>추가옵션</color>\n공격력 +" + equip_item.item_option[i];
                            }
                            else if (equip_item.attack_type == 3)
                            {
                                stats[10].text = "<color=#34495e>추가옵션</color>\n마력 +" + equip_item.item_option[i];
                            }
                        }
                        else if(equip_item.type != 1)
                        {
                            if (equip_item.attack_type == 4 || equip_item.attack_type == 5)
                            {
                                stats[10].text = "<color=#34495e>추가옵션</color>\n최대체력 +" + equip_item.item_option[i];
                            }
                            else if (equip_item.attack_type == 6)
                            {
                                stats[10].text = "<color=#34495e>추가옵션</color>\n최대마나 +" + equip_item.item_option[i];
                            }
                        }
                    }
                    else if (i == 1)
                    {
                        if (equip_item.type == 1)
                        {
                            if (equip_item.attack_type == 1)
                            {
                                stats[10].text += "  공격속도 +" + Mathf.Round(equip_item.item_option[i] * 100f) + "%";
                            }
                            else if (equip_item.attack_type == 2)
                            {
                                stats[10].text += "  마력 +" + equip_item.item_option[i];
                            }
                            else if (equip_item.attack_type == 3)
                            {
                                stats[10].text += "  최대마나 +" + equip_item.item_option[i];
                            }
                        }
                        else if (equip_item.type != 1)
                        {
                            if (equip_item.attack_type == 4 || equip_item.attack_type == 5 || equip_item.attack_type == 6)
                            {
                                stats[10].text += "\n방어력 +" + equip_item.item_option[i];
                            }
                        }
                    }
                    else if (i == 2)
                    {
                        if (equip_item.type == 1)
                        {
                            if (equip_item.attack_type == 1 || equip_item.attack_type == 2 || equip_item.attack_type == 3)
                            {
                                stats[10].text += "\n치명타확률 +" + Mathf.Round(equip_item.item_option[i] * 100f) + "%";
                            }
                        }
                        else if (equip_item.type != 1)
                        {
                            if (equip_item.attack_type == 4 || equip_item.attack_type == 5)
                            {
                                stats[10].text += "\n최대마나 +" + equip_item.item_option[i];
                            }
                            else if (equip_item.attack_type == 6)
                            {
                                stats[10].text += "\n이동속도 +" + Mathf.Round((equip_item.item_option[i] / 5.6f) * 100f) + "%";
                            }
                        }
                    }

                    option_num++;
                }
            }

            if (option_num <= 0)
            {
                stats[10].gameObject.SetActive(false);
            }
            if (skill_option_num <= 0)
            {
                stats[11].gameObject.SetActive(false);
            }

            stats[12].text = "\n" + equip_item.item_exp;
        }
        // 소비
        else if (tool_type == 1) 
        {
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i].gameObject.SetActive(false);
            }
            string rare = null;
            switch (use_item.rare)
            {
                case 0:
                    rare = "노말";
                    break;
                case 1:
                    rare = "언커먼";
                    break;
                case 2:
                    rare = "레어";
                    break;
                case 3:
                    rare = "에픽";
                    break;
                case 4:
                    rare = "레전더리";
                    break;
            }
            stats[0].text = use_item.item_name;
            stats[2].text = rare;
            stats[12].text = use_item.item_exp;

            stats[0].gameObject.SetActive(true);
            stats[2].gameObject.SetActive(true);
            stats[12].gameObject.SetActive(true);
        }
        // 기타
        else if (tool_type == 2)
        {
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i].gameObject.SetActive(false);
            }
            string rare = null;
            switch (etc_item.rare)
            {
                case 0:
                    rare = "노말";
                    break;
                case 1:
                    rare = "언커먼";
                    break;
                case 2:
                    rare = "레어";
                    break;
                case 3:
                    rare = "에픽";
                    break;
                case 4:
                    rare = "레전더리";
                    break;
            }
            stats[0].text = etc_item.item_name;
            stats[2].text = rare;
            stats[12].text = etc_item.item_exp;

            stats[0].gameObject.SetActive(true);
            stats[2].gameObject.SetActive(true);
            stats[12].gameObject.SetActive(true);
        }
    }
}
