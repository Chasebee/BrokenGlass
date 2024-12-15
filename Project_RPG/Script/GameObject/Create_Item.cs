using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Create_Item : MonoBehaviour
{
    public Equip_Item equip_origin_item;
    public Etc_Item etc_origin_item;
    public Image item_img;
    public TextMeshProUGUI item_name;
    public GameObject material;
    public Button create_btn;
    Stage_Manager st;
    GameObject stm;

    void Start() 
    {
        stm = GameObject.FindWithTag("Manager");
        st = stm.GetComponent<Stage_Manager>();
        create_btn = st.UI_btns[10];
        if (equip_origin_item != null)
        {
            item_img.sprite = equip_origin_item.item_img[0];
            item_name.text = equip_origin_item.item_name;
        }
        else if (etc_origin_item != null) 
        {
            item_img.sprite = etc_origin_item.item_img;
        }
    }

    public void Click() 
    {
        Material_List_Clear();
        // 장비
        if (equip_origin_item != null)
        {
            st.create_item_img.sprite = item_img.sprite;
            st.create_item_img.gameObject.SetActive(true);
            st.create_item_name.text = equip_origin_item.item_name;

            for (int i = 0; i < equip_origin_item.create_Item.Length; i++) 
            {
                GameObject a = Instantiate(material, st.material_slot_place.transform);
                
                for (int l = 0; l < GameManager.instance.allEtcitems.Length; l++) 
                {
                    if (equip_origin_item.create_Item[i].Equals(GameManager.instance.allEtcitems[l].item_name)) 
                    {
                        a.GetComponent<Material_Item>().item_img.sprite = GameManager.instance.allEtcitems[l].item_img;
                        int cnt = 0;
                        for (int r = 0; r < GameManager.instance.inventory_etc.Count; r++) 
                        {
                            if (GameManager.instance.inventory_etc[r].item_name.Equals(equip_origin_item.create_Item[i]))
                            {
                                cnt = GameManager.instance.inventory_etc[r].reserves;
                            }
                        }
                        a.GetComponent<Material_Item>().count_text.text = cnt + " / " + equip_origin_item.material_count[i];
                        create_btn.GetComponent<Create_Btn>().inven_cnt.Add(cnt);
                        create_btn.GetComponent<Create_Btn>().material_cnt.Add(equip_origin_item.material_count[i]);
                    }
                }
                create_btn.GetComponent<Create_Btn>().equip_item = equip_origin_item;
                create_btn.GetComponent<Create_Btn>().item_type = 1;
                create_btn.interactable = true;
                st.material_list.Add(a);
            }
        }
        // 기타
        else if (etc_origin_item != null) 
        {
            
        }
        
    }

    public void Material_List_Clear() 
    {
        if (st.material_list != null && st.material_list.Count >= 1) 
        {
            for(int i = 0; i < st.material_list.Count; i++) 
            {
                Destroy(st.material_list[i]);
            }
            st.material_list.Clear();
        }
    }
}
