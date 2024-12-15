using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Shop_Npc_Item : MonoBehaviour
{
    public TextMeshProUGUI[] item_infos;
    public Equip_Item equip_item;
    public Use_Item use_item;
    public Etc_Item etc_item;

    public InvenTory_Equipment p_equip_item;
    public InvenTory_Use p_use_item;
    public InvenTory_Etc p_etc_item;
    public Image item_img;

    public bool p_item;
    public int p_num_equip;
    public int p_num_use;
    public int p_num_etc;
    public GameObject shop_obejct;
    void Start() 
    {
        GameObject stm = GameObject.FindWithTag("Manager");
        Stage_Manager st = stm.GetComponent<Stage_Manager>();

        for (int i = 0; i < st.shop_psell_list.Count; i++)
        {
            if (p_equip_item != null)
            {
                if (st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().p_origin_equip == p_num_equip)
                {
                    gameObject.GetComponent<Button>().interactable = false;
                }
            }
            else if (p_use_item != null)
            {
                if (st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().p_origin_use == p_num_use)
                {
                    gameObject.GetComponent<Button>().interactable = false;
                }
            }
            else if (p_etc_item != null)
            {
                if (st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().p_origin_etc == p_num_etc)
                {
                    gameObject.GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public void Click() 
    {
        GameObject stm = GameObject.FindWithTag("Manager");
        Stage_Manager st = stm.GetComponent<Stage_Manager>();

        // 상인 아이템
        if (equip_item != null)
        {
            GameObject a = Instantiate(shop_obejct, st.shop_places[1].transform);
            Shop_Npc_SellItem b = a.GetComponent<Shop_Npc_SellItem>();
            b.equip = equip_item;
            b.item_img.sprite = equip_item.item_img[0];
            b.price = equip_item.buy_price;
            b.cnt_text.gameObject.SetActive(false);
            st.shop_nsell_list.Add(a);
            st.dial_object[3].GetComponent<Shop_UI>().shop_btn[0].interactable = true;
            Price_Calcul();
        }
        else if (use_item != null || etc_item != null)
        {
            st.dial_object[3].SetActive(true);
            st.dial_object[3].GetComponent<Shop_UI>().use = null;
            st.dial_object[3].GetComponent<Shop_UI>().etc = null;

            st.dial_object[3].GetComponent<Shop_UI>().p_use = null;
            st.dial_object[3].GetComponent<Shop_UI>().p_etc = null;

            st.dial_object[3].GetComponent<Shop_UI>().use = use_item;
            st.dial_object[3].GetComponent<Shop_UI>().etc = etc_item;
        }

        // 유저 아이템
        else if (p_equip_item != null)
        {
            GameObject a = Instantiate(shop_obejct, st.shop_places[3].transform);
            Shop_Npc_SellItem b = a.GetComponent<Shop_Npc_SellItem>();
            b.p_equip = p_equip_item;
            for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++)
            {
                if (p_equip_item.item_id == GameManager.instance.allEquipItems[i].item_id)
                {
                    b.item_img.sprite = GameManager.instance.allEquipItems[i].item_img[0];
                    break;
                }
            }
            b.price = p_equip_item.sell_price;
            b.cnt_text.gameObject.SetActive(false);
            b.p_item = true;
            b.p_origin_equip = p_num_equip;
            gameObject.GetComponent<Button>().interactable = false;
            st.shop_psell_list.Add(a);
            st.dial_object[3].GetComponent<Shop_UI>().shop_btn[1].interactable = true;
            Price_Calcul();
        }
        else if (p_use_item != null || p_etc_item != null) 
        {
            st.dial_object[3].SetActive(true);
            st.dial_object[3].GetComponent<Shop_UI>().use = null;
            st.dial_object[3].GetComponent<Shop_UI>().etc = null;

            st.dial_object[3].GetComponent<Shop_UI>().p_use = null;
            st.dial_object[3].GetComponent<Shop_UI>().p_etc = null;

            st.dial_object[3].GetComponent<Shop_UI>().p_num = 0;
            st.dial_object[3].GetComponent<Shop_UI>().player_item = null;

            st.dial_object[3].GetComponent<Shop_UI>().p_use = p_use_item;
            st.dial_object[3].GetComponent<Shop_UI>().p_etc = p_etc_item;
            if (p_num_use >= 1)
            {
                st.dial_object[3].GetComponent<Shop_UI>().p_num = p_num_use;
            }
            else if (p_num_etc >= 1)
            {
                st.dial_object[3].GetComponent<Shop_UI>().p_num = p_num_etc;
            }
            st.dial_object[3].GetComponent<Shop_UI>().player_item = gameObject;
        }
    }
    void Price_Calcul()
    {
        GameObject stm = GameObject.FindWithTag("Manager");
        Stage_Manager st = stm.GetComponent<Stage_Manager>();
        int result = 0;
        if (!p_item)
        {
            for (int i = 0; i < st.shop_nsell_list.Count; i++)
            {
                result += st.shop_nsell_list[i].GetComponent<Shop_Npc_SellItem>().price;
            }
            st.dial_object[3].GetComponent<Shop_UI>().price.text = result.ToString();
        }
        else 
        {
            for (int i = 0; i < st.shop_psell_list.Count; i++)
            {
                result += st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().price;
            }
            st.dial_object[3].GetComponent<Shop_UI>().p_price.text = result.ToString();
        }
    }
}
