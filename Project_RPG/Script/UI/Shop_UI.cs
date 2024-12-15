using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_UI : MonoBehaviour
{
    public Use_Item use;
    public Etc_Item etc;

    public InvenTory_Use p_use;
    public InvenTory_Etc p_etc;

    public GameObject objects;
    public GameObject item_box;
    public TMP_InputField input_cnt;
    public Button[] shop_btn;

    [Header("UI 부분 영역")]
    public int price_int;
    public int p_price_int;
    public TextMeshProUGUI price;
    public TextMeshProUGUI p_price;

    public GameObject player_item;
    public int p_num;
    public void Accept() 
    {
        int cnt = int.Parse(input_cnt.text);

        if (cnt >= 1 && cnt <= 999)
        {
            GameObject stm = GameObject.FindWithTag("Manager");
            Stage_Manager st = stm.GetComponent<Stage_Manager>();

            if (use != null)
            {
                GameObject a = Instantiate(item_box, st.shop_places[1].transform);
                Shop_Npc_SellItem b = a.GetComponent<Shop_Npc_SellItem>();
                b.use = use;
                b.item_img.sprite = use.item_img;
                b.cnt = cnt;
                b.cnt_text.text = cnt.ToString();
                b.price = use.buy_price * cnt;
                b.type = 1;
                st.shop_nsell_list.Add(a);
                Cnt_Logic(0);
            }
            else if (etc != null)
            {
                GameObject a = Instantiate(item_box, st.shop_places[1].transform);
                Shop_Npc_SellItem b = a.GetComponent<Shop_Npc_SellItem>();
                b.etc = etc;
                b.item_img.sprite = etc.item_img;
                b.cnt = cnt;
                b.cnt_text.text = cnt.ToString();
                b.price = etc.buy_price * cnt;
                b.type = 2;
                st.shop_nsell_list.Add(a);
                Cnt_Logic(0);
            }
            else if (p_use != null && p_use.reserves >= cnt)
            {
                GameObject c = Instantiate(item_box, st.shop_places[3].transform);
                Shop_Npc_SellItem b = c.GetComponent<Shop_Npc_SellItem>();
                b.p_use = p_use;
                for (int i = 0; i < GameManager.instance.allUseItems.Length; i++) 
                {
                    if (p_use.item_name.Equals(GameManager.instance.allUseItems[i].item_name))
                    {
                        b.item_img.sprite = GameManager.instance.allUseItems[i].item_img; 
                        break;
                    }
                }
                b.cnt = cnt;
                b.cnt_text.text = cnt.ToString();
                b.price = p_use.sell_price * cnt;
                b.type = 1;
                b.p_origin_use = p_num;
                b.p_item = true;
                player_item.GetComponent<Button>().interactable = false;
                st.shop_psell_list.Add(c);
                Cnt_Logic(1);
            }
            else if (p_etc != null && p_etc.reserves >= cnt)
            {
                GameObject c = Instantiate(item_box, st.shop_places[3].transform);
                Shop_Npc_SellItem b = c.GetComponent<Shop_Npc_SellItem>();
                b.p_etc = p_etc;
                for (int i = 0; i < GameManager.instance.allEtcitems.Length; i++)
                {
                    if (p_etc.item_name.Equals(GameManager.instance.allEtcitems[i].item_name))
                    {
                        b.item_img.sprite = GameManager.instance.allEtcitems[i].item_img;
                        break;
                    }
                }
                b.cnt = cnt;
                b.cnt_text.text = cnt.ToString();
                b.price = p_etc.sell_price * cnt;
                b.type = 2;
                b.p_origin_etc = p_num;
                b.p_item = true;
                player_item.GetComponent<Button>().interactable = false;
                st.shop_psell_list.Add(c);
                Cnt_Logic(1);
            }
        }
    }
    public void Cnt_Logic(int i) 
    {
        objects.SetActive(false);
        shop_btn[i].interactable = true;
        Price_Calcul();
        Player_Price_Calcul();
    }
    public void Close() 
    {
        input_cnt.text = "";
        objects.SetActive(false);
    }

    void Price_Calcul()
    {
        GameObject stm = GameObject.FindWithTag("Manager");
        Stage_Manager st = stm.GetComponent<Stage_Manager>();
        int result = 0;
        for(int i = 0; i < st.shop_nsell_list.Count; i++) 
        {
            result += st.shop_nsell_list[i].GetComponent<Shop_Npc_SellItem>().price;
        }
        price_int = result;
        price.text = result.ToString();
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
        p_price_int = result;
        p_price.text = result.ToString();
    }

    public void Buy()
    {
        GameObject stm = GameObject.FindWithTag("Manager");
        Stage_Manager st = stm.GetComponent<Stage_Manager>();

        if (GameManager.instance.gold >= price_int) 
        {
            for (int r = 0; r < st.shop_nsell_list.Count; r++) 
            {
                if (st.shop_nsell_list[r].GetComponent<Shop_Npc_SellItem>().type == 0)
                {
                    Equip_Item equip_item = st.shop_nsell_list[r].GetComponent<Shop_Npc_SellItem>().equip;
                    InvenTory_Equipment inven_item = new InvenTory_Equipment();
                    float[] plus_option = equip_item.item_option;
                    // 추가옵션 랜덤 지정
                    if (equip_item.item_option != null && equip_item.item_option.Length > 0)
                    {
                        for (int i = 0; i < equip_item.item_option.Length; i++)
                        {
                            plus_option[i] = equip_item.item_option[i] = Mathf.RoundToInt(Random.Range(equip_item.item_option_min[i], equip_item.item_option_max[i]));
                            if (i == 1 && equip_item.attack_type == 1)
                            {
                                plus_option[i] = equip_item.item_option[i] = Mathf.Round(Random.Range(equip_item.item_option_min[i], equip_item.item_option_max[i]) * 100f) / 100;
                            }
                            if (i == 2)
                            {
                                plus_option[i] = equip_item.item_option[i] = Mathf.Round(Random.Range(equip_item.item_option_min[i], equip_item.item_option_max[i]) * 100f) / 100;
                            }
                        }
                    }

                    // 인벤토리 추가 로직
                    inven_item.item_id = equip_item.item_id;
                    inven_item.item_name = equip_item.item_name;
                    inven_item.item_exp = equip_item.item_exp;
                    inven_item.item_lv = equip_item.item_lv;
                    inven_item.type = equip_item.type;
                    inven_item.attack_type = equip_item.attack_type;
                    inven_item.base_option = equip_item.base_option;
                    inven_item.enforce = equip_item.enforce;
                    inven_item.sell_price = equip_item.sell_price;
                    inven_item.rare = equip_item.rare;
                    inven_item.item_option = plus_option;
                    //inven_item.item_skill_option = equip.item_skill_option; 아직 스킬로직은 없으니!

                    GameManager.instance.inventory_equip.Add(inven_item);
                }
                else if (st.shop_nsell_list[r].GetComponent<Shop_Npc_SellItem>().type == 1)
                {
                    Use_Item use_item = st.shop_nsell_list[r].GetComponent<Shop_Npc_SellItem>().use;
                    InvenTory_Use inven_item = new InvenTory_Use();
                    inven_item.item_id = use_item.item_id;
                    inven_item.item_type = use_item.item_type;
                    inven_item.item_name = use_item.item_name;
                    inven_item.item_exp = use_item.item_exp;
                    inven_item.sell_price = use_item.sell_price;
                    inven_item.rare = use_item.rare;

                    inven_item.hp_recover = use_item.hp_recover;
                    inven_item.mp_recover = use_item.mp_recover;
                    inven_item.map_name = use_item.map_name;
                    inven_item.reserves = st.shop_nsell_list[r].GetComponent<Shop_Npc_SellItem>().cnt;
                    if (GameManager.instance.inventory_use.Count >= 1)
                    {
                        bool found = false;
                        for (int i = 0; i < GameManager.instance.inventory_use.Count; i++)
                        {
                            if (GameManager.instance.inventory_use[i].item_id == inven_item.item_id)
                            {
                                GameManager.instance.inventory_use[i].reserves += inven_item.reserves;
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            GameManager.instance.inventory_use.Add(inven_item);
                        }
                    }
                    else
                    {
                        GameManager.instance.inventory_use.Add(inven_item);
                    }
                }
                else if (st.shop_nsell_list[r].GetComponent<Shop_Npc_SellItem>().type == 2)
                {
                    Etc_Item etc_item = st.shop_nsell_list[r].GetComponent<Shop_Npc_SellItem>().etc;
                    InvenTory_Etc inven_item = new InvenTory_Etc();

                    inven_item.item_id = etc_item.item_id;
                    inven_item.item_name = etc_item.item_name;
                    inven_item.item_exp = etc_item.item_exp;
                    inven_item.sell_price = etc_item.sell_price;
                    inven_item.rare = etc_item.rare;
                    inven_item.reserves = st.shop_nsell_list[r].GetComponent<Shop_Npc_SellItem>().cnt;

                    if (GameManager.instance.inventory_etc.Count >= 1)
                    {
                        bool found = false;
                        for (int i = 0; i < GameManager.instance.inventory_etc.Count; i++)
                        {
                            if (GameManager.instance.inventory_etc[i].item_id == inven_item.item_id)
                            {
                                GameManager.instance.inventory_etc[i].reserves += inven_item.reserves;
                                found = true;
                            }
                        }
                        if (found == false)
                        {
                            GameManager.instance.inventory_etc.Add(inven_item);
                        }
                    }
                    else
                    {
                        GameManager.instance.inventory_etc.Add(inven_item);
                    }
                }
            }
            // 거래 끝
            GameManager.instance.gold -= price_int;
            price.text = "";
            price_int = 0;

            for(int i = 0; i < st.shop_nsell_list.Count; i++)
            {
                Destroy(st.shop_nsell_list[i]);
            }
            st.shop_nsell_list.Clear();
            shop_btn[0].interactable = false;

            if (st.shop_now == 1)
            {
                st.Shop_Btn_Equip();
            }
            else if (st.shop_now == 2)
            {
                st.Shop_Btn_Use();
            }
            else if (st.shop_now == 3)
            {
                st.Shop_Btn_Etc();
            }
        }
    }

    public void Sell()
    {
        GameObject stm = GameObject.FindWithTag("Manager");
        Stage_Manager st = stm.GetComponent<Stage_Manager>();
        bool quick_set = false;

        for (int i = 0; i < st.shop_psell_list.Count; i++)
        {
            if (st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().type == 0)
            {
                GameManager.instance.gold += st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().p_equip.sell_price;
                for (int l = 0; l < GameManager.instance.inventory_equip.Count; l++)
                {
                    if (GameManager.instance.inventory_equip[l] == st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().p_equip)
                    {
                        GameManager.instance.inventory_equip.RemoveAt(l);
                    }
                }
            }
            else if (st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().type == 1)
            {
                int calcul = st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().p_use.sell_price * st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().cnt; ;
                GameManager.instance.gold += calcul;
                for (int l = 0; l < GameManager.instance.inventory_use.Count; l++)
                {
                    if (GameManager.instance.inventory_use[l] == st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().p_use)
                    {
                        GameManager.instance.inventory_use[l].reserves -= st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().cnt;

                        for (int r = 0; r < st.item_quick_slot.Length; r++)
                        {
                            if (st.item_quick_slot[r].GetComponent<Quick_Item_Slot>().use == st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().p_use)
                            {
                                st.item_quick_slot[r].GetComponent<Quick_Item_Slot>().Set();
                                quick_set = true;
                            }
                        }

                        if (!quick_set && GameManager.instance.inventory_use[l].reserves <= 0)
                        {
                            GameManager.instance.inventory_use.RemoveAt(l);
                        }
                    }
                }
            }
            else if (st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().type == 2)
            {
                int calcul = st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().p_etc.sell_price * st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().cnt; ;
                GameManager.instance.gold += calcul;
                for (int l = 0; l < GameManager.instance.inventory_etc.Count; l++)
                {
                    if (GameManager.instance.inventory_etc[l] == st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().p_etc)
                    {
                        GameManager.instance.inventory_etc[l].reserves -= st.shop_psell_list[i].GetComponent<Shop_Npc_SellItem>().cnt;

                        if (GameManager.instance.inventory_etc[l].reserves <= 0)
                        {
                            GameManager.instance.inventory_etc.RemoveAt(l);
                        }
                    }
                }
            }
        }
        
        for (int r = 0; r < st.shop_psell_list.Count; r++)
        {
            Destroy(st.shop_psell_list[r]);
        }
        st.shop_psell_list.Clear();

        if (st.shop_now == 1)
        {
            st.Shop_Btn_Equip();
        }
        else if (st.shop_now == 2)
        {
            st.Shop_Btn_Use();
        }
        else if (st.shop_now == 3)
        {
            st.Shop_Btn_Etc();
        }
        
        p_price.text = "";
        p_price_int = 0;
        shop_btn[1].interactable = false;
    }
}
