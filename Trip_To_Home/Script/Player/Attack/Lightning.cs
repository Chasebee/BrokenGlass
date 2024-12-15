using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    GameObject player;
    SpriteRenderer rend;
    Animator anim;
    AudioSource clip_source;
    public AudioClip[] clips;
    public float timmer = 0.3f;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        clip_source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player.GetComponent<Player_Move>().direction == 1)
        {
            Vector2 pos = new Vector2(player.transform.position.x + 2f, player.transform.position.y);
            transform.localPosition = pos;
            rend.flipX = false;
        }
        if (player.GetComponent<Player_Move>().direction == -1)
        {
            Vector2 pos = new Vector2(player.transform.position.x - 2f, player.transform.position.y);
            transform.localPosition = pos;
            rend.flipX = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster2"))
        {
            clip_source.clip = clips[1];
            clip_source.Play();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster2"))
        {
            clip_source.clip = clips[0];
            clip_source.Play();
        }
    }
}
