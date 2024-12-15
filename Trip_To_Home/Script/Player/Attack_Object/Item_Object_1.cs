using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item_Object_1 : MonoBehaviour
{
    public GameObject player;
    public Transform center; // 중심점
    public float radius;
    public float speed = 3.0f;

    private float angle = 0;

    public float timmer = 0.0f;
    public float cooltime = 5f;
    public LayerMask isLayer;
    public bool hit;
    public GameObject last_Hit;
    RaycastHit2D collision;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        center = player.transform;
    }

    void Update()
    {
        // 레이캐스트 - 박스캐스트
        collision = Physics2D.BoxCast(gameObject.transform.position, gameObject.transform.localScale, 0, Vector2.left, 0, isLayer);

        if (collision.collider != null && hit == false && last_Hit != collision.collider.gameObject)
        {
            if (collision.collider.CompareTag("Monster2"))
            {
                int dmg = (int)(GameManager.instance.atk * 0.75) * -1;
                // 미스처리
                if (dmg > 0)
                {
                    dmg = -1;
                }
                collision.collider.GetComponent<Monster2>().mhp += dmg;
                dmg *= -1;
                collision.collider.GetComponent<Monster2>().dmg = dmg;
                collision.collider.GetComponent<Monster2>().hit = true;
                hit = true;
                last_Hit = collision.collider.gameObject;
            }

            else if (collision.collider.CompareTag("Summon"))
            {
                int dmg = (int)(GameManager.instance.atk * 0.75) * -1;
                // 미스처리
                if (dmg > 0)
                {
                    dmg = -1;
                }
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().mhp += dmg;
                dmg *= -1;
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().dmg = dmg;
                collision.collider.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().hit = true;
                hit = true;
                last_Hit = collision.collider.gameObject;
            }

            else if (collision.collider.CompareTag("S_Monster"))
            {
                bool critical = false;
                int dmg = (int)(GameManager.instance.atk * 0.75);
                int crit = (int)(Random.Range(0, 100) + GameManager.instance.crit_per);
                if (crit >= 80)
                {
                    dmg = (int)(dmg * GameManager.instance.crit_dmg);
                    critical = true;
                }
                collision.collider.GetComponent<SurvivalMod_Monster>().mhp -= dmg;
                collision.collider.GetComponent<SurvivalMod_Monster>().dmg = dmg;
                if (critical == true)
                {
                    collision.collider.GetComponent<SurvivalMod_Monster>().critical = true;
                }
                collision.collider.GetComponent<SurvivalMod_Monster>().hit = true;
                hit = true;
                last_Hit = collision.collider.gameObject;
            }
        }

        if (hit == true) 
        {
            hit = false;
        }
        if (collision.collider == null) 
        {
            last_Hit = null;
        }

        angle += speed * Time.deltaTime;
        transform.position = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

        timmer += Time.deltaTime;

        if (timmer >= cooltime) 
        {
            Destroy(gameObject);
        }
    }

}
