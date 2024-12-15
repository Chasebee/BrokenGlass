using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning_Blade : MonoBehaviour
{
    Animator anim;
    CircleCollider2D circle;
    public float speed;
    public int move_set, dmg;
    public bool vertical;
    void Start()
    {
        anim = GetComponent<Animator>();
        circle = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (vertical == false)
        {
            transform.Translate(transform.right * move_set * speed * Time.deltaTime);
        }
        else if (vertical == true) 
        {
            transform.Translate(transform.up * move_set * speed * Time.deltaTime);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("patrol"))
        {
            move_set *= -1;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.hp -= dmg;

            collision.gameObject.GetComponent<Player_Move>().PlayerHit(gameObject.transform.position);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
