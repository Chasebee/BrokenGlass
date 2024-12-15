using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Spawner_Filed : MonoBehaviour
{
    public int mob_max; // ���Ͱ� �ʿ� ������ �� �ִ� �ִ� ��
    public int now_max; // ���Ͱ� �ʿ� ������ �� �ִ� �ִ� ��
    public float spawn_timer; // mob_max �� ������ ���� ������ ���� Ÿ�̸Ӹ� �۵�
    public float spawn_time; // �� �ʵ��� ���� �ð�
    public int spawn_cnt; // �����Ǵ� ������ ��
    public GameObject[] spawn_position; // ���� ��ȯ�Ǵ� ��ġ
    public GameObject[] spawn_mob; // ���� ����Ʈ�� ( �������� �־�д� )

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
        // spawn_position���� �������� ��ġ ����
        int spawnPositionIndex = Random.Range(0, spawn_position.Length);
        Vector3 spawnPos = spawn_position[spawnPositionIndex].transform.position;

        // spawn_mob���� �������� ���� ����
        int mobIndex = Random.Range(0, spawn_mob.Length);
        GameObject mobPrefab = spawn_mob[mobIndex];

        // ���� ��ȯ
        Instantiate(mobPrefab, spawnPos, Quaternion.identity);
    }
}
