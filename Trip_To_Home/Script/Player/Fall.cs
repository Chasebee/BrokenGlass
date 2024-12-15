using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public RectTransform pos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = pos.position;
            int dmg = (int)(GameManager.instance.maxhp * 0.1);
            if (dmg <= 1) 
            {
                dmg = 1;
            }
            GameManager.instance.hp -= dmg;
        }
        else if (collision.gameObject.CompareTag("Monster2"))
        {
            collision.gameObject.GetComponent<Monster2>().mhp = 0;
        }
    }

}
