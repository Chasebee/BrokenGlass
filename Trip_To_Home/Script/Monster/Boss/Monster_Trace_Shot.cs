using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Trace_Shot : MonoBehaviour
{
    public float speed;
    public int atk;
    Vector3 p_pos;
    public void Start()
    {
        Invoke("Destory_Monster_Arrow", 26f);
        p_pos = GameObject.FindWithTag("Player").transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, p_pos, speed * Time.deltaTime);
        if (transform.position.x == p_pos.x) 
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 데미지 ( 보이지 않는 갑옷 적용 / 미적용 )
            if (GameManager.instance.attack_object[18] == false)
            {
                GameManager.instance.hp += GameManager.instance.def - atk;
            }
            else if (GameManager.instance.attack_object[18] == true)
            {
                int rnd = Random.Range(0, 10);
                if (rnd >= 6)
                {
                    GameManager.instance.hp += GameManager.instance.def - (atk / 2);
                }
                else
                {
                    GameManager.instance.hp += GameManager.instance.def - atk;
                }
            }
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(transform.position);
            Destory_Monster_Arrow();
        }
        if (GameManager.instance.attack_object[16] == true && collision.gameObject.CompareTag("Arrow"))
        {
            Destroy(collision.gameObject);
            Destory_Monster_Arrow();
        }
    }
    public void Destory_Monster_Arrow()
    {
        Destroy(gameObject);
    }
}
