using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace_Arrow_Monster : MonoBehaviour
{
    public int dmg;
    public float[] timer;
    public float speed;
    public int null_dir;

    GameObject player;
    Vector3 dir;
    Quaternion rotTarget;
    Rigidbody2D rigid;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        dir = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotTarget = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.fixedDeltaTime * speed);
        rigid.velocity = new Vector2(dir.x, dir.y) * speed;

        timer[0] += Time.fixedDeltaTime;
        if (timer[0] >= timer[1]) 
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            dmg -= GameManager.instance.def;
            GameManager.instance.hp -= dmg;
            Destroy(gameObject);
        }
    }
}
