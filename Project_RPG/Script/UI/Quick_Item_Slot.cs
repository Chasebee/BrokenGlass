using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quick_Item_Slot : MonoBehaviour
{
    public Image img, cooltime_img;
    public TextMeshProUGUI cnt;
    public InvenTory_Use use;
    public int num;
    public int inven_num;
    GameObject stm;
    Stage_Manager st;
    void Start() 
    {
        stm = GameObject.FindWithTag("Manager");
        st = stm.GetComponent<Stage_Manager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeySet.keys[Key_Action.item_1]) && num == 0)
        {
            if (GameManager.instance.item_cool_time[0] <= 0 && !st.pause)
            {

                GameManager.instance.item_cool_time[0] = 10f;
                Item_Using();
            }
        }
        
        if (Input.GetKeyDown(KeySet.keys[Key_Action.item_2]) && num == 1)
        {
            if (GameManager.instance.item_cool_time[1] <= 0 && !st.pause)
            {
                GameManager.instance.item_cool_time[1] = 10f;
                Item_Using();
            }
        }

        if (GameManager.instance.item_cool_time[num] >= 0) 
        {
            GameManager.instance.item_cool_time[num] -= Time.deltaTime;
        }

        cooltime_img.fillAmount = GameManager.instance.item_cool_time[num] / 10f;
        if (cnt.gameObject.activeSelf && use != null)
        {
            cnt.text = use.reserves.ToString();
        }
    }
    public void Set() 
    {
        use = GameManager.instance.item_qucik_slot[num];
        if (use != null && use.reserves >= 1)
        {
            for (int i = 0; i < GameManager.instance.allUseItems.Length; i++)
            {
                if (GameManager.instance.allUseItems[i].item_name.Equals(use.item_name))
                {
                    img.gameObject.SetActive(true);
                    img.sprite = GameManager.instance.allUseItems[i].item_img;
                    cnt.text = use.reserves.ToString();
                }
            }
        }
        else 
        {
            use = null;
            img.gameObject.SetActive(false);
            img.sprite = null;
            inven_num = 0;
            cnt.text = "";
            GameManager.instance.item_qucik_slot[num] = null;
            GameManager.instance.save_item_quck_slot[num] = null;
            GameManager.instance.inventory_use.RemoveAt(inven_num);
        }
    }

    public void Item_Using() 
    {
        if (use != null) 
        {
            use.reserves--;
            GameManager.instance.hp += use.hp_recover;
            GameManager.instance.mp += use.mp_recover;
            Set();
        }
    }
}
