using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Arrow_Summon : MonoBehaviour
{
    private Rigidbody2D rigid;
    public int dmg;
    public float speed = 10f;
    public float timmer, time;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = speed * transform.right;
    }

    void Update()
    {
        timmer += Time.deltaTime;
        if (timmer >= time)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(gameObject.transform.position);
            GameManager.instance.hp += GameManager.instance.def - dmg;
        }
    }
}
