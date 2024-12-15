using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Ground : MonoBehaviour
{
    public float jump_power;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<Player_Move>().jump_cnt = 0;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * jump_power;
        }
    }
}
