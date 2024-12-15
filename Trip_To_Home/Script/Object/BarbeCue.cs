using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbeCue : MonoBehaviour
{
    Animator anim;
    public float time;
    public bool cooked;

    public GameObject btm;
    BattleManager bt;
    void Start()
    {
        anim = GetComponent<Animator>();
        btm = GameObject.FindWithTag("Manager");
        bt = GameObject.FindWithTag("Manager").GetComponent<BattleManager>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 10f) 
        {
            cooked = true;
        }
        if (cooked == true) 
        {
            anim.SetBool("Cooked", true);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster_Arrow"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player") && cooked == true)
        {
            // 이.. 이맛은!
            if (GameManager.instance.playerdata.Achievements[25] == false && GameManager.instance.achievement_bool == false)
            {
                GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                GameManager.instance.achievement_bool = true;
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>도전과제 달성!</color>\n이.. 이맛은!";
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[12];
                Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                GameManager.instance.playerdata.Achievements[25] = true;
                GameManager.instance.Save_PlayerData_ToJson();
            }
            int heal = 30 + (int)(GameManager.instance.maxhp * 0.2);
            bt.heal_Alert.text = heal.ToString();
            Instantiate(bt.heal_Alert);
            GameManager.instance.hp += heal; 
            Destroy(gameObject);
        }
    }
}
