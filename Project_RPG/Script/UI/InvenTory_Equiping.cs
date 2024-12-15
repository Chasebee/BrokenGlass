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
        //  ��� �ִ°�� ( �ε� )
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
        // Tooltip RectTransform �ʱ�ȭ
        if (tooltip != null)
        {
            tooltipRectTransform = tooltip.GetComponent<RectTransform>();
            canvas = tooltip.GetComponentInParent<Canvas>();
        }
    }
    void Update()
    {
        if (clicking_now == true) // ���콺 ���� ��ư Ŭ��
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
        // ����
        if (item_type == 2 && GameManager.instance.body != null) 
        {
            GameManager.instance.EquipItem_UnEquip(eq_item);
            item_img.gameObject.SetActive(false);
            stm.Item_Equipment_Create();
            stm.tool_tip.SetActive(false);
            eq_item = null;
        }
        // ����
        else if (item_type == 3 && GameManager.instance.bottom != null)
        {
            GameManager.instance.EquipItem_UnEquip(eq_item);
            item_img.gameObject.SetActive(false);
            stm.Item_Equipment_Create();
            stm.tool_tip.SetActive(false);
            eq_item = null;
        }
        // ����
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
        // Tooltip�� RectTransform�� Canvas�� �ʱ�ȭ�Ǿ����� Ȯ��
        if (tooltipRectTransform == null || canvas == null)
            return;

        // ���콺 ��ġ�� ȭ�� ��ǥ���� ���� ��ǥ�� ��ȯ
        Vector2 localPointerPosition;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            edata.position,
            canvas.worldCamera,
            out localPointerPosition))
        {
            return;
        }

        // ���콺 ��ġ�� �ణ�� ������(����)�� �߰�
        Vector2 offset = new Vector2(10f, -10f); // X��� Y�࿡ ���� 20�ȼ��� ������ �߰�
        Vector2 newLocalPosition = localPointerPosition + new Vector2(tooltipRectTransform.sizeDelta.x / 2, -tooltipRectTransform.sizeDelta.y / 2) + offset;

        // Tooltip�� ȭ���� ��踦 �Ѿ�� �ʵ��� ����
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

        // ���� ��ġ�� Tooltip�� RectTransform�� ����
        tooltipRectTransform.localPosition = newLocalPosition;
    }

    public void Tool_Tip_Data()
    {
        InvenTory_ToolTip a = tooltip.GetComponent<InvenTory_ToolTip>();

        if (eq_item != null) { a.equip_item = eq_item; a.tool_type = 0; }

        a.ToolTip_Stat();
    }
}
