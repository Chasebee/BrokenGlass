using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public enum Key_Action {Pause, Up, Down, Left, Right, Jump, Attack, skill_1, skill_2, skill_3, skill_4, item_1, item_2, count }
public static class KeySet { public static Dictionary<Key_Action, KeyCode> keys = new Dictionary<Key_Action, KeyCode>(); }

public class GameManager : MonoBehaviour
{
    public PlayerData playerdata;
    // 플레이어 키 설정
    public KeyCode[] default_key = new KeyCode[] {KeyCode.Tab, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Space, KeyCode.Z,
    KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.Alpha1, KeyCode.Alpha2};

    [Header("캐릭터 기본 스테이터스")]
    public int maxhp;
    public int lv, hp, maxmp, mp, maxexp, exp, atk, magic, def, max_jump, skill_point;
    public float speed, attack_speed, jump_power, knockback_power, critical;
    public int[] plus_stat; // 버프 목록 체력, 마나, 방어력, 공격력, 마력, 이동속도, 치명타 확률
    public bool equipment_change;
    public int ability_point;
    public int[] ability_pts;
    [Header("캐릭터 인벤토리")]
    public int gold;
    public List<InvenTory_Equipment> inventory_equip = new List<InvenTory_Equipment>();
    public List<InvenTory_Use> inventory_use = new List<InvenTory_Use>();
    public List<InvenTory_Etc> inventory_etc = new List<InvenTory_Etc>();

    public InvenTory_Use[] item_qucik_slot = new InvenTory_Use[2];
    public string[] save_item_quck_slot;
    public float[] item_cool_time = new float[2];

    [Header("캐릭터 장착 장비")]
    public InvenTory_Equipment head, body, bottom, weapon;

    [Header("캐릭터 스킬 관련")]
    public int[] sword_skill;
    public int[] bow_skill;
    public int[] magic_skill;
    public bool[] elemental;
    public Skill_List[] quick_slot;
    public string[] save_quick_slot;

    [Header ("캐릭터 퀘스트 관련")]
    public List<User_Quest_List> quest_list= new List<User_Quest_List>();

    [Header("캐릭터 오브젝트")]
    public GameObject[] attack_object;
    public GameObject[] magic_object;
    public Equip_Item[] allEquipItems;
    public Use_Item[] allUseItems;
    public Etc_Item[] allEtcitems;
    public Skill_List[] allSkills;
    public Quest[] allQuest;

    [Header("맵 관련")]
    public string save_map;
    public float[] save_p_pos;
    public bool day_night;
    public float day_time;
    public float[] day_color;
    public float[] chr_start;
    public bool portal_bool;
    // 싱글톤
    public static GameManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < (int)Key_Action.count; i++)
        {
            KeySet.keys.Add((Key_Action)i, default_key[i]);
        }

        // 프레임
        Application.targetFrameRate = 120;
    }

    public void Equip_Stat_Plus(int num)
    {
        if (num == 3) 
        {
            atk += Mathf.RoundToInt(body.base_option[0]);
            magic += Mathf.RoundToInt(body.base_option[1]);
            attack_speed += body.base_option[2];
            maxhp += Mathf.RoundToInt(body.base_option[3]);
            maxmp += Mathf.RoundToInt(body.base_option[4]);
            def += Mathf.RoundToInt(body.base_option[5]);
            speed += body.base_option[6];

            maxhp += Mathf.RoundToInt(body.item_option[0]);
            def += Mathf.RoundToInt(body.item_option[1]);
            maxmp += Mathf.RoundToInt(body.item_option[2]);
        }
        else if (num == 4) 
        {
            atk += Mathf.RoundToInt(bottom.base_option[0]);
            magic += Mathf.RoundToInt(bottom.base_option[1]);
            attack_speed += bottom.base_option[2];
            maxhp += Mathf.RoundToInt(bottom.base_option[3]);
            maxmp += Mathf.RoundToInt(bottom.base_option[4]);
            def += Mathf.RoundToInt(bottom.base_option[5]);
            speed += bottom.base_option[6];

            maxmp += Mathf.RoundToInt(bottom.item_option[0]);
            def += Mathf.RoundToInt(bottom.item_option[1]);
            speed += bottom.item_option[2];
        }
        else if (num == 1)
        {
            atk += Mathf.RoundToInt(weapon.base_option[0]);
            magic += Mathf.RoundToInt(weapon.base_option[1]);
            attack_speed += weapon.base_option[2];
            maxhp += Mathf.RoundToInt(weapon.base_option[3]);
            maxmp += Mathf.RoundToInt(weapon.base_option[4]);
            def += Mathf.RoundToInt(weapon.base_option[5]);
            speed += weapon.base_option[6];
            if (weapon.attack_type == 1)
            {
                atk += Mathf.RoundToInt(weapon.item_option[0]);
                attack_speed += weapon.item_option[1];
                critical += weapon.item_option[2];
            }
            else if (weapon.attack_type == 2)
            {
                atk += Mathf.RoundToInt(weapon.item_option[0]);
                magic += Mathf.RoundToInt(weapon.item_option[1]);
                critical += weapon.item_option[2];
            }
            else if (weapon.attack_type == 3)
            {
                magic += Mathf.RoundToInt(weapon.item_option[0]);
                maxmp += Mathf.RoundToInt(weapon.item_option[1]);
                critical += weapon.item_option[2];
            }
        }
    }
    public void Equip_Stat_Down(int num)
    {
        if (num == 3)
        {
            atk -= Mathf.RoundToInt(body.base_option[0]);
            magic -= Mathf.RoundToInt(body.base_option[1]);
            attack_speed -= body.base_option[2];
            maxhp -= Mathf.RoundToInt(body.base_option[3]);
            maxmp -= Mathf.RoundToInt(body.base_option[4]);
            def -= Mathf.RoundToInt(body.base_option[5]);
            speed -= body.base_option[6];

            maxhp -= Mathf.RoundToInt(body.item_option[0]);
            def -= Mathf.RoundToInt(body.item_option[1]);
            maxmp -= Mathf.RoundToInt(body.item_option[2]);
        }
        else if (num == 4) 
        {
            atk -= Mathf.RoundToInt(bottom.base_option[0]);
            magic -= Mathf.RoundToInt(bottom.base_option[1]);
            attack_speed -= bottom.base_option[2];
            maxhp -= Mathf.RoundToInt(bottom.base_option[3]);
            maxmp -= Mathf.RoundToInt(bottom.base_option[4]);
            def -= Mathf.RoundToInt(bottom.base_option[5]);
            speed -= bottom.base_option[6];

            maxmp -= Mathf.RoundToInt(bottom.item_option[0]);
            def -= Mathf.RoundToInt(bottom.item_option[1]);
            speed -= bottom.item_option[2];
        }
        else if (num == 1)
        {
            atk -= Mathf.RoundToInt(weapon.base_option[0]);
            magic -= Mathf.RoundToInt(weapon.base_option[1]);
            attack_speed -= weapon.base_option[2];
            maxhp -= Mathf.RoundToInt(weapon.base_option[3]);
            maxmp -= Mathf.RoundToInt(weapon.base_option[4]);
            def -= Mathf.RoundToInt(weapon.base_option[5]);
            speed -= weapon.base_option[6];
            if (weapon.attack_type == 1)
            {
                atk -= Mathf.RoundToInt(weapon.item_option[0]);
                attack_speed -= weapon.item_option[1];
                critical -= weapon.item_option[2];
            }
            else if (weapon.attack_type == 2)
            {
                atk -= Mathf.RoundToInt(weapon.item_option[0]);
                magic -= Mathf.RoundToInt(weapon.item_option[1]);
                critical -= weapon.item_option[2];
            }
            else if (weapon.attack_type == 3)
            {
                magic -= Mathf.RoundToInt(weapon.item_option[0]);
                maxmp -= Mathf.RoundToInt(weapon.item_option[1]);
                critical -= weapon.item_option[2];
            }
        }
    }
    
    public void EquipItem_Change(InvenTory_Equipment newItem, int num)
    {
        // 머리
        if (num == 2) 
        {
            if (head != null)
            {
                inventory_equip.Add(head);
                Equip_Stat_Down(num);
            }
            head = newItem;
            Equip_Stat_Plus(num);
            inventory_equip.Remove(newItem);
        }
        // 상의
        else if (num == 3) 
        {
            if (body != null)
            {
                inventory_equip.Add(body);
                Equip_Stat_Down(num);
            }
            body = newItem;
            Equip_Stat_Plus(num);
            inventory_equip.Remove(newItem);
        }
        // 하의
        else if (num == 4) 
        {
            if (bottom != null) 
            {
                inventory_equip.Add(bottom);
                Equip_Stat_Down(num);
            }
            bottom = newItem;
            Equip_Stat_Plus(num);
            inventory_equip.Remove(newItem);
        }
        // 무기
        else if (num == 1)
        {
            if (weapon != null)
            {
                inventory_equip.Add(weapon);
                Equip_Stat_Down(num);
            }

            weapon = newItem;
            Equip_Stat_Plus(num);

            inventory_equip.Remove(newItem);
        }
        equipment_change = true;
    }
    public void EquipItem_UnEquip(InvenTory_Equipment newItem)
    {
        inventory_equip.Add(newItem);
        Equip_Stat_Down(newItem.type);
        if (newItem.type == 2) 
        {
            head = null;
        }
        else if (newItem.type == 3)
        {
            body = null;
        }
        else if (newItem.type == 4)
        {
            bottom = null;
        }
        else if (newItem.type == 1)
        {
            weapon = null;
        }
        equipment_change = true;
    }

    [ContextMenu("Save")]
    public void Save_PlayerData_ToJson()
    {
        SaveData();
        string jsonData = JsonConvert.SerializeObject(playerdata, Formatting.Indented); // Newtonsoft.Json 직렬화
        string path = Path.Combine(Application.persistentDataPath, "SaveData/Save.json");
        Directory.CreateDirectory(Path.GetDirectoryName(path)); // SaveData 폴더가 없을 경우 생성
        File.WriteAllText(path, jsonData);
        Debug.Log("PlayerData가 저장되었습니다: " + path);
    }
    [ContextMenu("Load")]
    public void Load_PlayerData()
    {
        string path = Path.Combine(Application.persistentDataPath, "SaveData/Save.json");

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            playerdata = JsonConvert.DeserializeObject<PlayerData>(jsonData); // Newtonsoft.Json 역직렬화
            Load_Data();
            Debug.Log("PlayerData가 로드되었습니다: " + path);
        }
        else
        {
            Debug.LogWarning("세이브 파일을 찾을 수 없습니다: " + path);
        }
    }

    public void SaveData()
    {        
        playerdata.lv = lv;
        playerdata.maxhp = maxhp;
        playerdata.hp = hp;
        playerdata.maxmp = maxmp;
        playerdata.mp = mp;
        playerdata.maxexp = maxexp;
        playerdata.exp = exp;
        playerdata.atk = atk;
        playerdata.magic = magic;
        playerdata.def = def;
        playerdata.max_jump = max_jump;
        playerdata.skill_point = skill_point;
        playerdata.speed = speed;
        playerdata.attack_speed = attack_speed;
        playerdata.jump_power = jump_power;
        playerdata.knockback_power = knockback_power;
        playerdata.critical = critical;
        playerdata.plus_stat = plus_stat;
        playerdata.gold = gold;
        playerdata.ability_point = ability_point;
        playerdata.ability_pts = ability_pts;
        playerdata.inventory_equip = inventory_equip;
        playerdata.inventory_use = inventory_use;
        playerdata.inventory_etc = inventory_etc;
        playerdata.save_item_quck_slot = save_item_quck_slot;
        playerdata.head = head;
        playerdata.body = body;
        playerdata.bottom = bottom;
        playerdata.weapon = weapon;
        playerdata.sword_skill = sword_skill;
        playerdata.bow_skill = bow_skill;
        playerdata.magic_skill = magic_skill;
        playerdata.save_quick_slot = save_quick_slot;
        playerdata.quest_list = quest_list;
        playerdata.save_map = save_map;
        playerdata.save_p_pos = save_p_pos;
        playerdata.day_night = day_night;
        playerdata.day_time = day_time;
        playerdata.day_color = day_color;
    }
    public void Load_Data() 
    {
        lv = playerdata.lv;
        maxhp = playerdata.maxhp;
        hp = playerdata.hp;
        maxmp = playerdata.maxmp;
        mp = playerdata.mp;
        maxexp = playerdata.maxexp;
        exp = playerdata.exp;
        atk = playerdata.atk;
        magic = playerdata.magic;
        def = playerdata.def;
        max_jump = playerdata.max_jump;
        skill_point = playerdata.skill_point;
        speed = playerdata.speed;
        attack_speed = playerdata.attack_speed;
        jump_power = playerdata.jump_power;
        knockback_power = playerdata.knockback_power;
        critical = playerdata.critical;
        plus_stat = playerdata.plus_stat;
        gold = playerdata.gold;
        ability_point = playerdata.ability_point;
        ability_pts = playerdata.ability_pts;
        inventory_equip = playerdata.inventory_equip;
        inventory_use = playerdata.inventory_use;
        inventory_etc = playerdata.inventory_etc;
        save_item_quck_slot = playerdata.save_item_quck_slot;
        head = playerdata.head;
        body = playerdata.body;
        bottom = playerdata.bottom;
        weapon = playerdata.weapon;
        sword_skill = playerdata.sword_skill;
        bow_skill = playerdata.bow_skill;
        magic_skill = playerdata.magic_skill;
        save_quick_slot= playerdata.save_quick_slot;
        quest_list = playerdata.quest_list;
        save_map = playerdata.save_map;
        save_p_pos = playerdata.save_p_pos;
        day_night = playerdata.day_night;
        day_time = playerdata.day_time;
        day_color = playerdata.day_color;
    }

    public void OnApplicationQuit()
    {
        if (GameObject.FindWithTag("Player")) 
        {
            GameObject p = GameObject.FindWithTag("Player");
            save_p_pos[0] = p.transform.position.x;
            save_p_pos[1] = p.transform.position.y;
            Save_PlayerData_ToJson();
        }
    }
}
[System.Serializable]
public class PlayerData
{
    public int lv, maxhp, hp, maxmp, mp, maxexp, exp, atk, magic, def, max_jump, skill_point;
    public float speed, attack_speed, jump_power, knockback_power, critical;
    public int[] plus_stat; // 버프 목록 체력, 마나, 방어력, 공격력, 마력, 이동속도, 치명타 확률
    public int gold;
    public int ability_point;
    public int[] ability_pts;

    public List<InvenTory_Equipment> inventory_equip = new List<InvenTory_Equipment>();
    public List<InvenTory_Use> inventory_use = new List<InvenTory_Use>();
    public List<InvenTory_Etc> inventory_etc = new List<InvenTory_Etc>();
    public string[] save_item_quck_slot;

    public InvenTory_Equipment head, body, bottom, weapon;
    public int[] sword_skill;
    public int[] bow_skill;
    public int[] magic_skill;
    public string[] save_quick_slot;

    public List<User_Quest_List> quest_list = new List<User_Quest_List>();
    public string save_map;
    public float[] save_p_pos;
    public bool day_night;
    public float day_time;
    public float[] day_color;
} 