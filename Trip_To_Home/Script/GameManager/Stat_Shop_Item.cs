using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stat_Shop_Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int num;
    public int cost;

    public TextMeshProUGUI keeper_text;
    public string m_text;

    public GameObject[] btns;
    public GameObject[] items;

    public GameObject buy_alert;
    public TextMeshProUGUI buy_cost_text;
    Button self_btn;
    void Start()
    {
        self_btn = GetComponent<Button>();
    }
    void Update() 
    {
        // 최대레벨 달성 시 더이상 못사게
        if (num != 7 && GameManager.instance.playerdata.bonus_lv[num] == 5) 
        {
            self_btn.interactable = false;
        }
        if (num == 7 && GameManager.instance.playerdata.bonus_lv[7] == 1)
        {
            self_btn.interactable = false;
        }

        // 가격 설정
        if (num != 7)
        {
            if (GameManager.instance.playerdata.bonus_lv[num] == 0)
            {
                cost = 100;
            }
            else if (GameManager.instance.playerdata.bonus_lv[num] == 1)
            {
                cost = 300;
            }
            else if (GameManager.instance.playerdata.bonus_lv[num] == 2)
            {
                cost = 750;
            }
            else if (GameManager.instance.playerdata.bonus_lv[num] == 3)
            {
                cost = 1500;
            }
            else if (GameManager.instance.playerdata.bonus_lv[num] == 4)
            {
                cost = 3000;
            }
        }
        else
        {
            if (GameManager.instance.playerdata.bonus_lv[num] == 0)
            {
                cost = 1350;
            }
        }
    }
    // 마우스 올리기
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (self_btn.interactable == true) 
        {
            switch (num)
        {
            case 0:
                m_text = "영구적으로 최대 체력을 증가시킵니다. 가격은" + cost + "코인입니다. 구매하시겠어요? ";
                if (GameManager.instance.playerdata.bonus_lv[num] == 5) 
                {
                    m_text = "이미 최대 레벨이라 더는 구매하실 수 없으세요.";
                }
                break;
            case 1:
                m_text = "영구적으로 공격력을 증가시킵니다. 가격은" + cost + "코인입니다. 구매하시겠어요? ";
                if (GameManager.instance.playerdata.bonus_lv[num] == 5)
                {
                    m_text = "이미 최대 레벨이라 더는 구매하실 수 없으세요.";
                }
                break;
            case 2:
                m_text = "영구적으로 방어력을 증가시킵니다. 가격은" + cost + "코인입니다. 구매하시겠어요? ";
                if (GameManager.instance.playerdata.bonus_lv[num] == 5)
                {
                    m_text = "이미 최대 레벨이라 더는 구매하실 수 없으세요.";
                }
                break;
            case 3:
                m_text = "영구적으로 공격속도를 증가시킵니다. 가격은" + cost + "코인입니다. 구매하시겠어요? ";
                if (GameManager.instance.playerdata.bonus_lv[num] == 5)
                {
                    m_text = "이미 최대 레벨이라 더는 구매하실 수 없으세요.";
                }
                break;
            case 4:
                m_text = "영구적으로 탄속을 증가시킵니다. 가격은" + cost + "코인입니다. 구매하시겠어요? ";
                if (GameManager.instance.playerdata.bonus_lv[num] == 5)
                {
                    m_text = "이미 최대 레벨이라 더는 구매하실 수 없으세요.";
                }
                break;
            case 5:
                m_text = "영구적으로 이동속도를 증가시킵니다. 가격은" + cost + "코인입니다. 구매하시겠어요? ";
                if (GameManager.instance.playerdata.bonus_lv[num] == 5)
                {
                    m_text = "이미 최대 레벨이라 더는 구매하실 수 없으세요.";
                }
                break;
            case 6:
                m_text = "영구적으로 점프력을 증가시킵니다. 가격은" + cost + "코인입니다. 구매하시겠어요? ";
                if (GameManager.instance.playerdata.bonus_lv[num] == 5)
                {
                    m_text = "이미 최대 레벨이라 더는 구매하실 수 없으세요.";
                }
                break;
            case 7:
                m_text = "영구적으로 점프횟수를 증가시킵니다. 가격은" + cost + "코인입니다. 구매하시겠어요? ";
                if (GameManager.instance.playerdata.bonus_lv[num] == 3)
                {
                    m_text = "이미 최대 레벨이라 더는 구매하실 수 없으세요.";
                }
                break;
        }
            Chk();
        }
    }
    public void Chk()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].GetComponent<Button>().interactable = false;
        }
        StopCoroutine(Typing_effect());
        StartCoroutine(Typing_effect());

    }
    // 마우스 내리기
    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(Typing_effect());
        m_text = null;
        keeper_text.text = null;        
        m_text = "어떤 물건을 보시겠어요??";
        StartCoroutine(Typing_effect());

        for (int i = 0; i < items.Length; i++)
        {
            items[i].GetComponent<Button>().interactable = true;
        }
    }
    
    IEnumerator Typing_effect() 
    {
        for (int i = 0; i < m_text.Length; i++) 
        {
            keeper_text.text = m_text.Substring(0, i);
            yield return new WaitForSeconds(0.02f); 
        }
    }

    // 구매
    public void Buy()
    {
        buy_alert.SetActive(true);
        btns[0].SetActive(true);
        btns[1].SetActive(true);
        btns[2].SetActive(false);
        buy_cost_text.text = "정말로 구매하시겠습니까?";
        GameManager.instance.bonus_shop_cost = cost;
        GameManager.instance.bonus_shop_value = num;
    }
}
