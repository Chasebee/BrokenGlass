using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borken_Wall : MonoBehaviour
{
    public int hp;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow") || collision.gameObject.CompareTag("Laser_Arrow"))
        {
            hp--;
            Destroy(collision.gameObject);

            if (hp <= 0) 
            {
                Destroy(gameObject);
            }
        }
    }
}
