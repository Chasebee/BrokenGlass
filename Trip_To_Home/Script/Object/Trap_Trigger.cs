using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap_Trigger : MonoBehaviour
{
    public GameObject trap;
    public GameObject clear_Object;
    public GameObject reward;
    public GameObject[] traps;
    public int type;

    void Update()
    {
        if (type == 1 && clear_Object != null)
        {
            // ����ִ°� �¾�
        }
        else if (type == 1 && clear_Object == null) 
        {
            reward.SetActive(true);
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && type == 0)
        {
            trap.GetComponent<Ground_Wall>().on_off = true;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player") && type == 1)
        {
            Debug.Log("�׽�Ʈ");
            traps[0].GetComponent<Ground_Wall>().on_off = true;
            traps[1].GetComponent<Ground_Wall>().on_off = true;
        }
    }
}
