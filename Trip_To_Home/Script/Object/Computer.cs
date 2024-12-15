using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    Image self_Image;
    public Sprite[] imgs;
    public Animator anim;
    public int number;
    void Start()
    {
        anim = GetComponent<Animator>();
        self_Image = GetComponent<Image>();
    }


    public void Status_Now() 
    {
        anim.SetBool("Load", true);
        anim.SetInteger("Num", number);
    }
}
