using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Script : MonoBehaviour
{
    public int h, loop;
    public bool shop, quest;
    public string npc_name;
    public Quest[] quest_list;
    public int quest_select;

    public Equip_Item[] equip_items;
    public Use_Item[] use_items;
    public Etc_Item[] etc_items;

    public GameObject npc_text_bubble;
    public string[] dials;
    void Start()
    {
        InvokeRepeating("NPC_Handle", 0f, loop);
    }
    void Update()
    {
        if (h == 1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (h == -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void NPC_Handle() 
    {
        h = Random.Range(-1, 2);
        loop = Random.Range(5, 11);
    }

}
