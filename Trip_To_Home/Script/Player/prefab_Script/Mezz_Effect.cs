using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mezz_Effect : MonoBehaviour
{
    Animator anim;
    public int mezz_type;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("Type", mezz_type);
        switch (mezz_type) 
        {
            case 1:
                anim.SetBool("Stun", true);
                break;
            case 2:
                anim.SetBool("Poison", true);
                break;
            case 3:
                anim.SetBool("Bleeding", true);
                break;
        }
    }
}
