using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack_Magic_Summon : MonoBehaviour
{
    public GameObject center_obj;
    public Transform center_pos;
    float radius = 3;
    public float speed = 3.0f;

    private float angle = 0;
    public LayerMask isLayer;
    Vector3 destroy_pos;
    void Start() 
    {
        center_pos = center_obj.transform;
    }
    void Update()
    {
        if (center_pos != null)
        {
            angle += speed * Time.deltaTime;
            destroy_pos = center_pos.position;
            transform.position = center_pos.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        }
        else
        {
            transform.position = destroy_pos + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        }

        if (radius >= 0.1f)
        {
            radius -= 0.02f;
            speed += 0.02f;
        }
        else if (radius < 0.1f) 
        {
            Destroy(gameObject);
        }

        Vector3 scale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        // 레이캐스트 - 박스캐스트
        RaycastHit2D col = Physics2D.BoxCast(gameObject.transform.position, scale, 0, Vector2.left, 0, isLayer);

        if (col.collider != null) 
        {
            if (col.collider.gameObject.name == center_obj.name && col.collider.CompareTag("Monster2"))
            {
                int dmg = (int)(GameManager.instance.atk * GameManager.instance.attack_object_cnt[24]);
                int cal = center_obj.GetComponent<Monster2>().mdef - dmg;
                if (cal >= 0) 
                {
                    cal = -1;
                }
                center_obj.GetComponent<Monster2>().mhp += cal;
                cal *= -1;
                center_obj.GetComponent<Monster2>().hit = true;
                center_obj.GetComponent<Monster2>().dmg = cal;

                Destroy(gameObject);
            }

            if (col.collider.gameObject.name == center_obj.name && col.collider.CompareTag("Summon"))
            {
                int dmg = (int)(GameManager.instance.atk * GameManager.instance.attack_object_cnt[24]);
                int cal = center_obj.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mdef - dmg;
                if (cal >= 0)
                {
                    cal = -1;
                }
                center_obj.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp += cal;
                cal *= -1;
                center_obj.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;
                center_obj.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = cal;

                Destroy(gameObject);
            }
        }
    }
}
