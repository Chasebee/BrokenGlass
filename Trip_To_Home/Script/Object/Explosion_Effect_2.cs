using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Effect_2 : MonoBehaviour
{
    public AudioClip[] clips;

    void Start()
    {
        int rnd = Random.Range(0, 2);
        GameManager.instance.SFX_Play("B2_Explosion", clips[rnd], 0.7f);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            GameManager.instance.hp -= (int)(GameManager.instance.maxhp * 0.35);
            collision.gameObject.GetComponent<Player_Move>().PlayerHit(gameObject.transform.position);
        }
    }

    public void Destory_Self() 
    {
        Destroy(gameObject);
    }
}
