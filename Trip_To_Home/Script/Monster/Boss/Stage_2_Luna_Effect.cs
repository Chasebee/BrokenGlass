using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_2_Luna_Effect : MonoBehaviour
{
    public Animator anim;
    void Start() 
    {
        anim = GetComponent<Animator>();
    }
    void Update() 
    {
        if (GameObject.FindWithTag("Player").layer == 8 && anim.GetBool("Effect") == true)
        {
            GameObject.FindWithTag("Player").GetComponent<Player_Move>().Stun(11.5f);
        }
    }

    // 애니메이션 관련

    public void End() 
    {
        anim.SetBool("Effect", false);
    }
    public void Effect_False() 
    {
        gameObject.GetComponent<Image>().enabled = false;
        GameObject.Find("Stage_2_Boss 1").GetComponent<Boss_Pattern>().pt_bool[3] = true;
        Destroy(GameObject.FindWithTag("Infinity"));
    }
}
