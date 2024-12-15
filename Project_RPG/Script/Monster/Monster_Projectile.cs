using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Projectile : MonoBehaviour
{
    Animator anim;
    BoxCollider2D b_col;
    float timer;
    
    public int way;
    public int atk;
    public float speed, criti, time,knock;
    public bool critical;
    void Start() 
    {
        anim = GetComponent<Animator>();
        b_col = GetComponent<BoxCollider2D>();
        float crit = Random.Range(0.0f, 1.0f);

        if (criti >= crit) 
        {
            atk = (int)(atk * 1.3f);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (way == -1)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        else if (way == 1)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }

        if (timer >= time) 
        {
            Destroy(gameObject);
        }
    }
}
