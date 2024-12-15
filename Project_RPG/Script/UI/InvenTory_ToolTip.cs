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
        // ���콺 ��ġ ��������
        Vector3 mousePosition = Input.mousePosition;

        Vector3 tooltipPosition = mousePosition + new Vector3(pos_x, pos_y, 0); 
        transform.position = tooltipPosition;

        // ������ ȭ���� ����� �ʵ��� ó��
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
        // ���
        if (tool_type == 0)
        {
            string rare = null;
            switch (equip_item.rare)
            {
                case 0:
                    rare = "�븻";
                    break;
                case 1:
                    rare = "��Ŀ��";
                    break;
                case 2:
                    rare = "����";
                    break;
                case 3:
                    rare = "����";
                    break;
                case 4:
                    rare = "��������";
                    break;
            }

            string equipment_type = null;
            switch (equip_item.attack_type)
            {
                case 1:
                    equipment_type = "��/�ְ�";
                    break;
                case 2:
                    equipment_type = "Ȱ";
                    break;
                case 3:
                    equipment_type = "��ī�� ���ڵ�";
                    break;
                case 4:
                    equipment_type = "����/����";
                    break;
                case 5:
                    equipment_type = "����";
                    break;
                case 6:
                    equipment_type = "����";
                    break;
            }

            stats[0].text = equip_item.item_name;
            stats[1].text = equipment_type + " Lv." + equip_item.item_lv;
            stats[2].text = rare;
            // True ó���� �ٽ� False ó��
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i].gameObject.SetActive(true);
            }

            // �⺻����
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
                        stats[3].text = "���ݷ� +" + equip_item.base_option[i];
                    }
                    else if (i == 1)
                    {
                        stats[4].text = "���� +" + equip_item.base_option[i];
                    }
                    else if (i == 2)
                    {
                        stats[5].text = "���ݼӵ� " + Mathf.Round(equip_item.base_option[i] * 100) + "%";
                    }
                    else if (i == 3)
                    {
                        stats[6].text = "�ִ�ü�� +" + equip_item.base_option[i];
                    }
                    else if (i == 4)
                    {
                        stats[7].text = "�ִ븶�� +" + equip_item.base_option[i];
                    }
                    else if (i == 5)
                    {
                        stats[8].text = "���� +" + equip_item.base_option[i];
                    }
                    else if (i == 6)
                    {
                        stats[9].text = "�̵��ӵ� +" + (equip_item.base_option[i] / 5.6f) * 100 + "%";
                    }
                }
            }

            // �ɼ��� �ִ��� ������ üũ��
            int option_num = 0;
            int skill_option_num = 0;

            // �߰�����
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
                                stats[10].text = "<color=#34495e>�߰��ɼ�</color>\n���ݷ� +" + equip_item.item_option[i];
                            }
                            else if (equip_item.attack_type == 2)
                            {
                                stats[10].text = "<color=#34495e>�߰��ɼ�</color>\n���ݷ� +" + equip_item.item_option[i];
                            }
                            else if (equip_item.attack_type == 3)
                            {
                                stats[10].text = "<color=#34495e>�߰��ɼ�</color>\n���� +" + equip_item.item_option[i];
                            }
                        }
                        else if(equip_item.type != 1)
                        {
                            if (equip_item.attack_type == 4 || equip_item.attack_type == 5)
                            {
                                stats[10].text = "<color=#34495e>�߰��ɼ�</color>\n�ִ�ü�� +" + equip_item.item_option[i];
                            }
                            else if (equip_item.attack_type == 6)
                            {
                                stats[10].text = "<color=#34495e>�߰��ɼ�</color>\n�ִ븶�� +" + equip_item.item_option[i];
                            }
                        }
                    }
                    else if (i == 1)
                    {
                        if (equip_item.type == 1)
                        {
                            if (equip_item.attack_type == 1)
                            {
                                stats[10].text += "  ���ݼӵ� +" + Mathf.Round(equip_item.item_option[i] * 100f) + "%";
                            }
                            else if (equip_item.attack_type == 2)
                            {
                                stats[10].text += "  ���� +" + equip_item.item_option[i];
                            }
                            else if (equip_item.attack_type == 3)
                            {
                                stats[10].text += "  �ִ븶�� +" + equip_item.item_option[i];
                            }
                        }
                        else if (equip_item.type != 1)
                        {
                            if (equip_item.attack_type == 4 || equip_item.attack_type == 5 || equip_item.attack_type == 6)
                            {
                                stats[10].text += "\n���� +" + equip_item.item_option[i];
                            }
                        }
                    }
                    else if (i == 2)
                    {
                        if (equip_item.type == 1)
                        {
                            if (equip_item.attack_type == 1 || equip_item.attack_type == 2 || equip_item.attack_type == 3)
                            {
                                stats[10].text += "\nġ��ŸȮ�� +" + Mathf.Round(equip_item.item_option[i] * 100f) + "%";
                            }
                        }
                        else if (equip_item.type != 1)
                        {
                            if (equip_item.attack_type == 4 || equip_item.attack_type == 5)
                            {
                                stats[10].text += "\n�ִ븶�� +" + equip_item.item_option[i];
                            }
                            else if (equip_item.attack_type == 6)
                            {
                                stats[10].text += "\n�̵��ӵ� +" + Mathf.Round((equip_item.item_option[i] / 5.6f) * 100f) + "%";
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
        // �Һ�
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
                    rare = "�븻";
                    break;
                case 1:
                    rare = "��Ŀ��";
                    break;
                case 2:
                    rare = "����";
                    break;
                case 3:
                    rare = "����";
                    break;
                case 4:
                    rare = "��������";
                    break;
            }
            stats[0].text = use_item.item_name;
            stats[2].text = rare;
            stats[12].text = use_item.item_exp;

            stats[0].gameObject.SetActive(true);
            stats[2].gameObject.SetActive(true);
            stats[12].gameObject.SetActive(true);
        }
        // ��Ÿ
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
                    rare = "�븻";
                    break;
                case 1:
                    rare = "��Ŀ��";
                    break;
                case 2:
                    rare = "����";
                    break;
                case 3:
                    rare = "����";
                    break;
                case 4:
                    rare = "��������";
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
