using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject arrow, arrow_Pos;
    public int direction, cnt;
    public float arrow_speed, arrow_life, delay, shot_timmer, timmer;

    Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (shot_timmer <= timmer) 
        {
            StartCoroutine(Tureet_fire());
            timmer = 0f;
        }
        
        // 사격중이 아닌 경우 시간 증가 금지
        if (timmer <= shot_timmer)
        {
            timmer += Time.deltaTime;
        }
    }

    IEnumerator Tureet_fire()
    {
        for (int i = 0; i < cnt; i++)
        {
            anim.SetBool("Fire", true);
            arrow.GetComponent<Turret_Arrow>().speed = arrow_speed;
            arrow.GetComponent<Turret_Arrow>().dir = direction;
            arrow.GetComponent<Turret_Arrow>().life = arrow_life;
            Instantiate(arrow, arrow_Pos.transform.position, transform.rotation);
            Invoke("Anim_False", 0.133f);
            yield return new WaitForSeconds(delay);
        }
    }

    public void Anim_False() 
    {
        anim.SetBool("Fire", false);
    }
}
