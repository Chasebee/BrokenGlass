using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Area : MonoBehaviour
{
    public bool heal_on;
    public int dmg;
    public int heal_val;
    public ParticleSystem heal_Effect;

    CircleCollider2D circle;

    void Start()
    {
        circle = GetComponent<CircleCollider2D>();        
    }

    void Update()
    {
        float sca_x = transform.localScale.x / 5;
        float sca_y = transform.localScale.y / 5;
        heal_Effect.transform.localScale = new Vector3(sca_x, sca_y, 1);
        if (heal_on == true)
        {
            heal_Effect.Play();
            circle.enabled = true;
            heal_on = false;
        }
        else if (heal_on == false) 
        {
            circle.enabled = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("S_Monster"))
        {
            circle.enabled = false;
            bool critical = false;
            dmg = (int)(GameManager.instance.atk * 0.15) + (int)(heal_val * 1.3);
            int crit = (int)(Random.Range(0, 100) + GameManager.instance.crit_per);
            if (crit >= 80)
            {
                dmg = (int)(dmg * GameManager.instance.crit_dmg);
                critical = true;
            }
            collision.gameObject.GetComponent<SurvivalMod_Monster>().mhp -= dmg;
            collision.gameObject.GetComponent<SurvivalMod_Monster>().dmg = dmg;
            if (critical == true)
            {
                collision.gameObject.GetComponent<SurvivalMod_Monster>().critical = true;
            }
            collision.gameObject.GetComponent<SurvivalMod_Monster>().hit = true;
        }
    }
}
