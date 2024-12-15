using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Npc_SellItem : MonoBehaviour
{
    public Equip_Item equip;
    public Use_Item use;
    public Etc_Item etc;

    public InvenTory_Equipment p_equip;
    public InvenTory_Use p_use;
    public InvenTory_Etc p_etc;

    public int type;
    public int cnt;
    public int price;
    public bool p_item;
    public int p_origin_equip;
    public int p_origin_use;
    public int p_origin_etc;
    public TextMeshProUGUI cnt_text;
    public Image item_img;

    public void Click() 
    {
        GameObject stm = GameObject.FindWithTag("Manager");
        Stage_Manager st = stm.GetComponent<Stage_Manager>();

        if (!p_item)
        {
            for (int i = 0; i < st.shop_nsell_list.Count; i++)
            {
                if (st.shop_nsell_list[i] == gameObject)
                {
                    Destroy(gameObject);
                    st.shop_nsell_list.RemoveAt(i);
                }
            }
            if (st.shop_nsell_list.Count <= 0)
            {
                st.dial_object[3].GetComponent<Shop_UI>().shop_btn[0].interactable = false;
            }
            Price_Calcul();
        }
        else 
        {
            for (int i = 0; i < st.shop_player_list.Count; i++)
            {
                if (p_equip != null)
                {
                    if (p_origin_equip == st.shop_player_list[i].GetComponent<Shop_Npc_Item>().p_num_equip)
                    {
                        st.shop_player_list[i].GetComponent<Button>().interactable = true;
                    }
                }
                else if (p_use != null)
                {
                    if (p_origin_use == st.shop_player_list[i].GetComponent<Shop_Npc_Item>().p_num_use)
                    {
                        st.shop_player_list[i].GetComponent<Button>().interactable = true;
                    }
                }
                else if (p_etc != null)
                {
                    if (p_origin_etc == st.shop_player_list[i].GetComponent<Shop_Npc_Item>().p_num_etc)
                    {
                        st.shop_player_list[i].GetComponent<Button>().interactable = true;
                    }
                }
            }
            for (int i = 0; i < st.shop_psell_list.Count; i++)
            {
                if (st.shop_psell_list[i] == gameObject)
                {
                    Destroy(gameObject);
                    st.shop_psell_list.RemoveAt(i);
                }
            }
            if (st.shop_psell_list.Count <= 0)
            {
                st.dial_object[3].GetComponent<Shop_UI>().shop_btn[1].interactable = false;
            }
            Player_Price_Calcul();
        }
    }
    void Price_Calcul()
    {
        GameObject stm = GameObject.FindWithTag("Manager");
        Stage_Manager st = stm.GetComponent<Stage_Manager>();
        int result = 0;
        for (int i = 0; i < st.shop_nsell_list.Count; i++)
        {
            result += st.shop_nsell_list[i].GetComponent<Shop_Npc_SellItem>().price;
        }
        st.dial_object[3].GetComponent<Shop_UI>().price.text = result.ToString();
    }
    void Player_Price_Calcul()
    {
        GameObject stm = GameObject.FindWithTag("Manager");
        Stage_Manager st = stm.GetComponent<Stage_Manager>();
        int result = 0;
        for (int i = 0; i < st.shop_psell_list.Count; i++)
        {
            result += st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().price;
        }
        st.dial_object[3].GetComponent<Shop_UI>().p_price.text = result.ToString();
    }
}
