using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holy_Cross : MonoBehaviour
{
    public float timmer;
    public float time;
    public float summon_timmer;
    public float summon_time;
    private bool heal;
    private GameObject btm;
    BattleManager bt;
    void Start()
    {
        btm = GameObject.FindWithTag("Manager");
        bt = btm.GetComponent<BattleManager>();
    }
    void Update()
    {
        if (heal == true) 
        {
            heal = false;
        }
        if (summon_time > summon_timmer)
        {
            summon_timmer += Time.deltaTime;
        }
        else if (summon_time < summon_timmer) 
        {
            Destroy(gameObject);
        }
        if (time > timmer)
        {
            timmer += Time.deltaTime;
        }
        else if (time < timmer)
        {
            timmer = 0f;
            heal = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && heal == true) 
        {
            int heal = (int)(GameManager.instance.maxhp * 0.07);
            GameManager.instance.hp += heal;
            if (GameManager.instance.hp > GameManager.instance.maxhp) 
            {
                GameManager.instance.hp = GameManager.instance.maxhp;   
            }
            bt.heal_Alert.text = heal.ToString();
            Instantiate(bt.heal_Alert);
        }
        if (collision.gameObject.CompareTag("Monster2") && heal == true) 
        {
            collision.gameObject.GetComponent<Monster2>().mhp -= 10;
            collision.gameObject.GetComponent<Monster2>().dmg = 10;
            collision.gameObject.GetComponent<Monster2>().hit = true;
        }
    }
}
