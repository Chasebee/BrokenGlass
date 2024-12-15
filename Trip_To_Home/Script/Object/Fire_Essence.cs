using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Essence : MonoBehaviour
{
    public GameObject origin;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            origin.GetComponent<Monster2>().anim.SetBool("Success", true);
            collision.gameObject.GetComponent<Player_Move>().Infinity_Item(5f);
            collision.gameObject.GetComponent<Player_Move>().spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            collision.gameObject.transform.position = new Vector2(-11, 1.6f);
        }
    }
}
