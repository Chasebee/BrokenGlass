using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Skill : MonoBehaviour
{
    public GameObject quick_slot_object;
    public int quick_slot_num;
    public GameObject[] ui_object;
    [Header("��ų����")]
    public int skill_point_type; // ��ų ���� ��1 / �ٿ�0 / ����2
    public Skill_List sk_list;
    public TextMeshProUGUI skill_text; // ��ų ����κ�
    public Button skill_img; // ���õ� ��ų �̹���
    public GameObject[] sk_tree_main;
    public GameObject[] sk_tree;
    public Sprite default_slot;

    void Update() 
    {
        // ��ųâ ��ų��ư
        if (skill_point_type == 2)
        {
            if (sk_list.skill_type == 1 && sk_list.pre_skill >= 1 && GameManager.instance.sword_skill[sk_list.pre_skill] <= 0)
            {
                gameObject.GetComponent<Button>().interactable = false;
            }
            else if (sk_list.skill_type == 2 && sk_list.pre_skill >= 1 && GameManager.instance.bow_skill[sk_list.pre_skill] <= 0)
            {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
        // �� ���� ���ĭ
        else if (skill_point_type == 10) 
        {
            if (GameManager.instance.quick_slot[quick_slot_num] != null)
            {
                gameObject.GetComponent<Image>().sprite = GameManager.instance.quick_slot[quick_slot_num].skill_img;
            }
        }
    }

    public void Skill_Select() 
    {
        // �̹��� �ֱ�, ��ų����Ʈ ���� ��ư�� sk_list �߰����ֱ�
        if (skill_point_type == 2)
        {
            skill_img.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
            skill_img.GetComponent<UI_Skill>().sk_list = sk_list;

            ui_object[0].GetComponent<UI_Skill>().sk_list = sk_list;
            ui_object[1].GetComponent<UI_Skill>().sk_list = sk_list;

            if (sk_list.passive == false)
            {
                quick_slot_object.GetComponent<Button>().interactable = true;
            }
            else
            {
                quick_slot_object.GetComponent<Button>().interactable = false;
            }
        }

        // �� �迭
        if (sk_list.skill_type == 1)
        {
            // ��Ƽ�� ��ų % ǥ��
            if (sk_list.explane_type == true && sk_list.passive == false)
            {
                int sk_lv = GameManager.instance.sword_skill[sk_list.skill_num];
                int sk_value = 0;
                // ��ų����
                if (sk_lv >= 1 && sk_lv <= 3)
                {
                    sk_value = (int)sk_list.skill_var[1];
                }
                else if (sk_lv >= 4 && sk_lv <= 7)
                {
                    sk_value = (int)sk_list.skill_var[2];
                }
                else if (sk_lv >= 8 && sk_lv <= 11)
                {
                    sk_value = (int)sk_list.skill_var[3];
                }
                else if (sk_lv >= 12 && sk_lv <= 15)
                {
                    sk_value = (int)sk_list.skill_var[4];
                }
                else if (sk_lv >= 16 && sk_lv <= 19)
                {
                    sk_value = (int)sk_list.skill_var[5];
                }
                else if (sk_lv == 20)
                {
                    sk_value = (int)sk_list.skill_var[6];
                }

                skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " " + sk_value + "% �������� ����" + "</size>";
            }

            // % �� ���� �нú�
            else if (sk_list.explane_type == false && sk_list.passive == true)
            {
                if (sk_list.double_stat == false)
                {
                    int sk_lv = GameManager.instance.sword_skill[sk_list.skill_num];
                    if (sk_list.skill_num == 0)
                    {
                        skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                        skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " �ִ�ü�� " + sk_list.skill_var[sk_lv] + "����" + "</size>";
                    }
                    else if (sk_list.skill_num == 1)
                    {
                        skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                        skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " ���ݷ� " + sk_list.skill_var[sk_lv] + "����" + "</size>";
                    }
                }
                else if (sk_list.double_stat == true)
                {
                    int sk_lv = GameManager.instance.sword_skill[sk_list.skill_num];

                    if (sk_list.skill_num == 2)
                    {
                        skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                        skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " ���ݷ� " + sk_list.skill_var[sk_lv] + "���� ���ݼӵ� ";
                        skill_text.text += Mathf.FloorToInt(sk_list.skill_var_2[sk_lv] * 100) + "% ����" + "</size>";
                    }
                }
            }
        }
        // Ȱ �迭
        else if (sk_list.skill_type == 2)
        {
            // ��Ƽ�� ��ų % ǥ��
            if (sk_list.explane_type == true && sk_list.passive == false)
            {
                int sk_lv = GameManager.instance.bow_skill[sk_list.skill_num];
                int sk_value = 0;
                // ��ų����
                if (sk_lv >= 1 && sk_lv <= 3)
                {
                    sk_value = (int)Mathf.Round(sk_list.skill_var[1] * 100);
                }
                else if (sk_lv >= 4 && sk_lv <= 7)
                {
                    sk_value = (int)Mathf.Round(sk_list.skill_var[2] * 100);
                }
                else if (sk_lv >= 8 && sk_lv <= 11)
                {
                    sk_value = (int)Mathf.Round(sk_list.skill_var[3] * 100);
                }
                else if (sk_lv >= 12 && sk_lv <= 15)
                {
                    sk_value = (int)Mathf.Round(sk_list.skill_var[4] * 100);
                }
                else if (sk_lv >= 16 && sk_lv <= 19)
                {
                    sk_value = (int)Mathf.Round(sk_list.skill_var[5] * 100);
                }
                else if (sk_lv == 20)
                {
                    sk_value = (int)Mathf.Round(sk_list.skill_var[6] * 100);
                }

                skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " " + sk_value + "% �������� ����" + "</size>";
            }

            // % �� ���� �нú�
            else if (sk_list.explane_type == false && sk_list.passive == true)
            {
                if (sk_list.double_stat == false)
                {
                    int sk_lv = GameManager.instance.bow_skill[sk_list.skill_num];
                    if (sk_list.skill_num == 1)
                    {
                        skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                        skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " ���ݷ� " + sk_list.skill_var[sk_lv] + "����" + "</size>";
                    }
                }
                // ���� ��������
                else if (sk_list.double_stat == true)
                {
                    int sk_lv = GameManager.instance.bow_skill[sk_list.skill_num];

                    if (sk_list.skill_num == 2)
                    {
                        skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                        skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " ���� " + sk_list.skill_var[sk_lv] + "���� �ִ�MP ";
                        skill_text.text += sk_list.skill_var_2[sk_lv] + " ����" + "</size>";
                    }
                }
            }

            // % �� �ִ� �нú�
            else if (sk_list.explane_type == true && sk_list.passive == true)
            {
                if (sk_list.double_stat == false)
                {
                    int sk_lv = GameManager.instance.bow_skill[sk_list.skill_num];
                    if (sk_list.skill_num == 0)
                    {
                        skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                        skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " ġ��Ÿ Ȯ�� " + Mathf.Round(sk_list.skill_var[sk_lv] * 100) + "% ����" + "</size>";
                    }
                }
            }
        }
        // ���� �迭
        else if (sk_list.skill_type == 3) 
        {
            // ��Ƽ�� ��ų % ǥ��
            if (sk_list.explane_type == true && sk_list.passive == false)
            {
                int sk_lv = GameManager.instance.magic_skill[sk_list.skill_num];
                int sk_value = 0;
                // ��ų����
                if (sk_lv >= 1 && sk_lv <= 3)
                {
                    sk_value = (int)Mathf.Round(sk_list.skill_var[1] * 100);
                }
                else if (sk_lv >= 4 && sk_lv <= 7)
                {
                    sk_value = (int)Mathf.Round(sk_list.skill_var[2] * 100);
                }
                else if (sk_lv >= 8 && sk_lv <= 11)
                {
                    sk_value = (int)Mathf.Round(sk_list.skill_var[3] * 100);
                }
                else if (sk_lv >= 12 && sk_lv <= 15)
                {
                    sk_value = (int)Mathf.Round(sk_list.skill_var[4] * 100);
                }
                else if (sk_lv >= 16 && sk_lv <= 19)
                {
                    sk_value = (int)Mathf.Round(sk_list.skill_var[5] * 100);
                }
                else if (sk_lv == 20)
                {
                    sk_value = (int)Mathf.Round(sk_list.skill_var[6] * 100);
                }

                skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " " + sk_value + "% �������� ����" + "</size>";
            }

            // % �� ���� �нú�
            else if (sk_list.explane_type == false && sk_list.passive == true)
            {
                if (sk_list.double_stat == false)
                {
                    int sk_lv = GameManager.instance.magic_skill[sk_list.skill_num];
                    if (sk_list.skill_num == 99)
                    {
                        skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                        skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " ���ݷ� " + sk_list.skill_var[sk_lv] + "����" + "</size>";
                    }
                }
                // ���� ��������
                else if (sk_list.double_stat == true)
                {
                    int sk_lv = GameManager.instance.magic_skill[sk_list.skill_num];
                    if (sk_list.skill_num == 0)
                    {
                        skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                        skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " �ִ�MP " + sk_list.skill_var[sk_lv] + "���� ���� ";
                        skill_text.text += sk_list.skill_var_2[sk_lv] + " ����" + "</size>";
                    }
                    else if (sk_list.skill_num == 1)
                    {
                        skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                        skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " ���� " + sk_list.skill_var[sk_lv] + "���� �ִ�MP ";
                        skill_text.text += sk_list.skill_var_2[sk_lv] + " ����" + "</size>";
                    }
                    else if (sk_list.skill_num == 2)
                    {
                        skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                        skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " ���� " + sk_list.skill_var[sk_lv] + "���� �ִ�MP ";
                        skill_text.text += sk_list.skill_var_2[sk_lv] + " ����" + "</size>";
                    }
                }
            }

            // % �� �ִ� �нú�
            else if (sk_list.explane_type == true && sk_list.passive == true)
            {
                if (sk_list.double_stat == false)
                {
                    int sk_lv = GameManager.instance.magic_skill[sk_list.skill_num];
                    if (sk_list.skill_num == 0)
                    {
                        skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
                        skill_text.text += "\n<size=20>���� ��ų ���� " + sk_lv + " ġ��Ÿ Ȯ�� " + Mathf.Round(sk_list.skill_var[sk_lv] * 100) + "% ����" + "</size>";
                    }
                }
            }

            // ������
            else if (sk_list.explane_type == false && sk_list.passive == false) 
            {
                skill_text.text = "<size=40>" + sk_list.skill_name + "</size>\n" + sk_list.skill_text;
            }
        }
    }

    // ��ų����Ʈ ���
    public void Skill_Point_Use() 
    {
        bool confirm = false;
        // ��ų����Ʈ ���
        if (skill_point_type == 1 && GameManager.instance.skill_point >= 1 && skill_img.GetComponent<UI_Skill>().sk_list != null)
        {
            // �� �迭 ��ų
            if (skill_img.GetComponent<UI_Skill>().sk_list.skill_type == 1)
            {
                if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 1 && GameManager.instance.sword_skill[2] >= 1)
                {
                    confirm = false;
                    Debug.Log("��ų���� ���ø��� ����1");
                }
                else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 2 && GameManager.instance.sword_skill[1] >= 1)
                {
                    confirm = false;
                    Debug.Log("��ų���� ���ø��� ����2");
                }
                // ���ེų
                else if (skill_img.GetComponent<UI_Skill>().sk_list.pre_skill != 0 && GameManager.instance.sword_skill[skill_img.GetComponent<UI_Skill>().sk_list.pre_skill] >= 1)
                {
                    Debug.Log("���ེų �����");
                    confirm = true;
                }
                // �׿� ��ų
                else if (skill_img.GetComponent<UI_Skill>().sk_list.pre_skill == 0)
                {
                    confirm = true;
                }

                // �̻���� ��ų ������
                if (confirm && GameManager.instance.sword_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num] < skill_img.GetComponent<UI_Skill>().sk_list.max_lv)
                {
                    GameManager.instance.sword_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num]++;
                    GameManager.instance.skill_point--;
                    // ü������
                    if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 0)
                    {
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Up_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }
                    // ��� �����͸�
                    if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 1)
                    {
                        sk_tree[0].SetActive(true);
                        sk_tree_main[1].GetComponent<Button>().interactable = false;
                    }
                    // �ְ� 
                    else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 2)
                    {
                        sk_tree[1].SetActive(true);
                        sk_tree_main[0].GetComponent<Button>().interactable = false;
                    }

                    Skill_Select();
                }
            }
            
            // Ȱ �Կ�
            else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_type == 2) 
            {
                if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 1 && GameManager.instance.bow_skill[2] >= 1)
                {
                    confirm = false;
                    Debug.Log("��ų���� ���ø��� ����1");
                }
                else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 2 && GameManager.instance.bow_skill[1] >= 1)
                {
                    confirm = false;
                    Debug.Log("��ų���� ���ø��� ����2");
                }
                // ���ེų
                else if (skill_img.GetComponent<UI_Skill>().sk_list.pre_skill != 0 && GameManager.instance.bow_skill[skill_img.GetComponent<UI_Skill>().sk_list.pre_skill] >= 1)
                {
                    Debug.Log("���ེų �����");
                    confirm = true;
                }
                // �׿� ��ų
                else if (skill_img.GetComponent<UI_Skill>().sk_list.pre_skill == 0)
                {
                    confirm = true;
                }

                // �̻���� ��ų ������
                if (confirm && GameManager.instance.bow_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num] < skill_img.GetComponent<UI_Skill>().sk_list.max_lv)
                {
                    GameManager.instance.bow_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num]++;
                    GameManager.instance.skill_point--;
                    // ����
                    if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 0) 
                    {
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Up_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }
                    // �ü�
                    else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 1)
                    {
                        sk_tree[2].SetActive(true);
                        sk_tree_main[3].GetComponent<Button>().interactable = false;
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Up_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }
                    // ���ü� 
                    else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 2)
                    {
                        sk_tree[3].SetActive(true);
                        sk_tree_main[2].GetComponent<Button>().interactable = false;
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Up_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }

                    Skill_Select();
                }
            }

            // ���� �迭
            else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_type == 3)
            {
                // Ưȭ��ų
                if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 1 && GameManager.instance.magic_skill[2] >= 1)
                {
                    confirm = false;
                }
                else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 2 && GameManager.instance.magic_skill[1] >= 1)
                {
                    confirm = false;
                }
                // ���ེų
                else if (skill_img.GetComponent<UI_Skill>().sk_list.pre_skill != 0 && GameManager.instance.magic_skill[skill_img.GetComponent<UI_Skill>().sk_list.pre_skill] >= 1)
                {
                    Debug.Log("���ེų �����");
                    confirm = true;
                }
                // �׿� ��ų
                else if (skill_img.GetComponent<UI_Skill>().sk_list.pre_skill == 0)
                {
                    confirm = true;
                }

                // �̻���� ��ų ������
                if (confirm && GameManager.instance.magic_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num] < skill_img.GetComponent<UI_Skill>().sk_list.max_lv)
                {
                    GameManager.instance.magic_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num]++;
                    GameManager.instance.skill_point--;
                    // �Ѹ�
                    if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 0)
                    {
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Up_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }
                    // ��������
                    else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 1)
                    {
                        sk_tree[4].SetActive(true);
                        sk_tree_main[5].GetComponent<Button>().interactable = false;
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Up_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }
                    // ������Ż
                    else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 2)
                    {
                        sk_tree[5].SetActive(true);
                        sk_tree_main[4].GetComponent<Button>().interactable = false;
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Up_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }

                    Skill_Select();
                }
            }
        }
        
        // ��ų����Ʈ �϶�
        else if (skill_point_type == 0 && skill_img.GetComponent<UI_Skill>().sk_list != null) 
        {
            // �� �迭 ��ų
            if (skill_img.GetComponent<UI_Skill>().sk_list.skill_type == 1) 
            {
                // ���� ��ų���� �ִ� ���
                if (skill_img.GetComponent<UI_Skill>().sk_list.sub_skill != null && skill_img.GetComponent<UI_Skill>().sk_list.sub_skill.Length >= 1)
                {
                    Debug.Log(skill_img.GetComponent<UI_Skill>().sk_list.sub_skill.Length + " ĭ");
                    for (int i = 0; i < skill_img.GetComponent<UI_Skill>().sk_list.sub_skill.Length; i++)
                    {
                        int a = skill_img.GetComponent<UI_Skill>().sk_list.sub_skill[i];
                        if (GameManager.instance.sword_skill[a] <= 0)
                        {
                            confirm = true;
                        }
                    }
                }
                // �׿� �Ϲ� ��ų
                else
                {
                    confirm = true;
                }

                // �̻���� ��ų �����ٿ�
                if (confirm && GameManager.instance.sword_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num] > 0) 
                {
                    // ü������
                    if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 0)
                    {
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Down_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }
                    // �����͸� UI
                    if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 1 || skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 2)
                    {
                        sk_tree[0].SetActive(false);
                        sk_tree[1].SetActive(false);
                        sk_tree_main[0].GetComponent<Button>().interactable = true;
                        sk_tree_main[1].GetComponent<Button>().interactable = true;
                        int skill_class = skill_img.GetComponent<UI_Skill>().sk_list.skill_class;
                        for (int i = 0; i < GameManager.instance.quick_slot.Length; i++) 
                        {
                            if (GameManager.instance.quick_slot[i] != null && GameManager.instance.quick_slot[i].skill_class == skill_class)
                            {
                                GameManager.instance.quick_slot[i] = null;
                                ui_object[i].GetComponent<Image>().sprite = default_slot;
                            }
                        }
                    }

                    GameManager.instance.sword_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num]--;
                    GameManager.instance.skill_point++;

                    // ��ų���� 0�ΰ��
                    if (GameManager.instance.sword_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num] <= 0)
                    {
                        for (int i = 0; i < GameManager.instance.quick_slot.Length; i++)
                        {
                            if (GameManager.instance.quick_slot[i] != null && (GameManager.instance.quick_slot[i] == skill_img.GetComponent<UI_Skill>().sk_list))
                            {
                                GameManager.instance.quick_slot[i] = null;
                                GameManager.instance.save_quick_slot[i] = null;
                                ui_object[i].GetComponent<Image>().sprite = default_slot;
                            }
                        }
                    }
                    Skill_Select();
                }
            }

            // Ȱ �迭 ��ų
            else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_type == 2)
            {
                // ���� ��ų���� �ִ� ���
                if (skill_img.GetComponent<UI_Skill>().sk_list.sub_skill != null && skill_img.GetComponent<UI_Skill>().sk_list.sub_skill.Length >= 1)
                {
                    Debug.Log(skill_img.GetComponent<UI_Skill>().sk_list.sub_skill.Length + " ĭ");
                    for (int i = 0; i < skill_img.GetComponent<UI_Skill>().sk_list.sub_skill.Length; i++)
                    {
                        int a = skill_img.GetComponent<UI_Skill>().sk_list.sub_skill[i];
                        if (GameManager.instance.bow_skill[a] <= 0)
                        {
                            confirm = true;
                        }
                    }
                }
                // �׿� �Ϲ� ��ų
                else
                {
                    confirm = true;
                }

                // �̻���� ��ų �����ٿ�
                if (confirm && GameManager.instance.bow_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num] > 0)
                {
                    // ����
                    if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 0)
                    {
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Down_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }
                    // Ưȭ : Ȱ
                    else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 1)
                    {
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Down_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }
                    // Ưȭ : ����ȭ��
                    else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 2)
                    {
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Down_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }

                    GameManager.instance.bow_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num]--;
                    GameManager.instance.skill_point++;

                    // �����͸� UI
                    if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 1 || skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 2)
                    {
                        sk_tree[2].SetActive(false);
                        sk_tree[3].SetActive(false);
                        sk_tree_main[2].GetComponent<Button>().interactable = true;
                        sk_tree_main[3].GetComponent<Button>().interactable = true;
                        int skill_class = skill_img.GetComponent<UI_Skill>().sk_list.skill_class;
                        for (int i = 0; i < GameManager.instance.quick_slot.Length; i++)
                        {
                            if (GameManager.instance.quick_slot[i] != null && GameManager.instance.quick_slot[i].skill_class == skill_class)
                            {
                                GameManager.instance.quick_slot[i] = null;
                                ui_object[i].GetComponent<Image>().sprite = default_slot;
                            }
                        }
                    }

                    // ��ų���� 0�ΰ��
                    if (GameManager.instance.bow_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num] <= 0) 
                    {
                        for (int i = 0; i < GameManager.instance.quick_slot.Length; i++)
                        {
                            if (GameManager.instance.quick_slot[i] != null && (GameManager.instance.quick_slot[i] == skill_img.GetComponent<UI_Skill>().sk_list))
                            {
                                GameManager.instance.quick_slot[i] = null;
                                GameManager.instance.save_quick_slot[i] = null;
                                ui_object[i].GetComponent<Image>().sprite = default_slot;
                            }
                        }
                    }

                    Skill_Select();
                }
            }
            // ���� �迭 ��ų
            else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_type == 3)
            {
                // ���� ��ų���� �ִ� ���
                if (skill_img.GetComponent<UI_Skill>().sk_list.sub_skill != null && skill_img.GetComponent<UI_Skill>().sk_list.sub_skill.Length >= 1)
                {
                    Debug.Log(skill_img.GetComponent<UI_Skill>().sk_list.sub_skill.Length + " ĭ");
                    for (int i = 0; i < skill_img.GetComponent<UI_Skill>().sk_list.sub_skill.Length; i++)
                    {
                        int a = skill_img.GetComponent<UI_Skill>().sk_list.sub_skill[i];
                        if (GameManager.instance.magic_skill[a] <= 0)
                        {
                            confirm = true;
                        }
                    }
                }
                // �׿� �Ϲ� ��ų
                else
                {
                    confirm = true;
                }

                // �̻���� ��ų �����ٿ�
                if (confirm && GameManager.instance.magic_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num] > 0)
                {
                    // �Ѹ�
                    if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 0)
                    {
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Down_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }
                    // Ưȭ : ��������
                    else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 1)
                    {
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Down_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }
                    // Ưȭ : ������Ż
                    else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 2)
                    {
                        UI_Skill sk = skill_img.GetComponent<UI_Skill>();
                        Skill_Stat_Down_1(sk.sk_list.skill_type, sk.sk_list.skill_num, sk);
                    }

                    GameManager.instance.magic_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num]--;
                    GameManager.instance.skill_point++;

                    // �����͸� UI
                    if (skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 1 || skill_img.GetComponent<UI_Skill>().sk_list.skill_num == 2)
                    {
                        sk_tree[4].SetActive(false);
                        sk_tree[5].SetActive(false);
                        sk_tree_main[4].GetComponent<Button>().interactable = true;
                        sk_tree_main[5].GetComponent<Button>().interactable = true;
                        int skill_class = skill_img.GetComponent<UI_Skill>().sk_list.skill_class;
                        for (int i = 0; i < GameManager.instance.quick_slot.Length; i++)
                        {
                            if (GameManager.instance.quick_slot[i] != null && GameManager.instance.quick_slot[i].skill_class == skill_class)
                            {
                                GameManager.instance.quick_slot[i] = null;
                                ui_object[i].GetComponent<Image>().sprite = default_slot;
                            }
                        }
                    }

                    // ��ų���� 0�ΰ��
                    if (GameManager.instance.magic_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num] <= 0)
                    {
                        for (int i = 0; i < GameManager.instance.quick_slot.Length; i++)
                        {
                            if (GameManager.instance.quick_slot[i] != null && (GameManager.instance.quick_slot[i] == skill_img.GetComponent<UI_Skill>().sk_list))
                            {
                                GameManager.instance.quick_slot[i] = null;
                                GameManager.instance.save_quick_slot[i] = null;
                                ui_object[i].GetComponent<Image>().sprite = default_slot;
                            }
                        }
                    }

                    Skill_Select();
                }
            }
        }
    }

    // ������ ���
    public void Quick_Slot_Add_Open() 
    {
        bool confirm = false;
        if (skill_img.GetComponent<UI_Skill>().sk_list.skill_type == 1)
        {
            if (GameManager.instance.sword_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num] >= 1)
            {
                confirm = true;
            }
        }
        else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_type == 2)
        {
            if (GameManager.instance.bow_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num] >= 1)
            {
                confirm = true;
            }
        }
        else if (skill_img.GetComponent<UI_Skill>().sk_list.skill_type == 3)
        {
            if (GameManager.instance.magic_skill[skill_img.GetComponent<UI_Skill>().sk_list.skill_num] >= 1)
            {
                confirm = true;
            }
        }
        if (confirm)
        {
            quick_slot_object.SetActive(true);
        }
    }
    public void Quick_Slot_Add(int a) 
    {
        GameManager.instance.quick_slot[a] = skill_img.GetComponent<UI_Skill>().sk_list;
        ui_object[0].GetComponent<Image>().sprite = skill_img.GetComponent<UI_Skill>().sk_list.skill_img;

        // ������ ������ �����
        UI_Skill save_sk = skill_img.GetComponent<UI_Skill>();
        GameManager.instance.save_quick_slot[a] = save_sk.sk_list.skill_name;
        // ������ UI �ݱ�
        quick_slot_object.SetActive(false);
    }

    public void Quick_Slot_Add_Close()
    {
        quick_slot_object.SetActive(false);
    }

    // ��ų �нú� �������ͽ�
    public void Skill_Stat_Up_1(int type, int num, UI_Skill sk)
    {
        if (type == 1)
        {
            if (num == 0)
            {
                int skillLevel = GameManager.instance.sword_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);

                GameManager.instance.maxhp += (int)currentSkillValue;
                GameManager.instance.maxhp -= (int)previousSkillValue;
            }

        }
        else if (type == 2)
        {
            if (num == 0)
            {
                int skillLevel = GameManager.instance.bow_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);

                GameManager.instance.critical += currentSkillValue;
                GameManager.instance.critical -= previousSkillValue;
            }
            else if (num == 1)
            {
                int skillLevel = GameManager.instance.bow_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);

                GameManager.instance.atk += (int)currentSkillValue;
                GameManager.instance.atk -= (int)previousSkillValue;
            }
            else if (num == 2)
            {
                int skillLevel = GameManager.instance.bow_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);
                float currentSkillValue_2 = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var_2);
                float previousSkillValue_2 = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var_2);

                GameManager.instance.magic += (int)currentSkillValue;
                GameManager.instance.magic -= (int)previousSkillValue;
                GameManager.instance.maxmp += (int)currentSkillValue_2;
                GameManager.instance.maxmp -= (int)previousSkillValue_2;
            }
        }
        else if (type == 3) 
        {
            if (num == 0)
            {
                int skillLevel = GameManager.instance.magic_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);
                float currentSkillValue_2 = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var_2);
                float previousSkillValue_2 = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var_2);

                GameManager.instance.maxmp += (int)currentSkillValue;
                GameManager.instance.maxmp -= (int)previousSkillValue;
                GameManager.instance.magic += (int)currentSkillValue_2;
                GameManager.instance.magic -= (int)previousSkillValue_2;
            }
            else if (num == 1)
            {
                int skillLevel = GameManager.instance.magic_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);
                float currentSkillValue_2 = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var_2);
                float previousSkillValue_2 = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var_2);

                GameManager.instance.magic += (int)currentSkillValue;
                GameManager.instance.magic -= (int)previousSkillValue;
                GameManager.instance.maxmp += (int)currentSkillValue_2;
                GameManager.instance.maxmp -= (int)previousSkillValue_2;
            }
            else if (num == 2) 
            {
                int skillLevel = GameManager.instance.magic_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);
                float currentSkillValue_2 = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var_2);
                float previousSkillValue_2 = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var_2);

                GameManager.instance.magic += (int)currentSkillValue;
                GameManager.instance.magic -= (int)previousSkillValue;
                GameManager.instance.maxmp += (int)currentSkillValue_2;
                GameManager.instance.maxmp -= (int)previousSkillValue_2;
            }
        }
    }
    public void Skill_Stat_Down_1(int type, int num, UI_Skill sk) 
    {
        if (type == 1)
        {
            if (num == 0)
            {
                int skillLevel = GameManager.instance.sword_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);
                GameManager.instance.maxhp -= (int)currentSkillValue;
                GameManager.instance.maxhp += (int)previousSkillValue;
            }
        }
        else if (type == 2)
        {
            if (num == 0)
            {
                int skillLevel = GameManager.instance.bow_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);
                GameManager.instance.critical -= currentSkillValue;
                GameManager.instance.critical += previousSkillValue;
            }
            else if (num == 1)
            {
                int skillLevel = GameManager.instance.bow_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);
                GameManager.instance.atk -= (int)currentSkillValue;
                GameManager.instance.atk += (int)previousSkillValue;
            }
            else if (num == 2)
            {
                int skillLevel = GameManager.instance.bow_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);
                float currentSkillValue_2 = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var_2);
                float previousSkillValue_2 = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var_2);

                GameManager.instance.magic -= (int)currentSkillValue;
                GameManager.instance.magic += (int)previousSkillValue;
                GameManager.instance.maxmp -= (int)currentSkillValue_2;
                GameManager.instance.maxmp += (int)previousSkillValue_2;
            }
        }
        else if (type == 3)
        {
            if (num == 0)
            {
                int skillLevel = GameManager.instance.magic_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);
                float currentSkillValue_2 = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var_2);
                float previousSkillValue_2 = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var_2);

                GameManager.instance.maxmp -= (int)currentSkillValue;
                GameManager.instance.maxmp += (int)previousSkillValue;
                GameManager.instance.magic -= (int)currentSkillValue_2;
                GameManager.instance.magic += (int)previousSkillValue_2;
            }
            else if (num == 1)
            {
                int skillLevel = GameManager.instance.magic_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);
                float currentSkillValue_2 = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var_2);
                float previousSkillValue_2 = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var_2);

                GameManager.instance.magic -= (int)currentSkillValue;
                GameManager.instance.magic += (int)previousSkillValue;
                GameManager.instance.maxmp -= (int)currentSkillValue_2;
                GameManager.instance.maxmp += (int)previousSkillValue_2;
            }
            else if (num == 2)
            {
                int skillLevel = GameManager.instance.magic_skill[num];
                float currentSkillValue = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var);
                float previousSkillValue = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var);
                float currentSkillValue_2 = GetSafeSkillVar(skillLevel, sk.sk_list.skill_var_2);
                float previousSkillValue_2 = GetSafeSkillVar(skillLevel - 1, sk.sk_list.skill_var_2);

                GameManager.instance.magic -= (int)currentSkillValue;
                GameManager.instance.magic += (int)previousSkillValue;
                GameManager.instance.maxmp -= (int)currentSkillValue_2;
                GameManager.instance.maxmp += (int)previousSkillValue_2;
            }
        }
    }
    private float GetSafeSkillVar(int index, float[] skillVar)
    {
        // �迭 �ε����� ��ȿ���� Ȯ�� �� �� ��ȯ
        if (index >= 0 && index < skillVar.Length)
        {
            return (float)Math.Round(skillVar[index], 3);
        }
        return 0f; // �ε����� ��ȿ���� ������ �⺻�� ��ȯ
    }
    public void Move_To_Title() 
    {
        SceneManager.LoadScene("Title");
    }
}
