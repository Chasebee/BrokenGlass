using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eject_LightNing : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster2"))
        {
            int dmg = collision.gameObject.GetComponent<Monster2>().mdef - (GameManager.instance.atk + (int)(GameManager.instance.attack_object_cnt[25] * 5));
            if (dmg >= 0) 
            {
                dmg = -1;
            }
            collision.gameObject.GetComponent<Monster2>().mhp += dmg;
            dmg *= -1;
            collision.gameObject.GetComponent<Monster2>().dmg = dmg;
            collision.gameObject.GetComponent<Monster2>().hit = true;
            int random_cc = Random.Range(0, 101);
            if (random_cc >= 55) 
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
