using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Object : MonoBehaviour
{
    public int dmg;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            GameManager.instance.hp -= dmg;
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(gameObject.transform.position);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground")) 
        {
            Destroy(gameObject);
        }
    }
}
