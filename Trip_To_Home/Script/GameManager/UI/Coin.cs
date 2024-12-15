using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public int value;
    public Sprite[] coint_img;
    float[] percent = { 83.25f, 11.7f, 5.05f};
    SpriteRenderer rend;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        // 1¿ø 5¿ø 10¿ø
        float total = 0;
        for(int i = 0; i < percent.Length; ++i) 
        {
            total += percent[i];
        }
        int piv = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));
        float weight = 0;
        for(int i = 0; i < percent.Length ; ++i) 
        {
            weight += percent[i];
            if (piv <= weight) 
            {
                value = i;
                break;
            }
        }

        if (value == 1)
        {
            rend.sprite = coint_img[0];
        }
        else if (value == 2)
        {
            rend.sprite = coint_img[1];
        }
    }
}
