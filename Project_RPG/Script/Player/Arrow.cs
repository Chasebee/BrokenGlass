using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    SpriteRenderer rend;
    public int atk, num, type, attack_type;
    public bool piercing;
    public float dmg, dmg_reset;
    public HashSet<GameObject> piercing_object = new HashSet<GameObject>();
    public int way;
    public float speed, skill_var;
    float timer;
    public bool ciritical;
    public ParticleSystem particle;
    public bool full;
    Animator anim;
    void Start()
    {
        anim= GetComponent<Animator>();
        anim.SetInteger("Num", num);
        rend = GetComponent<SpriteRenderer>();
        float crit = Random.Range(0.0f, 1.0f);
        
        // 치명타 처리
        if (GameManager.instance.critical >= crit)
        {
            dmg = dmg * 1.3f;
            ciritical = true;
        }
        atk = (int)dmg;
        
        GameObject player = GameObject.FindWithTag("Player");
        if (player.GetComponent<Player_Set>().dir == -1)
        {
            way = -1;
        }
        else if(player.GetComponent<Player_Set>().dir == 1) 
        {
            way = 1;
        }

        // 파티클 관련
        if (full) 
        {
            particle.gameObject.SetActive(true);
            if (particle != null) 
            {
                var shape = particle.shape;
                particle.Play();
                if (way == -1)
                {
                    shape.rotation = new Vector3(0, 180, 0);
                }
                else if(way == 1)
                {
                    shape.rotation = new Vector3(0, 0, 0);
                }
            }
        }

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (way == -1)
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
        else if (way == 1)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }

        // 검사 검기 패시브
        if (type == 1)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / 0.6f);
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, alpha);
            if (timer >= 0.3f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (timer >= 1.5f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Damage_Reset() 
    {
        if (type == 1)
        {
            if (num == 0) 
            {
                dmg = (int)(Random.Range(GameManager.instance.atk * 0.55f, GameManager.instance.atk * 1.05f));
                atk = (int)dmg;
                Critical_Calcul();
            }
        }
        else
        {
            // 피어스 샷
            if (num == 1)
            {
                dmg = (int)(dmg_reset * Random.Range(GameManager.instance.magic, GameManager.instance.magic * 1.15f) * skill_var);
                atk = (int)dmg;
                Critical_Calcul();
            }
        }
    }

    void Critical_Calcul()
    {
        float crit = Random.Range(0.0f, 1.0f);
        // 치명타 처리
        if (GameManager.instance.critical >= crit)
        {
            atk = (int)(dmg * 1.3f);
            ciritical = true;
        }
        else
        {
            ciritical = false;
        }
    }
}
