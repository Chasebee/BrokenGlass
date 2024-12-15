using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_Btn : MonoBehaviour
{
    public int item_type;
    public List<int> inven_cnt = new List<int>();
    public List<int> material_cnt = new List<int>();
    public Equip_Item equip_item;
    public Etc_Item etc_item;
    GameObject stm;
    Stage_Manager st;
    void Start()
    {
        stm = GameObject.FindWithTag("Manager");
        st = stm.GetComponent<Stage_Manager>();

    }
    public void Create_Item()
    {
        bool confirm = true;
        for (int i = 0; i < inven_cnt.Count; i++)
        {
            if (inven_cnt[i] < material_cnt[i])
            {
                confirm = false;
            }
        }
        if (confirm)
        {
            if (item_type == 1)
            {
                InvenTory_Equipment inven_item = new InvenTory_Equipment();
                float[] plus_option = equip_item.item_option;
                // 추가옵션 랜덤 지정
                if (equip_item.item_option != null && equip_item.item_option.Length > 0)
                {
                    for (int i = 0; i < equip_item.item_option.Length; i++)
                    {
                        plus_option[i] = equip_item.item_option[i] = Mathf.RoundToInt(Random.Range(equip_item.item_option_min[i], equip_item.item_option_max[i]));
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
                gameObject.GetComponent<Button>().interactable = false;
                st.Item_Equipment_Create();
            }
            //기타
            else if (item_type == 3)
            {
                InvenTory_Etc inven_item = new InvenTory_Etc();

                inven_item.item_id = etc_item.item_id;
                inven_item.item_name = etc_item.item_name;
                inven_item.item_exp = etc_item.item_exp;
                inven_item.sell_price = etc_item.sell_price;
                inven_item.rare = etc_item.rare;
                inven_item.reserves = 1;

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
        else 
        {
            Debug.Log("재료 부족");
        }
    }
}
