using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public GameObject cheat_Object;
    public Slider hpbar, expbar;
    public RectTransform[] Select;
    public GameObject LvUp_Object, item_decoy;
    public List<GameObject> item_decoy_list = new List<GameObject>();
    public List<GameObject> item_decoy_list2 = new List<GameObject>();
    public Transform using_Item_list, using_Item_list2;
    public GameObject item_Option;
    public List<GameObject> LvUp_Object_List = new List<GameObject>();
    public TextMeshProUGUI[] stat_text;
    public TextMeshProUGUI[] stat_text2;
    public TextMeshProUGUI[] stat_text3;
    public TextMeshProUGUI time_score;
    public int[] times;
    public GameObject stage_alert;
    public GameObject GameOver;
    public TextMeshProUGUI score_Result, player_Hp_text;
    public TextMeshPro player_Alert, heal_Alert;
    public GameObject active_Item, Shop_Keeper, shop;
    public List<GameObject> shop_items = new List<GameObject>();
    public RectTransform shop_item_pos;
    public GameObject buy_alert;
    public int buy_number, buy_type, buy_cost;
    public TextMeshProUGUI buy_alery_text;
    public GameObject[] dialogue;
    public GameObject paused;
    public bool paused_now, pause;
    public AudioClip[] clip;

    // ���� ������Ʈ
    public GameObject[] attack_objects;
    public float[] attack_object_timeer;
    public float[] attack_object_cooltime;
    public int lvup_cnt = 0;
    public GameObject[] grid, stage_monster, map_Object;
    int[] n_item = new int[33], r_item = new int[26], u_item = new int[6];
    private float red_jewel = 0;

    void Start()
    {
        // ġƮ ��� ����
        if (GameManager.instance.cheat == true) 
        {
            cheat_Object.SetActive(true);
        }
        // ���� ���� �������� ��ȣ �ޱ�
        GameManager.instance.stage_num = SceneManager.GetActiveScene().buildIndex;

        // ���丮 ��ȭ
        if (GameManager.instance.area_num == 1)
        {
            if (GameManager.instance.stage_num == 2)
            {
                Stroy_Dialog(6);
            }
            else if (GameManager.instance.stage_num == 7)
            {
                Stroy_Dialog(5);
            }
        }
        else if (GameManager.instance.area_num == 2)
        {
            if (GameManager.instance.stage_num == 2)
            {
                Stroy_Dialog(3);
            }
            else if (GameManager.instance.stage_num == 7)
            {
                Stroy_Dialog(4);
            }
        }
        else if (GameManager.instance.area_num == 3)
        {
            if (GameManager.instance.stage_num == 2)
            {
                Stroy_Dialog(4);
            }
            else if (GameManager.instance.stage_num == 7)
            {
                Stroy_Dialog(8);
            }
        }
        else if (GameManager.instance.area_num == 4)
        {
            if (GameManager.instance.stage_num == 2)
            {
                Stroy_Dialog(4);
            }
            if (GameManager.instance.stage_num == 7)
            {
                Stroy_Dialog(4);
            }
        }

        // ���� ���̱�
        switch (GameManager.instance.area_num)
        {
            case 1:
                grid[0].SetActive(true);
                break;
            case 2:
                grid[1].SetActive(true);
                break;
            case 3:
                grid[2].SetActive(true);
                break;
            case 4:
                grid[3].SetActive(true);
                break;
        }

        // ���� ��ġ�ϱ�
        switch (GameManager.instance.area_num) 
        {
            case 1:
                stage_monster[0].SetActive(true);
                break;
            case 2:
                stage_monster[1].SetActive(true);
                break;
            case 3:
                stage_monster[2].SetActive(true);
                break;
            case 4:
                stage_monster[3].SetActive(true);
                break;
        }

        // ������Ʈ ��ġ�ϱ�
        switch (GameManager.instance.area_num)
        {
            case 1:
                map_Object[0].SetActive(true);
                break;
            case 2:
                map_Object[1].SetActive(true);
                break;
            case 3:
                map_Object[2].SetActive(true);
                break;
            case 4:
                map_Object[3].SetActive(true);
                break;
        }

        // ��Ƽ�� ������ ���� Ȯ��
        if (GameManager.instance.active_item_bool == true)
        {
            active_Item.SetActive(true);
        }

        // �׷� ��ȯ
        if (GameManager.instance.attack_object[21] == true)
        {
            Instantiate(attack_objects[1]);
        }

        // ���� ���� ���� ��ȯ
        if (GameManager.instance.attack_object[22] == true)
        {
            Instantiate(attack_objects[2]);
        }

        // ���� ��ȯ
        if (GameManager.instance.attack_object[12] == true)
        {
            Instantiate(attack_objects[4]);
        }

        // ����Ʈ��!
        if (GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] == 0) 
        {
            Instantiate(attack_objects[3]);
        }

        // Ǫ�� ������ ���� ��ȯ
        if (GameManager.instance.attack_object[30] == true) 
        {
            Instantiate(GameManager.instance.summon_Object[3]);
        }
    }

    public void Update()
    {
        // ����
        hpbar.value = (float)GameManager.instance.hp / (float)GameManager.instance.maxhp;
        expbar.value = (float)GameManager.instance.exp / (float)GameManager.instance.maxexp;

        stat_text[0].text = GameManager.instance.maxhp.ToString();
        stat_text[1].text = GameManager.instance.atk.ToString();
        stat_text[2].text = GameManager.instance.def.ToString();
        stat_text[3].text = (Mathf.Floor(GameManager.instance.atk_cool * 100f) / 100f).ToString();
        stat_text[4].text = (Mathf.Floor(GameManager.instance.arrow_speed * 100f) / 100f).ToString();
        stat_text[5].text = (Mathf.Floor(GameManager.instance.speed * 100f) / 100f).ToString();
        stat_text[6].text = (Mathf.Floor(GameManager.instance.jump_power * 100f) / 100f).ToString();
        stat_text[7].text = GameManager.instance.jump_cnt.ToString();

        stat_text2[0].text = GameManager.instance.maxhp.ToString();
        stat_text2[1].text = GameManager.instance.atk.ToString();
        stat_text2[2].text = GameManager.instance.def.ToString();
        stat_text2[3].text = (Mathf.Floor(GameManager.instance.atk_cool * 100f) / 100f).ToString();
        stat_text2[4].text = (Mathf.Floor(GameManager.instance.arrow_speed * 100f) / 100f).ToString();
        stat_text2[5].text = (Mathf.Floor(GameManager.instance.speed * 100f) / 100f).ToString();
        stat_text2[6].text = (Mathf.Floor(GameManager.instance.jump_power * 100f) / 100f).ToString();
        stat_text2[7].text = GameManager.instance.jump_cnt.ToString();

        if (GameManager.instance.coin == 0)
        {
            stat_text2[8].text = "0";
        }
        else
        {
            stat_text2[8].text = CommaText(GameManager.instance.coin);
        }

        stat_text3[0].text = GameManager.instance.coin.ToString();
        stat_text3[1].text = GameManager.instance.maxhp.ToString();
        stat_text3[2].text = GameManager.instance.atk.ToString();
        stat_text3[3].text = GameManager.instance.def.ToString();
        stat_text3[4].text = (Mathf.Floor(GameManager.instance.atk_cool * 100f) / 100f).ToString();
        stat_text3[5].text = (Mathf.Floor(GameManager.instance.arrow_speed * 100f) / 100f).ToString();
        stat_text3[6].text = (Mathf.Floor(GameManager.instance.speed * 100f) / 100f).ToString();
        stat_text3[7].text = (Mathf.Floor(GameManager.instance.jump_power * 100f) / 100f).ToString();
        stat_text3[8].text = GameManager.instance.jump_cnt.ToString();

        GameManager.instance.time[0] += Time.deltaTime;
        if (GameManager.instance.time[0] >= 60)
        {
            GameManager.instance.time[1]++;
            GameManager.instance.time[0] = 0;
        }
        else if (GameManager.instance.time[1] >= 60) 
        {
            GameManager.instance.time[2]++;
            GameManager.instance.time[1] = 0;
        }
        if (GameManager.instance.score == 0)
        {
            time_score.text = "�÷��� Ÿ�� : " + GameManager.instance.time[2] + " : " + GameManager.instance.time[1] + " : " + (int)GameManager.instance.time[0] + "\n���� : 0";
        }
        else
        {
            time_score.text = "�÷��� Ÿ�� : " + GameManager.instance.time[2] + " : " + GameManager.instance.time[1] + " : " + (int)GameManager.instance.time[0] + "\n���� : " + CommaText(GameManager.instance.score);
        }

        player_Hp_text.text = GameManager.instance.hp.ToString() + " / " + GameManager.instance.maxhp.ToString();

        // ���� ����~�ִ� ����
        if (GameManager.instance.atk_cool <= 0.13)
        {
            GameManager.instance.atk_cool = 0.13f;
        }

        // ������ ����
        if (GameManager.instance.arrow_size <= 0.15f) 
        {
            GameManager.instance.arrow_size = 0.15f;
        }

        // �ִ� ü���� ���� �ʵ���!
        if (GameManager.instance.hp > GameManager.instance.maxhp)
        {
            GameManager.instance.hp = GameManager.instance.maxhp;
        }

        // ������
        if (GameManager.instance.exp >= GameManager.instance.maxexp)
        {
            // ������
            Time.timeScale = 0.0f;
            GameManager.instance.lv++;
            GameManager.instance.maxhp += 2;
            LvUp_Object.SetActive(true);

            // ������ ��ȣ ����
            // �븻
            for (int l = 0; l < n_item.Length; l++)
            {
                n_item[l] = l;
            }
            for (int l = 0; l < n_item.Length; l++)
            {
                int temp = n_item[l];
                int rnd = Random.Range(0, 33);
                n_item[l] = n_item[rnd];
                n_item[rnd] = temp;
            }

            // ����
            for (int l = 0; l < r_item.Length; l++)
            {
                r_item[l] = l;
            }
            for (int l = 0; l < r_item.Length; l++)
            {
                int temp = r_item[l];
                int rnd = Random.Range(0, 26);
                r_item[l] = r_item[rnd];
                r_item[rnd] = temp;
            }

            // ����ũ
            for (int l = 0; l < u_item.Length; l++)
            {
                u_item[l] = l;
            }
            for (int l = 0; l < u_item.Length; l++)
            {
                int temp = u_item[l];
                int rnd = Random.Range(0, 6);
                u_item[l] = u_item[rnd];
                u_item[rnd] = temp;
            }

            // ��ũ ����
            for (int i = 0; i < Select.Length; i++)
            {
                int t = 100;
                int p = Mathf.RoundToInt(t * Random.Range(0.0f, 1.0f));
                // �븻
                if (p >= 0 && p <= 65)
                {
                    item_Option.GetComponent<LvUP_Item>().option_num = n_item[i];
                    item_Option.GetComponent<LvUP_Item>().option_rank = 1;

                }
                // ����
                else if (p >= 66 && p <= 97)
                {
                    item_Option.GetComponent<LvUP_Item>().option_num = r_item[i];
                    item_Option.GetComponent<LvUP_Item>().option_rank = 2;
                }
                // ����ũ
                else if (p >= 98)
                {
                    item_Option.GetComponent<LvUP_Item>().option_num = u_item[i];
                    item_Option.GetComponent<LvUP_Item>().option_rank = 3;
                }
                GameObject Lvup_Item = Instantiate(item_Option, Select[i]);
                LvUp_Object_List.Add(Lvup_Item);
            }

            // �Ծ��� ������ ����Ʈ ( �ܼ� ���ݾ��� ���� )
            List_Clean();
            Using_Item_Create_List();

            GameManager.instance.exp -= GameManager.instance.maxexp;
            GameManager.instance.maxexp += (int)(GameManager.instance.maxexp * 0.2);
            lvup_cnt++;
        }

        // ���� ����
        if (GameManager.instance.hp <= 0)
        {
            GameOver.SetActive(true);
            if (GameManager.instance.score < GameManager.instance.playerdata.max_Scroe)
            {
                score_Result.text = "���� ���� : " + GameManager.instance.score + "\n<size=24>�ְ� ���� : " + GameManager.instance.playerdata.max_Scroe + "</size>";
            }
            else if (GameManager.instance.score > GameManager.instance.playerdata.max_Scroe)
            {
                score_Result.text = "���� ���� : " + GameManager.instance.score + "\n�ű��!";
                GameManager.instance.playerdata.max_Scroe = GameManager.instance.score;
                GameManager.instance.Save_PlayerData_ToJson();
            }
            GameManager.instance.bgm_audioSource.Stop();
            Time.timeScale = 0f;
        }

        // ȸ��ź��
        if (GameManager.instance.attack_object[0] == true)
        {
            attack_object_cooltime[0] = 13f;

            attack_object_timeer[0] += Time.deltaTime;
            if (attack_object_timeer[0] >= attack_object_cooltime[0])
            {
                StartCoroutine(Attack_Summon_1());
                attack_object_timeer[0] = 0.0f;
            }
        }

        // ȸ��ź�� ��ȯ
        IEnumerator Attack_Summon_1()
        {
            for (int i = 0; i < GameManager.instance.attack_object_cnt[0]; i++)
            {
                yield return new WaitForSeconds(0.3f);
                Instantiate(attack_objects[0]);
            }
        }

        // ����� ����
        if (GameManager.instance.attack_object[7] == true)
        {
            if (GameManager.instance.consume >= GameManager.instance.attack_object_cnt[7])
            {
                int random = Random.Range(3, 6);
                heal_Alert.text = random.ToSafeString();
                Instantiate(heal_Alert);
                GameManager.instance.maxhp += random;
                GameManager.instance.hp += random;
                GameManager.instance.consume = 0;
            }
        }

        // ���� ����
        if (GameManager.instance.attack_object[17] == true)
        {
            red_jewel += Time.deltaTime;
            if (red_jewel >= GameManager.instance.attack_object_cnt[17])
            {
                int heal = (int)(GameManager.instance.maxhp * 0.03);
                if (heal <= 1)
                {
                    heal = 1;
                }
                GameManager.instance.hp += heal;
                heal_Alert.GetComponent<TextMeshPro>().text = heal.ToString();
                Instantiate(heal_Alert);
                red_jewel = 0f;
            }
        }

        // �Ͻ�����
        if (Input.GetKeyDown(KeyCode.Escape) && paused_now == false)
        {
            List_Clean2();
            Using_Item_Create_List2();
            paused_now = true;
            pause = true;
            paused.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause == true) 
        {
            pause = false;
            Game_Resumed();
        }

        /* ---------- ���� ���� ---------- */
        // ù ���� ����
        if (GameManager.instance.playerdata.Achievements[1] == false && GameManager.instance.achievement_bool == false)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n��������!!";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[1];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[1] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
        // �׷� ����
        if (GameManager.instance.attack_object[21] == true && GameManager.instance.playerdata.Achievements[2] == false && GameManager.instance.achievement_bool == false)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�ĳĳĿ� �Ŀ�!</color>\n�ٸ�������~  �Ͽ�!";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Lv_Up_item_Img_r[12];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[2] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
        // ���� ����
        if (GameManager.instance.attack_object[12] == true && GameManager.instance.playerdata.Achievements[3] == false && GameManager.instance.achievement_bool == false)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>������ ��!</color>\n��! �۸۸۸�! �۸�!";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Lv_Up_item_Img_r[16];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[3] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
        // ������ : ���Ǻ����� ģ����
        if (GameManager.instance.attack_object[21] == true && GameManager.instance.playerdata.Achievements[4] == false && GameManager.instance.attack_object[12] == true && GameManager.instance.achievement_bool == false)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n���Ǻ����� ģ����";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Lv_Up_item_Img_r[14];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[4] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
        // �ִ� ���� �޼�
        if (GameManager.instance.atk_cool <= 0.13 && GameManager.instance.playerdata.Achievements[11] == false && GameManager.instance.achievement_bool == false)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n������ HIGH�� ���!";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Lv_Up_item_Img_u[3];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[11] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
        // ������ ��
        if (GameManager.instance.playerdata.Achievements[15] == false && GameManager.instance.achievement_bool == false && GameManager.instance.hp == 1)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n�̰ɻ��";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[2];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[15] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
        // ����� �� 
        if (GameManager.instance.playerdata.Achievements[21] == false && GameManager.instance.achievement_bool == false && (GameManager.instance.double_shot == true && GameManager.instance.tripple_shpt == true))
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n��Ƽ ����";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[8];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[21] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
        // ��!
        if (GameManager.instance.playerdata.Achievements[24] == false && GameManager.instance.attack_object[22] == true && GameManager.instance.attack_object[30] == true && GameManager.instance.achievement_bool == false) 
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n��!";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[11];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[24] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
        // ���� n ȸ �̻�
        if (GameManager.instance.playerdata.Achievements[26] == false  && GameManager.instance.playerdata.atk_cnt >= 10 && GameManager.instance.achievement_bool == false) 
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n�����Ʒå�";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[13];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[26] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
        if (GameManager.instance.playerdata.Achievements[27] == false && GameManager.instance.playerdata.atk_cnt >= 100 && GameManager.instance.achievement_bool == false)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n�����Ʒå�";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[14];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[27] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
        if (GameManager.instance.playerdata.Achievements[28] == false && GameManager.instance.playerdata.atk_cnt >= 1000 && GameManager.instance.achievement_bool == false)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n�����Ʒå�";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[15];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[28] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
    }

    // ���� ������, ���� ����
    public void List_Clean()
    {
        // ����Ʈ �ʱ�ȭ
        int count = item_decoy_list.Count;
        int b = count - 1;
        for (int i = 0; i < count; i++) 
        {
            Destroy(item_decoy_list[b]);
            b--;
        }
        for (int i = 0; i < count; i++)
        {
            item_decoy_list.RemoveAt(0);
        }
    }
    public void List_Clean2()
    {
        // ����Ʈ �ʱ�ȭ
        int count = item_decoy_list2.Count;
        int b = count - 1;
        for (int i = 0; i < count; i++)
        {
            Destroy(item_decoy_list2[b]);
            b--;
        }
        for (int i = 0; i < count; i++)
        {
            item_decoy_list2.RemoveAt(0);
        }
    }

    public void Using_Item_Create_List()
    {            
        // ����
        if (GameManager.instance.double_shot == true)
        {
            item_decoy.GetComponent<Item_Decoy>().db = true;
            Using_Item_Decoy(2, 0);
            item_decoy.GetComponent<Item_Decoy>().db = false;
        }
        // Ʈ���� ��
        if (GameManager.instance.tripple_shpt == true)
        {
            item_decoy.GetComponent<Item_Decoy>().trp = true;
            Using_Item_Decoy(3, 0);
            item_decoy.GetComponent<Item_Decoy>().trp = false;
        }
        // ������ Ư��������
        for (int i = 0; i < GameManager.instance.attack_object.Length; i++)
        {
            if (GameManager.instance.attack_object[i] == true)
            {
                item_decoy.GetComponent<Item_Decoy>().number = i;
                GameObject a = Instantiate(item_decoy, using_Item_list);
                item_decoy_list.Add(a);
            }
        }
    }
    public void Using_Item_Create_List2()
    {
        // ����
        if (GameManager.instance.double_shot == true)
        {
            item_decoy.GetComponent<Item_Decoy>().db = true;
            Using_Item_Decoy2(2, 0);
            item_decoy.GetComponent<Item_Decoy>().db = false;
        }
        // Ʈ���� ��
        if (GameManager.instance.tripple_shpt == true)
        {
            item_decoy.GetComponent<Item_Decoy>().trp = true;
            Using_Item_Decoy2(3, 0);
            item_decoy.GetComponent<Item_Decoy>().trp = false;
        }
        // ������ Ư��������
        for (int i = 0; i < GameManager.instance.attack_object.Length; i++)
        {
            if (GameManager.instance.attack_object[i] == true)
            {
                item_decoy.GetComponent<Item_Decoy>().number = i;
                GameObject a = Instantiate(item_decoy, using_Item_list2);
                item_decoy_list2.Add(a);
            }
        }
    }

    public void Using_Item_Decoy(int rank, int num)
    {
        item_decoy.GetComponent<Item_Decoy>().rank = rank;
        item_decoy.GetComponent<Item_Decoy>().number = num;
        GameObject a = Instantiate(item_decoy, using_Item_list);
        item_decoy_list.Add(a);
    }
    public void Using_Item_Decoy2(int rank, int num)
    {
        item_decoy.GetComponent<Item_Decoy>().rank = rank;
        item_decoy.GetComponent<Item_Decoy>().number = num;
        GameObject a = Instantiate(item_decoy, using_Item_list2);
        item_decoy_list2.Add(a);
    }

    public void Game_Resumed()
    {
        GameManager.instance.SFX_Play("Button_SFX", clip[0], 1);
        List_Clean2();
        paused_now = false;
        paused.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Go_To_Title() 
    {
        GameManager.instance.SFX_Play("Button_SFX", clip[0], 1);
        GameManager.instance.playerdata.coin_now = GameManager.instance.coin;
        GameManager.instance.Setting();
        Time.timeScale = 1.0f;
        Loading_Scene.LoadScene("Title");
        GameManager.instance.BGM_Play(GameManager.instance.bgm[0]);
    }

    public void Stroy_Dialog(int num)
    {
        dialogue[0].SetActive(true);
        GameObject.Find("Story_Dialogue_Btn").GetComponent<Story_Doal>().end = num;
    }

    public string CommaText(int gold)
    {
        return string.Format("{0:#,###}", gold);
    }
}