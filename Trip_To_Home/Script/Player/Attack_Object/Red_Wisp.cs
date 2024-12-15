using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Wisp : MonoBehaviour
{
    public GameObject player, Laser;
    
    Transform pos, arrow_pos;
    Animator anim;
    SpriteRenderer rend;
    Rigidbody2D rigid;
    Vector2 x_pos;
    public int direct;
    public float trace_pos, speed, maxSpeed, timmer, distance;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        pos = player.GetComponent<Transform>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player.GetComponent<Player_Move>().fly_object.Add(gameObject);
        int leng = player.GetComponent<Player_Move>().fly_object.Count;
        anim.SetInteger("Num", 0);
        if (leng == 1)
        {
            distance = player.GetComponent<Player_Move>().fly_object.Count * 0.37f;
        }
        else
        {
            distance = player.GetComponent<Player_Move>().fly_object.Count * 0.33f;
        }
    }

    private void Update()
    {
        timmer += Time.deltaTime;
        if (timmer >= 10f) 
        {
            timmer = 0f;
            Instantiate(Laser, x_pos, transform.rotation) ;
        }
        transform.position = Vector2.Lerp(transform.position, x_pos, Time.deltaTime * speed);
        
        Move_Setting();
    }

    public void Move_Setting() 
    {
        if (player.GetComponent<Player_Move>().direction == 1)
        {
            direct = 1;
            rend.flipX = true;
            x_pos = new Vector2(player.GetComponent<Player_Move>().arrow_Rect.transform.position.x - (distance * 5), player.GetComponent<Player_Move>().arrow_Rect.transform.position.y + 0.2f);
        }
        else if (player.GetComponent<Player_Move>().direction == -1)
        {
            x_pos = new Vector2(player.GetComponent<Player_Move>().arrow_Rect.transform.position.x + (distance * 5), player.GetComponent<Player_Move>().arrow_Rect.transform.position.y + 0.2f);
            direct = -1;
            rend.flipX = false;
        }
    }
}
