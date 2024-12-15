using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Magic_Arrow : MonoBehaviour
{
    public GameObject[] mobs;
    public int target_num;
    public float speed;
    public int dmg;

    public int null_dir;
    Vector3 dir;
    Quaternion rotTarget;
    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        mobs = GameObject.FindGameObjectsWithTag("S_Monster");
        target_num = Random.Range(0, mobs.Length);
        dmg = (int)(GameManager.instance.atk * GameManager.instance.s_attack_object_cnt[4]);
        
    }

    void FixedUpdate()
    {
        if (mobs.Length != 0)
        {
            if (mobs[target_num] != null)
            {
                dir = (mobs[target_num].transform.position - transform.position).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                rotTarget = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.fixedDeltaTime * 20);
                rigid.velocity = new Vector2(dir.x, dir.y) * speed;
            }
            if (mobs[target_num] == null)
            {
                Destroy(gameObject);
            }
        }
        else if (mobs.Length == 0) 
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("S_Monster")) 
        {
            bool critical = false;
            int half_dmg = dmg / 2;
            int crit = (int)(Random.Range(0, 100) + GameManager.instance.crit_per);
            if (crit >= 80)
            {
                half_dmg = (int)(half_dmg * GameManager.instance.crit_dmg);
                critical = true;
            }
            collision.gameObject.GetComponent<SurvivalMod_Monster>().mhp -= half_dmg;
            collision.gameObject.GetComponent<SurvivalMod_Monster>().dmg = half_dmg;
            if (critical == true)
            {
                collision.gameObject.GetComponent<SurvivalMod_Monster>().critical = true;
            }
            collision.gameObject.GetComponent<SurvivalMod_Monster>().hit = true;
        }

        if (collision.gameObject == mobs[target_num]) 
        {
            bool critical = false;
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
            Destroy(gameObject);
        }
    }
}
