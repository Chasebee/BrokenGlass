using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Player_Move : MonoBehaviour
{
    GameObject btm;
    BattleManager bt;

    Rigidbody2D rigid;
    FixedJoint2D fixjo;
    CapsuleCollider2D cap_col;

    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public GameObject arrow;
    public RectTransform arrow_Rect, flying_pos, mezz_pos, charge_bar_pos, ground_pos;
    public List<GameObject> fly_object = new List<GameObject>();
    public GameObject mezz_Effect;
    public GameObject shiled;
    public bool is_rope;
    public bool ground_pound = false;
    public bool[] mezz;
    public int[] mezz_dmg;
    public float[] mezz_timmer;
    public float infinity_delay;

    public float maxSpeed, jumppower;
    public int jump_cnt;
    public int maxjump_cnt = 2;

    public int dash_cnt_l, dash_cnt_r;
    public float dash_input_time;
    public float dash_time;
    public float dash_cool;
    public GameObject dash_effect;
    public float dash_effect_time;
    public GameObject charge_bar, charge_bar_gage;

    public int direction;

    bool hit = false;
    bool portal_use = false;
    public LayerMask layer_select;
    // ������ ���� �����
    Vector3 dirVec;

    // ���̷� ��ĵ�ϴ� ��ü �޾ƿ��� ����
    GameObject scanObject;

    public float h;
    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        fixjo = GetComponent<FixedJoint2D>();
        cap_col = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        btm = GameObject.FindWithTag("Manager");
        bt = GameObject.FindWithTag("Manager").GetComponent<BattleManager>();
        arrow_Rect = arrow.GetComponent<RectTransform>();
        charge_bar = GameObject.Find("Charge_Bar_Dash");
        charge_bar_gage = GameObject.Find("Charge_Gage_Dash");
    }

    void Update()
    {
        // ĳ���� ������
        transform.localScale = new Vector3(GameManager.instance.player_size, GameManager.instance.player_size, -1);

        // ��, �� ����
        if (Input.GetKeyUp(KeySet.keys[Key_Action.Right]) || Input.GetKeyUp(KeySet.keys[Key_Action.Left]))
        {
            h = 0;
        }
        else if (Input.GetKey(KeySet.keys[Key_Action.Left]))
        {
            h = -1;
        }
        else if (Input.GetKey(KeySet.keys[Key_Action.Right]))
        {
            h = 1;
        }

        // �뽬 ����
        if (GameManager.instance.dash == false)
        {
            if (Input.GetKeyDown(KeySet.keys[Key_Action.Left]) && GameManager.instance.attack_object[29] == true && portal_use == false && dash_cool >= 2 && direction == -1)
            {
                dash_cnt_l++;
            }
            else if (Input.GetKeyDown(KeySet.keys[Key_Action.Right]) && GameManager.instance.attack_object[29] == true && portal_use == false && dash_cool >= 2 && direction == 1)
            {
                dash_cnt_r++;
            }
        }
        if (GameManager.instance.attack_object[29] == true)
        {
            if (dash_cool <= 2)
            {
                charge_bar.SetActive(true);
                charge_bar.transform.position = Camera.main.WorldToScreenPoint(gameObject.GetComponent<Player_Move>().charge_bar_pos.transform.position);
                charge_bar_gage.GetComponent<Image>().fillAmount = dash_cool / 2;
                dash_cool += Time.deltaTime;
            }
            else if (dash_cool >= 2)
            {
                charge_bar.SetActive(false);
            }
            if (dash_cnt_l >= 1 || dash_cnt_r >= 1)
            {
                dash_input_time += Time.deltaTime;
            }
            if ((dash_cnt_l >= 1 || dash_cnt_r >= 1) && dash_input_time >= 0.7f)
            {
                dash_cnt_l = 0;
                dash_cnt_r = 0;
                dash_input_time = 0;
            }
            if ((dash_cnt_l >= 2 || dash_cnt_r >= 2) && GameManager.instance.dash == false)
            {
                dash_input_time = 0;
                GameManager.instance.dash = true;
                StartCoroutine(Dash_Time_Set());
            }
        }

        // �� �ǰ� ���� ���� ĳ��Ʈ( ���� ĳ��Ʈ�� �ݸ��� �ƴϿ��� �浹 �Դ´� )
        RaycastHit2D hit_Ray = Physics2D.BoxCast(rigid.position, new Vector2(GameManager.instance.player_size, GameManager.instance.player_size), 0f, Vector2.down, 0.2f, layer_select);

        if (hit_Ray.collider != null && gameObject.layer == 8 && ground_pound == false && shiled.GetComponent<Item_Object3>().oper == false && GameManager.instance.infinity == false)
        {
            // ( �ǰ� ������ ���� ) ( ���� )
            if (hit_Ray.collider.gameObject.CompareTag("Monster2"))
            {
                gameObject.layer = 9;
                PlayerHit(hit_Ray.collider.gameObject.transform.position);

                if (GameManager.instance.attack_object[18] == false)
                {
                    GameManager.instance.hp = GameManager.instance.hp + GameManager.instance.def - hit_Ray.collider.gameObject.GetComponent<Monster2>().atk;
                }
                else if (GameManager.instance.attack_object[18] == true)
                {
                    int rnd = Random.Range(0, 10);
                    if (rnd >= 6)
                    {
                        GameManager.instance.hp = GameManager.instance.hp + GameManager.instance.def - (hit_Ray.collider.gameObject.GetComponent<Monster2>().atk / 2);
                    }
                    else
                    {
                        GameManager.instance.hp = GameManager.instance.hp + GameManager.instance.def - hit_Ray.collider.gameObject.GetComponent<Monster2>().atk;
                    }

                }
            }
            // ( �ǰ� ������ ���� ) ( ��ȯ�� )
            else if (hit_Ray.collider.gameObject.CompareTag("Summon"))
            {
                // ��Ʈ
                if (hit_Ray.collider.GetComponent<Stage_3_Tentacle>().hit_sw == true)
                {
                    gameObject.layer = 9;
                    PlayerHit(hit_Ray.collider.gameObject.transform.position);
                    if (GameManager.instance.attack_object[18] == false)
                    {
                        GameManager.instance.hp = GameManager.instance.hp + GameManager.instance.def - hit_Ray.collider.gameObject.GetComponent<Stage_3_Tentacle>().atk;
                    }
                    else if (GameManager.instance.attack_object[18] == true)
                    {
                        int rnd = Random.Range(0, 10);
                        if (rnd >= 6)
                        {
                            GameManager.instance.hp = GameManager.instance.hp + GameManager.instance.def - (hit_Ray.collider.gameObject.GetComponent<Stage_3_Tentacle>().atk / 2);
                        }
                        else
                        {
                            GameManager.instance.hp = GameManager.instance.hp + GameManager.instance.def - hit_Ray.collider.gameObject.GetComponent<Stage_3_Tentacle>().atk;
                        }

                    }


                    // CC�� ( �̸����� ������ )
                    if (hit_Ray.collider.gameObject.name == "Boss_3_Tentacle_0(Clone)")
                    {
                        int rnd = Random.Range(0, 101);
                        if (rnd <= 20)
                        {
                            Stun(3f);
                        }
                    }
                }
            }
        }
        else if (hit_Ray.collider != null && GameManager.instance.infinity == true) 
        {
            infinity_delay += Time.deltaTime;
            if (infinity_delay >= 0.7f)
            {
                int dg = GameManager.instance.atk;
                hit_Ray.collider.gameObject.GetComponent<Monster2>().mhp -= dg;
                hit_Ray.collider.gameObject.GetComponent<Monster2>().hit = true;
                hit_Ray.collider.gameObject.GetComponent<Monster2>().dmg = dg;
                infinity_delay = 0f;
            }
        }

        // ���� ����
        maxSpeed = GameManager.instance.speed;
        maxjump_cnt = GameManager.instance.jump_cnt;
        jumppower = GameManager.instance.jump_power;

        // ���� mezz[0] = ����
        if (Input.GetKeyDown(KeySet.keys[Key_Action.Jump]) && jump_cnt < maxjump_cnt && mezz[0] == false)
        {
            GameManager.instance.SFX_Play("Jump", GameManager.instance.player_Clips[0], 0.5f);
            anim.SetBool("Jump", true);
            //rigid.AddForce(Vector2.up * GameManager.instance.jump_power, ForceMode2D.Impulse);
            rigid.velocity = Vector2.up * GameManager.instance.jump_power;

            // ���� ����
            fixjo.connectedBody = null;
            fixjo.enabled = false;
            is_rope = false;
            jump_cnt++;
        }

        // ���⶧ ��¦�̲������� ȿ��
        if (Input.GetKeyUp(KeySet.keys[Key_Action.Left]) || Input.GetKeyUp(KeySet.keys[Key_Action.Right]))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        
        // �ִϸ��̼� ���� ( �̵� )
        if (Mathf.Abs(rigid.velocity.x) < 0.1f || jump_cnt > 0)
            anim.SetBool("Idle", true);
        else
            anim.SetBool("Idle", false);

        // ��, ��
        if (h == -1)
        {
            spriteRenderer.flipX = false;
            dirVec = Vector3.left;
            arrow_Rect.anchoredPosition = new Vector2(-1f, 0);
            flying_pos.anchoredPosition = new Vector2(1, 0.8f);
            charge_bar_pos.anchoredPosition = new Vector2(0.7f, 0.35f);

            direction = -1;
        }
        else if (h == 1)
        {
            spriteRenderer.flipX = true;
            dirVec = Vector3.right;
            arrow_Rect.anchoredPosition = new Vector2(1f, 0);
            flying_pos.anchoredPosition = new Vector2(-1, 0.8f);
            charge_bar_pos.anchoredPosition = new Vector2(-0.7f, 0.35f);
            direction = 1;
        }

        /* ��Ʈ ������ (0~1ĭ�� �ߵ� �̷������� 2ĭ��)*/
        // �ߵ�
        if (mezz[1] == true)
        {
            mezz_timmer[1] += Time.deltaTime;
            if (mezz_timmer[0] <= mezz_timmer[1])
            {
                mezz_timmer[1] = 0f;
                GameManager.instance.hp -= mezz_dmg[0];
            }
        }
        // ����
        if (mezz[2] == true)
        {
            mezz_timmer[3] += Time.deltaTime;
            if (mezz_timmer[2] <= mezz_timmer[3])
            {
                mezz_timmer[3] = 0f;
                GameManager.instance.hp -= mezz_dmg[1];
            }
        }
    }

    void FixedUpdate()
    {
        // ����� X�� �� ���� ( ��� ���� )
        if (hit == false && GameManager.instance.dash == false)
        {
            if (h == 0)
            {
                rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        else 
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // �̵� (mezz[0] = ���� �ɸ��� �������̿��� ������)
        if (mezz[0] == false && GameManager.instance.hp >= 1)
        {
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        }

        // �ִ� ���ǵ� maxspeed�� ���Ѱ�
        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -maxSpeed)
        {
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        }

        // ���
        if (GameManager.instance.dash == true) 
        {
            if (GameManager.instance.playerdata.Achievements[23] == false && GameManager.instance.achievement_bool == false)
            {
                GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                GameManager.instance.achievement_bool = true;
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n�װ��� �� �ܻ��Դϴ�.";
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[10];
                Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                GameManager.instance.playerdata.Achievements[23] = true;
                GameManager.instance.Save_PlayerData_ToJson();
            }
            //  ���
            if (dash_cnt_l >= 2)
            {
                rigid.AddForce(Vector2.left * (GameManager.instance.speed * GameManager.instance.attack_object_cnt[29]), ForceMode2D.Impulse);
            }
            else if (dash_cnt_r >= 2)
            {
                rigid.AddForce(Vector2.right * (GameManager.instance.speed + GameManager.instance.attack_object_cnt[29]), ForceMode2D.Impulse);
            }
            // �ܻ�����Ʈ
            dash_effect_time += Time.deltaTime;
            if (dash_effect_time >= 0.03) 
            {
                dash_effect.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
                GameObject effect = Instantiate(dash_effect, transform.position, transform.rotation);
                effect.transform.localScale = new Vector3(GameManager.instance.player_size, GameManager.instance.player_size);
            }
        }

        // ���� ����
        if (rigid.velocity.y < 0)
        {
            Vector2 cast_size = new Vector2(cap_col.size.x, 0.05f);
            RaycastHit2D down_Ray = Physics2D.BoxCast(ground_pos.position, cast_size, 0f, Vector2.down, 0f, LayerMask.GetMask("Ground"));

            // ���� ������
            if (down_Ray.collider != null)
            {
                anim.SetBool("Jump", false);
                if (anim.GetBool("Hit") == true)
                {
                    anim.SetBool("Hit", false);
                }
                jump_cnt = 0;
                rigid.gravityScale = 2f;
                ground_pound = false;
            }
            else
            {
                anim.SetBool("Jump", true);
            }
        }
    }

    // �浹
    private void OnCollisionEnter2D(Collision2D col)
    {       
        if (col.gameObject.CompareTag("Coin"))
        {
            GameManager.instance.SFX_Play("Coin", GameManager.instance.player_Clips[2], 1);
            // ù ȹ��
            if (GameManager.instance.playerdata.Achievements[18] == false && GameManager.instance.achievement_bool == false)
            {
                GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                GameManager.instance.achievement_bool = true;
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n©�׶�!";
                GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[5];
                Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                GameManager.instance.playerdata.Achievements[18] = true;
                GameManager.instance.Save_PlayerData_ToJson();
            }
            if (GameManager.instance.attack_object[19] == false)
            {
                if (col.gameObject.GetComponent<Coin>().value == 0)
                {
                    GameManager.instance.coin++;
                }
                else if (col.gameObject.GetComponent<Coin>().value == 1)
                {
                    GameManager.instance.coin += 5;
                }
                else if (col.gameObject.GetComponent<Coin>().value == 2)
                {
                    GameManager.instance.coin += 10;
                }
            }
            else if (GameManager.instance.attack_object[19] == true)
            {
                if (col.gameObject.GetComponent<Coin>().value == 0)
                {
                    GameManager.instance.coin+=2;
                }
                else if (col.gameObject.GetComponent<Coin>().value == 1)
                {
                    GameManager.instance.coin += 10;
                }
                else if (col.gameObject.GetComponent<Coin>().value == 2)
                {
                    GameManager.instance.coin += 20;
                }
            }
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("Trap")) 
        {
            PlayerHit(col.transform.position);
            GameManager.instance.hp -= (int)(GameManager.instance.maxhp * 0.05);
        }

        ground_pound = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rope") && is_rope == false && Input.GetKey(KeySet.keys[Key_Action.Up])) 
        {
            Rigidbody2D rigd = collision.gameObject.GetComponent<Rigidbody2D>();
            fixjo.enabled = true;
            fixjo.connectedBody = rigd;
            is_rope = true;
            jump_cnt = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Portal")) 
        {
            if (Input.GetKey(KeySet.keys[Key_Action.Up])) 
            {
                if (portal_use == false)
                {
                    portal_use = true;
                    GameManager.instance.SFX_Play("Portal", GameManager.instance.sfx[3], 1);
                    gameObject.layer = 9;
                    bt.stage_alert.SetActive(true);
                }
            }
        }
        if (collision.gameObject.CompareTag("Shop")) 
        {
            if (Input.GetKey(KeySet.keys[Key_Action.Up]))
            {
                Time.timeScale = 0.0f;
                bt.dialogue[1].SetActive(true);
            }
        }
        if (collision.gameObject.CompareTag("Infinity")) 
        {
            gameObject.layer = 9;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Infinity")) 
        {
            gameObject.layer = 8;
        }
    }

    // ��Ʈ ����
    public void PlayerHit(Vector2 targetPos)
    {
        GameManager.instance.SFX_Play("player_hit", GameManager.instance.player_Clips[1], 1);
        hit = true;
        h = 0;
        gameObject.layer = 9;

        anim.SetBool("Hit", true);

        // �¾Ƽ� ����(��������)
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        // ƨ���� ������
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc * GameManager.instance.knockback, 10f), ForceMode2D.Impulse);

        Invoke("PlayerHit_false", 2f);
    }

    // ��Ʈ���� ���� ����
    void PlayerHit_false() 
    {
        if (anim.GetBool("Hit") == true)
        {
            anim.SetBool("Hit", false);
        }
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        hit = false;
    }

    // ����
    public void Stun(float time) 
    {
        // ����
        if (GameManager.instance.playerdata.Achievements[16] == false && GameManager.instance.achievement_bool == false)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n�ൿ�� �����մϴ�.";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[3];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[16] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
        if (mezz[0] == false)
        {
            mezz[0] = true;
            mezz_Effect.name = "Stun_Effect";
            mezz_Effect.GetComponent<Mezz_Effect>().mezz_type = 1;
            Instantiate(mezz_Effect, mezz_pos);
            Invoke("Stun_Disable", time);
        }
        else if (mezz[0] == true) 
        {
            CancelInvoke("Stun_Disable");
            Invoke("Stun_Disable", time);
        }
    }
    public void Stun_Disable() 
    {
        mezz[0] = false;
        Destroy(GameObject.Find("Stun_Effect(Clone)"));
    }

    // �ߵ�
    public void Poison(float time, float delay, int dmg)
    {
        if (mezz[1] == false)
        {
            mezz[1] = true;
            mezz_Effect.GetComponent<Mezz_Effect>().mezz_type = 2;
            mezz_Effect.name = "Poison_Effect";
            mezz_timmer[0] = delay;
            mezz_dmg[0] = dmg;
            Instantiate(mezz_Effect, mezz_pos);
            Invoke("Poison_Disable", time);
        }
        else if (mezz[1] == true) 
        {
            CancelInvoke("Poison_Disable");
            if (mezz_dmg[0] <= dmg)
            {
                mezz_dmg[0] = dmg;
            }
            Invoke("Poison_Disable", time);
        }
    }
    public void Poison_Disable() 
    {
        mezz[1] = false;
        mezz_dmg[0] = 0;
        mezz_timmer[0] = 0f;
        mezz_timmer[1] = 0f;
        Destroy(GameObject.Find("Poison_Effect(Clone)"));
    }

    // ����
    public void Bleeding(float time, float delay, int dmg)
    {
        if (mezz[2] == false)
        {
            mezz[2] = true;
            mezz_Effect.GetComponent<Mezz_Effect>().mezz_type = 3;
            mezz_Effect.name = "Bleeding_Effect";
            mezz_timmer[2] = delay;
            mezz_dmg[1] = dmg;
            Instantiate(mezz_Effect, mezz_pos);
            Invoke("Bleeding_Disable", time);
        }
        else if (mezz[2] == true) 
        {
            CancelInvoke("Bleeding_Disable");
            if (mezz_dmg[1] <= dmg) 
            {
                mezz_dmg[1] = dmg;
            }
            Invoke("Bleeding_Disable", time);
        }
    }
    public void Bleeding_Disable() 
    {
        mezz[2] = false;
        mezz_dmg[1] = 0;
        mezz_timmer[2] = 0f;
        mezz_timmer[3] = 0f;
        Destroy(GameObject.Find("Bleeding_Effect(Clone)"));
    }

    // ����
    public void Infinity_Item(float time) 
    {
        gameObject.layer = 9;
        Invoke("Infinity_Disable_Item", time);
    }
    public void Infinity_Disable_Item() 
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    // �׶��� �Ŀ��
    public void Ground_Pounding() 
    {
        rigid.AddForce(new Vector2(transform.position.x, GameManager.instance.jump_power), ForceMode2D.Impulse);
    }
    // �뽬
    IEnumerator Dash_Time_Set() 
    {
        yield return new WaitForSeconds(dash_time);
        GameManager.instance.dash = false;
        dash_cnt_l= 0;
        dash_cnt_r= 0;
        dash_cool = 0; 
    }
}
