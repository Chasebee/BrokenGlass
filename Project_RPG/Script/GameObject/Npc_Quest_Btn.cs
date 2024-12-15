using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Npc_Quest_Btn : MonoBehaviour
{
    public TextMeshProUGUI q_name;
    public int q_num;
    public Quest quest;
    public GameObject reward_object;
    GameObject stm;
    Stage_Manager st;

    void Start() 
    {
        stm = GameObject.FindWithTag("Manager");
        st = stm.GetComponent<Stage_Manager>();
    }

    public void Clicking()
    {
        st.Npc_Quest_Reward_Clear();
        st.dial_texts[2].text = quest.quest_name;
        st.dial_texts[3].text = quest.quest_explane;
        st.dial_object[2].GetComponent<Button>().interactable = true;
        st.npc_quest_select = q_num;

        for (int i = 0; i < quest.reward_item_name.Length; i++)
        {
            for (int l = 0; l < GameManager.instance.allEquipItems.Length; l++)
            {
                if (quest.reward_item_name[i].Equals(GameManager.instance.allEquipItems[l].item_name))
                {
                    GameObject a = Instantiate(reward_object, st.npc_reward_place.transform);
                    Reward_Object b = a.GetComponent<Reward_Object>();
                    b.item_img.sprite = GameManager.instance.allEquipItems[l].item_img[0];
                    if (quest.reward_item_cnt.Length >= 1)
                    {
                        b.cnt.text = quest.reward_item_cnt[i].ToString();
                    }
                    else 
                    {
                        b.cnt.gameObject.SetActive(false);
                    }
                    st.npc_reward_list.Add(a);
                }
            }
            for (int l = 0; l < GameManager.instance.allUseItems.Length; l++) 
            {
                if (quest.reward_item_name[i].Equals(GameManager.instance.allUseItems[l].item_name)) 
                {
                    GameObject a = Instantiate(reward_object, st.npc_reward_place.transform);
                    Reward_Object b = a.GetComponent<Reward_Object>();
                    b.item_img.sprite = GameManager.instance.allUseItems[l].item_img;
                    if (quest.reward_item_cnt.Length >= 1)
                    {
                        b.cnt.text = quest.reward_item_cnt[i].ToString();
                    }
                    else 
                    {
                        b.cnt.gameObject.SetActive(false);
                    }
                    st.npc_reward_list.Add(a);
                }
            }
            for (int l = 0; l < GameManager.instance.allEtcitems.Length; l++)
            {
                if (quest.reward_item_name[i].Equals(GameManager.instance.allEtcitems[l].item_name))
                {
                    GameObject a = Instantiate(reward_object, st.npc_reward_place.transform);
                    Reward_Object b = a.GetComponent<Reward_Object>();
                    b.item_img.sprite = GameManager.instance.allEtcitems[l].item_img;
                    if (quest.reward_item_cnt.Length >= 1)
                    {
                        b.cnt.text = quest.reward_item_cnt[i].ToString();
                    }
                    else
                    {
                        b.cnt.gameObject.SetActive(false);
                    }

                    st.npc_reward_list.Add(a);
                }
            }
        }
    }

    public void Reward_List_Clear() 
    {
        if (st.npc_reward_list.Count >= 1) 
        {
            for (int i = 0; i < st.npc_reward_list.Count; i++)
            {
                Destroy(st.npc_reward_list[i]);
            }

            st.npc_reward_list.Clear();
        }
    }
}
