using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Monster_Normal : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer rend;
    BoxCollider2D b_col;

    public bool rig_type, aggro_type;
    public string monster_name;
    public int num, lv, hp, def, atk, exp, dir, loop, dir_now;
    public float speed, project_speed, project_time, project_range, critical, origin_speed, origin_chase_speed, knockback;
    public float atk_cool, atk_cooltime;
    public bool idle, hit, move, range, attack_now;
    public float chase_speed, chase_range, stop_chase_range;
    public bool chasing_now;
    public int hit_dir;
    public Transform p_transform;

    public GameObject projectile, projectile_pos;
    public RectTransform text_pos;
    public TextMeshPro dmg_text;
    public GameObject hit_effect;
    public RectTransform hit_effect_pos;
    public LayerMask layers;

    public List<Equip_Item> equip;
    public List<Use_Item> use;
    public List<Etc_Item> etc;
    public Money_Item money;
    public GameObject drop_item;
    public float[] equip_drop_percentage;
    public float[] use_drop_percentage;
    public float[] etc_drop_percentage;
    public int[] use_max;
    public int[] etc_max;

    void Start()
    {
        if (!rig_type)
        {
            rend = GetComponent<SpriteRenderer>();
        }
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        b_col = GetComponent<BoxCollider2D>();
        InvokeRepeating("Think", 0f, loop);
        origin_chase_speed = chase_speed;
        origin_speed = speed;
        dir_now = dir;
        anim.SetInteger("Num", num);
        if (GameManager.instance.day_night) 
        {
            int p_hp, p_atk, p_exp;
            p_hp = (int)(hp * 0.1f) > 0 ? (int)(hp * 0.1f) : 1;
            p_atk = (int)(atk * 0.1f) > 0 ? (int)(atk * 0.1f) : 1;
            p_exp = (int)(exp * 0.05f) > 0 ? (int)(exp * 0.05f) : 5;

            hp += p_hp;
            atk += p_atk;
            exp += p_exp;

            for (int i = 0; i < equip_drop_percentage.Length; i++) 
            {
                equip_drop_percentage[i] += 0.015f;
            }
            for (int i = 0; i < use_drop_percentage.Length; i++) 
            {
                use_drop_percentage[i] += 0.015f;
            }
            for (int i = 0; i < etc_drop_percentage.Length; i++)
            {
                etc_drop_percentage[i] += 0.015f;
            }
        }
    }

    void Update()
    {
        if (hp <= 0 && (anim.GetBool("Die") == false))
        {
            anim.SetBool("Die", true);
            speed = 0;
            GameManager.instance.exp += exp;
            gameObject.layer = 0;
            Drop_Item();
        }

        if (!rig_type)
        {
            rend.flipX = dir == -1 ? false : true;
        }
        else
        {
            transform.rotation = dir == -1 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        }

        if (atk_cool <= atk_cooltime) 
        {
            atk_cool += Time.deltaTime;
        }

        if (chasing_now) 
        {
            if (IsInvoking("Think"))
            {
                CancelInvoke("Think");
            }
            Chase_Player();
        }
        else
        {
            if(!IsInvoking("Think"))
            {
                InvokeRepeating("Think", 0f, loop);
            }
        }
    }

    void FixedUpdate()
    {
        // ���� �̵�
        if (anim.GetBool("Move") && !chasing_now)
        {
            rigid.linearVelocity = new Vector2(dir * speed, rigid.linearVelocity.y);
        }
        else if(!anim.GetBool("Move")) 
        {
            rigid.linearVelocity = new Vector2(0, rigid.linearVelocity.y);
        }

        // ���� ��Ʈ�ڽ�
        RaycastHit2D mcast = Physics2D.BoxCast(rigid.position, b_col.size, 0f, Vector2.down, 0, layers);
        if (mcast.collider != null && hp >= 1)
        {
            // ������ ó��
            GameObject a = mcast.collider.gameObject;
            if (a.CompareTag("Arrow"))
            {
                Arrow arrow = a.GetComponent<Arrow>();
                int atk_types = arrow.attack_type;
                hit_dir = arrow.way;
                if (!arrow.piercing_object.Contains(gameObject))
                {
                    int dmg = def - arrow.atk;
                    if (dmg >= 0)
                    {
                        dmg = 0;
                    }
                    hp += dmg;

                    hit = true;
                    anim.SetBool("Hit", true);
                    Vector2 hitSourcePosition = a.transform.position;
                    StartCoroutine(Hit_Effect(0.2f, hitSourcePosition, dmg, a.GetComponent<Arrow>().ciritical, atk_types));

                    Chasing_Start();

                    // ������ üũ
                    if (arrow.piercing == true && !arrow.piercing_object.Contains(gameObject))
                    {
                        arrow.piercing_object.Add(gameObject);
                        arrow.Damage_Reset();
                    }
                    else
                    {
                        Destroy(a);
                    }
                }
            }
            else if (a.CompareTag("Melee"))
            {
                Melee melee = a.GetComponent<Melee>();
                if (!melee.piercing_object.Contains(gameObject))
                {
                    int dmg = def - melee.atk;
                    if (dmg >= 0)
                    {
                        dmg = 0;
                    }
                    hp += dmg;

                    hit = true;
                    anim.SetBool("Hit", true);
                    Chasing_Start();
                    melee.piercing_object.Add(gameObject); // ���� 1Ÿ�� �ǰ�
                    Vector2 hitSourcePosition = a.transform.position;
                    hitSourcePosition.y *= 0.07f;
                    
                    StartCoroutine(Hit_Effect(0.45f, hitSourcePosition, dmg, a.GetComponent<Melee>().ciritical, 1));
                }
            }
        }

        // ����
        if (atk_cool >= atk_cooltime)
        {
            // ���� ���� ����
            if (range)
            {
                RaycastHit2D attack_range = Physics2D.Raycast(transform.position, transform.right * -1, project_range, layers);
                if (aggro_type)
                {
                    if (attack_range.collider != null)
                    {
                        if (anim.GetBool("Attack") == false)
                        {
                            anim.SetBool("Attack", true);
                            if (anim.GetBool("Move"))
                            {
                                anim.SetBool("Move", false);
                                attack_now = true;
                            }
                        }
                    }
                }
                else if (!aggro_type && chasing_now)
                {
                    if (attack_range.collider != null)
                    {
                        if (anim.GetBool("Attack") == false)
                        {
                            anim.SetBool("Attack", true);
                            if (anim.GetBool("Move"))
                            {
                                anim.SetBool("Move", false);
                                attack_now = true;
                            }
                        }
                    }
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Wall") || col.collider.CompareTag("Portal"))
        {
            dir *= -1;
            dir_now = dir;
        }
    }

    IEnumerator Hit_Effect(float time, Vector2 hitSourcePosition, int dmg, bool critical, int atk_type)
    {
        float rnd_x = Random.Range(-1.1f, 1.1f);
        float rnd_y = Random.Range(-1.1f, 1.1f);
        Vector3 dmg_pos = new Vector3(gameObject.transform.position.x + rnd_x, gameObject.transform.position.y + rnd_y, -2);

        GameObject hef = Instantiate(hit_effect, hit_effect_pos.transform);
        hef.GetComponent<HIt_Effect>().num = atk_type;
        hef.GetComponent<HIt_Effect>().dir = hit_dir;
        // ������ ǥ��
        if (critical == false)
        {
            dmg_text.text = dmg.ToString();
            
            Instantiate(dmg_text, dmg_pos, Quaternion.Euler(0, 0, 0));
        }
        else 
        {
            TextMeshPro texts = Instantiate(dmg_text, dmg_pos, Quaternion.Euler(0, 0, 0));
            texts.text = dmg.ToString() + "!";
            texts.color = new Color(1, 0, 0);            
        }
        Vector2 knockbackDirection = (rigid.position - hitSourcePosition).normalized;
        rigid.AddForce(knockbackDirection * GameManager.instance.knockback_power, ForceMode2D.Impulse);

        speed = 0;
        chase_speed = 0;
        yield return new WaitForSeconds(time);

        speed = origin_speed;
        chase_speed = origin_chase_speed;
        hit = false;
        anim.SetBool("Hit", false);
    }

    public void Think()
    {
        dir = Random.Range(-1, 2);
        loop = Random.Range(4, 10);
        if (dir == 0)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Move", false);
        }
        else
        {
            dir_now = dir;
            anim.SetBool("Idle", false);
            anim.SetBool("Move", true);
        }
    }

    public void Die_Effect()
    {
        Destroy(gameObject);
    }

    public void Drop_Item()
    {
        // ��� ��ġ ������ ���� ���� (��: x�� y ������ ���� -0.5 ~ 0.5 ���� ���� ������ �����ϰ�)
        Vector2 dropOffsetRange = new Vector2(1.5f, 0f);

        for (int i = 0; i < equip_drop_percentage.Length; i++)
        {
            if (Random.Range(0.000f, 1.001f) <= equip_drop_percentage[i])
            {
                // ��� ���
                Vector2 dropPosition = transform.position + (Vector3)GetRandomDropOffset(dropOffsetRange);
                GameObject equip_drop = Instantiate(drop_item, dropPosition, transform.rotation);
                equip_drop.GetComponent<SpriteRenderer>().sprite = equip[i].item_img[0];
                equip_drop.GetComponent<Monster_Drop_Item>().type = 1;
                equip_drop.GetComponent<Monster_Drop_Item>().equip_item = equip[i];
            }
        }
        for (int i = 0; i < use_drop_percentage.Length; i++) 
        {
            if (Random.Range(0.000f, 1.001f) <= use_drop_percentage[i]) 
            {
                Vector2 dropPosition = transform.position + (Vector3)GetRandomDropOffset(dropOffsetRange);
                GameObject use_drop = Instantiate(drop_item, dropPosition, transform.rotation);
                use_drop.GetComponent<SpriteRenderer>().sprite = use[i].item_img;
                use_drop.GetComponent<Monster_Drop_Item>().type = 2;
                use_drop.GetComponent<Monster_Drop_Item>().use_item = use[i];
                int rnd = Random.Range(1, use_max[i]);
                use_drop.GetComponent<Monster_Drop_Item>().use_max = rnd;
            }
        }
        for (int i = 0; i < etc_drop_percentage.Length; i++) 
        {
            if (Random.Range(0.000f, 1.001f) <= etc_drop_percentage[i])
            {
                Vector2 dropPosition = transform.position + (Vector3)GetRandomDropOffset(dropOffsetRange);
                GameObject etc_drop = Instantiate(drop_item, dropPosition, transform.rotation);
                etc_drop.GetComponent<SpriteRenderer>().sprite = etc[i].item_img;
                etc_drop.GetComponent<Monster_Drop_Item>().type = 3;
                etc_drop.GetComponent<Monster_Drop_Item>().etc_item = etc[i];
                int rnd = Random.Range(1, etc_max[i]);
                etc_drop.GetComponent<Monster_Drop_Item>().etc_max = rnd;
            }
        }

        if (Random.Range(0.0f, 1.0f) <= 0.75f)
        {
            // ��� ���
            Vector2 dropPosition = transform.position + (Vector3)GetRandomDropOffset(dropOffsetRange);
            GameObject gold_drop = Instantiate(drop_item, dropPosition, transform.rotation);
            gold_drop.GetComponent<SpriteRenderer>().sprite = money.item_img;
            gold_drop.GetComponent<Monster_Drop_Item>().type = 0;
            int a = Random.Range(money.money[0], money.money[1]);
            gold_drop.GetComponent<Monster_Drop_Item>().money = a;
        }
    }

    public void Attack()
    {
        if (range) 
        {
            GameObject a = Instantiate(projectile, transform.position, transform.rotation);
            Monster_Projectile b = a.GetComponent<Monster_Projectile>();
            b.atk = atk;
            b.way = dir_now;
            b.criti = critical;
            b.speed = project_speed;
            b.time = project_time;
            b.knock = knockback;
        }
        speed = 0;
        chase_speed= 0;
        atk_cool = 0f;
    
    }
    public void Attack_End() 
    {
        if (attack_now)
        {
            attack_now = false;
            anim.SetBool("Move", true);
        }
        speed = origin_speed;
        chase_speed = origin_chase_speed;
        anim.SetBool("Attack", false);
    }

    private Vector2 GetRandomDropOffset(Vector2 range)
    {
        float offsetX = Random.Range(-range.x, range.x);
        float offsetY = Random.Range(-range.y, range.y);
        return new Vector2(offsetX, offsetY);
    }

    void Chasing_Start() 
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) 
        {
            p_transform = p.transform;
            chasing_now = true;
        }
    }

    void Chase_Player()
    {
        if (p_transform != null)
        {
            float dis = Vector2.Distance(transform.position, p_transform.position);

            if (dis <= chase_range)
            {
                // �÷��̾ �����ϴ� ���� ����
                Vector2 chase_dir = (p_transform.position - transform.position).normalized;
                
                dir = chase_dir.x > 0 ? 1 : -1;
                dir_now = dir;

                // �ִϸ��̼� ����
                anim.SetBool("Idle", false);
                anim.SetBool("Move", true);


                // ���� �̵� ó��
                if (!anim.GetBool("Attack"))
                {
                    rigid.linearVelocity = new Vector2(chase_dir.x * chase_speed, rigid.linearVelocity.y);
                }
                else if (anim.GetBool("Attack"))
                {
                    rigid.linearVelocity = new Vector2(0, rigid.linearVelocity.y);
                }

            }
            else if (dis >= stop_chase_range)
            {
                // ���� �ߴ�
                chasing_now = false;

                // Think ȣ���� ���� ���� �ൿ���� ���ư��� �ϱ�
                if (!IsInvoking("Think"))
                {
                    InvokeRepeating("Think", 0f, loop);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ���� ������ �ð������� Ȯ���ϱ� ���� Gizmo �׸���
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * -1 * project_range);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chase_range);
        Gizmos.color = Color.black; // ���� ���� ������ �Ķ������� ǥ��
        Gizmos.DrawWireSphere(transform.position, stop_chase_range); // ���� ���� ���� ǥ��
    }
}
