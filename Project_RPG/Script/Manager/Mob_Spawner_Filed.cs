using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Spawner_Filed : MonoBehaviour
{
    public int mob_max; // 몬스터가 맵에 존재할 수 있는 최대 값
    public int now_max; // 몬스터가 맵에 존재할 수 있는 최대 값
    public float spawn_timer; // mob_max 의 수보다 몹이 적을시 스폰 타이머를 작동
    public float spawn_time; // 이 필드의 스폰 시간
    public int spawn_cnt; // 스폰되는 몬스터의 수
    public GameObject[] spawn_position; // 몹이 소환되는 위치
    public GameObject[] spawn_mob; // 몹의 리스트들 ( 프리팹을 넣어둔다 )

    void Start() 
    {
        if (mob_max <= 10)
        {
            spawn_cnt = 4;
        }
        else if (mob_max >= 11 && mob_max <= 20)
        {
            spawn_cnt = 7;
        }
        else if (mob_max >= 21 && mob_max <= 30)
        {
            spawn_cnt = 11;
        }
    }

    void Update() 
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("Monster");
        now_max = a.Length;

        if(mob_max >= now_max) 
        {
            spawn_timer += Time.deltaTime;
        }
        if (spawn_timer >= spawn_time) 
        {
            for(int i = 0; i < spawn_cnt; i++) 
            {
                Spawn_Random_Mob();
            }
            spawn_timer = 0;
        }
    }
    void Spawn_Random_Mob()
    {
        // spawn_position에서 랜덤으로 위치 선택
        int spawnPositionIndex = Random.Range(0, spawn_position.Length);
        Vector3 spawnPos = spawn_position[spawnPositionIndex].transform.position;

        // spawn_mob에서 랜덤으로 몬스터 선택
        int mobIndex = Random.Range(0, spawn_mob.Length);
        GameObject mobPrefab = spawn_mob[mobIndex];

        // 몬스터 소환
        Instantiate(mobPrefab, spawnPos, Quaternion.identity);
    }
}
