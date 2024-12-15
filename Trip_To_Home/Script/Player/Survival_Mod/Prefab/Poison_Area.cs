using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_Area : MonoBehaviour
{
    SpriteRenderer rend;
    public float timer;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        StartCoroutine(Alpha_Plus());
    }
    void Update() 
    {
        timer += Time.deltaTime;

        if (timer >= 6.5f) 
        {
            timer = -100;
            StartCoroutine(Alpha_Minus());
        }
    }
    IEnumerator Alpha_Plus()
    {
        for (int i = 0; i < 10; i++)
        {
            float f = i / 10.0f;
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Alpha_Minus()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("S_Monster")) 
        {
            collision.gameObject.GetComponent<SurvivalMod_Monster>().area_dmg[0] = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("S_Monster"))
        {
            collision.gameObject.GetComponent<SurvivalMod_Monster>().area_dmg[0] = false;
        }
    }
}
