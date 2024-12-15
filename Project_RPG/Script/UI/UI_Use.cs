using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Use : MonoBehaviour
{
    public int object_type; // 0 = Äü½½·Ô µî·Ï ¿ÀºêÁ§Æ® 1 = ½½·Ô
    public int get_num;
    public GameObject[] slots;

    public GameObject quick_slot_object;
    public int quick_slot_num;
    public GameObject ui_object;
    public Image img;



    public void Slot_set()
    {
        for(int i = 0; i < slots.Length; i++) 
        {
            int a = slots[i].GetComponent<UI_Use>().quick_slot_num;
            for (int l = 0; l < GameManager.instance.allUseItems.Length; l++) 
            {
                if (GameManager.instance.allUseItems[l].item_name.Equals(GameManager.instance.save_item_quck_slot[a])) 
                {
                    slots[i].GetComponent<UI_Use>().img.gameObject.SetActive(true);
                    slots[i].GetComponent<UI_Use>().img.sprite = GameManager.instance.allUseItems[l].item_img;
                    break;
                }
            }
        }
    }

    public void Add_Item_Slot() 
    {
        get_num = quick_slot_object.GetComponent<UI_Use>().get_num;
        GameManager.instance.item_qucik_slot[quick_slot_num] = GameManager.instance.inventory_use[get_num];
        GameManager.instance.save_item_quck_slot[quick_slot_num] = GameManager.instance.inventory_use[get_num].item_name;

        Quick_Item_Slot a = ui_object.GetComponent<Quick_Item_Slot>();
        a.inven_num = get_num;
        a.Set();

        quick_slot_object.SetActive(false);
    }
}
