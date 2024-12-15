using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survival_Mod_Monster_Attack : MonoBehaviour
{
    public int mon_num;
    public float[] pattern_timer;
    public GameObject[] ptn_object;

    void Update()
    {
        pattern_timer[0] += Time.fixedDeltaTime;
        if (pattern_timer[0] >= pattern_timer[1])
        {
            pattern_timer[0] = 0;
            StartCoroutine(Pattern());
        }
    }

    IEnumerator Pattern() 
    {
        // 특정 패턴을 가진 몬스터 구분
        if (mon_num == 7)
        {
            for (int i = 0; i < 3; i++) 
            {
                ptn_object[0].GetComponent<Trace_Arrow_Monster>().dmg = gameObject.GetComponent<SurvivalMod_Monster>().m_atk;
                Instantiate(ptn_object[0], transform.position, Quaternion.Euler(0,0,0));
                yield return new WaitForSeconds(1.7f);
            }
        }
    }
}
