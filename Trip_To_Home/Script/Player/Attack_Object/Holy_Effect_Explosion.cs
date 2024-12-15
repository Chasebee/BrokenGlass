using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holy_Effect_Explosion : MonoBehaviour
{
    public int cnt;
    public int dmg;
    CircleCollider2D c_col;

    public GameObject btm;
    BattleManager bt;
    void Start()
    {
        btm = GameObject.FindWithTag("Manager");
        bt = GameObject.FindWithTag("Manager").GetComponent<BattleManager>();
        c_col = GetComponent<CircleCollider2D>();
        if (cnt <= 2)
        {
            float cal = (float)1.3 + (float)(GameManager.instance.attack_object_cnt[27] * 0.15);
            dmg = (int)(GameManager.instance.atk * cal);
        } 
        else if (cnt >= 5)
        {
            gameObject.transform.localScale = new Vector2(2, 2);
            float cal = (float)2.15 + (float)(GameManager.instance.attack_object_cnt[27] * 0.15);
            dmg = (int)(GameManager.instance.atk * cal);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster2")) 
        {
            // 데미지 계산
            c_col.enabled = false;
            collision.gameObject.GetComponent<Monster2>().mhp -= dmg;
            collision.gameObject.GetComponent<Monster2>().hit = true;
            collision.GetComponent<Monster2>().dmg = dmg;
            dmg = (int)(dmg * 0.2);
            GameManager.instance.hp += dmg;
            bt.heal_Alert.text = dmg.ToString();
            Instantiate(bt.heal_Alert);
        }
        if (collision.gameObject.CompareTag("Summon"))
        {
            // 데미지 계산
            c_col.enabled = false;
            collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp -= dmg;
            collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;
            collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = dmg;
            dmg = (int)(dmg * 0.2);
            GameManager.instance.hp += dmg;
            bt.heal_Alert.text = dmg.ToString();
            Instantiate(bt.heal_Alert);
        }
    }

    public void Drop_Object() 
    {
        Destroy(gameObject);
    }
}
