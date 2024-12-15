using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Stage_Manager : MonoBehaviour
{
    [Header("NPC / 기본 UI")]
    public GameObject pause_Object;
    public GameObject[] npc_Dial;
    public GameObject[] dial_object;
    public TextMeshProUGUI[] dial_texts;
    public Npc_Script target_npc;
    public GameObject npc_quest;
    public GameObject npc_quest_place;
    public GameObject npc_reward_place;
    public List<GameObject> npc_q_list = new List<GameObject>();
    public List<GameObject> npc_reward_list = new List<GameObject>();
    public int npc_quest_select;

    public int shop_now;
    public GameObject shop_item;
    public GameObject[] shop_places;
    public List<GameObject> shop_npc_list = new List<GameObject>();
    public List<GameObject> shop_player_list = new List<GameObject>();
    public List<GameObject> shop_nsell_list = new List<GameObject>();
    public List<GameObject> shop_psell_list = new List<GameObject>();

    public bool pause = false;
    public GameObject[] p_object_list;
    
    public Button[] skill_UI_btn;
    public GameObject[] skill_UI_Object;
    public GameObject[] sk_qucik_slot;
    public GameObject[] plus_skill;
    public TextMeshProUGUI skill_point;

    [Header("인벤토리 관련")]
    public int inven_now;
    public TextMeshProUGUI gold, shop_gold;
    public GameObject item_slot;
    public GameObject item_slot_place;
    public GameObject tool_tip;
    public GameObject[] equiping_now;
    public GameObject use_quick_object;
    public List<GameObject> item_list = new List<GameObject>();
    public GameObject[] item_quick_slot;
    [Header("제작 관련")]
    public GameObject create_slot;
    public GameObject create_slot_place;
    public TextMeshProUGUI create_item_name;
    public Image create_item_img;
    public List<GameObject> create_list = new List<GameObject>();
    public GameObject material_slot_place;
    public List<GameObject> material_list = new List<GameObject>();
    [Header("퀘스트 관련")]
    public int quest_now;
    public GameObject quest;
    public GameObject quest_list_place;
    public List<GameObject> quest_list = new List<GameObject>();
    public TextMeshProUGUI[] quest_infos;
    public int quest_select;
    public GameObject quest_reward_place;
    public List<GameObject> quest_reward_list = new List<GameObject>();
    public Button quest_Clear;
    public Button give_up;

    public Slider hp_bar, mp_bar, exp_bar;
    public Sprite[] btn_imgs;
    public Button[] UI_btns;
    public TextMeshProUGUI[] stats;

    [Header("시간 관련")]
    public bool Swaping = false;
    public Color day;
    public Color night;
    public SpriteRenderer Object_Day;
    public float Day;

    [Range(0.01f, 0.2f)]
    public float trans_Time;

    void Start()
    {
        Object_Day.color = new Color
        (
            GameManager.instance.day_color[0],
            GameManager.instance.day_color[1],
            GameManager.instance.day_color[2],
            GameManager.instance.day_color[3]
        );
        // 낮/밤
        if (GameManager.instance.day_night)
        {
            Debug.Log("밤");
            StopCoroutine("Swap_Day");
            StartCoroutine(Swap_Day(Object_Day.color, night));
        }
        else if (!GameManager.instance.day_night)
        {
            Debug.Log("낮");
            StopCoroutine("Swap_Day");
            StartCoroutine(Swap_Day(Object_Day.color, day));
        }

        inven_now = 1; // 인벤토리 아이템 생성 1 = 장비 2 = 소비 3 = 기타
        quest_now = 1;
        shop_now = 1;
        // 아이템 퀵슬롯 등록 확인
        for (int i = 0; i < GameManager.instance.save_item_quck_slot.Length; i++) 
        {
            for (int l = 0; l < GameManager.instance.inventory_use.Count; l++) 
            {
                if (GameManager.instance.inventory_use[l].item_name.Equals(GameManager.instance.save_item_quck_slot[i])) 
                {
                    GameManager.instance.item_qucik_slot[i] = GameManager.instance.inventory_use[l];
                    item_quick_slot[i].GetComponent<Quick_Item_Slot>().Set();
                }
            }
        }
        // 스킬 퀵슬롯 등록 확인
        for(int i = 0; i < GameManager.instance.quick_slot.Length; i++) 
        {
            for (int l = 0; l < GameManager.instance.allSkills.Length; l++)
            {
                if (GameManager.instance.allSkills[l].skill_name == GameManager.instance.save_quick_slot[i])
                {
                    GameManager.instance.quick_slot[i] = GameManager.instance.allSkills[l];
                    sk_qucik_slot[i].GetComponent<Image>().sprite = GameManager.instance.allSkills[l].skill_img;
                }
            }
        }

        // 특성 스킬 활성화
        if (GameManager.instance.sword_skill[1] >= 1)
        {
            plus_skill[0].SetActive(true);
        }
        else if (GameManager.instance.sword_skill[2] >= 1)
        {
            plus_skill[1].SetActive(true);
        }
        
        if (GameManager.instance.bow_skill[1] >= 1)
        {
            plus_skill[2].SetActive(true);
        }
        else if (GameManager.instance.bow_skill[2] >= 1)
        {
            plus_skill[3].SetActive(true);
        }

        if (GameManager.instance.magic_skill[1] >= 1)
        {
            plus_skill[4].SetActive(true);
        }
        else if (GameManager.instance.magic_skill[2] >= 1)
        {
            plus_skill[5].SetActive(true);
        }
    }
    void Update()
    {
        hp_bar.value = Mathf.Lerp(hp_bar.value,
            (float)GameManager.instance.hp / (float)GameManager.instance.maxhp, Time.deltaTime * 10);
        mp_bar.value = Mathf.Lerp(mp_bar.value,
            (float)GameManager.instance.mp / (float)GameManager.instance.maxmp, Time.deltaTime * 10);
        exp_bar.value = Mathf.Lerp(exp_bar.value,
            (float)GameManager.instance.exp / (float)GameManager.instance.maxexp, Time.deltaTime * 10);

        if (GameManager.instance.hp >= GameManager.instance.maxhp)
        {
            GameManager.instance.hp = GameManager.instance.maxhp;
        }
        if (GameManager.instance.mp >= GameManager.instance.maxmp)
        {
            GameManager.instance.mp = GameManager.instance.maxmp;
        }
        if (GameManager.instance.exp >= GameManager.instance.maxexp) 
        {
            GameManager.instance.exp -= GameManager.instance.maxexp;
            GameManager.instance.maxexp = (int)(GameManager.instance.maxexp * 1.35f);
            GameManager.instance.lv++;
            GameManager.instance.ability_point += 5;
        }

        // 스텟
        stats[0].text = "체력 <size=22>" + GameManager.instance.hp.ToString() + " / " + GameManager.instance.maxhp + "</size>";
        stats[1].text = "마나 <size=22>" + GameManager.instance.mp.ToString() + " / " + GameManager.instance.maxmp + "</size>";
        stats[2].text = "경험치 <size=22>" + GameManager.instance.exp.ToString() + " / " + GameManager.instance.maxexp + "</size>";
        stats[3].text = "방어력 " + GameManager.instance.def.ToString();
        stats[4].text = "공격력 " + GameManager.instance.atk.ToString();
        stats[5].text = "마력 " + GameManager.instance.magic.ToString();
        stats[6].text = "이동속도 " + Mathf.Round((GameManager.instance.speed / 5.6f) * 100) + "%";
        stats[7].text = "Lv. " + GameManager.instance.lv;
        stats[8].text = "공격속도 " + Mathf.RoundToInt(GameManager.instance.attack_speed  * 100) + "%";
        stats[9].text = "치명타 확률 " + Mathf.RoundToInt(GameManager.instance.critical * 100) + "%";
        stats[10].text = "스텟 포인트 " + GameManager.instance.ability_point;

        if (GameManager.instance.gold >= 1)
        {
            gold.text = CommaText(GameManager.instance.gold);
            shop_gold.text = CommaText(GameManager.instance.gold);
        }
        else if(GameManager.instance.gold == 0)
        {
            gold.text = "0";
            shop_gold.text = "0";
        }
        skill_point.text = GameManager.instance.skill_point.ToString();

        // 낮, 밤
        GameManager.instance.day_time += Time.deltaTime;
        if (GameManager.instance.day_time >= Day) 
        {
            GameManager.instance.day_time = 0;
        }

        if (Swaping == false)
        {
            if ((Mathf.FloorToInt(Day * 0.4f) == Mathf.FloorToInt(GameManager.instance.day_time)) && GameManager.instance.day_night == false)
            {
                // 낮 => 밤
                GameManager.instance.day_night = true;
                Swaping= true;
                StopCoroutine("Swap_Day");
                StartCoroutine(Swap_Day(Object_Day.color, night));
            }
            else if ((Mathf.FloorToInt(Day * 0.9f) == Mathf.FloorToInt(GameManager.instance.day_time)) && GameManager.instance.day_night == true)
            {
                // 밤 => 낮
                GameManager.instance.day_night = false;
                Swaping = false;
                StopCoroutine("Swap_Day");
                StartCoroutine(Swap_Day(Object_Day.color, day));
            }
        }

        // 일시정지 ( 인벤토리 / 스킬 등 )
        if (Input.GetKeyDown(KeySet.keys[Key_Action.Pause]) && !npc_Dial[0].activeSelf)
        {
            if (pause == false)
            {
                pause = true;
                pause_Object.SetActive(true);
                Time.timeScale = 0f;
                // 인벤토리
                if (inven_now == 1)
                {
                    // 장비창 생성
                    Inven_Equip_Btn();
                    Equip_Item_Create_List_Add(); // 임시용
                }
                else if (inven_now == 2)
                {
                    // 소비창 생성
                    Item_Use_Create();
                }
                else if (inven_now == 3)
                {
                    // 기타창 생성
                    Item_Etc_Create();
                }
                // 퀘스트
                if (quest_now == 1)
                {
                    Quest_List_Btn();
                }
                else if (quest_now == 2)
                {
                    Quest_Now_Btn();
                }
                else if (quest_now == 3) 
                {
                    Quest_Clear_Btn();
                }
            }
            else
            {
                pause = false;
                pause_Object.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
        // NPC 대화창 닫기
        else if(Input.GetKeyDown(KeySet.keys[Key_Action.Pause]) && npc_Dial[0].activeSelf)
        {
            if (npc_Dial[1].activeSelf)
            {
                npc_Dial[1].SetActive(false);
            }
            else if (npc_Dial[2].activeSelf)
            {
                npc_Dial[2].SetActive(false);
            }
            else
            {
                Npc_Close();
            }
        }
    }

    IEnumerator Swap_Day(Color start, Color end) 
    {
        float a = 0;
        while (a < 1) 
        {
            a += Time.deltaTime * ((1 / (trans_Time * Day)));
            Object_Day.color = Color.Lerp(start, end, a);
            yield return null;
        }
        Swaping = false;
    }

    // UI 버튼 기능들
    public void InvenTory_Click()
    {
        UI_btns[0].GetComponent<Image>().sprite = btn_imgs[4];
        UI_btns[1].GetComponent<Image>().sprite = btn_imgs[1];
        UI_btns[2].GetComponent<Image>().sprite = btn_imgs[2];
        UI_btns[3].GetComponent<Image>().sprite = btn_imgs[3];

        p_object_list[0].SetActive(true);
        p_object_list[1].SetActive(false);
        p_object_list[2].SetActive(false);
        p_object_list[3].SetActive(false);
    }
    public void Skill_Click()
    {
        UI_btns[0].GetComponent<Image>().sprite = btn_imgs[0];
        UI_btns[1].GetComponent<Image>().sprite = btn_imgs[5];
        UI_btns[2].GetComponent<Image>().sprite = btn_imgs[2];
        UI_btns[3].GetComponent<Image>().sprite = btn_imgs[3];
        
        p_object_list[0].SetActive(false);
        p_object_list[1].SetActive(true);
        p_object_list[2].SetActive(false);
        p_object_list[3].SetActive(false);
    }
    public void Create_Click()
    {
        UI_btns[0].GetComponent<Image>().sprite = btn_imgs[0];
        UI_btns[1].GetComponent<Image>().sprite = btn_imgs[1];
        UI_btns[2].GetComponent<Image>().sprite = btn_imgs[6];
        UI_btns[3].GetComponent<Image>().sprite = btn_imgs[3];
        p_object_list[0].SetActive(false);
        p_object_list[1].SetActive(false);
        p_object_list[2].SetActive(true);
        p_object_list[3].SetActive(false);
    }
    public void Quest_Click()
    {
        UI_btns[0].GetComponent<Image>().sprite = btn_imgs[0];
        UI_btns[1].GetComponent<Image>().sprite = btn_imgs[1];
        UI_btns[2].GetComponent<Image>().sprite = btn_imgs[2];
        UI_btns[3].GetComponent<Image>().sprite = btn_imgs[7];
        p_object_list[0].SetActive(false);
        p_object_list[1].SetActive(false);
        p_object_list[2].SetActive(false);
        p_object_list[3].SetActive(true);
    }

    // 인벤 상세 버튼
    public void Inven_Equip_Btn()
    {
        Item_Equipment_Create();
        UI_btns[4].GetComponent<Image>().sprite = btn_imgs[15];
        UI_btns[5].GetComponent<Image>().sprite = btn_imgs[14];
        UI_btns[6].GetComponent<Image>().sprite = btn_imgs[14];
        inven_now = 1;
    }
    public void Inven_Use_Btn()
    {
        Item_Use_Create();
        UI_btns[4].GetComponent<Image>().sprite = btn_imgs[14];
        UI_btns[5].GetComponent<Image>().sprite = btn_imgs[15];
        UI_btns[6].GetComponent<Image>().sprite = btn_imgs[14];
        inven_now = 2;
    }
    public void Inven_Etc_Btn()
    {
        Item_Etc_Create();
        UI_btns[4].GetComponent<Image>().sprite = btn_imgs[14];
        UI_btns[5].GetComponent<Image>().sprite = btn_imgs[14];
        UI_btns[6].GetComponent<Image>().sprite = btn_imgs[15];
        inven_now = 3;
    }

    // 스킬 상세 버튼
    public void Skill_Type_Click()
    {
        skill_UI_Object[0].SetActive(true);
        skill_UI_Object[1].SetActive(false);
        skill_UI_Object[2].SetActive(false);

        skill_UI_btn[0].GetComponent<Image>().sprite = btn_imgs[8];
        skill_UI_btn[1].GetComponent<Image>().sprite = btn_imgs[12];
        skill_UI_btn[2].GetComponent<Image>().sprite = btn_imgs[13];

    }
    public void Skill_Type_Click_2()
    {
        skill_UI_Object[0].SetActive(false);
        skill_UI_Object[1].SetActive(true);
        skill_UI_Object[2].SetActive(false);

        skill_UI_btn[0].GetComponent<Image>().sprite = btn_imgs[11];
        skill_UI_btn[1].GetComponent<Image>().sprite = btn_imgs[9];
        skill_UI_btn[2].GetComponent<Image>().sprite = btn_imgs[13];
    }
    public void Skill_Type_Click_3()
    {
        skill_UI_Object[0].SetActive(false);
        skill_UI_Object[1].SetActive(false);
        skill_UI_Object[2].SetActive(true);

        skill_UI_btn[0].GetComponent<Image>().sprite = btn_imgs[11];
        skill_UI_btn[1].GetComponent<Image>().sprite = btn_imgs[12];
        skill_UI_btn[2].GetComponent<Image>().sprite = btn_imgs[10];
    }

    // 제작/분해 상세버튼
    public void Create_Equip_Btn() 
    {
        Item_Equipment_Create();
        UI_btns[7].GetComponent<Image>().sprite = btn_imgs[15];
        UI_btns[8].GetComponent<Image>().sprite = btn_imgs[14];
        UI_btns[9].GetComponent<Image>().sprite = btn_imgs[14];
    }

    // 퀘스트 상세버튼
    public void Quest_List_Btn()
    {
        Quest_List_Create();
        quest_now = 1;
        UI_btns[11].GetComponent<Image>().sprite = btn_imgs[15];
        UI_btns[12].GetComponent<Image>().sprite = btn_imgs[14];
        UI_btns[13].GetComponent<Image>().sprite = btn_imgs[14];
    }
    public void Quest_Now_Btn()
    {
        Quest_List_Create_Now();
        quest_now = 2;
        UI_btns[11].GetComponent<Image>().sprite = btn_imgs[14];
        UI_btns[12].GetComponent<Image>().sprite = btn_imgs[15];
        UI_btns[13].GetComponent<Image>().sprite = btn_imgs[14];
    }
    public void Quest_Clear_Btn()
    {
        Quest_List_Create_Clear();
        quest_now = 3;
        UI_btns[11].GetComponent<Image>().sprite = btn_imgs[14];
        UI_btns[12].GetComponent<Image>().sprite = btn_imgs[14];
        UI_btns[13].GetComponent<Image>().sprite = btn_imgs[15];
    }


    // 인벤토리 리스트 생성
    public void Item_Equipment_Create()
    {
        Item_List_Clear();
        if (GameManager.instance.inventory_equip != null && GameManager.instance.inventory_equip.Count >= 1)
        {
            for (int i = 0; i < GameManager.instance.inventory_equip.Count; i++)
            {
                GameObject a = Instantiate(item_slot, item_slot_place.transform);
                a.GetComponent<Inventory_Item>().item_type = 0;
                a.GetComponent<Inventory_Item>().inven_num = i;
                a.GetComponent<Inventory_Item>().eq_item = GameManager.instance.inventory_equip[i];

                item_list.Add(a);
            }
        }
    }
    public void Item_Use_Create()
    {
        Item_List_Clear();
        if (GameManager.instance.inventory_use != null && GameManager.instance.inventory_use.Count >= 1)
        {
            for (int i = 0; i < GameManager.instance.inventory_use.Count; i++)
            {
                GameObject a = Instantiate(item_slot, item_slot_place.transform);
                a.GetComponent<Inventory_Item>().item_type = 1;
                a.GetComponent<Inventory_Item>().inven_num = i;
                a.GetComponent<Inventory_Item>().use_item = GameManager.instance.inventory_use[i];
                a.GetComponent<Inventory_Item>().item_cnt.text = GameManager.instance.inventory_use[i].reserves.ToString();
                a.GetComponent<Inventory_Item>().item_cnt.gameObject.SetActive(true);

                item_list.Add(a);
            }
        }
    }
    public void Item_Etc_Create()
    {
        Item_List_Clear();
        if (GameManager.instance.inventory_etc != null && GameManager.instance.inventory_etc.Count >= 1)
        {
            for (int i = 0; i < GameManager.instance.inventory_etc.Count; i++)
            {
                GameObject a = Instantiate(item_slot, item_slot_place.transform);
                a.GetComponent<Inventory_Item>().item_type = 2;
                a.GetComponent<Inventory_Item>().inven_num = i;
                a.GetComponent<Inventory_Item>().etc_item = GameManager.instance.inventory_etc[i];
                a.GetComponent<Inventory_Item>().item_cnt.text = GameManager.instance.inventory_etc[i].reserves.ToString();
                a.GetComponent<Inventory_Item>().item_cnt.gameObject.SetActive(true);

                item_list.Add(a);
            }
        }
    }
    public void Item_List_Clear() 
    {
        if (item_list != null && item_list.Count >= 1)
        {
            for (int i = 0; i < item_list.Count; i++)
            {
                Destroy(item_list[i]);
            }
            item_list.Clear();
        }
    }
    
    // 장비 제작 / 분해 리스트
    public void Equip_Item_Create_List_Add() 
    {
        Create_List_Clear();
        for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++)
        {
            GameObject a = Instantiate(create_slot, create_slot_place.transform);
            if (!GameManager.instance.allEquipItems[i].cant_Create)
            {
                a.GetComponent<Create_Item>().equip_origin_item = GameManager.instance.allEquipItems[i];

                create_list.Add(a);
            }
        }
    }
    public void Create_List_Clear() 
    {
        if (create_list != null && create_list.Count >= 1) 
        {
            for (int i = 0; i < create_list.Count; i++) 
            {
                Destroy(create_list[i]);
            }
            create_list.Clear();
        }
    }
    public string CommaText(int gold)
    {
        return string.Format("{0:#,###}", gold);
    }

    // 퀘스트 관련
    public void Quest_List_Create() 
    {
        Quest_List_Clear();
        for (int i = 0; i < GameManager.instance.allQuest.Length; i++)
        {
            bool questExists = false; // 중복 확인을 위한 플래그

            for (int l = 0; l < GameManager.instance.quest_list.Count; l++)
            {
                // quest_value와 quest_num이 같다면 이미 존재하는 퀘스트로 간주
                if (GameManager.instance.allQuest[i].quest_value == GameManager.instance.quest_list[l].quest_value &&
                    GameManager.instance.allQuest[i].quest_num == GameManager.instance.quest_list[l].quest_num)
                {
                    questExists = true;
                    break; // 중복된 퀘스트가 있으면 내부 루프를 빠져나감
                }
            }

            // 중복된 퀘스트가 없을 경우에만 새로운 퀘스트 생성
            if (!questExists)
            {
                GameObject a = Instantiate(quest, quest_list_place.transform);
                Quest_List_Btn b = a.GetComponent<Quest_List_Btn>();
                b.quest_type = 0;
                b.quest = GameManager.instance.allQuest[i];
                b.quest_name.text = GameManager.instance.allQuest[i].quest_name;

                quest_list.Add(a);
            }
        }

    }
    public void Quest_List_Create_Now() 
    {
        Quest_List_Clear();
        for (int i = 0; i < GameManager.instance.allQuest.Length; i++)
        {
            bool questExists = false;
            int c = 0;
            for (int l = 0; l < GameManager.instance.quest_list.Count; l++)
            {
                if (GameManager.instance.allQuest[i].quest_value == GameManager.instance.quest_list[l].quest_value &&
                    GameManager.instance.allQuest[i].quest_num == GameManager.instance.quest_list[l].quest_num && GameManager.instance.quest_list[l].now)
                {
                    questExists = true;
                    c = l;
                    break;
                }
            }

            // 수행중인 퀘스트 목록 활성화
            if (questExists)
            {
                GameObject a = Instantiate(quest, quest_list_place.transform);
                Quest_List_Btn b = a.GetComponent<Quest_List_Btn>();
                b.num = c;
                b.quest_type = 1;
                b.user_quest = GameManager.instance.quest_list[c];
                b.quest_name.text = b.user_quest.quest_name;

                quest_list.Add(a);
            }
        }
    }
    public void Quest_List_Create_Clear()
    {
        Quest_List_Clear();
        for (int i = 0; i < GameManager.instance.allQuest.Length; i++)
        {
            bool questExists = false;
            int c = 0;
            for (int l = 0; l < GameManager.instance.quest_list.Count; l++)
            {
                if (GameManager.instance.allQuest[i].quest_value == GameManager.instance.quest_list[l].quest_value &&
                    GameManager.instance.allQuest[i].quest_num == GameManager.instance.quest_list[l].quest_num && GameManager.instance.quest_list[l].cleard)
                {
                    questExists = true;
                    c = l;
                    break;
                }
            }

            // 수행중인 퀘스트 목록 활성화
            if (questExists)
            {
                GameObject a = Instantiate(quest, quest_list_place.transform);
                Quest_List_Btn b = a.GetComponent<Quest_List_Btn>();
                b.quest_type = 2;
                b.user_quest = GameManager.instance.quest_list[c];
                b.quest_name.text = b.user_quest.quest_name;

                quest_list.Add(a);
            }
        }
    }
    public void Quest_List_Clear() 
    {
        if (quest_list != null && quest_list.Count >= 1) 
        {
            for (int i = 0; i < quest_list.Count; i++) 
            {
                Destroy(quest_list[i]);
            }
            quest_list.Clear();
        }
        quest_infos[0].text = "";
        quest_infos[1].text = "";

        if (quest_reward_list.Count >= 1)
        {
            for (int i = 0; i < quest_reward_list.Count; i++)
            {
                Destroy(quest_reward_list[i]);
            }
            quest_reward_list.Clear();
        }
    }

    // 퀘스트 수락
    public void Quest_Accept() 
    {
        User_Quest_List uq =new User_Quest_List();
        Quest q = target_npc.quest_list[npc_quest_select]; // 임시용 ( NPC 스크립트에 Quest 스크립터블 오브젝트를 받아오자)
        
        uq.quest_value = q.quest_value;
        uq.quest_type = q.quest_type;
        uq.quest_num = q.quest_num;
        uq.quest_name = q.quest_name;
        uq.quest_explane = q.quest_explane;
        uq.object_npc = q.object_npc;
        uq.object_item_name = q.object_item_name;
        uq.object_item_cnt = q.object_item_cnt;
        uq.p_object_item_cnt = new int[q.object_item_cnt.Length];
        uq.object_mob = q.object_mob;
        uq.object_mob_cnt = q.object_mob_cnt;
        uq.p_object_mob_cnt = new int[q.object_mob_cnt.Length];
        uq.reward_exp = q.reward_exp;
        uq.reward_item_name = q.reward_item_name;
        uq.reward_item_cnt = q.reward_item_cnt;
        uq.now = true;
        GameManager.instance.quest_list.Add(uq);
        Npc_Quest_Open();
        Npc_Quest_Reward_Clear();
    }
    // 퀘스트 클리어 기능
    public void Quest_Clear()
    {
        User_Quest_List uq = GameManager.instance.quest_list[quest_select];
        int alert = 0;
        int cnt = 0;
        
        GameManager.instance.exp += uq.reward_exp;
        uq.now = false;
        uq.cleard = true;

        // 퀘스트 보상 확인
        for (int i = 0; i < uq.reward_item_name.Length; i++)
        {
            Equip_Item q_eq_item = null;
            Use_Item q_use_item = null;
            Etc_Item q_etc_item = null;
            alert = 0;

            // 장비 아이템 확인
            for (int l = 0; l < GameManager.instance.allEquipItems.Length; l++)
            {
                if (uq.reward_item_name[i].Equals(GameManager.instance.allEquipItems[l].item_name))
                {
                    q_eq_item = GameManager.instance.allEquipItems[l];
                    alert = 1;
                    break;
                }
            }

            // 소비 아이템 확인
            for (int l = 0; l < GameManager.instance.allUseItems.Length; l++)
            {
                if (uq.reward_item_name[i].Equals(GameManager.instance.allUseItems[l].item_name))
                {
                    q_use_item = GameManager.instance.allUseItems[l];
                    cnt = uq.reward_item_cnt[i];
                    alert = 2;
                    break;
                }
            }

            // 기타 아이템 확인
            for (int l = 0; l < GameManager.instance.allEtcitems.Length; l++)
            {
                if (uq.reward_item_name[i].Equals(GameManager.instance.allEtcitems[l].item_name))
                {
                    q_etc_item = GameManager.instance.allEtcitems[l];
                    cnt = uq.reward_item_cnt[i];
                    alert = 3;
                    break;
                }
            }

            if (alert == 1)
            {
                InvenTory_Equipment inven_eq = new InvenTory_Equipment();
                float[] plus_option = new float[q_eq_item.item_option.Length];

                if (q_eq_item.item_option != null && q_eq_item.item_option.Length > 0)
                {
                    for (int r = 0; r < q_eq_item.item_option.Length; r++)
                    {
                        plus_option[r] = Mathf.RoundToInt(Random.Range(q_eq_item.item_option_min[r], q_eq_item.item_option_max[r]));
                        if (q_eq_item.attack_type == 1 && r == 1)
                        {
                            plus_option[r] = Mathf.Round(Random.Range(q_eq_item.item_option_min[r], q_eq_item.item_option_max[r]) * 100f) / 100;
                        }
                        if (r == 2)
                        {
                            plus_option[r] = Mathf.Round(Random.Range(q_eq_item.item_option_min[r], q_eq_item.item_option_max[r]) * 100f) / 100;
                        }
                    }
                }

                inven_eq.item_id = q_eq_item.item_id;
                inven_eq.item_name = q_eq_item.item_name;
                inven_eq.item_exp = q_eq_item.item_exp;
                inven_eq.item_lv = q_eq_item.item_lv;
                inven_eq.type = q_eq_item.type;
                inven_eq.attack_type = q_eq_item.attack_type;
                inven_eq.base_option = q_eq_item.base_option;
                inven_eq.enforce = q_eq_item.enforce;
                inven_eq.sell_price = q_eq_item.sell_price;
                inven_eq.rare = q_eq_item.rare;
                inven_eq.item_option = plus_option;

                GameManager.instance.inventory_equip.Add(inven_eq);
                Item_Equipment_Create();
            }
            else if (alert == 2)
            {
                InvenTory_Use inven_use = new InvenTory_Use(); // 반복문 안에서 새 인스턴스 생성
                inven_use.item_id = q_use_item.item_id;
                inven_use.item_type = q_use_item.item_type;
                inven_use.item_name = q_use_item.item_name;
                inven_use.item_exp = q_use_item.item_exp;
                inven_use.sell_price = q_use_item.sell_price;
                inven_use.rare = q_use_item.rare;
                inven_use.hp_recover = q_use_item.hp_recover;
                inven_use.mp_recover = q_use_item.mp_recover;
                inven_use.map_name = q_use_item.map_name;
                inven_use.cant_Create = q_use_item.cant_Create;
                inven_use.create_item = q_use_item.create_item;
                inven_use.material_count = q_use_item.material_count;
                inven_use.reserves = cnt;

                bool found = false;
                for (int x = 0; x < GameManager.instance.inventory_use.Count; x++)
                {
                    if (GameManager.instance.inventory_use[x].item_id == inven_use.item_id)
                    {
                        GameManager.instance.inventory_use[x].reserves += cnt;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    GameManager.instance.inventory_use.Add(inven_use);
                }
            }
            else if (alert == 3)
            {
                InvenTory_Etc inven_etc = new InvenTory_Etc(); // 반복문 안에서 새 인스턴스 생성
                inven_etc.item_id = q_etc_item.item_id;
                inven_etc.item_name = q_etc_item.item_name;
                inven_etc.item_exp = q_etc_item.item_exp;
                inven_etc.sell_price = q_etc_item.sell_price;
                inven_etc.rare = q_etc_item.rare;
                inven_etc.reserves = cnt;

                bool found = false;
                for (int x = 0; x < GameManager.instance.inventory_etc.Count; x++)
                {
                    if (GameManager.instance.inventory_etc[x].item_id == inven_etc.item_id)
                    {
                        GameManager.instance.inventory_etc[x].reserves += cnt;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    GameManager.instance.inventory_etc.Add(inven_etc);
                }
            }
        }
        for (int l = 0; l < GameManager.instance.inventory_etc.Count; l++)
        {
            for(int x = 0; x < uq.object_item_name.Length; x++)
            if (GameManager.instance.inventory_etc[l].item_name.Equals(uq.object_item_name[x]))
            {
                GameManager.instance.inventory_etc[l].reserves -= uq.object_item_cnt[x];

                if (GameManager.instance.inventory_etc[l].reserves <= 0) 
                {
                    GameManager.instance.inventory_etc.RemoveAt(l);
                }
            }
        }

        quest_Clear.interactable = false;
        Item_Equipment_Create();
        Item_Use_Create();
        Item_Etc_Create();
        Quest_List_Create_Now();
    }

    // 퀘스트 포기
    public void Quest_Giveup() 
    {
        GameManager.instance.quest_list.RemoveAt(quest_select);
        Quest_List_Create_Now();
    }

    // NPC 상호작용 ( 퀘스트 )
    public void Npc_Dialogue(Npc_Script npc) 
    {
        pause = true;
        npc_Dial[0].SetActive(true);
        Time.timeScale = 0f;

        dial_texts[0].text = npc.npc_name;
        int a= Random.Range(0, npc.dials.Length);
        dial_texts[1].text = npc.dials[a];
        if (npc.quest) 
        {
            dial_object[0].GetComponent<Button>().interactable = true;
        }
        if (npc.shop) 
        {
            dial_object[1].GetComponent<Button>().interactable = true;
        }
        target_npc = npc;
    }

    public void Npc_Quest_Open() 
    {
        Npc_Quest_List_Clear();
        for (int i = 0; i < target_npc.quest_list.Length; i++)
        {
            GameObject a = Instantiate(npc_quest, npc_quest_place.transform);
            Npc_Quest_Btn b = a.GetComponent<Npc_Quest_Btn>();
            b.q_name.text = target_npc.quest_list[i].quest_name;
            b.quest = target_npc.quest_list[i];
            b.q_num = i;
            npc_q_list.Add(a);
        }

        for (int i = 0; i < GameManager.instance.quest_list.Count; i++) 
        {
            for (int l = 0; l < npc_q_list.Count; l++)
            {
                if (npc_q_list[l].GetComponent<Npc_Quest_Btn>().q_num == GameManager.instance.quest_list[i].quest_num)
                {
                    Destroy(npc_q_list[l]);
                    npc_q_list.RemoveAt(l);
                }
            }
        }
        npc_Dial[1].SetActive(true);
    }

    public void Npc_Quest_List_Clear() 
    {
        if (npc_q_list != null && npc_q_list.Count >= 1) 
        {
            for (int i = 0; i < npc_q_list.Count; i++) 
            {
                Destroy(npc_q_list[i]);
            }
            npc_q_list.Clear();
            dial_object[2].GetComponent<Button>().interactable = false;
            dial_texts[2].text = "";
            dial_texts[3].text = "";
        }
    }

    public void Npc_Quest_Reward_Clear() 
    {
        if (npc_reward_list.Count >= 1) 
        {
            for (int i = 0; i < npc_reward_list.Count; i++) 
            {
                Destroy(npc_reward_list[i]);
            }
            npc_reward_list.Clear();
        }
    }

    // 상점
    public void Npc_Trade_Open() 
    {
        Npc_Trade_Item_List_Clear();
        Npc_Trade_Item_Create();
        npc_Dial[2].SetActive(true);
        if (shop_now == 1)
        {
            Shop_Btn_Equip();
        }
        else if (shop_now == 2)
        {
            Shop_Btn_Use();
        }
        else if (shop_now == 3)
        {
            Shop_Btn_Etc();
        }
    }
    public void Npc_Trade_Item_Create()
    {
        for (int i = 0; i < target_npc.equip_items.Length; i++)
        {
            GameObject a = Instantiate(shop_item, shop_places[0].transform);
            Shop_Npc_Item b = a.GetComponent<Shop_Npc_Item>();
            b.equip_item = target_npc.equip_items[i];
            b.item_img.sprite = target_npc.equip_items[i].item_img[0];
            shop_npc_list.Add(a);
        }
        for (int i = 0; i < target_npc.use_items.Length; i++)
        {
            GameObject a = Instantiate(shop_item, shop_places[0].transform);
            Shop_Npc_Item b = a.GetComponent<Shop_Npc_Item>();
            b.use_item = target_npc.use_items[i];
            b.item_img.sprite = target_npc.use_items[i].item_img;
            b.item_infos[0].text = target_npc.use_items[i].item_name;
            b.item_infos[1].text = target_npc.use_items[i].buy_price.ToString() + "G";
            shop_npc_list.Add(a);
        }
        for (int i = 0; i < target_npc.etc_items.Length; i++)
        {
            GameObject a = Instantiate(shop_item, shop_places[0].transform);
            Shop_Npc_Item b = a.GetComponent<Shop_Npc_Item>();
            b.etc_item = target_npc.etc_items[i];
            b.item_img.sprite = target_npc.etc_items[i].item_img;
            shop_npc_list.Add(a);
        }
    }
    public void Npc_Trade_Item_List_Clear() 
    {
        if (shop_npc_list != null && shop_npc_list.Count >= 1)
        {
            for (int i = 0; i < shop_npc_list.Count; i++)
            {
                Destroy(shop_npc_list[i]);
            }
            shop_npc_list.Clear();
        }
        if (shop_nsell_list != null && shop_nsell_list.Count >= 1)
        {
            for (int i = 0; i < shop_nsell_list.Count; i++)
            {
                Destroy(shop_nsell_list[i]);
            }
            dial_object[3].GetComponent<Shop_UI>().shop_btn[0].interactable = false;
            dial_object[3].GetComponent<Shop_UI>().price.text = "";
            shop_nsell_list.Clear();
        }

        if (shop_player_list != null && shop_player_list.Count >= 1)
        {
            for (int i = 0; i < shop_player_list.Count; i++)
            {
                Destroy(shop_player_list[i]);
            }
            shop_player_list.Clear();
        }
        if (shop_psell_list != null && shop_psell_list.Count >= 1)
        {
            for (int i = 0; i < shop_psell_list.Count; i++)
            {
                Destroy(shop_psell_list[i]);
            }
            shop_psell_list.Clear();
        }
    }

    public void Shop_Btn_Equip()
    {
        shop_now = 1;
        if (shop_player_list != null && shop_player_list.Count >= 1)
        {
            for (int i = 0; i < shop_player_list.Count; i++)
            {
                Destroy(shop_player_list[i]);
            }
            shop_player_list.Clear();
        }
        Npc_Trade_User_Item_Equip();
        npc_Dial[3].GetComponent<Image>().sprite = btn_imgs[16];
        npc_Dial[4].GetComponent<Image>().sprite = btn_imgs[17];
        npc_Dial[5].GetComponent<Image>().sprite = btn_imgs[17];
    }
    public void Shop_Btn_Use()
    {
        shop_now = 2;
        if (shop_player_list != null && shop_player_list.Count >= 1)
        {
            for (int i = 0; i < shop_player_list.Count; i++)
            {
                Destroy(shop_player_list[i]);
            }
            shop_player_list.Clear();
        }
        Npc_Trade_User_Item_Use();
        npc_Dial[3].GetComponent<Image>().sprite = btn_imgs[17];
        npc_Dial[4].GetComponent<Image>().sprite = btn_imgs[16];
        npc_Dial[5].GetComponent<Image>().sprite = btn_imgs[17];
    }
    public void Shop_Btn_Etc()
    {
        shop_now = 3;
        if (shop_player_list != null && shop_player_list.Count >= 1)
        {
            for (int i = 0; i < shop_player_list.Count; i++)
            {
                Destroy(shop_player_list[i]);
            }
            shop_player_list.Clear();
        }
        Npc_Trade_User_Item_Etc();
        npc_Dial[3].GetComponent<Image>().sprite = btn_imgs[17];
        npc_Dial[4].GetComponent<Image>().sprite = btn_imgs[17];
        npc_Dial[5].GetComponent<Image>().sprite = btn_imgs[16];
    }

    public void Npc_Trade_User_Item_Equip() 
    {
        for(int i = 0; i < GameManager.instance.inventory_equip.Count; i++) 
        {
            GameObject a = Instantiate(shop_item, shop_places[2].transform);
            Shop_Npc_Item b = a.GetComponent<Shop_Npc_Item>();
            b.p_equip_item = GameManager.instance.inventory_equip[i] ;
            for(int l = 0; l < GameManager.instance.allEquipItems.Length; l++)
            {
                if (GameManager.instance.allEquipItems[l].item_id == GameManager.instance.inventory_equip[i].item_id)
                {
                    b.item_img.sprite = GameManager.instance.allEquipItems[l].item_img[0];
                    break;
                }
            }
            b.item_infos[0].text = GameManager.instance.inventory_equip[i].item_name;
            b.item_infos[1].text = GameManager.instance.inventory_equip[i].sell_price.ToString() + "G";
            b.p_item = true;
            b.p_num_equip = i + 1;
            shop_player_list.Add(a);
        }
    }
    public void Npc_Trade_User_Item_Use()
    {
        for (int i = 0; i < GameManager.instance.inventory_use.Count; i++)
        {
            GameObject a = Instantiate(shop_item, shop_places[2].transform);
            Shop_Npc_Item b = a.GetComponent<Shop_Npc_Item>();
            b.p_use_item = GameManager.instance.inventory_use[i];
            for (int l = 0; l < GameManager.instance.allUseItems.Length; l++)
            {
                if (GameManager.instance.allUseItems[l].item_id == GameManager.instance.inventory_use[i].item_id)
                {
                    b.item_img.sprite = GameManager.instance.allUseItems[l].item_img;
                    break;
                }
            }
            b.item_infos[0].text = GameManager.instance.inventory_use[i].item_name;
            b.item_infos[1].text = GameManager.instance.inventory_use[i].sell_price.ToString() + "G";
            b.p_num_use = i + 1;
            b.p_item = true;
            b.item_infos[2].gameObject.SetActive(true);
            b.item_infos[2].text = GameManager.instance.inventory_use[i].reserves.ToString();
            shop_player_list.Add(a);
        }
    }
    public void Npc_Trade_User_Item_Etc()
    {
        for (int i = 0; i < GameManager.instance.inventory_etc.Count; i++)
        {
            GameObject a = Instantiate(shop_item, shop_places[2].transform);
            Shop_Npc_Item b = a.GetComponent<Shop_Npc_Item>();
            b.p_etc_item = GameManager.instance.inventory_etc[i];
            for (int l = 0; l < GameManager.instance.allEtcitems.Length; l++)
            {
                if (GameManager.instance.allEtcitems[l].item_id == GameManager.instance.inventory_etc[i].item_id)
                {
                    b.item_img.sprite = GameManager.instance.allEtcitems[l].item_img;
                    break;
                }
            }
            b.item_infos[0].text = GameManager.instance.inventory_etc[i].item_name;
            b.item_infos[1].text = GameManager.instance.inventory_etc[i].sell_price.ToString() + "G";
            b.p_item = true;
            b.p_num_etc = i + 1;
            b.item_infos[2].gameObject.SetActive(true);
            b.item_infos[2].text = GameManager.instance.inventory_etc[i].reserves.ToString();
            shop_player_list.Add(a);
        }
    }
    public void Npc_Close() 
    {
        pause = false;
        Time.timeScale = 1.0f;
        npc_Dial[0].SetActive(false);
        npc_Dial[1].SetActive(false);
        target_npc = null;
    }

    // 스텟 증가
    public void Stat_Up(int i) 
    {
        if (GameManager.instance.ability_point >= 1) 
        {
            switch (i)
            {
                case 0:
                    GameManager.instance.atk++;
                    break;
                case 1:
                    GameManager.instance.magic++;
                    break;
                case 2:
                    GameManager.instance.def++;
                    break;
                case 3:
                    GameManager.instance.maxhp += 50;
                    break;
                case 4:
                    GameManager.instance.maxmp += 50;
                    break;
            }
            GameManager.instance.ability_pts[i]++;
            GameManager.instance.ability_point--;
        }
    }
    public void Stat_Down(int i) 
    {
        if (GameManager.instance.ability_pts[i] >= 1)
        {
            switch (i)
            {
                case 0:
                    GameManager.instance.atk--;
                    break;
                case 1:
                    GameManager.instance.magic--;
                    break;
                case 2:
                    GameManager.instance.def--;
                    break;
                case 3:
                    GameManager.instance.maxhp -= 50;
                    break;
                case 4:
                    GameManager.instance.maxmp -= 50;
                    break;
            }
            GameManager.instance.ability_pts[i]--;
            GameManager.instance.ability_point++;
        }
    }
}