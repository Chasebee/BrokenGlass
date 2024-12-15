using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Chacing : MonoBehaviour
{
    public float distance;
    public float[] chase_scale, hide_chase_scale;
    public int number;
    Vector2 monster_pos;
    RaycastHit2D Hide_End;
    Vector2 box_size;
    Vector2 Hide_End_Box;

    void Start()
    {
        number = gameObject.GetComponent<Monster2>().monster_number;    
    }
    public void Update()
    {

        box_size = new Vector2(chase_scale[0], chase_scale[1]);
        Hide_End_Box = Vector2.zero;
        // 박스 캐스트 사이즈
        if (transform.GetComponent<Monster2>().hide_type == true)
        {
            Hide_End_Box = new Vector2(hide_chase_scale[0], hide_chase_scale[1]);
        }

        // monster_pos 지정 ( 박스 위치 설정 )
        if (transform.GetComponent<Monster2>().move_set == 1)
        {
            monster_pos = new Vector2(transform.position.x + distance, transform.position.y);
        }
        else if (transform.GetComponent<Monster2>().move_set == -1)
        {

            monster_pos = new Vector2(transform.position.x - distance, transform.position.y);
        }

        // 통상형
        RaycastHit2D player_Chase = Physics2D.BoxCast(monster_pos, box_size, 0f, Vector2.left, 0f, LayerMask.GetMask("Player"));

        // 잠복형
        if (transform.GetComponent<Monster2>().hide_type == true)
        {
            Hide_End = Physics2D.BoxCast(transform.position, Hide_End_Box, 0f, Vector2.left, 0f, LayerMask.GetMask("Player"));
        }

        // 추적 범위안에 플레이어가 있는 경우
        if (player_Chase.collider != null)
        {
            // 기본타입 류
            if (transform.GetComponent<Monster2>().hide_type == false)
            {
                if (GameObject.FindWithTag("Player").transform.position.x - transform.position.x > 0)
                {
                    if (GameObject.FindWithTag("Player").layer == 8)
                    {
                        transform.GetComponent<Monster2>().move_set = 1;

                        // 모든 공격타입으로 변경 가능한 종류
                        if (number == 18) 
                        {
                            gameObject.GetComponent<Monster2>().attack_type = true;
                            gameObject.GetComponent<Monster2>().attack_cnt = 3;
                            gameObject.GetComponent<Monster2>().attack_cnt_time = 1.7f;
                        }
                    }
                    if (gameObject.GetComponent<Monster2>().monster_number == 19) 
                    {
                        if (gameObject.GetComponent<Monster2>().attack_delay <= gameObject.GetComponent<Monster2>().attack_timmer) 
                        {
                            Attack_Anim_True();
                        }
                    }
                }
                else
                {
                    if (GameObject.FindWithTag("Player").layer == 8)
                    {
                        transform.GetComponent<Monster2>().move_set = -1;

                        // 모든 공격타입으로 변경 가능한 종류
                        if (number == 18)
                        {
                            gameObject.GetComponent<Monster2>().attack_type = true;
                            gameObject.GetComponent<Monster2>().attack_cnt = 3;
                            gameObject.GetComponent<Monster2>().attack_cnt_time = 1.7f;
                        }
                    }
                    if (gameObject.GetComponent<Monster2>().monster_number == 19)
                    {
                        if (gameObject.GetComponent<Monster2>().attack_delay <= gameObject.GetComponent<Monster2>().attack_timmer)
                        {
                            Attack_Anim_True();
                        }
                    }
                }
            }

            // 잠복류
            if (transform.GetComponent<Monster2>().hide_type == true)
            {
                if (GameObject.FindWithTag("Player").transform.position.x - transform.position.x > 0)
                {
                    transform.GetComponent<Monster2>().move_set = 1;
                }
                else
                {
                    transform.GetComponent<Monster2>().move_set = -1;
                }
            }
        }
        else if(player_Chase.collider == null)
        {
            // 모든 공격타입
            if (number == 18) 
            {
                gameObject.GetComponent<Monster2>().attack_type = false;
                gameObject.GetComponent<Monster2>().attack_cnt = 2;
                gameObject.GetComponent<Monster2>().attack_cnt_time = 0.7f;
            }
        }

        // 잠복형 몬스터 추가
        if (Hide_End.collider != null && transform.GetComponent<Monster2>().hide == true)
        {
            transform.GetComponent<Monster2>().hide_found = true;
        }
    }
    public void OnDrawGizmos()
    {

        box_size = new Vector2(chase_scale[0], chase_scale[1]);
        Hide_End_Box = Vector2.zero;
        // 박스 캐스트 사이즈
        if (transform.GetComponent<Monster2>().hide_type == true)
        {
            Hide_End_Box = new Vector2(hide_chase_scale[0], hide_chase_scale[1]);
        }

        // monster_pos 지정 ( 박스 위치 설정 )
        if (transform.GetComponent<Monster2>().move_set == 1)
        {
            monster_pos = new Vector2(transform.position.x + distance, transform.position.y);
        }
        else if (transform.GetComponent<Monster2>().move_set == -1)
        {

            monster_pos = new Vector2(transform.position.x - distance, transform.position.y);
        }

        // 통상형
        RaycastHit2D player_Chase = Physics2D.BoxCast(monster_pos, box_size, 0f, Vector2.left, 0f, LayerMask.GetMask("Player"));

        // 잠복형
        if (transform.GetComponent<Monster2>().hide_type == true)
        {
            Hide_End = Physics2D.BoxCast(transform.position, Hide_End_Box, 0f, Vector2.left, 0f, LayerMask.GetMask("Player"));
        }

        Vector2 pos = new Vector2(monster_pos.x, transform.position.y);
        Gizmos.DrawWireCube(pos, box_size);
        Gizmos.DrawWireCube(pos, Hide_End_Box);
    }

    // 특정 몬스터 공격 
    
    /*------ 4챕터 보스 -----*/
    public void Attack_Anim_True()
    {
        if (gameObject.GetComponent<Monster2>().anim.GetBool("Hide") == false && gameObject.GetComponent<Monster2>().mhp >= 30051 && gameObject.GetComponent<Monster2>().anim.GetBool("Attack") == false)
        {
            int rand = 0;
            gameObject.GetComponent<Monster2>().anim.SetBool("Attack", true);
            gameObject.GetComponent<Monster2>().anim.SetInteger("Attack_Num", rand);
            gameObject.GetComponent<Monster2>().attack_timmer = 0f;
            switch (rand)
            {
                case 0:
                    Invoke("Summon_Rock", 0.6f);
                    Invoke("Attack_Anim_False", 1f);
                    break;
            }
        }
    }
    public void Attack_Anim_False()
    {

        gameObject.GetComponent<Monster2>().anim.SetBool("Attack", false);
        gameObject.GetComponent<Monster2>().anim.SetInteger("Attack_Num", 0);
    }
    public void Summon_Rock()
    {
        int num = Random.Range(6, 9);
        GameObject rock = gameObject.GetComponent<Boss_Pattern>().pt_1[0];
        rock.GetComponent<Lava_Rock_Summon_Arrow>().dmgs = gameObject.GetComponent<Monster2>().atk;
        for (int i = 0; i < num; i++)
        {
            Instantiate(rock, gameObject.GetComponent<Monster2>().monster_arrow_pos.transform.position, transform.rotation);
        }
    }
}
