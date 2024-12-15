using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Move : MonoBehaviour
{
    public int location;
    public float move_power;
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            switch (location)
            {
                case 1:
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * move_power);
                    break;
                case 2:
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * move_power);
                    break;
            }
        }
    }
}
