using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Hp_Bar : MonoBehaviour
{
    public GameObject[] boss_Object;
    public int[] boss_maxhp = new int[4];
    public GameObject boss;
    public Slider boss_hpbar;

    public int area;

    void Start()
    {
        switch (GameManager.instance.area_num)
        {
            case 1:
                area = 0;
                break;
            case 2:
                area = 1;
                break;
            case 3:
                area = 2;
                break;
            case 4:
                area = 3;
                break;
        }
        boss = boss_Object[area];
    }
    void Update()
    {
        if (boss != null)
        {
            gameObject.SetActive(true);
            boss_hpbar.value = (float)boss.GetComponent<Monster2>().mhp / (float)boss_maxhp[area];
        }
        else 
        {
            gameObject.SetActive(false);
        }
    }
}
