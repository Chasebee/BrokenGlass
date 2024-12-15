using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Item : MonoBehaviour
{
    public int type_num;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 유체화 효과
            if (type_num == 0)
            {
                collision.gameObject.GetComponent<Player_Move>().Infinity_Item(5f);
                collision.gameObject.GetComponent<Player_Move>().spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            }
            Destroy(gameObject);
        }
    }
}
