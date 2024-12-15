using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nero : MonoBehaviour
{
    public float speed, maxSpeed;
    public float distance, tel_distance;
    public Transform player;
    public int move_set, atk;
    public GameObject paw;

    Animator anim;
    SpriteRenderer sprite;
    Rigidbody2D rigid;
    public bool jump;
    float timmer;
    void Start()

    {
        rigid = GetComponent<Rigidbody2D>();
        sprite= GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        timmer += Time.deltaTime;

        // �߹ٴ� ź ��ȯ
        if (timmer >= GameManager.instance.attack_object_cnt[21])
        {
            timmer = 0f;
            Instantiate(paw);
        }
    }
    void FixedUpdate()
    {
        // �̵�
        if (Mathf.Abs(transform.position.x - player.position.x) > distance)
        {
            rigid.AddForce(Vector2.right * move_set, ForceMode2D.Impulse);
            // �ִ� ���ǵ� maxspeed�� ���Ѱ�
            if (rigid.velocity.x > maxSpeed)
            {
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            }
            else if (rigid.velocity.x < -maxSpeed)
            {
                rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
            }

            // ������ �϶� �ִϸ��̼� �������� ����
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

        // �� �׷��ֱ� ( ���� ���� )
        Vector2 frontVec = new Vector2(rigid.position.x + move_set, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(1, 0, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 6, LayerMask.GetMask("Ground"));

        // �������� ����
        if (rayHit.collider == null && jump == false)
        {
            jump = true;
            rigid.velocity = Vector2.up * GameManager.instance.jump_power * 0.8f;
            anim.SetBool("Jump", true);
        }

        // �ʹ� �־����� ����
        if (Vector2.Distance(player.position, transform.position) > tel_distance) 
        {
            transform.position = player.position;
            // �ִϸ��̼� �����Ÿ� �߰�����
        }
    }

    // ����
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") 
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
            move_set= -1;
            sprite.flipX = false;
        }
    }

}
