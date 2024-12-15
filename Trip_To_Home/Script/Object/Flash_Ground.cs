using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Flash_Ground : MonoBehaviour
{
    public float timmer, time;
    public bool visible;
    SpriteRenderer sprite;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        timmer += Time.deltaTime;
        if (timmer >= time && visible == true)
        {
            visible = false;
            Active_False();
            timmer = 0f;
        }
        else if (timmer >= time && visible == false)
        {
            visible = true;
            Active_true();
            timmer = 0f;
        }
    }

    public void Active_true() 
    {
        sprite.enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;    
    }
    public void Active_False()
    {
        sprite.enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
