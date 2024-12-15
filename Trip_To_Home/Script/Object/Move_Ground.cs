using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Ground : MonoBehaviour
{
    Rigidbody2D rigid;
    public float speed;
    public int move_set;
    public RectTransform[] patrol;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); 
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(move_set * speed, rigid.velocity.y);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("patrol"))
        {
            move_set *= -1;
        }
    }
}
