using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Survival_Mod_Manager : MonoBehaviour
{
    [Header("UI관련")]
    // 기본 UI
    public TextMeshProUGUI time_score;
    public GameObject game_over;
    public TextMeshProUGUI total_Scroe;
    public GameObject paused;
    public bool paused_now, pause;
    public AudioClip[] clip;

    // 레벨업 아이템
    public RectTransform[] Select;
    public List<GameObject> LvUp_Object_List = new List<GameObject>();
    public GameObject LvUp_Object;
    public GameObject item_Option;
    public int lvup_cnt;
    int[] n_item = new int[16], r_item = new int[9], u_item = new int[6];

    // 먹었던 아이템 / 스텟
    public TextMeshProUGUI[] stat_text;
    public TextMeshProUGUI[] stat_text2;
    public GameObject item_decoy;
    public List<GameObject> item_decoy_list = new List<GameObject>();
    public List<GameObject> item_decoy_list2 = new List<GameObject>();
    public Transform using_Item_list, using_Item_list2;
    public GameObject[] attack_objects;
    public float[] attack_object_cooltime;
    public float[] attack_object_timeer;

    [Header("플레이어 관련")]
    public GameObject hp_Bar;
    public GameObject hp_pos;
    public GameObject exp_Bar;
    public float critical;

    [Header("몬스터 관련")]
    public Transform[] spawn_pos;
    public GameObject mob_parent;
    public GameObject[] mobs;
    public GameObject[] boss;
    [Header("스테이지 관련")]
    public int stage;
    public int stage_wave;
    public int spawn_mob_cnt;
    public float spawn_time, spawn_timer;

    [Header("특수아이템 관련")]
    public GameObject[] s_objects;
    public float[] s_objects_time;
    void Start()
    {
        stage = 1;
        stage_wave = 1;
        spawn_time = 10.4f;
        spawn_timer = spawn_time / 2;
        spawn_mob_cnt = 16;
        critical = 20;
    }
    void Update()
    {
        hp_Bar.GetComponent<Slider>().value = (float)GameManager.instance.hp / (float)GameManager.instance.maxhp;
        exp_Bar.GetComponent<Slider>().value = (float)GameManager.instance.exp / (float)GameManager.instance.maxexp;

        stat_text[0].text = GameManager.instance.maxhp.ToString();
        stat_text[1].text = GameManager.instance.atk.ToString();
        stat_text[2].text = GameManager.instance.def.ToString();
        stat_text[3].text = (Mathf.Floor(GameManager.instance.atk_cool * 100f) / 100f).ToString();
        stat_text[4].text = (Mathf.Floor(GameManager.instance.arrow_speed * 100f) / 100f).ToString();
        stat_text[5].text = (Mathf.Floor(GameManager.instance.speed * 100f) / 100f).ToString();

        stat_text2[0].text = GameManager.instance.maxhp.ToString();
        stat_text2[1].text = GameManager.instance.atk.ToString();
        stat_text2[2].text = GameManager.instance.def.ToString();
        stat_text2[3].text = (Mathf.Floor(GameManager.instance.atk_cool * 100f) / 100f).ToString();
        stat_text2[4].text = (Mathf.Floor(GameManager.instance.arrow_speed * 100f) / 100f).ToString();
        stat_text2[5].text = (Mathf.Floor(GameManager.instance.speed * 100f) / 100f).ToString();
        // 비어있는 값 방지용 0
        if (GameManager.instance.coin == 0)
        {
            stat_text2[6].text = "0";
        }
        else
        {
            stat_text2[6].text = CommaText(GameManager.instance.coin);
        }

        if (spawn_timer < spawn_time)
        {
            spawn_timer += Time.deltaTime;
        }

        if (spawn_timer >= spawn_time)
        {
            stage_wave++;
            spawn_timer = 0f;
            // 스테이지 구분
            if (stage <= 5)
            {
                for (int i = 0; i < spawn_mob_cnt; i++)
                {
                    int mob_num = Random.Range(0, 3);
                    int spawn_pos_num = Random.Range(0, spawn_pos.Length);
                    GameObject spawn = Instantiate(mobs[mob_num], spawn_pos[spawn_pos_num]);
                    spawn.transform.parent = mob_parent.transform;
                }
            }
            else if (stage >= 6 && stage <= 10)
            {
                for (int i = 0; i < spawn_mob_cnt; i++)
                {
                    int mob_num = Random.Range(0, 6);
                    int spawn_pos_num = Random.Range(0, spawn_pos.Length);
                    GameObject spawn = Instantiate(mobs[mob_num], spawn_pos[spawn_pos_num]);
                    spawn.transform.parent = mob_parent.transform;
                }
            }
            // 첫 보스 몹
            else if (stage == 11)
            {
                int spawn_pos_num = Random.Range(0, spawn_pos.Length);
                GameObject spawn = Instantiate(boss[0], spawn_pos[spawn_pos_num]);
                spawn.transform.parent = mob_parent.transform;
                stage++;
                stage_wave = 0;
            }
            else if (stage >= 12 && stage <= 17)
            {
                for (int i = 0; i < spawn_mob_cnt; i++)
                {
                    int mob_num = Random.Range(3, mobs.Length);
                    int spawn_pos_num = Random.Range(0, spawn_pos.Length);
                    GameObject spawn = Instantiate(mobs[mob_num], spawn_pos[spawn_pos_num]);
                    spawn.transform.parent = mob_parent.transform;
                }
            }
            else if (stage >= 18) 
            {
                for (int i = 0; i < spawn_mob_cnt; i++)
                {
                    int mob_num = Random.Range(3, mobs.Length);
                    int spawn_pos_num = Random.Range(0, spawn_pos.Length);
                    GameObject spawn = Instantiate(mobs[mob_num], spawn_pos[spawn_pos_num]);
                    spawn.GetComponent<SurvivalMod_Monster>().mhp += stage * 5;
                    spawn.GetComponent<SurvivalMod_Monster>().m_atk += (int)(stage * 0.35f);
                    spawn.GetComponent<SurvivalMod_Monster>().exp += (int)(stage * 0.25f);
                    spawn.transform.parent = mob_parent.transform;
                }
            }
        }

        if (stage_wave >= 5) 
        {
            stage++;
            stage_wave = 1;

            spawn_time -= 0.3f;
            spawn_mob_cnt += 3;
            if (spawn_time <= 5) 
            {
                spawn_time = 5;
            }
            
        }

        hp_Bar.transform.position = Camera.main.WorldToScreenPoint(hp_pos.transform.position);

        // 시간 / 점수
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
            time_score.text = "플레이 타임 : " + GameManager.instance.time[2] + " : " + GameManager.instance.time[1] + " : " + (int)GameManager.instance.time[0] + "\n점수 : " + GameManager.instance.score;
        }
        else
        {
            time_score.text = "플레이 타임 : " + GameManager.instance.time[2] + " : " + GameManager.instance.time[1] + " : " + (int)GameManager.instance.time[0] + "\n점수 : " + CommaText(GameManager.instance.score);
        }

        // 게임오버 감지
        if (GameManager.instance.hp <= 0) 
        {
            Time.timeScale = 0;
            if (GameManager.instance.score < GameManager.instance.playerdata.s_max_Score)
            {
                total_Scroe.text = "최종 점수 : " + GameManager.instance.score + "\n<size=24>최고 점수 : " + GameManager.instance.playerdata.s_max_Score + "</size>";
            }
            else if (GameManager.instance.score > GameManager.instance.playerdata.s_max_Score)
            {
                total_Scroe.text = "최종 점수 : " + GameManager.instance.score + "\n신기록!";
                GameManager.instance.playerdata.s_max_Score = GameManager.instance.score;
                GameManager.instance.Save_PlayerData_ToJson();
            }
            GameManager.instance.bgm_audioSource.Stop();
            game_over.SetActive(true);
        }

        // 최대 체력을 넘지 않도록!
        if (GameManager.instance.hp > GameManager.instance.maxhp)
        {
            GameManager.instance.hp = GameManager.instance.maxhp;
        }

        // 최대 공속 지정
        if (GameManager.instance.atk_cool < 0.1) 
        {
            GameManager.instance.atk_cool = 0.1f;
        }

        // 레벨업
        if (GameManager.instance.exp >= GameManager.instance.maxexp)
        {
            // 레벨업
            Time.timeScale = 0.0f;
            GameManager.instance.lv++;
            GameManager.instance.maxhp += 2;
            LvUp_Object.SetActive(true);

            // 아이템 번호 지정
            // 노말
            for (int l = 0; l < n_item.Length; l++)
            {
                n_item[l] = l;
            }
            for (int l = 0; l < n_item.Length; l++)
            {
                int temp = n_item[l];
                int rnd = Random.Range(0, 16);
                n_item[l] = n_item[rnd];
                n_item[rnd] = temp;
            }

            // 레어
            for (int l = 0; l < r_item.Length; l++)
            {
                r_item[l] = l;
            }
            for (int l = 0; l < r_item.Length; l++)
            {
                int temp = r_item[l];
                int rnd = Random.Range(0, 8);
                r_item[l] = r_item[rnd];
                r_item[rnd] = temp;
            }

            // 유니크
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

            // 랭크 지정
            for (int i = 0; i < Select.Length; i++)
            {
                int t = 100;
                int p = Mathf.RoundToInt(t * Random.Range(0.0f, 1.0f));
                // 노말
                if (p >= 0 && p <= 65)
                {
                    item_Option.GetComponent<Survival_Mod_LvUp_Item>().option_num = n_item[i];
                    item_Option.GetComponent<Survival_Mod_LvUp_Item>().option_rank = 1;

                }
                // 레어
                else if (p >= 66 && p <= 97)
                {
                    item_Option.GetComponent<Survival_Mod_LvUp_Item>().option_num = r_item[i];
                    item_Option.GetComponent<Survival_Mod_LvUp_Item>().option_rank = 2;
                }
                // 유니크
                else if (p >= 98)
                {
                    item_Option.GetComponent<Survival_Mod_LvUp_Item>().option_num = u_item[i];
                    item_Option.GetComponent<Survival_Mod_LvUp_Item>().option_rank = 3;
                }
                GameObject Lvup_Item = Instantiate(item_Option, Select[i]);
                LvUp_Object_List.Add(Lvup_Item);
            }

            // 먹었던 아이템 리스트 ( 단순 스텟업은 제외 )
            List_Clean();
            Using_Item_Create_List();

            GameManager.instance.exp -= GameManager.instance.maxexp;
            GameManager.instance.maxexp += (int)(GameManager.instance.maxexp * 0.15);
            lvup_cnt++;
        }

        // 일시정지
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
        
        // 회전탄막
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

        // 회전탄막 소환
        IEnumerator Attack_Summon_1()
        {
            for (int i = 0; i < GameManager.instance.attack_object_cnt[0]; i++)
            {
                yield return new WaitForSeconds(0.3f);
                Instantiate(attack_objects[0]);
            }
        }

        // 마력 지뢰
        if (GameManager.instance.s_attack_object[3] == true) 
        {
            s_objects_time[0] += Time.deltaTime;
            if (s_objects_time[0] >= 4.8f) 
            {
                Vector3 p_pos = GameObject.FindWithTag("Player").transform.position;
                Instantiate(s_objects[0], p_pos, Quaternion.Euler(0, 0, 0));
                s_objects_time[0] = 0;
            }
        }

        // 마력의 화살
        if (GameManager.instance.s_attack_object[4] == true) 
        {
            s_objects_time[1] += Time.deltaTime;
            if (s_objects_time[1] >= 5)
            {
                for (int i = 0; i < GameManager.instance.s_attack_object_cnt[5]; i++)
                {
                    GameObject player = GameObject.FindWithTag("Player");
                    Instantiate(s_objects[1], player.transform.position, Quaternion.Euler(0, 0, 0));
                }
                s_objects_time[1] = 0;
            }
        }

        // 라이트닝 샷
        if (GameManager.instance.s_attack_object[6] == true) 
        {
            s_objects_time[2] += Time.deltaTime;
            if (s_objects_time[2] >= 7.4) 
            {
                s_objects_time[2] = 0;
                GameObject.FindWithTag("Player").GetComponent<Survival_Mod_Attack>().s_atk_obj[0] = true;
            }
        }

        // 튕기는 마력 구체
        if (GameManager.instance.s_attack_object[7] == true) 
        {
            s_objects_time[3] += Time.deltaTime;
            GameObject a = GameObject.FindWithTag("Player");
            if (s_objects_time[3] >= 25)
            {
                for (int i = 0; i < GameManager.instance.s_attack_object_cnt[7]; i++)
                {
                    Instantiate(s_objects[3], a.transform.position, Quaternion.Euler(0, 0, 0));
                }
                s_objects_time[3] = 0;
            }
        }

        // 독안개
        if (GameManager.instance.s_attack_object[8] == true) 
        {
            s_objects_time[4] += Time.deltaTime;
            if (s_objects_time[4] >= 18)
            {
                GameObject a = GameObject.FindWithTag("Player");
                int cnt = (int)(GameManager.instance.s_attack_object_cnt[8] * 10);
                for (int i = 0; i < cnt; i++)
                {
                    float rnd_x = Random.Range(-3.0f, 3.0f);
                    float rnd_y = Random.Range(-3.0f, 3.0f);
                    Vector3 pos = new Vector3(a.transform.position.x + rnd_x, a.transform.position.y + rnd_y, -1);
                    GameObject d = Instantiate(s_objects[4], pos, Quaternion.Euler(0, 0, 0));
                    float rnd_sx = (d.transform.localScale.x * GameManager.instance.s_attack_object_cnt[8]);
                    float rnd_sy = (d.transform.localScale.y * GameManager.instance.s_attack_object_cnt[8]);
                    d.transform.localScale = new Vector2(d.transform.localScale.x + rnd_sx, d.transform.localScale.y + rnd_sy);
                }
                s_objects_time[4] = 0;
            }
        }

        // 흡수의 부적
        if (GameManager.instance.attack_object[7] == true)
        {
            if (GameManager.instance.consume >= GameManager.instance.attack_object_cnt[7])
            {
                int random = Random.Range(1, 3);
                GameManager.instance.maxhp += random;
                GameManager.instance.hp += random;
                GameManager.instance.consume = 0;
            }
        }

        // 재생의 드림캐쳐
        if (GameManager.instance.s_attack_object[12] == true)
        {
            s_objects_time[5] += Time.deltaTime;
            if (s_objects_time[5] >= 3) 
            {
                GameManager.instance.hp += (int)(GameManager.instance.maxhp * 0.035);
                GameObject.FindWithTag("Player").GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_on = true;
                s_objects_time[5] = 0;
            }
        }
    }
    // 먹은 아이템, 스텟 관련
    public void List_Clean()
    {
        // 리스트 초기화
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
        // 리스트 초기화
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
        for (int i = 0; i < GameManager.instance.attack_object.Length; i++) 
        {
            if (GameManager.instance.attack_object[i] == true) 
            {
                item_decoy.GetComponent<Item_Decoy>().number = i;
                item_decoy.GetComponent<Item_Decoy>().s_mod = false;
                GameObject a = Instantiate(item_decoy, using_Item_list);
                item_decoy_list.Add(a);
            }
        }
        for (int i = 0; i < GameManager.instance.s_attack_object.Length; i++)
        {
            if (GameManager.instance.s_attack_object[i] == true)
            {
                item_decoy.GetComponent<Item_Decoy>().number = i;
                item_decoy.GetComponent<Item_Decoy>().s_mod = true;
                GameObject a = Instantiate(item_decoy, using_Item_list);
                item_decoy_list.Add(a);
            }
        }
    }
    public void Using_Item_Create_List2()
    {
        for(int i = 0; i < GameManager.instance.attack_object.Length; i++) 
        {
            if (GameManager.instance.attack_object[i] == true) 
            {
                item_decoy.GetComponent<Item_Decoy>().number = i;
                item_decoy.GetComponent<Item_Decoy>().s_mod = false;
                GameObject a = Instantiate(item_decoy, using_Item_list2);
                item_decoy_list2.Add(a);
            }
        }
        for (int i = 0; i < GameManager.instance.s_attack_object.Length; i++)
        {
            if (GameManager.instance.s_attack_object[i] == true)
            {
                item_decoy.GetComponent<Item_Decoy>().number = i;
                item_decoy.GetComponent<Item_Decoy>().s_mod = true;
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

    public string CommaText(int val)
    {
        return string.Format("{0:#,###}", val);
    }
}
