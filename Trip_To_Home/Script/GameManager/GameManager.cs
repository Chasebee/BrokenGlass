using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Audio;

public enum Key_Action {Up, Down, Left, Right, Jump, Attack, Active, count}
public static class KeySet { public static Dictionary<Key_Action, KeyCode> keys = new Dictionary<Key_Action, KeyCode>(); }

public class GameManager : MonoBehaviour
{
    // 세이브 / 게임 데이터
    [Header("세이브 데이터")]
    public PlayerData playerdata;
    public bool cheat;
    // 소리 데이터
    [Header("소리 데이터")]
    public AudioSource bgm_audioSource;
    public AudioClip[] bgm;
    public AudioClip[] sfx;
    public AudioMixer mixer;
    public float bgm_value, sfx_value;
    [Header("캐릭터의 기본 정보")]
    public KeyCode[] default_key = new KeyCode[] { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Space, KeyCode.Z, KeyCode.X};
    public int stage;
    public int score, coin;
    public float[] time;
    public AudioClip[] player_Clips;
    public AudioClip[] attack_Clip;
    public int lv, atk, real_atk, maxhp, hp, def, exp, maxexp, jump_cnt, calcul;
    public float speed, real_speed, jump_power, atk_cool;
    public float crit_dmg, crit_per;
    public bool dash;
    public float arrow_speed, knockback, mob_knockback, player_size, arrow_size;
    public float[] buff;
    public int weapon_atk;
    public int area_num, stage_num;
    public Sprite[] Lv_Up_item_Img_n, Lv_Up_item_Img_r, Lv_Up_item_Img_u;
    public Sprite[] s_Lv_Up_item_Img_n, s_Lv_Up_item_Img_r, s_Lv_Up_item_Img_u;
    public Sprite[] shop_Food_img, shop_Item_img;
    public int bonus_shop_cost;
    public int bonus_shop_value;
    public bool infinity;
    public bool giant, sweep;
    // 업적 관련
    [Header("업적알림 관련")]
    public GameObject Achievement_Alert;
    public AudioClip ach_clip;
    public Sprite[] Achievements_img_list;
    public bool achievement_bool;
    
    // 액티브 아이템 유무
    [Header("액티브 아이템 관련")]
    public bool active_item_bool;
    public float active_item_cool;
    public float active_timmer;
    public string active_item_name;
    public Sprite active_item_img;
    public bool[] active_buff;
    public GameObject[] summon_Object;

    // 아이템 관련 변수
    [Header("아이템 (능력) 관련")]
    public bool double_shot = false;
    public bool tripple_shpt = false;

    // 아이템 ( 스토리 모드 )
    public bool[] attack_object;
    public float[] attack_object_cnt;
    // 아이템 ( 서바이벌 모드 추가 아이템 )
    public bool[] s_attack_object;
    public float[] s_attack_object_cnt;

    public int consume = 0;
    public bool luna;
    // 몬스터 CC기
    public GameObject monster_cc;
    // 게임모드
    public bool survival_Mod;
    // 싱글톤
    public static GameManager instance;

    public int frame_set;

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
            KeySet.keys.Add((Key_Action) i, default_key[i]);
        }
    }

    void Start()
    {
        // 프레임 고정
        Application.targetFrameRate = 60;

        // 오디오 소스 설정
        bgm_audioSource = gameObject.GetComponent<AudioSource>();
        // 세이브 폴더 확인
        if (Directory.Exists(Application.persistentDataPath + "/SaveData") == true)
        {
        }
        else 
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
        }

        // 세이브 파일 확인
        if (File.Exists(Application.persistentDataPath + "/SaveData/Save.json") == true)
        {
            Load_PlayerData();
            // 로드 했으니 키설정 불러오기
            for (int i = 0; i < playerdata.save_Key.Length; i++)
            {
                if (playerdata.save_Key[i] != 0)
                {
                    KeySet.keys[(Key_Action)i] = playerdata.save_Key[i];
                }
            }
        }
        else 
        {
            Save_PlayerData_ToJson();
        }
        Setting();

        // 게임시작시 첫 업적 ( 플레이 감사 업적 )
        if (playerdata.Achievements[0] == false && achievement_bool == false) 
        {
            SFX_Play("ach_sound", ach_clip, 1);
            achievement_bool = true;
            playerdata.Achievements[0] = true;
            Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = Achievements_img_list[0];
            Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>도전과제 달성!</color>\n당신이 최고야";
            Instantiate(Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            Save_PlayerData_ToJson();
        }

        // 소리 초기값
        bgm_value = 0.75f;
        sfx_value = 0.75f;
        GameManager.instance.mixer.SetFloat("BGM", Mathf.Log10(bgm_value) * 20);
        GameManager.instance.mixer.SetFloat("SFX", Mathf.Log10(sfx_value) * 20);
    }

    // 게임 시작시 처음 설정 해야 하는거
    public void Setting()
    {
        // 보너스 스텟 ( 영구 증가 상점 1)
        switch (playerdata.bonus_lv[0])
        {
            case 0:
                playerdata.bonus_hp = 0;
                break;
            case 1:
                playerdata.bonus_hp = 5;
                break;
            case 2:
                playerdata.bonus_hp = 15;
                break;
            case 3:
                playerdata.bonus_hp = 30;
                break;
            case 4:
                playerdata.bonus_hp = 50;
                break;
            case 5:
                playerdata.bonus_hp = 75;
                break;
        }
        switch (playerdata.bonus_lv[1])
        {
            case 0:
                playerdata.bonus_atk = 0;
                break;
            case 1:
                playerdata.bonus_atk = 3;
                break;
            case 2:
                playerdata.bonus_atk = 6;
                break;
            case 3:
                playerdata.bonus_atk = 11;
                break;
            case 4:
                playerdata.bonus_atk = 16;
                break;
            case 5:
                playerdata.bonus_atk = 26;
                break;
        }
        switch (playerdata.bonus_lv[2])
        {
            case 0:
                playerdata.bonus_def = 0;
                break;
            case 1:
                playerdata.bonus_def = 1;
                break;
            case 2:
                playerdata.bonus_def = 2;
                break;
            case 3:
                playerdata.bonus_def = 5;
                break;
            case 4:
                playerdata.bonus_def = 8;
                break;
            case 5:
                playerdata.bonus_def = 13;
                break;
        }
        switch (playerdata.bonus_lv[3])
        {
            case 0:
                playerdata.bonus_atk_cool = 0;
                break;
            case 1:
                playerdata.bonus_atk_cool = 0.02f;
                break;
            case 2:
                playerdata.bonus_atk_cool = 0.04f;
                break;
            case 3:
                playerdata.bonus_atk_cool = 0.08f;
                break;
            case 4:
                playerdata.bonus_atk_cool = 0.12f;
                break;
            case 5:
                playerdata.bonus_atk_cool = 0.17f;
                break;
        }
        switch (playerdata.bonus_lv[4])
        {
            case 0:
                playerdata.bonus_arrow_speed = 0;
                break;
            case 1:
                playerdata.bonus_arrow_speed = 0.03f;
                break;
            case 2:
                playerdata.bonus_arrow_speed = 0.06f;
                break;
            case 3:
                playerdata.bonus_arrow_speed = 0.11f;
                break;
            case 4:
                playerdata.bonus_arrow_speed = 0.16f;
                break;
            case 5:
                playerdata.bonus_arrow_speed = 0.26f;
                break;
        }
        switch (playerdata.bonus_lv[5])
        {
            case 0:
                playerdata.bonus_speed = 0;
                break;
            case 1:
                playerdata.bonus_speed = 0.03f;
                break;
            case 2:
                playerdata.bonus_speed = 0.06f;
                break;
            case 3:
                playerdata.bonus_speed = 0.12f;
                break;
            case 4:
                playerdata.bonus_speed = 0.17f;
                break;
            case 5:
                playerdata.bonus_speed = 0.27f;
                break;
        }
        switch (playerdata.bonus_lv[6])
        {
            case 0:
                playerdata.bonus_jump_power = 0;
                break;
            case 1:
                playerdata.bonus_jump_power = 0.03f;
                break;
            case 2:
                playerdata.bonus_speed = 0.06f;
                break;
            case 3:
                playerdata.bonus_speed = 0.11f;
                break;
            case 4:
                playerdata.bonus_speed = 0.16f;
                break;
            case 5:
                playerdata.bonus_speed = 0.23f;
                break;
        }
        if (playerdata.bonus_lv[7] >= 1)
        {
            playerdata.bonus_jump_cnt = 1;
        }
        else
        {
            playerdata.bonus_jump_cnt = 0;
        }
        // 기본 셋팅 ( 스토리 모드 )
        stage = 1;
        coin = playerdata.coin_now;
        lv = 1;
        atk = 5 + playerdata.bonus_atk;
        def = 0 + +playerdata.bonus_def;
        real_atk = 0;
        crit_per = 0;
        crit_dmg = 1.2f;
        mob_knockback = 1f;
        knockback = 1f;
        maxhp = 35 + playerdata.bonus_hp;
        hp = 35 + playerdata.bonus_hp;
        maxexp = 30;
        exp = 0;
        atk_cool = 0.65f - playerdata.bonus_atk_cool;
        speed = 6 + playerdata.bonus_speed;
        real_speed = speed;
        jump_power = 9.8f + playerdata.bonus_jump_power;
        jump_cnt = 2 + playerdata.bonus_jump_cnt;
        arrow_speed = 15 + playerdata.bonus_arrow_speed;
        player_size = 1.3f;
        arrow_size = 0.3f;
        score = 0;
        active_item_bool = false;
        active_item_name = "";

        double_shot = false;
        tripple_shpt = false;
        for (int i = 0; i < attack_object_cnt.Length; i++)
        {
            attack_object[i] = false;
            attack_object_cnt[i] = 0;
        }
        for (int i = 0; i < s_attack_object.Length; i++) 
        {
            s_attack_object[i] = false;
            s_attack_object_cnt[i] = 0;
        }
        for (int i = 0; i < time.Length; i++) 
        {
            time[i] = 0;
        }
        // 마지막 한발 계수
        attack_object_cnt[1] = 100;
        // (구)그라운드 파운드 데미지 계수
        attack_object_cnt[2] = 0;
        // 보호의 장막 쿨타임
        attack_object_cnt[3] = 20;
        // 마비탄 CC 지속시간
        attack_object_cnt[4] = 1;
        // 점착탄 공격력 계수
        attack_object_cnt[5] = 140;
        // 점착탄 공격력 계수
        attack_object_cnt[5] = 140;
        // 독성탄창 공격력 계수
        attack_object_cnt[6] = 0.03f;
        // 흡수의 부적
        attack_object_cnt[7] = 27;
        consume = 0;
        // 폭발탄창 폭발 공격력 계수
        attack_object_cnt[8] = 140;
        // 몽몽이 딜계수 초기화
        attack_object_cnt[12] = 0;
        // 신비한 돌 확률
        attack_object_cnt[14] = 75;
        // 붉은 수정(하트모양)
        attack_object_cnt[17] = 4.9f;
        // 대시 파워
        attack_object_cnt[29] = 1.45f;

        // (생존모드) 작은 악마의 뿔
        s_attack_object_cnt[1] = 2.5f;
    }

    // 사운드 ( 배경음)
    public void BGM_Play(AudioClip clip)
    {
        bgm_audioSource.Stop();
        bgm_audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        bgm_audioSource.clip = clip;
        bgm_audioSource.loop = true;
        bgm_audioSource.Play();
    }

    // 사운드 ( 효과음 )
    public void SFX_Play(string sfxName, AudioClip clip, float volume) 
    {
        GameObject play = new GameObject(sfxName + "_Sound");
        AudioSource audioSource = play.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(play, clip.length);
        // 사용 법
        // GameManager.instance.SFXPlay("사용할 이름", AudioClip타입);
    }

    // 세이브
    [ContextMenu("Save")]
    public void Save_PlayerData_ToJson()
    {
        string jsonData = JsonUtility.ToJson(playerdata, true);
        string path = Path.Combine(Application.persistentDataPath, "SaveData/Save.json");
        // 세이브경로를 이렇게 지정도 가능하다 "SaveData/Save.json"
        File.WriteAllText(path, jsonData);
    }
    // 로드
    [ContextMenu("Load")]
    public void Load_PlayerData()
    {
        string path = Path.Combine(Application.persistentDataPath, "SaveData/Save.json");
        string jsonData = File.ReadAllText(path);
        playerdata = JsonUtility.FromJson<PlayerData>(jsonData);
    }

    // 종료
    public void OnApplicationQuit()
    {
        playerdata.coin_now = coin;
        Save_PlayerData_ToJson();
    }

}

[System.Serializable]
public class PlayerData
{
    // 업적 시스템
    public bool[] Achievements = new bool[30];
    public bool story_Mod_Clear;
    public int atk_cnt;

    public int max_Scroe;
    public int s_max_Score;
    public int coin_now;

    public int[] bonus_lv = new int[8];
    public int bonus_hp;
    public int bonus_atk;
    public int bonus_def;
    public float bonus_atk_cool;
    public float bonus_arrow_speed;
    public float bonus_speed;
    public float bonus_jump_power;
    public int bonus_jump_cnt;

    public KeyCode[] save_Key = new KeyCode[7];
}