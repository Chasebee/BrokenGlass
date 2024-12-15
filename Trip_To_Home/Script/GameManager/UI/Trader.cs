using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trader : MonoBehaviour
{
    public GameObject btm;
    BattleManager bt;

    SpriteRenderer rend;

    public GameObject Shop_Item;
    public GameObject lv_Item;
    public GameObject pic;
    public int[] food_list, option_list;
    public void Start()
    {
        btm = GameObject.FindWithTag("Manager");
        bt = GameObject.FindWithTag("Manager").GetComponent<BattleManager>();
        rend = GetComponent<SpriteRenderer>();
        Item_Option();
    }

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Transform target = player.transform;
        float dir = target.position.x - transform.position.x;
        int dir_set = (dir < 0) ? -1 : 1;
        if (dir_set == -1)
        {
            rend.flipX = false;
        }
        else 
        {
            rend.flipX = true;
        }
    }

    public void Item_Option() 
    {
        // 여기서 레벨업 옵션 개 음식 아이템 4개 총 5개로 만들자
        // 음식 아이템
        for (int i = 0; i < food_list.Length; i++)
        {
            food_list[i] = i;
        }
        for (int i = 0; i < food_list.Length; i++)
        {
            int temp = food_list[i];
            int rnd = Random.Range(0, food_list.Length);
            food_list[i] = food_list[rnd];
            food_list[rnd] = temp;
        }

        for (int i = 0; i < 4; i++)
        {
            Shop_Item.GetComponent<Shop_Item>().item_num = food_list[i];
            Shop_Item.GetComponent<Shop_Item>().type = 0;
            GameObject shop_item = Instantiate(Shop_Item,bt.shop_item_pos);
            bt.shop_items.Add(shop_item);
        }

        // 옵션 아이템
        for (int i = 0; i < option_list.Length; i++)
        {
            option_list[i] = i;
        }
        for (int i = 0; i < option_list.Length; i++)
        {
            int temp = option_list[i];
            int rnd = Random.Range(0, option_list.Length);
            option_list[i] = option_list[rnd];
            option_list[rnd] = temp;
        }

        for (int i = 0; i < 1; i++) 
        {
            Shop_Item.GetComponent<Shop_Item>().item_num = option_list[i];
            Shop_Item.GetComponent<Shop_Item>().type = 1;
            GameObject item = Instantiate(Shop_Item, bt.shop_item_pos);
            bt.shop_items.Add(item);
        }

    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            pic.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            pic.SetActive(false);
    }
}
