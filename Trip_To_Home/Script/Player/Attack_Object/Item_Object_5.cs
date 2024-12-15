using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Object_5 : MonoBehaviour
{
    // 범위 도트뎀 ( dmg_area ) 태그를 사용하는 객체의 스크립트
    public float cooltime = 10;
    public float effect_time = 0;
    public float deal_delay_time;
    public int dmg;
    public GameObject player;
    public string object_name;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        // 데미지 설정 ( 이름으로 결정 )
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
        // 지속시간
        if (cooltime <= effect_time) 
        {
            Destroy(gameObject);
        }
    }

}
