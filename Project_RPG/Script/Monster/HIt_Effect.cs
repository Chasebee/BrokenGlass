using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIt_Effect : MonoBehaviour
{
    public int num;
    public int dir;
    Animator anim;
    SpriteRenderer rend;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim.SetInteger("Num", num);
        if (dir == -1) 
        {
            rend.flipX = true;
        }
    }

    public void Effect_End() 
    {
        Destroy(gameObject);
    }
}
