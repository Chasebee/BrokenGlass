using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Mine : MonoBehaviour
{
    public int type;
    public GameObject summon_explosion;
    Animator anim;

    void Start() 
    {
        anim = GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("S_Monster")) 
        {
            if (type == 0) 
            {
                Instantiate(summon_explosion, transform.position, Quaternion.Euler(0,0,-1));
                Destroy_Self();
            }
            if (type == 1)
            {
                bool critical = false;
                int dmg = (int)(GameManager.instance.atk * (GameManager.instance.s_attack_object_cnt[3] * 0.01));
                int crit = (int)(Random.Range(0, 100) + GameManager.instance.crit_per);
                if (crit >= 80)
                {
                    dmg = (int)(dmg * GameManager.instance.crit_dmg);
                    critical = true;
                }
                collision.gameObject.GetComponent<SurvivalMod_Monster>().mhp -= dmg;
                collision.gameObject.GetComponent<SurvivalMod_Monster>().dmg = dmg;
                if (critical == true)
                {
                    collision.gameObject.GetComponent<SurvivalMod_Monster>().critical = true;
                }
                collision.gameObject.GetComponent<SurvivalMod_Monster>().hit = true;
            }
        }
    }
    public void Destroy_Self() 
    {
        Destroy(gameObject);
    }
}
