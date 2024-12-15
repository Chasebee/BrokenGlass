using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Wisp_Eject_Lightning : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster2"))
        {
            int dmg = GameManager.instance.atk;
            collision.gameObject.GetComponent<Monster2>().mhp -= dmg;
            collision.gameObject.GetComponent<Monster2>().dmg = dmg;
            collision.gameObject.GetComponent<Monster2>().hit = true;
            int ran = Random.Range(0, 101);
            if (ran >= 55)
            {
                collision.gameObject.GetComponent<Monster2>().mezz[0] = true;
                collision.gameObject.GetComponent<Monster2>().mezz_time[0] = 3f;
            }
        }
    }
    public void Destroy_Self()
    {
        Destroy(gameObject);
    }
}
