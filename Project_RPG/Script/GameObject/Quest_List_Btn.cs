using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quest_List_Btn : MonoBehaviour
{
    public int quest_type; // 0 미수행 1 수행 2 완료
    public int num;
    public Quest quest;
    public User_Quest_List user_quest;
    public Image frame, status;
    public Sprite[] quest_imgs;
    public TextMeshProUGUI quest_name;
    public GameObject reward;
    GameObject stm;
    Stage_Manager st;
    void Start()
    {
        stm = GameObject.FindWithTag("Manager");
        st = stm.GetComponent<Stage_Manager>();
        if (quest != null && quest.quest_value == 0)
        {
            frame.sprite = quest_imgs[0];
        }
        else if (user_quest != null && user_quest.quest_value == 0)
        {
            frame.sprite = quest_imgs[0];
        }
        if (quest_type == 1)
        {
            status.sprite = quest_imgs[3];
        }
        else if (quest_type == 2) 
        {
            status.gameObject.SetActive(false);
        }
    }

    public void Touch() 
    {
        Reward_List_Clear();
        st.quest_select = num;
        // 퀘스트 본문 표시
        if (quest_type == 0)
        {
            st.quest_infos[0].text = quest.quest_name;
            st.quest_infos[1].text = quest.quest_explane;
            st.give_up.interactable = false;
            st.quest_Clear.interactable = false;
        }
        else if (quest_type == 1)
        {
            st.quest_infos[0].text = user_quest.quest_name;
            st.quest_infos[1].text = user_quest.quest_explane;

            bool[] confirm;
            bool[] confirm_2;
            int check = 0;
            int check_2 = 0;

            switch (user_quest.quest_type)
            {
                case 1:
                    st.quest_infos[1].text += user_quest.object_npc + " 와 대화한다.";
                    break;

                case 2:
                    confirm = new bool[user_quest.object_mob.Length];
                    for (int i = 0; i < user_quest.object_mob.Length; i++)
                    {
                        st.quest_infos[1].text += "\n" + user_quest.object_mob[i] + " " + user_quest.p_object_mob_cnt[i] + " / " + user_quest.object_mob_cnt[i];
                        if (user_quest.p_object_mob_cnt[i] >= user_quest.object_mob_cnt[i])
                        {
                            confirm[i] = true;
                        }

                    }
                    for (int i = 0; i < confirm.Length; i++)
                    {
                        if (confirm[i])
                        {
                            check++;
                        }
                    }

                    if (check == confirm.Length)
                    {
                        st.quest_Clear.interactable = true;
                    }
                    else if (check < confirm.Length)
                    {
                        st.quest_Clear.interactable = false;
                    }
                    break;

                case 3:
                    confirm = new bool[user_quest.object_item_name.Length];
                    // 인벤토리에 아이템이 하나라도 있는경우
                    if (GameManager.instance.inventory_etc.Count >= 1)
                    {
                        for (int i = 0; i < user_quest.object_item_name.Length; i++)
                        {
                            for (int l = 0; l < GameManager.instance.inventory_etc.Count; l++)
                            {
                                // 아이템 존재
                                if (GameManager.instance.inventory_etc[l].item_name.Equals(user_quest.object_item_name[i]) && GameManager.instance.inventory_etc[l].reserves >= 1)
                                {
                                    user_quest.p_object_item_cnt[i] = GameManager.instance.inventory_etc[l].reserves;
                                }
                                // 아이템이 퀘스트가 요구하는것을 만족한 경우
                                if (GameManager.instance.inventory_etc[l].reserves >= user_quest.object_item_cnt[i])
                                {
                                    confirm[i] = true;
                                    Debug.Log(confirm[i]);
                                }
                            }
                        }
                    }

                    for (int i = 0; i < user_quest.p_object_item_cnt.Length; i++)
                    {
                        st.quest_infos[1].text += "\n" + user_quest.object_item_name[i] + " " + user_quest.p_object_item_cnt[i] + " / " + user_quest.object_item_cnt[i];
                    }
                    // 클리어 조건 확인                    
                    for (int i = 0; i < confirm.Length; i++)
                    {
                        if (confirm[i])
                        {
                            check++;
                        }
                    }
                    
                    if (check == confirm.Length)
                    {
                        st.quest_Clear.interactable = true;
                    }
                    else if (check < confirm.Length)
                    {
                        st.quest_Clear.interactable = false;
                    }
                    break;

                case 4:
                    break;
                default:
                    st.quest_infos[1].text += "\n즉시 클리어 가능한 퀘스트입니다.";
                    st.quest_Clear.interactable = true;
                    break;
            }

            // 보상 정보
            for (int i = 0; i < user_quest.reward_item_name.Length; i++) 
            {
                GameObject a = Instantiate(reward, st.quest_reward_place.transform);
                Reward_Object b = a.GetComponent<Reward_Object>();
                for (int l = 0; l < GameManager.instance.allEquipItems.Length; l++)
                {
                    if (GameManager.instance.allEquipItems[l].item_name.Equals(user_quest.reward_item_name[i]))
                    {
                        b.item_img.sprite = GameManager.instance.allEquipItems[l].item_img[0];
                        b.cnt.text = "";
                        break;
                    }
                }
                for (int l = 0; l < GameManager.instance.allUseItems.Length; l++) 
                {
                    if (GameManager.instance.allUseItems[l].item_name.Equals(user_quest.reward_item_name[i])) 
                    {
                        b.item_img.sprite = GameManager.instance.allUseItems[l].item_img;
                        b.cnt.text = user_quest.reward_item_cnt[i].ToString();
                        break;
                    }
                }
                for (int l = 0; l < GameManager.instance.allEtcitems.Length; l++)
                {
                    if (GameManager.instance.allEtcitems[l].item_name.Equals(user_quest.reward_item_name[i]))
                    {
                        b.item_img.sprite = GameManager.instance.allEtcitems[l].item_img;
                        b.cnt.text = user_quest.reward_item_cnt[i].ToString();
                        break;
                    }
                }

                st.quest_reward_list.Add(a);
            }

            // 포기버튼
            if (user_quest.now)
            {
                st.give_up.interactable = true;
            }
        }
        else if (quest_type == 2) 
        {
            st.quest_infos[0].text = user_quest.quest_name;
            st.quest_infos[1].text = user_quest.quest_explane;
        }
    }

    public void Reward_List_Clear() 
    {
        if (st.quest_reward_list.Count >= 1) 
        {
            for(int i = 0; i < st.quest_reward_list.Count; i++) 
            {
                Destroy(st.quest_reward_list[i]);
            }   
            st.quest_reward_list.Clear();
        }
    }
}