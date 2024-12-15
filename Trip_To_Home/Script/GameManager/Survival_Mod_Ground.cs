using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survival_Mod_Ground : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Respawn"))
            return;

        float p_x = player.GetComponent<Survival_Mod_Player_Move>().transform.position.x;
        float g_x = transform.position.x;
        float p_y = player.GetComponent<Survival_Mod_Player_Move>().transform.position.y;
        float g_y = transform.position.y;

        float diff_x = Mathf.Abs(p_x - g_x);
        float diff_y = Mathf.Abs(p_y - g_y);

        float p_dir_x = player.GetComponent<Survival_Mod_Player_Move>().dir_x;
        float p_dir_y = player.GetComponent<Survival_Mod_Player_Move>().dir_y;
        Vector3 p_dir = new Vector2(p_dir_x, p_dir_y);
        float dirX = p_dir_x < 0 ? -1 : 1;
        float dirY = p_dir_y < 0 ? -1 : 1;

        switch (transform.tag) 
        {
            case "Ground":
                if (diff_x > diff_y)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if(diff_x < diff_y)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "S_Monster":
                if (collision.enabled) 
                {
                    transform.Translate(p_dir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
                break;
        }
    }
}
