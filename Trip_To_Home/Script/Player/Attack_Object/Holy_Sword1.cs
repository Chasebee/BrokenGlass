using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holy_Sword1 : MonoBehaviour
{
    public int cnt, dmg, number;
    public GameObject pos;
    public LayerMask isLayer;
    public string target_name;

    public GameObject btm;
    BattleManager bt;
    float speed = 17.7f;

    Animator anim;
    public bool set = false;
    void Start()
    {
        btm = GameObject.FindWithTag("Manager");
        bt = GameObject.FindWithTag("Manager").GetComponent<BattleManager>();
        anim = GetComponent<Animator>();

        if (cnt >= 5)
        {
            float cal = (float)0.75 + (float)(GameManager.instance.attack_object_cnt[27] * 0.15);
            dmg = (int)(GameManager.instance.atk * cal);
        }
        else
        {
            float cal = (float)1.85 + (float)(GameManager.instance.attack_object_cnt[27] * 0.15);
            dmg = (int)(GameManager.instance.atk * cal);
        }

        if (dmg <= 0) 
        {
            dmg = 1;
        }
        gameObject.layer = 3;
    }

    void Update() 
    {
        if (set == true) 
        {
            speed += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, pos.transform.position, speed * Time.deltaTime);
        }

        Vector3 scale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        // 레이캐스트 - 박스캐스트
        RaycastHit2D col = Physics2D.BoxCast(gameObject.transform.position, scale, 0, Vector2.left, 0, isLayer);

        if (col.collider != null) 
        {
            if (col.collider.CompareTag("Monster2") && target_name == col.collider.name)
            {
                col.collider.GetComponent<Monster2>().mhp -= dmg;
                col.collider.GetComponent<Monster2>().dmg = dmg;
                col.collider.GetComponent<Monster2>().hit = true;
                dmg = (int)(dmg * 0.2);
                GameManager.instance.hp += dmg;
                bt.heal_Alert.text = dmg.ToString();
                Instantiate(bt.heal_Alert);
            }

            else if (col.collider.CompareTag("Summon"))
            {
                col.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp -= dmg;
                col.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = dmg;
                col.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;
                dmg = (int)(dmg * 0.2);
                GameManager.instance.hp += dmg;
                bt.heal_Alert.text = dmg.ToString();
                Instantiate(bt.heal_Alert);
            }
            Destroy(gameObject);
        }

    }

    IEnumerator Sword_Summon() 
    {
        anim.SetBool("Ready", true);
        yield return new WaitForSeconds(0.8f);
        set = true;
    }

}
