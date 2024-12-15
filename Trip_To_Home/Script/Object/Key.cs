using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject[] objects;
    public int number;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            // é��1 3��������
            if (GameManager.instance.area_num == 1 && GameManager.instance.stage_num == 4)
            {
                objects[0].GetComponent<Ground_Wall>().on_off = false;
                objects[1].GetComponent<Falling_Ground>().dont_break = false;
            }
            // 2 é�� 3��������
            else if (GameManager.instance.area_num == 2 && GameManager.instance.stage_num == 4)
            {
                objects[0].GetComponent<Ground_Wall>().on_off = false;
                objects[1].GetComponent<Falling_Ground>().dont_break = false;
            }
            // 3é�� 5��������
            else if (GameManager.instance.area_num == 3 && GameManager.instance.stage_num == 6)
            {
                objects[0].SetActive(true);
            }
            // 4é�� 1��������
            else if (GameManager.instance.area_num == 4 && GameManager.instance.stage_num == 2)
            {
                objects[0].GetComponent<Ground_Wall>().on_off = false;
                objects[1].GetComponent<Ground_Wall>().on_off = false;
                objects[2].SetActive(true);
            }
            // 4é�� 2��������
            else if (GameManager.instance.area_num == 4 && GameManager.instance.stage_num == 3)
            {
                objects[0].GetComponent<Ground_Wall>().on_off = true;
            }
            // 4é�� 5�������� (1)
            else if (GameManager.instance.area_num == 4 && GameManager.instance.stage_num == 6 && number == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    int rnd = Random.Range(1, 4);
                    Instantiate(objects[rnd], objects[0].transform.position, transform.rotation);
                }
            }// 4é�� 5�������� (2)
            else if (GameManager.instance.area_num == 4 && GameManager.instance.stage_num == 6 && number == 1)
            {
                objects[0].GetComponent<Ground_Wall>().on_off = true;
            }
            Destroy(gameObject);
        }
    }
}
