using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survival_Mod_Player_Move : MonoBehaviour
{
    [Header("�̵�/�ǰ� ����")]
    public float dir_x, dir_y;
    public float speed;
    public int flip_img;
    public GameObject hit_sound;
    public bool hit;
    public bool protect;
    public float protect_time;
    public GameObject protect_Shiled;
    [Header("���� ����")]
    public bool holding;
    public GameObject heal_area;
    Rigidbody2D rigid;
    public SpriteRenderer rend;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent <SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        
        // ������� ù �÷���
        if (GameManager.instance.playerdata.Achievements[29] == false && GameManager.instance.achievement_bool == false)
        {
            GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
            GameManager.instance.achievement_bool = true;
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n����� �� ����?";
            GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Achievements_img_list[16];
            Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
            GameManager.instance.playerdata.Achievements[29] = true;
            GameManager.instance.Save_PlayerData_ToJson();
        }
    }

    void Update()
    {
        speed = (int)(GameManager.instance.speed * 0.7);
        // ��, �� ����
        if (Input.GetKeyUp(KeySet.keys[Key_Action.Right]) || Input.GetKeyUp(KeySet.keys[Key_Action.Left]))
        {
            dir_x = 0;
        }
        else if (Input.GetKey(KeySet.keys[Key_Action.Left]))
        {
            dir_x = -1;
            flip_img = -1;
        }
        else if (Input.GetKey(KeySet.keys[Key_Action.Right]))
        {
            dir_x = 1;
            flip_img = 1;
        }
        
        // ��, �� ����
        if (Input.GetKey(KeySet.keys[Key_Action.Down]))
        {
            dir_y = -1;

        }
        else if (Input.GetKeyUp(KeySet.keys[Key_Action.Down]))
        {
            dir_y = 0;
        }
        if (Input.GetKey(KeySet.keys[Key_Action.Up]))
        {
            dir_y = 1;
        }
        else if (Input.GetKeyUp(KeySet.keys[Key_Action.Up]))
        {
            dir_y = 0;
        }

        // ���� ���� Ȧ��
        if (Input.GetKeyDown(KeySet.keys[Key_Action.Attack]))
        {
            holding = true;
        }
        else if (Input.GetKeyUp(KeySet.keys[Key_Action.Attack]))
        {
            holding = false;
        }

        if (holding == false)
        {
            gameObject.GetComponent<Survival_Mod_Attack>().dir_X = dir_x;
            gameObject.GetComponent<Survival_Mod_Attack>().dir_Y = dir_y;
        }

        // �̹��� / �ִϸ��̼�
        if (flip_img == -1)
        {
            rend.flipX = false;
        }
        else if (flip_img == 1) 
        {
            rend.flipX = true;
        }

        if (dir_x != 0 || dir_y != 0)
            anim.SetBool("Move", true);
        else if(dir_x == 0 && dir_y == 0)
            anim.SetBool("Move", false);

        if (hit_sound != null)
        {
            hit = true;
        }
        else if(hit_sound == null)
        {
            hit = false;
        }
    }

    void FixedUpdate()
    {
        // Ű �¾� �����̸� �ȵ�
        if (dir_x == 0 && dir_y == 0)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // �����̱�
        if (GameManager.instance.hp >= 1)
        {
            Vector2 next_Vec = new Vector2(dir_x, dir_y) * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + next_Vec);
        }

        // ��/��
        if (rigid.velocity.x > speed) 
        {
            rigid.velocity = new Vector2(speed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -speed)
        {
            rigid.velocity = new Vector2(-speed, rigid.velocity.y);
        }
        // ��/��
        if (rigid.velocity.y > speed)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, speed);
        }
        else if (rigid.velocity.y < -speed)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -speed);
        }

    }

}
