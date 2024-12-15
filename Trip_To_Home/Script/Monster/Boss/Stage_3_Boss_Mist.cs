using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_3_Boss_Mist : MonoBehaviour
{
    GameObject cameras;
    float time;
    void Start() 
    {
        cameras = GameObject.Find("Main Camera");
    }

    void Update() 
    {
        time += Time.deltaTime;

        if (time >= 6.5f) 
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            cameras.GetComponent<Camera_Trace>().dark = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            cameras.GetComponent<Camera_Trace>().dark = false;
        }
    }
}
