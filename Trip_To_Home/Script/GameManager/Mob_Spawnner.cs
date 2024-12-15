using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Mob_Spawnner : MonoBehaviour
{
    public GameObject parent;
    public Transform[] spawn_Pos;
    public GameObject[] monsters;
    void Start()
    {
        StartCoroutine(mob_Spawn());
    }

    IEnumerator mob_Spawn() 
    {
        for (int i = 0; i < spawn_Pos.Length; i++)
        {
            for (int l = 0; l < spawn_Pos[i].GetComponent<Mob_Spawn_Child>().m_count; l++)
            {
                int ran = Random.Range(0, monsters.Length);
                GameObject spawn = Instantiate(monsters[ran], spawn_Pos[i]);
                spawn.transform.parent = parent.transform;
                yield return new WaitForSeconds(0.07f);
            }
        }
    }
}
