using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Pillar : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(gameObject.transform.position);
            GameManager.instance.hp -= (int)(GameManager.instance.maxhp * 0.2f);
        }
    }

    public void Destroy_Pillar() 
    {
        Destroy(gameObject);
    }
}
