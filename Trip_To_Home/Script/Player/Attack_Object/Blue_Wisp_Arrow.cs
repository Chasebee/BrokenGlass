using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Wisp_Arrow : MonoBehaviour
{
    public int dmg;
    public int dir;
    public float speed;
    public GameObject elec_Effect;
    public float timer;
    void Start()
    {
        dmg = (int)(GameManager.instance.atk * 0.75f);
    }

    void Update()
    {
        if (dir == -1) 
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
        if (dir == 1)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            Instantiate(elec_Effect, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster2")) 
        {
            collision.gameObject.GetComponent<Monster2>().mhp -= dmg;
            collision.gameObject.GetComponent<Monster2>().dmg = dmg;
            collision.gameObject.GetComponent<Monster2>().hit = true;
            Instantiate(elec_Effect, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy_Self();
        }
    }
    public void Destroy_Self() 
    {
        Destroy(gameObject);
    }
}
