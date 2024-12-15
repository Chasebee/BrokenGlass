using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder_Shot : MonoBehaviour
{
    public int dmg;
    public int cnt = 1;
    public Transform arrow_move;
    public Vector2 arrow_pos;
    public float speed;
    public float timer;
    float dir_X, dir_Y;
    void Start()
    {
        dmg = (int)(GameManager.instance.atk * GameManager.instance.s_attack_object_cnt[6]);
        arrow_move = GetComponent<Transform>();
    }

    void Update()
    {
        dir_X = arrow_pos.x;
        dir_Y = arrow_pos.y;
        timer += Time.deltaTime;
        if (timer >= 2.5f)
        {
            Destroy(gameObject);
        }
        Vector3 move = new Vector3(arrow_pos.x, arrow_pos.y, -1).normalized * speed * Time.deltaTime;
        arrow_move.position += move;
        if (arrow_move.position.z <= -2) 
        {
            arrow_move.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
        

        if (dir_X == 0 && dir_Y == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (dir_X == 1 && dir_Y == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        else if (dir_X == 1 && dir_Y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (dir_X == 1 && dir_Y == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, -135);
        }
        else if (dir_X == 0 && dir_Y == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (dir_X == -1 && dir_Y == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (dir_X == -1 && dir_Y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (dir_X == -1 && dir_Y == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 135);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("S_Monster")) 
        {
            int plus = (int)(dmg * (cnt * 0.1f));
            int end_dmg = dmg + plus;
            bool critical = false;
            int crit = (int)(Random.Range(0, 100) + GameManager.instance.crit_per);
            if (crit >= 80)
            {
                end_dmg = (int)(end_dmg * GameManager.instance.crit_dmg);
                critical = true;
            }
            collision.gameObject.GetComponent<SurvivalMod_Monster>().mhp -= end_dmg;
            collision.gameObject.GetComponent<SurvivalMod_Monster>().dmg = end_dmg;
            if (critical == true)
            {
                collision.gameObject.GetComponent<SurvivalMod_Monster>().critical = true;
            }
            collision.gameObject.GetComponent<SurvivalMod_Monster>().hit = true;
            cnt++;
            if (cnt >= 5) 
            {
                cnt = 5;
            }
        }
    }
}
