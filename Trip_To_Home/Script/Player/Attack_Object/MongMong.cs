using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MongMong : MonoBehaviour
{
    public float speed, maxSpeed;
    public float distance, tel_distance;
    public float attack_delay, cool;
    public Transform player;
    public int move_set, atk;

    Animator anim;
    SpriteRenderer sprite;
    Rigidbody2D rigid;
    public bool jump;
    public bool target_select;
    public LayerMask isLayer;
    public GameObject target;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            target_select = true;
            distance = 0f;
        }
        else 
        {
            distance = 5f;
            target_select = false;
            target = null;
        }

        // 통상 상태
        if (target_select == false)
        {
            // 이동
            if (Mathf.Abs(transform.position.x - player.position.x) > distance)
            {
                rigid.AddForce(Vector2.right * move_set, ForceMode2D.Impulse);
                // 최대 스피드 maxspeed를 못넘게
                if (rigid.velocity.x > maxSpeed)
                {
                    rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
                }
                else if (rigid.velocity.x < -maxSpeed)
                {
                    rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
                }

                // 점프중 일땐 애니메이션 움직이지 말자
                if (anim.GetBool("Jump") == true)
                {
                    anim.SetBool("Move", false);
                }
                else
                {

                    anim.SetBool("Move", true);
                }
                DirectPet();
            }
            else
            {
                anim.SetBool("Move", false);
            }

            // 너무 멀어지면 텔포
            if (Vector2.Distance(player.position, transform.position) > tel_distance)
            {
                transform.position = player.position;
                // 애니메이션 넣을거면 추가하자
            }
        }
        // 적 발견
        else if (target_select == true)
        {
            // 이동
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > distance)
            {
                rigid.AddForce(Vector2.right * move_set, ForceMode2D.Impulse);
                // 최대 스피드 maxspeed를 못넘게
                if (rigid.velocity.x > maxSpeed)
                {
                    rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
                }
                else if (rigid.velocity.x < -maxSpeed)
                {
                    rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
                }

                // 점프중 일땐 애니메이션 움직이지 말자
                if (anim.GetBool("Jump") == true)
                {
                    anim.SetBool("Move", false);
                }
                else
                {
                    anim.SetBool("Move", true);
                }
                DirectPet_2();
            }

            // 공격 쿨타임
            if (attack_delay > cool)
            {
                cool += Time.deltaTime;
            }

            // 공격
            Vector3 scale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y);

            // 레이캐스트 - 박스캐스트
            RaycastHit2D col = Physics2D.BoxCast(gameObject.transform.position, scale, 0, Vector2.left, 0, isLayer);
            if (col.collider != null)
            {
                if (col.collider.gameObject.name == target.name && attack_delay < cool && col.collider.CompareTag("Monster2"))
                {
                    int dmg = 0;
                    if (GameManager.instance.attack_object[23] == true)
                    {
                        dmg = (int)(GameManager.instance.atk * GameManager.instance.attack_object_cnt[12]) * 2;
                    }
                    else
                    {
                        dmg = (int)(GameManager.instance.atk * GameManager.instance.attack_object_cnt[12]);
                    }
                    int a = target.GetComponent<Monster2>().mdef - dmg;
                    if (a >= 0)
                    {
                        a = -1;
                    }
                    target.GetComponent<Monster2>().mhp += a;
                    a *= -1;
                    target.GetComponent<Monster2>().dmg = a;
                    target.GetComponent<Monster2>().hit = true;
                    cool = 0f;
                }

                if (col.collider.gameObject.name == target.name && attack_delay < cool && col.collider.CompareTag("Summon"))
                {
                    int dmg = 0;
                    if (GameManager.instance.attack_object[23] == true)
                    {
                        dmg = (int)(GameManager.instance.atk * GameManager.instance.attack_object_cnt[12]) * 2;
                    }
                    else
                    {
                        dmg = (int)(GameManager.instance.atk * GameManager.instance.attack_object_cnt[12]);
                    }
                    int a = target.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mdef - dmg;
                    if (a >= 0)
                    {
                        a = -1;
                    }
                    target.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp += a;
                    a *= -1;
                    target.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = a;
                    target.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;
                    cool = 0f;
                }
            }
        }

        // 선 그려주기 ( 낙사 방지 )
        Vector2 frontVec = new Vector2(rigid.position.x + move_set, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(1, 0, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 6, LayerMask.GetMask("Ground"));

        // 낭떨어지 방지
        if (rayHit.collider == null && jump == false)
        {
            jump = true;
            rigid.velocity = Vector2.up * GameManager.instance.jump_power * 0.8f;
            anim.SetBool("Jump", true);
        }

    }

    // 착지
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jump = false;
            anim.SetBool("Jump", false);
        }
    }

    public void DirectPet()
    {
        if (transform.position.x - player.position.x < 0)
        {
            move_set = 1;
            sprite.flipX = true;
        }
        else if (transform.position.x - player.position.x > 0)
        {
            move_set = -1;
            sprite.flipX = false;
        }
    }
    public void DirectPet_2()
    {
        if (transform.position.x - target.transform.position.x < 0)
        {
            move_set = 1;
            sprite.flipX = true;
        }
        else if (transform.position.x - target.transform.position.x > 0)
        {
            move_set = -1;
            sprite.flipX = false;
        }
    }
}
