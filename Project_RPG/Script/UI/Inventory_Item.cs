using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory_Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    GameObject st;
    Stage_Manager stm;
    float dbClick_time = 1.2f;
    float last_Click = 0f;
    public bool clicking_now = false;

    [Header("아이템 관련")]
    public int item_type; // 0 = 장비, 1 = 소비, 2 = 기타
    public int inven_num;
    public InvenTory_Equipment eq_item;
    public InvenTory_Use use_item;
    public InvenTory_Etc etc_item;
    public Image item_img;
    public TextMeshProUGUI item_cnt;
    public GameObject tooltip;
    private RectTransform tooltipRectTransform;
    private Canvas canvas;
    void Start()
    {
        st = GameObject.FindWithTag("Manager");
        stm = st.GetComponent<Stage_Manager>();
        tooltip = stm.tool_tip;
        if (item_type == 0)
        {
            for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++)
            {
                if (GameManager.instance.allEquipItems[i].item_id == eq_item.item_id)
                {
                    item_img.sprite = GameManager.instance.allEquipItems[i].item_img[0];
                }
            }
        }
        else if (item_type == 1) 
        {
            for (int i = 0; i < GameManager.instance.allUseItems.Length; i++)
            {
                if (GameManager.instance.allUseItems[i].item_id == use_item.item_id)
                {
                    item_img.sprite = GameManager.instance.allUseItems[i].item_img;
                }
            }
        }
        else if (item_type == 2)
        {
            for (int i = 0; i < GameManager.instance.allEtcitems.Length; i++)
            {
                if (GameManager.instance.allEtcitems[i].item_id == etc_item.item_id)
                {
                    item_img.sprite = GameManager.instance.allEtcitems[i].item_img;
                }
            }
        }
        // Tooltip RectTransform 초기화
        if (tooltip != null)
        {
            tooltipRectTransform = tooltip.GetComponent<RectTransform>();
            canvas = tooltip.GetComponentInParent<Canvas>();
        }
    }

    void Update()
    {
        if (clicking_now == true) // 마우스 왼쪽 버튼 클릭 ( ture 되어있는 시간 유지로 더블클릭 감지 )
        {
            float currentTime = Time.time;
            float timeSinceLastClick = currentTime - last_Click;

            if (timeSinceLastClick <= dbClick_time)
            {
                if (item_type == 0)
                {
                    OnDoubleClick();
                }
                else if (item_type == 1) 
                {
                    clicking_now = false;
                    Use_DB_Click();
                }
            }
            else 
            {
                clicking_now = false;
            }

            last_Click = currentTime;
        }

    }

    public void Clicking() 
    {
        clicking_now = true;
    }
    void OnDoubleClick()
    {
        if (GameManager.instance.lv >= eq_item.item_lv)
        {
            GameManager.instance.EquipItem_Change(eq_item, eq_item.type); // 실제 스크립터블 오브젝트 값 사용해야함
            stm.Item_Equipment_Create();
            stm.tool_tip.SetActive(false);
            // 무기
            if (eq_item.type == 1)
            {
                stm.equiping_now[3].GetComponent<InvenTory_Equiping>().eq_item = eq_item;
                stm.equiping_now[3].GetComponent<InvenTory_Equiping>().item_img.gameObject.SetActive(true);
                for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++)
                {
                    if (GameManager.instance.allEquipItems[i].item_id == eq_item.item_id)
                    {
                        stm.equiping_now[3].GetComponent<InvenTory_Equiping>().item_img.sprite = GameManager.instance.allEquipItems[i].item_img[0];
                    }
                }
            }
            // 상의
            else if (eq_item.type == 3)
            {
                stm.equiping_now[1].GetComponent<InvenTory_Equiping>().eq_item = eq_item;
                stm.equiping_now[1].GetComponent<InvenTory_Equiping>().item_img.gameObject.SetActive(true);
                for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++)
                {
                    if (GameManager.instance.allEquipItems[i].item_id == eq_item.item_id)
                    {
                        stm.equiping_now[1].GetComponent<InvenTory_Equiping>().item_img.sprite = GameManager.instance.allEquipItems[i].item_img[0];
                    }
                }
            }
            // 하의
            else if (eq_item.type == 4)
            {
                stm.equiping_now[2].GetComponent<InvenTory_Equiping>().eq_item = eq_item;
                stm.equiping_now[2].GetComponent<InvenTory_Equiping>().item_img.gameObject.SetActive(true);
                for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++) 
                {
                    if (GameManager.instance.allEquipItems[i].item_id == eq_item.item_id) 
                    {
                        stm.equiping_now[2].GetComponent<InvenTory_Equiping>().item_img.sprite = GameManager.instance.allEquipItems[i].item_img[0];
                    }
                }
            }
            Destroy(gameObject);
        }
    }
    void Use_DB_Click() 
    {
        // 포션인 경우
        if (GameManager.instance.inventory_use[inven_num].item_type == 0)
        {
            GameManager.instance.hp += GameManager.instance.inventory_use[inven_num].hp_recover;
            GameManager.instance.mp += GameManager.instance.inventory_use[inven_num].mp_recover;
        }

        GameManager.instance.inventory_use[inven_num].reserves--;

        // 전부 사용한 경우
        if (GameManager.instance.inventory_use[inven_num].reserves <= 0) 
        {
            GameManager.instance.inventory_use.RemoveAt(inven_num);
        }
        stm.Item_Use_Create();
    }

    public void OnPointerEnter(PointerEventData edata)
    {
        tooltip.SetActive(true);
        Tool_Tip_Data();
    }
    public void OnPointerExit(PointerEventData edata)
    {
        tooltip.SetActive(false);
    }
    public void OnPointerMove(PointerEventData edata)
    {
        tooltip.SetActive(true); 
    }

    // 우클릭
    public void OnPointerClick(PointerEventData edata) 
    {
        if (edata.button == PointerEventData.InputButton.Right && item_type == 1) 
        {
            if (use_item.item_type == 0) 
            {
                stm.use_quick_object.SetActive(true);
                stm.use_quick_object.GetComponent<UI_Use>().Slot_set();
                stm.use_quick_object.GetOrAddComponent<UI_Use>().get_num = inven_num;
            }
        }
    }
    public void Tool_Tip_Data() 
    {
        InvenTory_ToolTip a = tooltip.GetComponent<InvenTory_ToolTip>();
        
        a.tool_type = item_type;
        // 장비템인 경우
        if (eq_item != null) { a.equip_item = eq_item; a.tool_type = 0; }
        else if (use_item != null) {a.use_item = use_item; a.tool_type = 1; }
        else if (etc_item != null) { a.etc_item = etc_item; a.tool_type = 2; }
        a.ToolTip_Stat();
    }
    
}
