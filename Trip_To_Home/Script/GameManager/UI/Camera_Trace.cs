using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera_Trace : MonoBehaviour
{
    public GameObject player;
    public Transform target_pos;
    public float c_size = 3.5f;
    public bool dark;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        target_pos = player.GetComponent<Transform>();
        if (GameManager.instance.survival_Mod == true) 
        {
            c_size = 0.3f;
        }
    }

    void LateUpdate()
    {        
        if (GameManager.instance.survival_Mod == false)
        {
        Vector3 player_pos = new Vector3(target_pos.position.x, target_pos.position.y + c_size, target_pos.position.z);
        transform.position = Vector3.Lerp(transform.position, player_pos, Time.deltaTime * (GameManager.instance.speed - 1));
        transform.position = new Vector3(transform.position.x, transform.position.y, -21f);


            if (dark == true)
            {
                if (gameObject.GetComponent<Camera>().orthographicSize >= 2.5f)
                {
                    gameObject.GetComponent<Camera>().orthographicSize -= 0.05f;
                }
                if (c_size >= 1)
                {
                    c_size -= 0.02f;
                }
            }
            else
            {
                if (gameObject.GetComponent<Camera>().orthographicSize <= 7f)
                {
                    gameObject.GetComponent<Camera>().orthographicSize += 0.05f;
                }
                if (c_size <= 3.5)
                {
                    c_size += 0.02f;
                }
            }
        }
    }
    private void FixedUpdate()
    {

        if (GameManager.instance.survival_Mod == true)
        {
            Vector3 player_pos = new Vector3(target_pos.position.x, target_pos.position.y + c_size, target_pos.position.z);
            transform.position = Vector3.Lerp(transform.position, player_pos, Time.deltaTime * (GameManager.instance.speed - 1));
            transform.position = new Vector3(transform.position.x, transform.position.y, -21f);

            gameObject.GetComponent<Camera>().orthographicSize = 5f;
        }
    }
}
