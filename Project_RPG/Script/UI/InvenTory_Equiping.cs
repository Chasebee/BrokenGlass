using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenTory_Equiping : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    GameObject st;
    Stage_Manager stm;
    float dbClick_time = 1.2f;
    float last_Click = 0f;
    public bool clicking_now = false;

    public InvenTory_Equipment eq_item;
    public Image item_img;

    public GameObject tooltip;
    private RectTransform tooltipRectTransform;
    private Canvas canvas;

    public int item_type;
    void Start()
    {
        st = GameObject.FindWithTag("Manager");
        stm = st.GetComponent<Stage_Manager>();
        //  장비가 있는경우 ( 로드 )
        if (item_type == 2 && GameManager.instance.body != null)
        {
            eq_item = GameManager.instance.body;
            for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++)
            {
                if (GameManager.instance.allEquipItems[i].item_id == eq_item.item_id)
                {
                    item_img.sprite = GameManager.instance.allEquipItems[i].item_img[0];
                    break;
                }
            }
            item_img.gameObject.SetActive(true);
        }
        if (item_type == 3 && GameManager.instance.bottom != null)
        {
            eq_item = GameManager.instance.bottom;
            for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++)
            {
                if (GameManager.instance.allEquipItems[i].item_id == eq_item.item_id)
                {
                    item_img.sprite = GameManager.instance.allEquipItems[i].item_img[0];
                    break;
                }
            }
            item_img.gameObject.SetActive(true);
        }
        else if (item_type == 4 && GameManager.instance.weapon != null)
        {
            eq_item = GameManager.instance.weapon;
            for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++)
            {
                if (GameManager.instance.allEquipItems[i].item_id == eq_item.item_id)
                {
                    item_img.sprite = GameManager.instance.allEquipItems[i].item_img[0];
                    break;
                }
            }
            item_img.gameObject.SetActive(true);
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
        if (clicking_now == true) // 마우스 왼쪽 버튼 클릭
        {
            float currentTime = Time.time;
            float timeSinceLastClick = currentTime - last_Click;

            if (timeSinceLastClick <= dbClick_time)
            {
                clicking_now = false;
                OnDoubleClick();
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
        // 상의
        if (item_type == 2 && GameManager.instance.body != null) 
        {
            GameManager.instance.EquipItem_UnEquip(eq_item);
            item_img.gameObject.SetActive(false);
            stm.Item_Equipment_Create();
            stm.tool_tip.SetActive(false);
            eq_item = null;
        }
        // 하의
        else if (item_type == 3 && GameManager.instance.bottom != null)
        {
            GameManager.instance.EquipItem_UnEquip(eq_item);
            item_img.gameObject.SetActive(false);
            stm.Item_Equipment_Create();
            stm.tool_tip.SetActive(false);
            eq_item = null;
        }
        // 무기
        else if (item_type == 4 && GameManager.instance.weapon != null)
        {
            GameManager.instance.EquipItem_UnEquip(eq_item);
            item_img.gameObject.SetActive(false);
            stm.Item_Equipment_Create();
            stm.tool_tip.SetActive(false);
            eq_item = null;
        }
    }

    public void OnPointerEnter(PointerEventData edata)
    {
        if (eq_item != null)
        {
            tooltip.SetActive(true);
            Tool_Tip_Data();
            PositionTooltip(edata);
        }
    }
    public void OnPointerExit(PointerEventData edata)
    {
        tooltip.SetActive(false);
    }
    public void OnPointerMove(PointerEventData edata)
    {
        PositionTooltip(edata);
    }
    private void PositionTooltip(PointerEventData edata)
    {
        // Tooltip의 RectTransform과 Canvas가 초기화되었는지 확인
        if (tooltipRectTransform == null || canvas == null)
            return;

        // 마우스 위치를 화면 좌표에서 로컬 좌표로 변환
        Vector2 localPointerPosition;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            edata.position,
            canvas.worldCamera,
            out localPointerPosition))
        {
            return;
        }

        // 마우스 위치에 약간의 오프셋(여백)을 추가
        Vector2 offset = new Vector2(10f, -10f); // X축과 Y축에 각각 20픽셀의 여백을 추가
        Vector2 newLocalPosition = localPointerPosition + new Vector2(tooltipRectTransform.sizeDelta.x / 2, -tooltipRectTransform.sizeDelta.y / 2) + offset;

        // Tooltip이 화면의 경계를 넘어가지 않도록 조정
        Vector2 tooltipSize = tooltipRectTransform.sizeDelta;
        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        if (localPointerPosition.x + tooltipSize.x / 2 > canvasSize.x / 2)
        {
            newLocalPosition.x = canvasSize.x / 2 - tooltipSize.x - 10;
        }
        else if (localPointerPosition.x - tooltipSize.x / 2 < -canvasSize.x / 2)
        {
            newLocalPosition.x = -canvasSize.x / 2 + 10;
        }

        if (localPointerPosition.y + tooltipSize.y / 2 > canvasSize.y / 2)
        {
            newLocalPosition.y = canvasSize.y / 2 - tooltipSize.y - 10;
        }
        else if (localPointerPosition.y - tooltipSize.y / 2 < -canvasSize.y / 2)
        {
            newLocalPosition.y = -canvasSize.y / 2 + 10;
        }

        // 계산된 위치로 Tooltip의 RectTransform을 조정
        tooltipRectTransform.localPosition = newLocalPosition;
    }

    public void Tool_Tip_Data()
    {
        InvenTory_ToolTip a = tooltip.GetComponent<InvenTory_ToolTip>();

        if (eq_item != null) { a.equip_item = eq_item; a.tool_type = 0; }

        a.ToolTip_Stat();
    }
}
