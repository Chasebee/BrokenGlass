using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class Player_Set : MonoBehaviour
{
    //anim.Rebind(); => �ִϸ��̼� ���ε�
    Animator anim;
    Rigidbody2D rigid;
    CapsuleCollider2D p_collider;
    float charge_time, charge_timer;
    [SerializeField]
    List<SpriteRenderer> rends;
    public SpriteResolver[] top_sprite;
    public SpriteResolver[] bottom_sprite;
    public SpriteRenderer weapon_sprite;

    public GameObject arrow_pos, melee_weapon, charge_pos, charge_bar, charge_gage;
    public Sprite[] head_rend;
    public LayerMask layers;

    // �ٶ󺸴� ���� ����
    public int h;
    public int dir;

    public float knockback_power;

    public int jump_cnt, maxjump_cnt, attack_type;
    float maxSpeed;
    public bool hit, atk_now, skill_now, hold, default_db, isGrounded, laddering;
    public int skill_number, quick_number;

    void Start()
    {
        anim = GetComponent<Animator>();
        p_collider = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

        rends = new List<SpriteRenderer>();
        foreach (var a in GetComponentsInChildren<SpriteRenderer>())
        {
            rends.Add(a);
        }
        Bow_Attack_Img_1();
        Equiping();
        // Attack_Img_1 ���� attack_type �� �����Ѵ�.
        anim.SetInteger("Attack_Type", attack_type);
        anim.SetFloat("Attack_Speed", GameManager.instance.attack_speed);
        // �÷��̾� ��ġ
        if (!GameManager.instance.portal_bool)
        {
            transform.position = new Vector2(GameManager.instance.save_p_pos[0], GameManager.instance.save_p_pos[1]);
        }
        else if (GameManager.instance.portal_bool)
        {
            transform.position = new Vector2(GameManager.instance.chr_start[0], GameManager.instance.chr_start[1]);
            GameManager.instance.portal_bool = false;
        }
    }

    void Update()
    {
        maxjump_cnt = GameManager.instance.max_jump;
        anim.SetBool("Attack", atk_now);
        anim.SetBool("Skill", skill_now);
        
        // ����/��� ��ü
        if (GameManager.instance.equipment_change == true)
        {
            Equiping();
            GameManager.instance.equipment_change= false;
        }
        // Ȧ����
        if (hold == true)
        {
            charge_timer += Time.deltaTime;
            charge_bar.SetActive(true);
            charge_bar.transform.position = Camera.main.WorldToScreenPoint(charge_pos.transform.position);
            charge_gage.GetComponent<Image>().fillAmount = charge_timer / charge_time;
        }

        // �ִ�ӵ� ����
        maxSpeed = GameManager.instance.speed;
        
        // �̵� ����
        if (!atk_now && !anim.GetBool("Attack"))
        {
            HandleMovementInput();
        }

        // ����
        if (Input.GetKeyDown(KeySet.keys[Key_Action.Jump]) && jump_cnt < maxjump_cnt)
        {
            Jump();
        }

        // ����
        if (Input.GetKeyDown(KeySet.keys[Key_Action.Attack]) && !anim.GetBool("Jump") && attack_type != 0)
        {
            if (!atk_now)
            {
                Attack(false);
            }
            else if (atk_now) 
            {
                if (attack_type == 1 && !default_db)
                {
                    if (!skill_now)
                    {
                        default_db = true;
                    } 
                    else if(skill_now && GameManager.instance.sword_skill[5] >= 1) 
                    {
                        default_db = true;
                    }
                }
            }
        }

        // ��ų 1��
        if (Input.GetKey(KeySet.keys[Key_Action.skill_1]) && !atk_now && !anim.GetBool("Jump") && GameManager.instance.quick_slot[0] != null)
        {
            Skill(0);
        }
        // ��ų 1�� Ȧ�� ��
        if (Input.GetKeyUp(KeySet.keys[Key_Action.skill_1]))
        {
            Skill_Hold_End(0);
        }
        
        // ��ų 2��
        if (Input.GetKey(KeySet.keys[Key_Action.skill_2]) && !atk_now && !anim.GetBool("Jump") && GameManager.instance.quick_slot[1] != null)
        {
            Skill(1);
        }
        // ��ų 2�� Ȧ�� ��
        if (Input.GetKeyUp(KeySet.keys[Key_Action.skill_2]))
        {
            Skill_Hold_End(1);
        }
        
        // ��ų 3��
        if (Input.GetKey(KeySet.keys[Key_Action.skill_3]) && !atk_now && !anim.GetBool("Jump") && GameManager.instance.quick_slot[2] != null)
        {
            Skill(2);
        }
        // ��ų 3�� Ȧ�� ��
        if (Input.GetKeyUp(KeySet.keys[Key_Action.skill_3]))
        {
            Skill_Hold_End(2);
        }
        
        // ��ų 4��
        if (Input.GetKey(KeySet.keys[Key_Action.skill_4]) && !atk_now && !anim.GetBool("Jump") && GameManager.instance.quick_slot[3] != null)
        {
            Skill(3);
        }
        // ��ų 4�� Ȧ�� ��
        if (Input.GetKeyUp(KeySet.keys[Key_Action.skill_4]))
        {
            Skill_Hold_End(3);
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleGroundCheck();
        HandleHitDetection();
        Jump_Ground_Ignore();
        Laddering();
    }

    void Jump()
    {
        anim.SetBool("Jump", true);
        rigid.linearVelocity = Vector2.up * GameManager.instance.jump_power;
        jump_cnt++;
    }

    void HandleGroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        Debug.DrawRay(rigid.position, Vector2.down, Color.red);
        RaycastHit2D rayJump = Physics2D.Raycast(rigid.position, Vector2.down, 1.2f, LayerMask.GetMask("Ground"));
        if (rayJump.collider != null)
        {
            isGrounded = true;
        }

        if (isGrounded && !wasGrounded)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Land", true);
            atk_now = false;
        }
        else if (!isGrounded && wasGrounded)
        {
            anim.SetBool("Jump", true);
        }
    }

    public void Landing()
    {
        anim.SetBool("Land", false);
        // ������ ���� �ʱ�ȭ
        jump_cnt = 0;
    }

    void HandleMovementInput()
    {
        if (Input.GetKeyUp(KeySet.keys[Key_Action.Right]) || Input.GetKeyUp(KeySet.keys[Key_Action.Left]))
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Move", false);
            h = 0;
        }
        else if (Input.GetKey(KeySet.keys[Key_Action.Right]))
        {
            h = 1;
            dir = 1;
            anim.SetBool("Move", true);
            anim.SetBool("Idle", false);
        }
        else if (Input.GetKey(KeySet.keys[Key_Action.Left]))
        {
            h = -1;
            dir = -1;
            anim.SetBool("Move", true);
            anim.SetBool("Idle", false);
        }
    }
    
    void HandleMovement()
    {
        if (GameManager.instance.hp >= 1 && !atk_now)
        {
            if (h == 1)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (h == -1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            rigid.linearVelocity = new Vector2(h * maxSpeed, rigid.linearVelocity.y);
        }

        if (h == 0 || atk_now)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // �ִ� �ӵ� ����
        if (rigid.linearVelocity.x > maxSpeed)
        {
            rigid.linearVelocity = new Vector2(maxSpeed, rigid.linearVelocity.y);
        }
        else if (rigid.linearVelocity.x < -maxSpeed)
        {
            rigid.linearVelocity = new Vector2(-maxSpeed, rigid.linearVelocity.y);
        }
    }

    void HandleHitDetection()
    {
        Vector2 boxSize = p_collider.size;
        Vector2 boxCenter = rigid.position + p_collider.offset;
        RaycastHit2D pcast = Physics2D.BoxCast(boxCenter, boxSize, 0f, Vector2.down, 0, layers);

        if (pcast.collider != null && gameObject.layer == 3)
        {
            hit = true;
            gameObject.layer = 8;
            rends[10].sprite = head_rend[1];
            // ���� ������
            if (pcast.collider.gameObject.layer != 11)
            {
                knockback_power = pcast.collider.GetComponent<Monster_Normal>().knockback;
                foreach (var a in rends)
                {
                    a.color = new Color(1, 1, 1, 0.8f);
                }

                Vector2 knockbackDirection = (rigid.position - pcast.collider.attachedRigidbody.position).normalized;
                knockbackDirection.y *= 0.08f;
                if (rigid.linearVelocity.y < 0)
                {
                    knockbackDirection.y *= 6;
                }
                int dmg = pcast.collider.GetComponent<Monster_Normal>().atk - GameManager.instance.def;
                if (dmg <= 0) { dmg = 0; }
                GameManager.instance.hp -= dmg;

                StartCoroutine(Hit_Knockback(knockbackDirection));
                StartCoroutine(Player_Hit());
            }
            else 
            {
                knockback_power = pcast.collider.GetComponent<Monster_Projectile>().knock;
                foreach (var a in rends)
                {
                    a.color = new Color(1, 1, 1, 0.8f);
                }

                Vector2 knockbackDirection = (rigid.position - pcast.collider.attachedRigidbody.position).normalized;
                knockbackDirection.y *= 0.08f;
                if (rigid.linearVelocity.y < 0)
                {
                    knockbackDirection.y *= 6;
                }
                int dmg = pcast.collider.GetComponent<Monster_Projectile>().atk - GameManager.instance.def;
                if (dmg <= 0) { dmg = 0; }
                GameManager.instance.hp -= dmg;
                StartCoroutine(Hit_Knockback(knockbackDirection));
                StartCoroutine(Player_Hit());
            }
        }
    }

    void Jump_Ground_Ignore() 
    {
        if (!laddering) 
        {
            int p_layer, g_layer, in_layer;
            p_layer = LayerMask.NameToLayer("Player");
            in_layer = LayerMask.NameToLayer("Infinity");
            g_layer = LayerMask.NameToLayer("Ground");
            
            // ������ ���� ���
            if (anim.GetBool("Jump") && rigid.linearVelocity.y > 0)
            {
                Physics2D.IgnoreLayerCollision(p_layer, g_layer, true);
                Physics2D.IgnoreLayerCollision(in_layer, g_layer, true);
            }
            else
            {
                Physics2D.IgnoreLayerCollision(p_layer, g_layer, false);
                Physics2D.IgnoreLayerCollision(in_layer, g_layer, false);
            }
        }
    }

    void Laddering()
    {
        int p_layer, g_layer, in_layer;
        p_layer = LayerMask.NameToLayer("Player");
        in_layer = LayerMask.NameToLayer("Infinity");
        g_layer = LayerMask.NameToLayer("Ground");

        // ��ٸ� ����� ���� �̵� ���� ó��
        if (laddering)
        {
            // ���� �̵�
            if (Input.GetKey(KeySet.keys[Key_Action.Up]))
            {
                Physics2D.IgnoreLayerCollision(p_layer, g_layer, true);
                Physics2D.IgnoreLayerCollision(in_layer, g_layer, true);
                rigid.linearVelocity = new Vector2(0, 1 * maxSpeed);
                anim.SetBool("Move", true);
            }
            // �Ʒ��� �̵�
            else if (Input.GetKey(KeySet.keys[Key_Action.Down]))
            {
                Physics2D.IgnoreLayerCollision(p_layer, g_layer, true);
                Physics2D.IgnoreLayerCollision(in_layer, g_layer, true);
                rigid.linearVelocity = new Vector2(0, -1 * maxSpeed);
                anim.SetBool("Move", true);
            }
            // �ƹ� Ű�� ������ ������ ����
            else
            {
                rigid.linearVelocity = new Vector2(0, 0);
                anim.SetBool("Move", false);
            }

            // ��ٸ� ������ ����ų�, ���� Ű ���� ������ ��ٸ� ��� ����
            if (Input.GetKey(KeySet.keys[Key_Action.Jump]))
            {
                laddering = false; // ��ٸ� ��� ����
                anim.SetBool("Ladder", laddering);
                rigid.gravityScale = 1; // �߷� ����
                Physics2D.IgnoreLayerCollision(p_layer, g_layer, false);
                Physics2D.IgnoreLayerCollision(in_layer, g_layer, false);
            }
        }
    }

    void Equiping()
    {
        if (GameManager.instance.body != null)
        {
            for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++)
            {
                if (GameManager.instance.allEquipItems[i].item_id == GameManager.instance.body.item_id)
                {
                    for (int l = 0; l < top_sprite.Length; l++)
                    {
                        top_sprite[l].SetCategoryAndLabel(top_sprite[l].GetCategory(), GameManager.instance.body.item_id.ToString());
                    }
                }
            }
        }
        else 
        {
            for (int i = 0; i < top_sprite.Length; i++) 
            {
                top_sprite[i].SetCategoryAndLabel(top_sprite[i].GetCategory(), "0");
            }
        }

        if (GameManager.instance.bottom != null)
        {
            for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++)
            {
                if (GameManager.instance.allEquipItems[i].item_id == GameManager.instance.bottom.item_id)
                {
                    for (int l = 0; l < bottom_sprite.Length; l++)
                    {
                        bottom_sprite[l].SetCategoryAndLabel(bottom_sprite[l].GetCategory(), GameManager.instance.bottom.item_id.ToString());
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < bottom_sprite.Length; i++)
            {
                bottom_sprite[i].SetCategoryAndLabel(bottom_sprite[i].GetCategory(), "0");
            }
        }

        anim.Rebind();
        if (GameManager.instance.weapon != null)
        {
            anim.SetInteger("Attack_Type", GameManager.instance.weapon.attack_type);
            anim.SetFloat("Attack_Speed", GameManager.instance.attack_speed);
            Bow_Attack_Img_1();
        }
        else
        {
            anim.SetInteger("Attack_Type", 0);
            anim.SetFloat("Attack_Speed", 1.0f);
            weapon_sprite.sprite = null;
        }
    }

    IEnumerator Hit_Knockback(Vector2 dir)
    {
        rigid.AddForce(dir * knockback_power, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.07f);
    }

    IEnumerator Player_Hit()
    {
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds(1.1f);

        hit = false;
        gameObject.layer = 3;
        rends[10].sprite = head_rend[0];
        foreach (var a in rends)
        {
            a.color = new Color(1, 1, 1, 1);
        }

        if (h == 0)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }
      
    // ���� ���� + 2024/09/05 ���������ؼ�... ���⼭ ������ ������ �迭������ �����״� �ű⼭ ���ؼ� �������.
    void Attack(bool skill)
    {
        atk_now = true;
        // �� Ÿ�� ���� 
        if (attack_type == 1)
        {
            if (!skill)
            {
                melee_weapon.GetComponent<Melee>().Attack_Start();
            }
            melee_weapon.gameObject.tag = "Melee";
        }

        // ��ų�� ��� �� ���
        if (skill == true)
        {
            skill_now = true;
        }
        anim.SetInteger("Attack_Type", attack_type);
    }
    
    // ���ݽ� ���� �̹��� ��ü
    public void Bow_Attack_Img_1()
    {
        if (GameManager.instance.weapon != null)
        {
            for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++)
            {
                if (GameManager.instance.allEquipItems[i].item_id == GameManager.instance.weapon.item_id)
                {
                    weapon_sprite.sprite = GameManager.instance.allEquipItems[i].item_img[0];
                    break;
                }
            }
            attack_type = GameManager.instance.weapon.attack_type;
        }        
    }
    public void Bow_Attack_Img_2()
    {
        for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++)
        {
            if (GameManager.instance.allEquipItems[i].item_id == GameManager.instance.weapon.item_id)
            {
                weapon_sprite.sprite = GameManager.instance.allEquipItems[i].item_img[1];
                break;
            }
        }
    }
    public void Bow_Attack_Img_3()
    {
        // Ȧ�������� �����ϱ� ����
        anim.SetBool("Hold", hold);
        for (int i = 0; i < GameManager.instance.allEquipItems.Length; i++)
        {
            if (GameManager.instance.allEquipItems[i].item_id == GameManager.instance.weapon.item_id)
            {
                weapon_sprite.sprite = GameManager.instance.allEquipItems[i].item_img[2];
                break;
            }
        }
    }

    // ��ų �޼ҵ�
    public void Skill(int num) 
    {
        // �� �迭
        if (attack_type == 1 && GameManager.instance.quick_slot[num] != null && GameManager.instance.quick_slot[num].skill_type == 1 && GameManager.instance.mp >= GameManager.instance.quick_slot[num].use_mp) 
        {
            int a = GameManager.instance.quick_slot[num].skill_num;
            quick_number = num;

            switch (a) 
            {
                case 3:
                    Attack(true);
                    skill_number = a;
                    melee_weapon.GetComponent<Melee>().Skill_Start(a);
                    break;
            }
            GameManager.instance.mp -= GameManager.instance.quick_slot[num].use_mp;
        }
        // Ȱ �迭
        if (attack_type == 2 && GameManager.instance.quick_slot[num] != null && GameManager.instance.quick_slot[num].skill_type == 2 && GameManager.instance.mp >= GameManager.instance.quick_slot[num].use_mp)
        {
            int a = GameManager.instance.quick_slot[num].skill_num;
            quick_number = num;
            // ��ų ��ȣ�� ����
            switch (a)
            {
                case 3:
                    Attack(true);
                    skill_number = a;
                    break;
                case 4:
                    Attack(true);
                    skill_number = a;
                    hold = true;
                    charge_time = GameManager.instance.quick_slot[num].charge_time;
                    break;
            }
            GameManager.instance.mp -= GameManager.instance.quick_slot[num].use_mp;
        }
        // ���� �迭
        if (attack_type == 3 && GameManager.instance.quick_slot[num] != null && GameManager.instance.quick_slot[num].skill_type == 3 && GameManager.instance.mp >= GameManager.instance.quick_slot[num].use_mp)
        {
            int a = GameManager.instance.quick_slot[num].skill_num;
            quick_number = num;
            // ��ų ��ȣ�� ����
            switch (a)
            {
                case 3:
                    Attack(true);
                    skill_number = a;
                    break;
                case 4:
                    Attack(true);
                    skill_number = a;
                    break;
                case 6:
                    Attack(true);
                    skill_number = a;
                    break;
                case 8:
                    Attack(true);
                    skill_number = a;
                    break;
            }
            GameManager.instance.mp -= GameManager.instance.quick_slot[num].use_mp;
        }
    }
    
    // ��ų Ȧ�� �� �޼ҵ�
    public void Skill_Hold_End(int num)
    {            
        // Ȱ�迭
        if (GameManager.instance.quick_slot[num] != null && GameManager.instance.quick_slot[num].skill_type == 2 && hold == true)
        {
            hold = false;
            anim.SetBool("Hold", hold);
        }

        charge_bar.SetActive(false);
    }
    // ���� / ��ų ��� ��
    public void Attack_End()
    {
        // �⺻���� ��
        if (attack_type == 1)
        {
            // �̰ɷ� �� 1���� �����°� üũ�Ѵ�
            melee_weapon.GetComponent<Melee>().piercing_object.Clear();
            melee_weapon.gameObject.tag = "Untagged";
        }
        // �⺻���� Ȱ
        else if (attack_type == 2)
        {
            Vector3 ar = new Vector3(arrow_pos.transform.position.x, arrow_pos.transform.position.y, -10);
            // �⺻
            if (skill_now == false)
            {
                GameObject arrow = Instantiate(GameManager.instance.attack_object[0], ar, transform.rotation);
                Arrow a = arrow.GetComponent<Arrow>();
                a.dmg = Random.Range(GameManager.instance.atk * 0.85f, GameManager.instance.atk * 1.15f);
                a.attack_type = attack_type;
            }
            // ��ų
            else if (skill_now == true)
            {
                int dmg = 0;
                float multiple = 0;
                int sk_var = GameManager.instance.bow_skill[skill_number];
                if (sk_var >= 1 && sk_var <= 3)
                {
                    multiple = GameManager.instance.quick_slot[quick_number].skill_var[1];
                }
                else if (sk_var >= 4 && sk_var <= 7)
                {
                    multiple = GameManager.instance.quick_slot[quick_number].skill_var[2];
                }
                else if (sk_var >= 8 && sk_var <= 11)
                {
                    multiple = GameManager.instance.quick_slot[quick_number].skill_var[3];
                }
                else if (sk_var >= 12 && sk_var <= 15)
                {
                    multiple = GameManager.instance.quick_slot[quick_number].skill_var[4];
                }
                else if (sk_var >= 16 && sk_var <= 19)
                {
                    multiple = GameManager.instance.quick_slot[quick_number].skill_var[5];
                }
                else if (sk_var == 20)
                {
                    multiple = GameManager.instance.quick_slot[quick_number].skill_var[6];
                }

                dmg = (int)(multiple * Random.Range(GameManager.instance.atk, GameManager.instance.atk * 1.15f));
                
                if (skill_number == 3)
                {
                    Vector3 ar_1 = new Vector3(arrow_pos.transform.position.x, arrow_pos.transform.position.y + 0.2f, -10);
                    Vector3 ar_2 = new Vector3(arrow_pos.transform.position.x, arrow_pos.transform.position.y - 0.2f, -10);
                    
                    GameObject arrow_1 = Instantiate(GameManager.instance.attack_object[0], ar_1, transform.rotation);
                    arrow_1.GetComponent<Arrow>().dmg = dmg;
                    arrow_1.GetComponent<Arrow>().attack_type = attack_type;

                    dmg = (int)(multiple * Random.Range(GameManager.instance.atk, GameManager.instance.atk * 1.15f));
                    GameObject arrow_2 = Instantiate(GameManager.instance.attack_object[0], ar_2, transform.rotation);
                    arrow_2.GetComponent<Arrow>().attack_type = attack_type;
                    arrow_2.GetComponent<Arrow>().dmg = dmg;
                }

                else if (skill_number == 4) 
                {
                    dmg = (int)(multiple * Random.Range(GameManager.instance.magic, GameManager.instance.magic * 1.15f));
                    float skill_var = 1;
                    if (charge_time * 0.3 >= charge_timer)
                    {
                        dmg = (int)(dmg * 0.3f);
                        skill_var = 0.3f;
                        Debug.Log("������ ���� 70%");
                    }
                    else if (charge_time * 0.6 >= charge_timer)
                    {
                        dmg = (int)(dmg * 0.6f);
                        skill_var = 0.6f;
                        Debug.Log("���������� 40%");
                    }
                    else if (charge_time * 0.8 >= charge_timer)
                    {
                        dmg = (int)(dmg * 0.8f);
                        skill_var = 0.8f;
                        Debug.Log("���������� 20%");
                    }
                    GameObject arrow = Instantiate(GameManager.instance.attack_object[1], ar, transform.rotation);
                    arrow.GetComponent<Arrow>().dmg = dmg;
                    arrow.GetComponent<Arrow>().dmg_reset = multiple;
                    arrow.GetComponent<Arrow>().skill_var = skill_var;
                    arrow.GetComponent<Arrow>().attack_type = attack_type;
                    if (charge_time <= charge_timer) 
                    {
                        arrow.GetComponent<Arrow>().full = true;
                    }
                }
                charge_timer = 0;
            }
        }
        atk_now = false;
        // ��ų�� ����� ���
        if (skill_now == true) 
        {
            skill_now = false;
            skill_number = 0;
        }
        
        h = 0;
        default_db = false;
        anim.SetBool("Move", false);
        anim.SetBool("Idle", true);
        anim.SetInteger("Attack_Cnt", 0);
    }

    public void Magic_Attack() 
    {
        Vector3 ar = new Vector3(arrow_pos.transform.position.x, arrow_pos.transform.position.y, -10);
        // �⺻
        if (skill_now == false)
        {
            // arrow ��ũ��Ʈ�� ������� ������ ������ ����
            GameObject arrow = Instantiate(GameManager.instance.magic_object[0], ar, transform.rotation);
            Arrow a = arrow.GetComponent<Arrow>();
            a.dmg = Random.Range(GameManager.instance.magic * 0.85f, GameManager.instance.magic * 1.15f);
            a.attack_type = attack_type;
        }
        // ��ų
        else if (skill_now == true)
        {
            int dmg = 0;
            float multiple = 0;
            int sk_var = GameManager.instance.bow_skill[skill_number];
            if (sk_var >= 1 && sk_var <= 3)
            {
                multiple = GameManager.instance.quick_slot[quick_number].skill_var[1];
            }
            else if (sk_var >= 4 && sk_var <= 7)
            {
                multiple = GameManager.instance.quick_slot[quick_number].skill_var[2];
            }
            else if (sk_var >= 8 && sk_var <= 11)
            {
                multiple = GameManager.instance.quick_slot[quick_number].skill_var[3];
            }
            else if (sk_var >= 12 && sk_var <= 15)
            {
                multiple = GameManager.instance.quick_slot[quick_number].skill_var[4];
            }
            else if (sk_var >= 16 && sk_var <= 19)
            {
                multiple = GameManager.instance.quick_slot[quick_number].skill_var[5];
            }
            else if (sk_var == 20)
            {
                multiple = GameManager.instance.quick_slot[quick_number].skill_var[6];
            }

            dmg = (int)(multiple * Random.Range(GameManager.instance.magic, GameManager.instance.magic * 1.15f));

            // ����źȯ
            if (skill_number == 3)
            {
                Vector3 ar_1 = new Vector3(arrow_pos.transform.position.x, arrow_pos.transform.position.y, -10);

                GameObject arrow_1 = Instantiate(GameManager.instance.magic_object[0], ar_1, transform.rotation);
                arrow_1.GetComponent<Arrow>().dmg = dmg;
                arrow_1.GetComponent <Arrow>().attack_type = attack_type;
            }
            // ������Ż : �÷��� ( ���� )
            else if (skill_number == 4)
            {
                GameManager.instance.elemental[0] = true;
                GameManager.instance.elemental[1] = false;
                GameManager.instance.elemental[2] = false;
            }
            // ������Ż : ������
            else if (skill_number == 6)
            {
                GameManager.instance.elemental[0] = false;
                GameManager.instance.elemental[1] = true;
                GameManager.instance.elemental[2] = false;
            }
            // ������Ż : ����ó
            else if (skill_number == 8)
            {
                GameManager.instance.elemental[0] = false;
                GameManager.instance.elemental[1] = false;
                GameManager.instance.elemental[2] = true;
            }
            charge_timer = 0;
        }
    }

    public void Magic_Attack_End() 
    {
        h = 0;
        default_db = false;
        atk_now = false;
        anim.SetBool("Move", false);
        anim.SetBool("Idle", true);
        anim.SetInteger("Attack_Cnt", 0);
    }

    public void Default_Double_Attack_CHK() 
    {
        if (default_db == true) 
        {
            melee_weapon.GetComponent<Melee>().piercing_object.Clear();
            int a = anim.GetInteger("Attack_Cnt") + 1;
            anim.SetInteger("Attack_Cnt",a);
        }
    }

    public void Sword_Aura()
    {
        if (GameManager.instance.sword_skill[7] >= 1)
        {
            Vector3 ar = new Vector3(arrow_pos.transform.position.x, arrow_pos.transform.position.y, -10);
            GameObject aura = Instantiate(GameManager.instance.attack_object[2], ar, transform.rotation);
            aura.GetComponent<Arrow>().dmg = (int)(Random.Range(GameManager.instance.atk * 0.55f, GameManager.instance.atk * 1.05f));
            aura.GetComponent<Arrow>().attack_type = 1;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Portal")) 
        {
            string map = col.gameObject.GetComponent<Portal>().map_name;
            col.gameObject.GetComponent<Portal>().Effect(map);
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("NPC"))
        {
            if(Input.GetKey(KeySet.keys[Key_Action.Up]))
            {
                GameObject stm = GameObject.FindWithTag("Manager");
                Stage_Manager st = stm.GetComponent<Stage_Manager>();

                st.pause = true;
                st.Npc_Dialogue(col.GetComponent<Npc_Script>());
                st.dial_object[2].GetComponent<Button>().interactable = false;
            }
        }

        if (col.CompareTag("Ladder")) 
        {
            if (Input.GetKey(KeySet.keys[Key_Action.Up]) || Input.GetKey(KeySet.keys[Key_Action.Down])) 
            {
                laddering = true; // ��ٸ� ���� ��ȯ
                anim.SetBool("Ladder", laddering);
                rigid.gravityScale = 0; // �߷� ����
                rigid.linearVelocity = new Vector2(0, 0); // ��ٸ��� ���� �� ����
                jump_cnt = 0;
            }
        }

        if (col.CompareTag("Entrance")) 
        {
            if (Input.GetKey(KeySet.keys[Key_Action.Up])) 
            {
                string map = col.GetComponent<Portal>().map_name;
                Loading_Scene.LoadScene(map);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ladder")) 
        {
            int p_layer, g_layer, in_layer;
            p_layer = LayerMask.NameToLayer("Player");
            in_layer = LayerMask.NameToLayer("Infinity");
            g_layer = LayerMask.NameToLayer("Ground");

            laddering = false; // ��ٸ� ��� ����
            anim.SetBool("Ladder", laddering);
            rigid.gravityScale = 1; // �߷� ����
            Physics2D.IgnoreLayerCollision(p_layer, g_layer, false);
            Physics2D.IgnoreLayerCollision(in_layer, g_layer, false);
        }
    }
}
