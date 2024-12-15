using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paw : MonoBehaviour
{
    public GameObject player;
    public Transform center; // 중심점
    public float radius = 1f;
    public float speed = 5.0f;
    private float angle = 0;
    
    public Sprite[] paws;
    public Transform enemey;
    public bool target, ready;

    SpriteRenderer rend;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        center = player.GetComponent<Transform>();
        rend = GetComponent<SpriteRenderer>();

        int rnd = Random.Range(0, 6);
        rend.sprite = paws[rnd];
        
    }
    void Update()
    {
        if (target == false && ready == true) 
        {
            angle += speed * Time.deltaTime;
            transform.position = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        }
        if (target == true && enemey != null)
        {
            Vector3 direction = enemey.position - transform.position;
            direction.Normalize();
            transform.position = Vector3.MoveTowards(transform.position, enemey.position, 0.2f);
            ready = false;
        }
        if (target == true && enemey == null) 
        {
            ready = false;
            enemey = null;
        }
        // 목표가 사라진 경우
        if (target == false && ready == false) 
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.2f);

            if (transform.position == player.transform.position) 
            {
                ready = true;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster2"))
        {
            int dmg = 0;
            if (GameManager.instance.attack_object[23] == false) 
            {
                dmg = collision.GetComponent<Monster2>().mdef - (int)(GameManager.instance.atk * 1.2);
            }
            else if (GameManager.instance.attack_object[23] == true)
            {
                dmg = collision.GetComponent<Monster2>().mdef - (int)(GameManager.instance.atk * 1.2) * 2;
            }
            // 미스처리
            if (dmg >= 0)
            {
                dmg = -1;
            }
            collision.GetComponent<Monster2>().mhp += dmg;
            dmg = dmg * -1;
            collision.GetComponent<Monster2>().hit = true;
            collision.GetComponent<Monster2>().dmg = dmg;
            Destroy(gameObject);
        }

        if (collision.CompareTag("Summon"))
        {
            int dmg = 0;
            if (GameManager.instance.attack_object[23] == false)
            {
                dmg = collision.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mdef - (int)(GameManager.instance.atk * 1.2);
            }
            else if (GameManager.instance.attack_object[23] == true)
            {
                dmg = collision.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mdef - (int)(GameManager.instance.atk * 1.2) * 2;
            }
            // 미스처리
            if (dmg >= 0)
            {
                dmg = -1;
            }
            collision.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp += dmg;
            dmg = dmg * -1;
            collision.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;
            collision.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = dmg;
            Destroy(gameObject);
        }
    }
}
