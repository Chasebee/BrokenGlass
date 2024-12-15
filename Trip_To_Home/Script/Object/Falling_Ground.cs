using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Ground : MonoBehaviour
{
    public float fall_timmer, fall_time, spawn_time, speed, fall_value;
    public bool drop, dont_break;
    public Vector2 start_position, position;
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        start_position = transform.position;
        position = new Vector2(transform.position.x, transform.position.y - fall_value);
    }

    void Update()
    {
        if (drop == true && fall_time >= fall_timmer) 
        {
            fall_timmer += Time.deltaTime;
        }

        if (fall_time <= fall_timmer && drop == true) 
        {
            transform.position = Vector2.MoveTowards(transform.position, position, Time.deltaTime * speed);
            Invoke("ReSpawn", spawn_time);
        }
    }

    public void ReSpawn() 
    {
        transform.position = start_position;
        fall_timmer = 0f;
        drop = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && dont_break == false) 
        {
            drop = true;
        }
    }
}
