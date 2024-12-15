using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cheat : MonoBehaviour
{
    public string s_name;
    public bool[] input_chk;
    public bool input;
    public AudioClip[] clips;

    void Start() 
    {
        Scene scene = SceneManager.GetActiveScene();
        s_name = scene.name;
        if (GameManager.instance.cheat == true) 
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && input == false) 
        {
            input = true;
            GameManager.instance.SFX_Play("Cheat_Input_On", clips[0], 1);
            GameManager.instance.bgm_audioSource.Stop();
        }
        if (input == true)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                input_chk[0] = true;
                GameManager.instance.SFX_Play("Cheat_Input_On", clips[1], 1);
            }
            else if (Input.GetKeyDown(KeyCode.R) && input_chk[0] == true)
            {
                input_chk[1] = true;
                GameManager.instance.SFX_Play("Cheat_Input_On", clips[1], 1);
            }
            else if (Input.GetKeyDown(KeyCode.O) && input_chk[1] == true)
            {
                input_chk[2] = true;
                GameManager.instance.SFX_Play("Cheat_Input_On", clips[1], 1);
            }
            else if (Input.GetKeyDown(KeyCode.K) && input_chk[2] == true)
            {
                input_chk[3] = true;
                GameManager.instance.SFX_Play("Cheat_Input_On", clips[1], 1);
            }
            else if (Input.GetKeyDown(KeyCode.E) && input_chk[3] == true)
            {
                input_chk[4] = true;
                GameManager.instance.SFX_Play("Cheat_Input_On", clips[1], 1);
            }
            else if (Input.GetKeyDown(KeyCode.N) && input_chk[4] == true)
            {
                input_chk[5] = true;
                GameManager.instance.SFX_Play("Cheat_Input_On", clips[1], 1);
            }
            else if (Input.GetKeyDown(KeyCode.G) && input_chk[5] == true)
            {
                input_chk[6] = true;
                GameManager.instance.SFX_Play("Cheat_Input_On", clips[1], 1);
            }
            else if (Input.GetKeyDown(KeyCode.L) && input_chk[6] == true)
            {
                input_chk[7] = true;
                GameManager.instance.SFX_Play("Cheat_Input_On", clips[1], 1);
            }
            else if (Input.GetKeyDown(KeyCode.A) && input_chk[7] == true)
            {
                input_chk[8] = true;
                GameManager.instance.SFX_Play("Cheat_Input_On", clips[1], 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) && (input_chk[8] == true && input_chk[9] == false))
            {
                input_chk[9] = true;
                GameManager.instance.SFX_Play("Cheat_Input_On", clips[1], 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) && (input_chk[8] == true && input_chk[9] == true))
            {
                input_chk[10] = true;
                Cheat_True();
            }
            else if (Input.anyKeyDown)
            {
                Cheat_Command_False();
            }
        }
    }
    public void Cheat_True() 
    {
        GameManager.instance.cheat = true;
        GameManager.instance.BGM_Play(GameManager.instance.bgm[0]);
        GameManager.instance.SFX_Play("Cheat_Input_On", clips[3], 1);


        // 쿼드라 샷 
        if (GameManager.instance.playerdata.Achievements[22] == false && GameManager.instance.achievement_bool == false)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>도전과제 달성!</color>\n찾아라 비밀의 열쇠";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[9];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[22] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
        Destroy(gameObject);
    }
    public void Cheat_Command_False()
    {
        for (int i = 0; i < input_chk.Length; i++)
        {
            input_chk[i] = false;
        }
        GameManager.instance.SFX_Play("Cheat_Input_On", clips[2], 1);
    }
}
