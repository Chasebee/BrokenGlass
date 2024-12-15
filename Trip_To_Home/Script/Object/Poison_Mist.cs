using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Mist : MonoBehaviour
{
    public float time;
    public int dmg;

    void Start()
    {
        Invoke("Destroy_Self", time);
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.gameObject.GetComponent<Player_Move>().Poison(5, 0.9f, 10);
        }
    }

    public void Destroy_Self() 
    {
        Destroy(gameObject);
    }
}
