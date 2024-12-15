using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Map_Sign : MonoBehaviour
{
    public string map_name;
    public TextMeshPro map_text;
    void Start()
    {
        map_text.text = map_name;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            map_text.gameObject.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            map_text.gameObject.SetActive(false);
        }
    }
}
