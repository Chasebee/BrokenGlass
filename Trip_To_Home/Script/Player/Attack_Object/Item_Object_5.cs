using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Object_5 : MonoBehaviour
{
    // ���� ��Ʈ�� ( dmg_area ) �±׸� ����ϴ� ��ü�� ��ũ��Ʈ
    public float cooltime = 10;
    public float effect_time = 0;
    public float deal_delay_time;
    public int dmg;
    public GameObject player;
    public string object_name;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        // ������ ���� ( �̸����� ���� )
        if (object_name == "Corruption") 
        {
            dmg = (int)(GameManager.instance.atk * 0.2);
            if (dmg <= 0) 
            {
                dmg = 1;
            }
            deal_delay_time = 0.4f;
        }
    }

    void Update()
    {
        transform.position =player.transform.position;
        effect_time += Time.deltaTime;
        // ���ӽð�
        if (cooltime <= effect_time) 
        {
            Destroy(gameObject);
        }
    }

}
