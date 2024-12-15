using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class SurvivalMod_Monster : MonoBehaviour
{
    public int mon_num;
    public int mhp;
    public int m_atk;
    public int exp;
    public int score;
    public float atk_cool;
    public float atk_cooltimer;
    public float speed;
    public bool hit, c_hit;
    public bool critical;
    public bool[] area_dmg;
    public float[] area_dmg_time;
    public int dmg;
    public RectTransform dmg_pos;
    public TextMeshPro[] damage_text;
    public Rigidbody2D target;

    public float[] gizmo_pos_plus;
    public Vector2 gizmo_pos;
    public Vector2 gizmo_size;

    Rigidbody2D rigid;
    SpriteRenderer rend;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        anim.SetInteger("Num", mon_num);
        
    }
    void FixedUpdate()
    {
        gizmo_pos.x = gizmo_pos_plus[0] + transform.position.x;
        gizmo_pos.y = gizmo_pos_plus[1] + transform.position.y;
        if (anim.GetBool("Die") == false)
        {
            if (c_hit == false)
            {
                Vector2 dirVec = target.position - rigid.position;
                Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
                rigid.MovePosition(rigid.position + nextVec);
                //rigid.velocity = Vector2.zero;
            }
            // 공격력 쿨타임
            if (atk_cooltimer < atk_cool)
            {
                atk_cooltimer += Time.fixedDeltaTime;
            }

            // 히트박스
            RaycastHit2D body = Physics2D.BoxCast(gizmo_pos, gizmo_size, 0f, Vector2.left, 0f, LayerMask.GetMask("Player"));
            if (body.collider != null)
            {
                if (body.collider.gameObject.CompareTag("Player") && (atk_cooltimer >= atk_cool))
                {
                    int dmg = GameManager.instance.def - m_atk;
                    if (dmg >= 1)
                    {
                        dmg = 0;
                    }
                    GameManager.instance.hp += dmg;
                    atk_cooltimer = 0;

                    // 몸빵데미지
                    if (body.collider.gameObject.GetComponent<Survival_Mod_Player_Move>().hit == false)
                    {
                        GameManager.instance.SFX_Play("player_hit", GameManager.instance.player_Clips[1], 1);
                    }
                }
            }
        }
        if (mhp <= 0 && anim.GetBool("Die") == false)
        {
            // 작은 뿔 효과
            if (GameManager.instance.s_attack_object[1] == true)
            {
                int devil = Random.Range(0, 100);
                if (devil >= 75) 
                {
                    int heal = (int)(GameManager.instance.maxhp * GameManager.instance.s_attack_object_cnt[1] * 0.01);
                    GameManager.instance.hp += heal;
                    GameObject p = GameObject.FindWithTag("Player");
                    if (GameManager.instance.s_attack_object[10] == true) 
                    {
                        p.GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_val = heal;
                        p.GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_on = true;
                    }
                }
            }
            GameManager.instance.score += score;
            GameManager.instance.exp += exp;
            // 괴짜 안경
            if (GameManager.instance.s_attack_object[2] == true) 
            {
                float p_exp = GameManager.instance.s_attack_object_cnt[2] * 0.01f;
                int plus = (int)(exp * p_exp);
                if (plus <= 0) 
                {
                    plus = 1;
                }
                GameManager.instance.exp += plus;
            }
            anim.SetBool("Die", true);
        }
        if (hit == true) 
        {
            if (critical == true)
            {
                damage_text[1].text = "-" + dmg.ToString();
                Instantiate(damage_text[1], dmg_pos.transform.position, Quaternion.Euler(0, 0, 0));
                if (GameManager.instance.s_attack_object[11] == true && GameManager.instance.s_attack_object[12] == true)
                {
                    int heal = (int)(dmg * 0.23);
                    GameManager.instance.hp += heal;
                    if (GameManager.instance.s_attack_object[10] == true)
                    {
                        GameObject.FindWithTag("Player").GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_val = heal;
                        GameObject.FindWithTag("Player").GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_on = true;
                    }
                    StartCoroutine(Critical_Again());
                }
                critical = false;
            }
            else if (critical == false)
            {
                damage_text[0].text = "-" + dmg.ToString();
                Instantiate(damage_text[0], dmg_pos.transform.position, Quaternion.Euler(0, 0, 0));
            }
            hit = false;
            c_hit = true;
            StartCoroutine(Hit_KnockBack());
        }

        /*--- 장판 데미지 ---*/
        if (area_dmg[0] == true)
        {
            if (area_dmg_time[0] <= 0.4)
            {
                area_dmg_time[0] += Time.fixedDeltaTime;
            }
            else if (area_dmg_time[0] >= 0.4)
            {
                area_dmg_time[0] = 0;

                int damage = (int)(GameManager.instance.atk * GameManager.instance.s_attack_object_cnt[9]);
                int crit = (int)(Random.Range(0, 100) + GameManager.instance.crit_per);
                if (crit >= 80)
                {
                    damage = (int)(damage * GameManager.instance.crit_dmg);
                    critical = true;
                }
                mhp -= damage;
                dmg = damage;
                if (critical == true)
                {
                    critical = true;
                }
                hit = true;
            }
        }
        else if (area_dmg[0] == false) 
        {
            area_dmg_time[0] = 0;
        }
    }
    void LateUpdate()
    {
        rend.flipX = target.position.x > rigid.position.x;
    }
    public void Mob_Die()
    {
        Destroy(gameObject);
    }

    public IEnumerator Hit_KnockBack()
    {
        rend.material.color = new Color(1, 0.65f, 0.65f);
        Vector3 player_pos = GameObject.FindWithTag("Player").transform.position;
        Vector3 dir_Vec = transform.position - player_pos;
        rigid.AddForce(dir_Vec.normalized * 3, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.15f);
        rigid.velocity = Vector2.zero;
        rend.material.color = new Color(1, 1, 1);
        c_hit = false;
    }

    // 드림캐쳐 2셋 효과
    public IEnumerator Critical_Again() 
    {
        yield return new WaitForSeconds(0.2f);
        int heal = (int)(dmg * 0.23);
        GameManager.instance.hp += heal;
        if (GameManager.instance.s_attack_object[10] == true)
        {
            GameObject.FindWithTag("Player").GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_val = heal;
            GameObject.FindWithTag("Player").GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_on = true;
        }
        mhp -= dmg;
        damage_text[1].text = "-" + dmg.ToString();
        Instantiate(damage_text[1], dmg_pos.transform.position, Quaternion.Euler(0, 0, 0));
    }

    // 기즈모
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(gizmo_pos, gizmo_size);
    }
}
