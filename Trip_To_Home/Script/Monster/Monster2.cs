using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Monster2 : MonoBehaviour
{
    // �⺻����
    public Rigidbody2D rigid;
    public SpriteRenderer rend;
    public Animator anim;
    public int move_set, monster_number;
    public float speed, remember_speed;
    public int mhp, mdef, atk, dmg, exp, score, drop_max_coin;
    public float knockback = 3f;
    public GameObject monster_arrow, monster_melee, monster_arrow_ro;
    public RectTransform monster_arrow_pos;
    public float attack_cnt, attack_cnt_time, attack_delay, attack_timmer, arrow_speed, arrow_size;
    public int[] attack_angle;
    public GameObject coin;
    public GameObject image_Decoy;
    public float fall_width, fall_hight;
    public bool attack_type, hide_type, fly_type, hold_type, hide, hide_found, boss;
    // �����̻�
    public GameObject mezz_Effect;
    public float[] mezz_time, mezz_timmer;
    public bool[] mezz;
    public int stack_magic_cnt = 1;
    public bool stack_magic;
    public int light_magic_cnt = 1;
    public bool magic_type_light;
    public bool infinity_monster;

    // �ٴ���Ʈ ������ ����
    public float[] deal_area_timmer;
    public bool[] deal_area;

    // ������ ǥ��
    public TextMeshPro damage_text;
    public RectTransform dmg_pos;

    // �÷��̾� ���� / �ǰ� ���� ����
    public bool player_recogn_bool = false;
    public float[] player_recogn_value;
    public bool hit = false, hit_Effect = false, hit_chase = false;
    public float hit_chase_time;

    public GameObject dialog;

    private void Awake()
    {
        remember_speed = speed;
        if (monster_number != 19 && monster_number != 20)
        {
            rigid = GetComponent<Rigidbody2D>();
            rend = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
        }
        
        // �ẹ Ÿ�� ���Ͱ� �ƴ� ���
        if (hide_type == false && hold_type == false)
        {
            Invoke("Mob_Action", 1f);
        }
        else if(hide_type == true && hold_type == false) 
        {
            Hiding();
        }

        anim.SetInteger("Number", monster_number);
    }

    void FixedUpdate()
    {
        // �� �̵� �ִϸ��̼�
        if (move_set != 0 && hide_type == false) 
        {
            anim.SetBool("Move", true);
        }

        // ���� AI
        if (hide_type == false && hold_type == false)
        {
            // �Ĵٺ��� ( ��� )
            if (monster_number == 20)
            {
                if (move_set == -1)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }

            if (hit_chase == false)
            {
                rigid.velocity = new Vector2(move_set * speed, rigid.velocity.y);
            }

            // �ǰݽ� �÷��̾� ���� / ���� ( ���� �ð� 15 �� )
            else if (hit_chase == true && hit_chase_time <= 15)
            {
                // ����
                GameObject player = GameObject.FindWithTag("Player");
                Transform target = player.transform;
                float dir = target.position.x - transform.position.x;
                move_set = (dir < 0) ? -1 : 1;
                rigid.velocity = new Vector2(move_set * speed, rigid.velocity.y);

                //���� ( ���Ÿ� )
                if (attack_delay <= attack_timmer && attack_type == false)
                {
                    anim.SetBool("Move", false);
                    anim.SetBool("Attack", true);
                    if (monster_number == 20)
                    {
                        speed = 0;
                    }
                    StartCoroutine(Attack_Range());
                    Invoke("Attack_Anim_Off", 1f);

                    attack_timmer = 0;
                }
                //���� ( �ٰŸ�)
                else if (attack_delay <= attack_timmer && attack_type == true)
                {
                    anim.SetBool("Move", false);
                    anim.SetBool("Attack", true);
                    StartCoroutine(Attack_Melee());
                    Invoke("Attack_Anim_Off", 1f);
                    attack_timmer = 0;
                }

                // �ð� ���� ( ������ �ʱ�ȣ �ȵ� )
                if (boss == false)
                {
                    hit_chase_time += Time.deltaTime;
                }
            }

            // ���� ������
            if (attack_timmer <= attack_delay)
            {
                attack_timmer += Time.deltaTime;
            }

            // ���� �� (15��)
            if (hit_chase == true && hit_chase_time >= 15)
            {
                hit_chase = false;
                hit_chase_time = 0f;
                Mob_Action();
            }
        }

        // �ẹ�� ���� AI
        else if (hide_type == true && hold_type == false)
        {
            // �ẹ �� �̵� ����
            if (hide == true && hit_chase == false)
            {
                rigid.velocity = new Vector2(move_set * speed, rigid.velocity.y);
            }

            // �ǰݽ� �� ����
            else if (hide == true && hit_chase == true && hit_chase_time <= 15)
            {
                GameObject player = GameObject.FindWithTag("Player");
                Transform target = player.transform;
                float dir = target.position.x - transform.position.x;
                move_set = (dir < 0) ? -1 : 1;
                rigid.velocity = new Vector2(move_set * speed, rigid.velocity.y);
                
                // �ð� ���� ( ������ �ʱ�ȣ �ȵ� )
                if (boss == false)
                {
                    hit_chase_time += Time.deltaTime;
                }
            }

            // �÷��̾� �ν�
            if (hide_found == true && hide == true)
            {
                Hide_End();
            }

            // ���� ����
            if (hide == false && hit_chase == true && attack_delay <= attack_timmer && attack_type == true)
            {
                anim.SetBool("Attack", true);
                StartCoroutine(Attack_Melee());
                Invoke("Attack_Anim_Off", 1f);
                attack_timmer = 0;
            }

            // ���� ������
            if (attack_timmer <= attack_delay && hit_chase == true && hide == false)
            {
                attack_timmer += Time.deltaTime;
            }

            // ���� �� (15��)
            if (hit_chase == true && hit_chase_time >= 15)
            {
                hit_chase = false;
                hit_chase_time = 0f;
            }
        }

        // ������ ���� AI
        else if (hold_type == true) 
        {
            // �Ĵٺ��� ( ���� )
            if (monster_number == 19)
            {
                GameObject player = GameObject.FindWithTag("Player");
                Transform target = player.transform;
                float dir = target.position.x - transform.position.x;
                move_set = (dir < 0) ? -1 : 1;
                if (move_set == -1)
                {
                    image_Decoy.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    image_Decoy.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }

            // 15�ʰ� �ν�
            if (hit_chase == true && hit_chase_time <= 15) 
            {
                // ����, ���Ÿ� �Ѵ� �ҰŸ� �׳� �̰ɷ� �ϰ� ���� ����Ʈ�� �׸���
                if (attack_delay <= attack_timmer && attack_type == false)
                {
                    // ����
                    if (monster_number == 14)
                    {
                        anim.SetBool("Attack", true);
                        StartCoroutine(Rotate_Attack_1());
                        Invoke("Attack_Anim_Off", 1f);
                    }

                    attack_timmer = 0;
                    // �ð� ����
                    hit_chase_time += Time.deltaTime;
                }
            }

            // ���� ������
            if (attack_timmer <= attack_delay)
            {
                attack_timmer += Time.deltaTime;
            }

            // ���� �� (15��)
            if (hit_chase == true && hit_chase_time >= 15)
            {
                hit_chase = false;
                hit_chase_time = 0f;
            }

        }

        // �� �׷��ֱ� ( ���� ���� )
        Vector2 frontVec = new Vector2(rigid.position.x + fall_width *move_set, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, fall_hight, LayerMask.GetMask("Ground"));
        Debug.DrawRay(frontVec, Vector3.down * fall_hight, new Color(1, 0, 0));

        // �������� ����
        if (rayHit.collider == null && fly_type == false && hold_type == false) 
        {
            // �÷��̾� �������̿��ٸ� ���� ���� �� �ݴ���� �̵� �� �̵� �缳��
            if (hit_chase == true) 
            {
                hit_chase = false;
                hit_chase_time = 0f;
            }
            
            if (hide_type == false)
            {
                CancelInvoke("Mob_Action");
                Invoke("Mob_Action", 3f);
            }

            else if(hide_type == true && hide == true)
            {
                CancelInvoke("Hide_Move");
                Invoke("Hide_Move", 1.5f);
            }
            move_set *= -1;
        }

        // �̹��� �¿� ����
        if (move_set == -1)
        {
            if (monster_number != 19 && monster_number != 20)
            {
                rend.flipX = false;
                monster_arrow_pos.anchoredPosition = new Vector2(player_recogn_value[2], player_recogn_value[3]);
            }
            else
            {
                monster_arrow_pos.anchoredPosition = new Vector2(player_recogn_value[2], player_recogn_value[3]);
            }
        }
        else if (move_set == 1)
        {
            if (monster_number != 19 && monster_number != 20)
            {
                rend.flipX = true;
                monster_arrow_pos.anchoredPosition = new Vector2(player_recogn_value[2] * -1, player_recogn_value[3]);
            }
            else
            {
                monster_arrow_pos.anchoredPosition = new Vector2(player_recogn_value[2], player_recogn_value[3]);
            }
        }

        // ������ ǥ�� �� ��Ʈ ����Ʈ �ߵ�
        if (hit == true)
        {
            GameManager.instance.SFX_Play("mob_hit", GameManager.instance.player_Clips[1], 1);
            // �����
            if (hide_type == false)
            {
                if (dmg == 0)
                {
                    damage_text.text = "Miss";
                    Instantiate(damage_text, dmg_pos.transform.position, Quaternion.Euler(0,0,0));
                    hit = false;
                }
                else
                {
                    // ������ ǥ��
                    damage_text.text = "-" + dmg.ToString();
                    Instantiate(damage_text, dmg_pos.transform.position, Quaternion.Euler(0, 0, 0));
                    hit = false;

                    // �� Ÿ�� ���� �̹���, ����, �з���
                    hit_Effect = true;
                    Mob_Hit();
                    CancelInvoke("Mob_Action");
                    move_set = 0;

                    // �÷��̾� ���� ����
                    hit_chase = true;
                    hit_chase_time = 0f;
                    //�������� 2 ���� �������
                    if (boss == true && gameObject.GetComponent<Boss_Pattern>().pt_bool[2] == true && monster_number == 10)
                    {
                        gameObject.GetComponent<Boss_Pattern>().pt_1[4].GetComponent<Slider>().value -= dmg;
                    }
                    else if (boss == true && gameObject.GetComponent<Boss_Pattern>().pt_bool[1] == true && monster_number == 19)
                    {
                        Debug.Log("sss");
                        gameObject.GetComponent<Boss_Pattern>().pt_1[15].GetComponent<Slider>().value -= dmg;
                    }
                }
            }
            // �ẹ��
            else if (hide_type == true)
            {
                if (dmg == 0)
                {
                    damage_text.text = "Miss";
                    Instantiate(damage_text, dmg_pos.transform.position, Quaternion.Euler(0, 0, 0));
                    hit = false;
                }
                else
                {
                    // ������ ǥ��
                    damage_text.text = "-" + dmg.ToString();
                    Instantiate(damage_text, dmg_pos.transform.position, Quaternion.Euler(0, 0, 0));
                    hit = false;

                    // �� Ÿ�� ���� �̹���, ����, �з���
                    hit_Effect = true;
                    Hide_Mob_Hit();

                    // �÷��̾� ���� ����
                    hit_chase = true;
                    hit_chase_time = 0f;
                }
            }
        }

        // CC�� ( ���� )
        if (mezz[0] == true) 
        {
            speed = 0;
            mezz_timmer[0] += Time.deltaTime;
        }
        if (mezz[0] == true && mezz_time[0] <= mezz_timmer[0]) 
        {
            mezz[0] = false;
            speed = remember_speed;
            mezz_timmer[0] = 0f;
            Destroy(mezz_Effect);
        }

        // �ߵ�
        if (mezz[1] == true) 
        {
            Poison();
            mezz[1] = false;
        }
        if (mezz[1] == false && mezz_time[1] < mezz_timmer[1])
        {
            CancelInvoke("Poison");
            mezz_time[1] = 0f;
            mezz_timmer[1] = 1f;
        }
        else if (mezz[1] == false && mezz_time[1] >= mezz_timmer[1])
        {
            mezz_timmer[1] += Time.deltaTime;
        }

        // Ÿ�� ó�� ( ������ �Ҵ�Ʈ )
        if (deal_area[0] == true) 
        {
            deal_area_timmer[0] += Time.deltaTime;
        }
        // Ÿ�� ó�� ( ����Ʈ��! )
        if (deal_area[1] == true) 
        {
            deal_area_timmer[1] += Time.deltaTime;
        }
        // ���ó��
        if (mhp <= 0)
        {
            GameManager.instance.exp += exp;
            GameManager.instance.score += score;
            
            // ����� ����
            if (GameManager.instance.attack_object[7] == true) 
            {
                GameManager.instance.consume++;
            }

            // ���� ���
            if (boss == false)
            {
                int coin_drop = Random.Range(0, 100);
                if (GameManager.instance.attack_object[20] == true)
                {
                    coin_drop += 15;
                }
                if (coin_drop >= 50 && boss == false)
                {
                    int result_coin = Random.Range(0, drop_max_coin + 1);
                    Transform drop_pos = gameObject.GetComponent<Transform>();
                    for (int i = 0; i < result_coin; i++)
                    {
                        Instantiate(coin, transform.position, transform.rotation);
                    }
                }
            }
            else if (boss == true) 
            {
                int rnd = Random.Range(15, 30);
                for (int i = 0; i < rnd; i++)
                {
                    GameObject co = Instantiate(coin, transform.position, transform.rotation);
                    int ran = Random.Range(-10, 10);
                    co.GetComponent<Rigidbody2D>().velocity = new Vector2(ran, 10f);
                }
            }

            // ���� ������Ʈ ����
            if (boss == true) 
            {
                // 1������
                int leng = gameObject.GetComponent<Boss_Pattern>().pt_1.Length;
                for (int i = 0; i < leng; i++) 
                {
                    gameObject.GetComponent<Boss_Pattern>().pt_1[i].SetActive(false);
                }
                GameObject.FindWithTag("Portal").transform.position = gameObject.transform.position;
            }

            // ����ų ��� ����
            if(GameManager.instance.area_num == 4 && monster_number == 19) 
            {
                if (GameManager.instance.stage_num == 7)
                {
                    dialog.SetActive(true);
                    dialog.GetComponent<Story_Doal>().clear_game = true;
                    dialog.GetComponent<Story_Doal>().now = 0;
                    dialog.GetComponent<Story_Doal>().end = 9;
                }
            }

            // ���� ��ȯ ( ���� ����ũ ���� )
            if (GameObject.FindWithTag("Shop") == false) 
            {
                // ����ų�� Ȯ��
                if (boss == true)
                {
                    Vector3 p_pos = GameObject.FindWithTag("Player").transform.position;
                    if (monster_number != 15)
                    {
                        Instantiate(GameObject.FindWithTag("Manager").GetComponent<BattleManager>().Shop_Keeper, p_pos, Quaternion.Euler(0, 0, 0));
                    }
                    else
                    {
                        Instantiate(GameObject.FindWithTag("Manager").GetComponent<BattleManager>().Shop_Keeper, new Vector3(-21.4f, 3.7f, -2), Quaternion.Euler(0, 0, 0));
                        gameObject.GetComponent<Boss_Pattern>().pt_1[9].SetActive(true);
                    }
                }
                else 
                {
                    int rnd = Random.Range(0, 100);
                    if (rnd >= 92)
                    {
                        Vector3 p_pos = GameObject.FindWithTag("Player").transform.position;
                        Instantiate(GameObject.FindWithTag("Manager").GetComponent<BattleManager>().Shop_Keeper, p_pos, Quaternion.Euler(0, 0, 0));
                    }
                }
            }

            // ���� ų ����
            if (monster_number == 3 && GameManager.instance.playerdata.Achievements[5] == false && GameManager.instance.achievement_bool == false)
            {
                GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                GameManager.instance.achievement_bool = true;
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n�׷����� ������";
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[0];
                Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                GameManager.instance.playerdata.Achievements[5] = true;
                GameManager.instance.Save_PlayerData_ToJson();
            }

            if (monster_number == 10 && GameManager.instance.playerdata.Achievements[6] == false && GameManager.instance.achievement_bool == false)
            {
                GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                GameManager.instance.achievement_bool = true;
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n";
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[1];
                Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                GameManager.instance.playerdata.Achievements[6] = true;
                GameManager.instance.Save_PlayerData_ToJson();
            }

            if (monster_number == 15 && GameManager.instance.playerdata.Achievements[7] == false && GameManager.instance.achievement_bool == false)
            {
                GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                GameManager.instance.achievement_bool = true;
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n������ ��";
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[2];
                Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                GameManager.instance.playerdata.Achievements[7] = true;
                GameManager.instance.Save_PlayerData_ToJson();
            }

            if (monster_number == 19 && GameManager.instance.playerdata.Achievements[8] == false && GameManager.instance.achievement_bool == false)
            {
                GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                GameManager.instance.achievement_bool = true;
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\nȭ���� ����";
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[3];
                Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                GameManager.instance.playerdata.Achievements[8] = true;
                GameManager.instance.Save_PlayerData_ToJson();
            }

            // 3é�� ����
            if (monster_number == 15) 
            {
                Destroy(gameObject.GetComponent<Boss_Pattern>().pt_1[8]);
                GameObject.FindWithTag("Portal").transform.position = GameObject.FindWithTag("Player").transform.position;
            }
            // 4é�� �����ش�
            if (monster_number == 19) 
            {
                GameManager.instance.coin += 1500;
                Destroy(image_Decoy);
            }
            Destroy(gameObject);
        }
    }

    // �� �̵�
    public void Mob_Action() 
    {
        // ������ �������
        move_set = Random.Range(-1, 2);
        // n �ʵ��� ����
        float think_rnd = Random.Range(2f, 5f);
        // ������ �ֱ�
        if (move_set == 0) 
        {
            think_rnd = 0.4f;
            anim.SetBool("Move", false);
        }

        // ��� �Լ�
        Invoke("Mob_Action", think_rnd);
    }

    public void Mob_Action_Cancle() 
    {
        CancelInvoke("Mob_Action");
    }

    /* ------ �ẹ�� ���� ------ */
    // �ẹ�ϱ� ( �ẹ �ִϸ��̼� ���� )
    public void Hiding() 
    {
        anim.SetBool("Hide", true);
        gameObject.layer = 15;
        hide = true;
    }
    // �̵� ����
    public void Hide_Move() 
    {
        anim.SetBool("Move", true);
        // ������ �������
        move_set = Random.Range(-1, 2);
        // n �ʵ��� ����
        float think_rnd = Random.Range(2f, 5f);
        // ������ �ֱ�� 0.2�ʸ�
        if (move_set == 0)
        {
            think_rnd = 0.1f;
        }
        // ��� �Լ�
        Invoke("Hide_Move", think_rnd);
    }

    public void Hide_End() 
    {
        hide_found = false;
        anim.SetBool("Move", false);
        CancelInvoke("Hide_Move");
    }

    public void Hide_False() 
    {
        hide = false;
        gameObject.layer = 7;
        anim.SetBool("Hide", false);
        float rndsec = Random.Range(3f, 6.5f);
        Invoke("Hiding", rndsec);
    }

    // ���� ó��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            move_set *= -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("patrol")) 
        {   
            move_set *= -1;
            CancelInvoke("Mob_Action");
            Invoke("Mob_Action", 4f);
        }
        else if (collision.gameObject.CompareTag("Laser_Arrow") && GameManager.instance.attack_object_cnt[25] >= 2) 
        {
            int percent = Random.Range(0, 101);
            if (percent >= 80)
            {
                Instantiate(collision.gameObject.GetComponent<Charge_Shot_Arrow>().arrow_Create[2], gameObject.transform.position, transform.rotation);
            }
        }
        if (collision.gameObject.CompareTag("Explosion"))
        {
            collision.gameObject.GetComponent<Item_Object_6_2>().cnt++;
            dmg = collision.gameObject.GetComponent<Item_Object_6_2>().dmg;
            mhp -= dmg;
            hit = true;
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("dmg_area"))
        {
            deal_area[0] = true;
            if (collision.gameObject.GetComponent<Item_Object_5>().deal_delay_time < deal_area_timmer[0])
            {
                mhp -= collision.gameObject.GetComponent<Item_Object_5>().dmg;
                dmg = collision.gameObject.GetComponent<Item_Object_5>().dmg;
                hit = true;
                deal_area_timmer[0] = 0f;
            }
        }

        if (collision.gameObject.CompareTag("Lightning")) 
        {
            deal_area[1] = true;
            if (collision.gameObject.GetComponent<Lightning>().timmer <= deal_area_timmer[1]) 
            {
                int damage = mdef - GameManager.instance.atk;
                if (damage >= 0) 
                {
                    damage = -1;
                }
                mhp += damage;
                dmg = damage * -1;
                hit = true;
                deal_area_timmer[1] = 0f;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("dmg_area"))
        {
            deal_area[0] = false;
        }
    }

    // �ߵ�����
    public void Poison() 
    {
        int damage = (int)(GameManager.instance.atk * GameManager.instance.attack_object_cnt[6]);
        if (damage <= 1) 
        {
            damage = 1;
        }
        mhp -= damage;
        dmg = damage;
        // ������ ǥ��
        damage_text.text = "-" + dmg.ToString();
        Instantiate(damage_text, dmg_pos.transform.position, Quaternion.Euler(0,0,0));
        Invoke("Poison", 0.7f);
    }

    //�� ������ �з���
    public void Mob_Hit()
    {
        anim.SetBool("Hit", true);
        // �ִϸ��̼� �ߴ� ( �����̱� )
        anim.SetBool("Move", false);
        // ���� �̼��� �������ʾ�
        if (boss == false)
        {
            speed = 0;
        }
        else if (boss == true) 
        {
            speed /= 2;
        }

        if (GameManager.instance.attack_object[13] == false)
        {
            if (boss == false)
            {
                if (hold_type == false)
                {
                    rigid.AddForce(new Vector2((knockback + GameManager.instance.mob_knockback) * (move_set * -1), 0f) * 3, ForceMode2D.Impulse);
                }
            }
           else if (boss == true)
            {
                if(hold_type == false)
                {
                    rigid.AddForce(new Vector2(((knockback / 2) + (GameManager.instance.mob_knockback / 2)) * (move_set * -1), 0f) * 3, ForceMode2D.Impulse);
                }
            }
        }
        else if (GameManager.instance.attack_object[13] == true)
        {
            if (boss == false)
            {
                if (hold_type == false)
                {
                    rigid.AddForce(new Vector2((knockback + GameManager.instance.mob_knockback) * move_set, 0.55f) * 3, ForceMode2D.Impulse);
                }
            }
            else if (boss == true)
            {
                if (hold_type == false)
                {
                    rigid.AddForce(new Vector2(((knockback / 2) + (GameManager.instance.mob_knockback / 2)) * move_set, 0.55f) * 3, ForceMode2D.Impulse);
                }
            }
        }
        Invoke("Hit_end", 0.5f);
    }

    // �ẹ�� �ǰ�
    public void Hide_Mob_Hit()
    {
        anim.SetBool("Hit", true);
        Invoke("Hit_end", 0.5f);
    }

    // ��Ʈ ���� ����
    public void Hit_end()
    {
        speed = remember_speed;
        if (hide_type == false)
        {
            Attack_Anim_Off();
        }
        else
        {
            Hide_Attack_Anim_Off();
        }
        //anim.SetBool("Move", true);
        hit_Effect = false;
    }

    // ���� �̸�� ����
    public void Attack_Anim_Off()
    {
        anim.SetBool("Hit", false);
        if (monster_number != 19)
        {
            anim.SetBool("Attack", false);
        }
        if (monster_number == 20) 
        {
            speed = remember_speed;
        }
        anim.SetBool("Move", true);
    }

    // ���� �̸�� ����(�ẹ��)
    public void Hide_Attack_Anim_Off()
    {
        anim.SetBool("Hit", false);
        anim.SetBool("Attack", false);
    }

    IEnumerator Attack_Range() 
    {
        monster_arrow.GetComponent<Monster_Arrow>().atk = atk;
        monster_arrow.GetComponent<Monster_Arrow>().speed = arrow_speed;
        monster_arrow.GetComponent<Monster_Arrow>().parent_monster = gameObject;
        monster_arrow.GetComponent<Monster_Arrow>().move_set = move_set;
        monster_arrow.GetComponent<Monster_Arrow>().num = monster_number;

        for (int i = 0; i < attack_cnt; i++) 
        {
            Instantiate(monster_arrow, monster_arrow_pos.position, Quaternion.Euler(0,0,0));
            yield return new WaitForSeconds(attack_cnt_time);
        }
    }
    IEnumerator Attack_Melee() 
    {
        monster_melee.GetComponent<Monster_Melee>().atk = atk;
        monster_melee.GetComponent<Monster_Melee>().number = monster_number;
        monster_melee.GetComponent<Monster_Melee>().parent_monster = gameObject;
        monster_melee.GetComponent<Monster_Melee>().monster_pos = monster_arrow_pos;
        monster_melee.GetComponent<Monster_Melee>().dir = move_set;
        float a = arrow_size / 10 * 10;
        float y_pos = monster_arrow_pos.position.y + ((a - arrow_size) * -1);
        Vector2 pos = new Vector2(monster_arrow_pos.position.x, y_pos);
        // ȭ������
        if (monster_number == 18)
        {
            if (move_set >= 1)
            {
                pos = new Vector2(pos.x, pos.y - 0.9f);
            }
            else if (move_set < 0)
            {
                pos = new Vector2(pos.x, pos.y - 0.9f);
            }
        }

        for (int i = 0; i < attack_cnt; i++) 
        {
            // �� ���� ��Ŀ�� ����
            if (monster_number == 2)
            {
                if (move_set >= 1)
                {
                    pos = new Vector2(pos.x + 1.5f, pos.y);
                }
                else if (move_set < 0)
                {
                    pos = new Vector2(pos.x - 1.5f, pos.y);
                }
            }

            // �� ������ ��Į��
            if (monster_number == 9)
            {
                if (move_set >= 1)
                {
                    pos = new Vector2(pos.x + 3f, (transform.position.y - 0.7f));
                }
                else if (move_set < 0)
                {
                    pos = new Vector2(pos.x - 3f, (transform.position.y - 0.7f));
                }
            }

            // ��� ���� ���� ��Ŀ�� ����
            if (monster_number == 16)
            {
                if (move_set >= 1)
                {
                    pos = new Vector2(pos.x + 4f, pos.y);
                }
                else if (move_set < 0)
                {
                    pos = new Vector2(pos.x - 4f, pos.y);
                }
            }

            // ȭ������
            if (monster_number == 18)
            {
                if (move_set >= 1)
                {
                    pos = new Vector2(pos.x + 4f, pos.y);
                }
                else if (move_set < 0)
                {
                    pos = new Vector2(pos.x - 4f, pos.y);
                }
            }

            Instantiate(monster_melee, pos, transform.rotation);
            yield return new WaitForSeconds(attack_cnt_time);
        }
    }
    IEnumerator Rotate_Attack_1() 
    {
        monster_arrow_ro.GetComponent<Rotation_arrow>().boss = false;
        monster_arrow_ro.GetComponent<Rotation_arrow>().num = monster_number;
        for (int fireAngle = attack_angle[0]; fireAngle < attack_angle[1]; fireAngle += attack_angle[2])
        {
            monster_arrow_ro.GetComponent<Rotation_arrow>().dmg = atk;
            GameObject tempObject = Instantiate(monster_arrow_ro, gameObject.GetComponent<Transform>(), true);
            Vector2 dir = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));

            tempObject.transform.right = dir;
            tempObject.transform.position = transform.position;
        }
        yield return new WaitForSeconds(0f);
    }

}
