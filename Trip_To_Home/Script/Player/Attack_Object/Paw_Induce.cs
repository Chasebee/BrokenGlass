using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paw_Induce : MonoBehaviour
{
    public GameObject enemy;

    void Update()
    {
        if (enemy == null)
        {
            transform.parent.GetComponent<Paw>().target = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemy == null)
        {
            if (collision.gameObject.CompareTag("Monster2"))
            {
                enemy = collision.gameObject;
                transform.parent.GetComponent<Paw>().enemey = enemy.GetComponent<Transform>();
                transform.parent.GetComponent<Paw>().target = true;
            }
            if (collision.gameObject.CompareTag("Summon"))
            {
                enemy = collision.gameObject;
                transform.parent.GetComponent<Paw>().enemey = enemy.GetComponent<Transform>();
                transform.parent.GetComponent<Paw>().target = true;
            }
        }
    }
}
