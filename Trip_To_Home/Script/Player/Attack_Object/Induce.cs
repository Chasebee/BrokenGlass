using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Induce : MonoBehaviour
{
    public GameObject enemy;

    void Update()
    {
        if (enemy == null) 
        {
            transform.parent.GetComponent<arrow>().induce_target = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemy == null) 
        {
            if (collision.gameObject.CompareTag("Monster2")) 
            {
                enemy = collision.gameObject;
                transform.parent.GetComponent<arrow>().target_pos = enemy.GetComponent<Transform>();
                transform.parent.GetComponent<arrow>().induce_target = true;
            }
        }    
    }
}
