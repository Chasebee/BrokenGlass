using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce_Energy_Ball : MonoBehaviour
{
    public int dmg;
    public float move_speed;
    public float timer;
    float move_x_rate;
    float move_y_rate;

    void Start()
    {
        move_speed = 5.0f;
        move_x_rate = Random.Range(-1.0f, 1.0f);
        move_y_rate = Random.Range(-1.0f, 1.0f);

        while (Mathf.Abs(move_x_rate) < 0.3f)
        {
            move_x_rate = Random.Range(-1.0f, 1.0f);
        }
        while (Mathf.Abs(move_y_rate) < 0.3f)
        {
            move_y_rate = Random.Range(-1.0f, 1.0f);
        }
    }
    
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * move_speed * move_x_rate, Space.World);
        transform.Translate(Vector3.up * Time.deltaTime * move_speed * move_y_rate, Space.World);

        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        if (position.x < 0f) 
        {
            position.x = 0f;
            move_x_rate = Random.Range(0.3f, 1.0f);
        }
        if (position.y < 0f) 
        {
            position.y = 0f;
            move_y_rate = Random.Range(0.3f, 1.0f);
        }
        if (position.x > 1f) 
        {
            position.x = 1f;
            move_x_rate = Random.Range(-1.0f, -0.3f);
        }
        if (position.y > 1f) 
        {
            position.y = 1f;
            move_y_rate = Random.Range(-1.0f, -0.3f);
        }
        transform.position = Camera.main.ViewportToWorldPoint(position);

        timer += Time.deltaTime;
        if (timer >= 10) 
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("S_Monster"))
        {
            dmg = (int)(GameManager.instance.atk * 0.8);
            bool critical = false;
            int crit = (int)(Random.Range(0, 100) + GameManager.instance.crit_per);
            if (crit >= 80)
            {
                dmg = (int)(dmg * GameManager.instance.crit_dmg);
                critical = true;
            }
            collision.gameObject.GetComponent<SurvivalMod_Monster>().mhp -= dmg;
            collision.gameObject.GetComponent<SurvivalMod_Monster>().dmg = dmg;
            if (critical == true)
            {
                collision.gameObject.GetComponent<SurvivalMod_Monster>().critical = true;
            }
            collision.gameObject.GetComponent<SurvivalMod_Monster>().hit = true;
        }
    }
}
