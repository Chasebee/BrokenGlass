using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luna_Altar : MonoBehaviour
{
    public Sprite[] img, correct_img;
    public GameObject object_pattern;
    public float time, timmer;
    public int correct, img_num;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        if (correct == 0)
        {
            object_pattern.GetComponent<SpriteRenderer>().sprite = img[img_num];
        }
        else 
        {
            object_pattern.GetComponent<SpriteRenderer>().sprite = correct_img[img_num];
        }
    }

    void Update() 
    {
        if (time >= timmer)
        {
            timmer += Time.deltaTime;
        }

        if (timmer >= time) 
        {
            
            if (correct != 1) 
            {
                Destroy(gameObject);
                GameObject.Find("Stage_2_Boss 1").GetComponent<Boss_Pattern>().pt_time[2] = timmer;
            }    
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && correct == 1) 
        {
            collision.gameObject.layer = 9;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && correct == 1) 
        {
            collision.gameObject.layer = 8;
        }
    }
}
