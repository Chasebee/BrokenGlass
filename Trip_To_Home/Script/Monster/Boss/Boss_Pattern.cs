using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Pattern : MonoBehaviour
{
    // 기본적 내용
    public GameObject btm;
    public BattleManager bt;
    public int boss_number;
    // 타이머 관련
    public float timmer;
    public bool timmer_stop;
    // 보스 패턴 객체 관련
    public GameObject[] pt_1;
    public float[] pt_time;
    public Transform[] drop_pos;
    public int[] pt_cnt;
    public bool[] pt_bool;
    public float circle_Scale;
    bool special;
    // Start End 합쳐서 360 나와야함
    public int angle_Interval, start_Angle, end_Angle;

    // 코루틴
    private IEnumerator rota_bullet, rota_bullet2;

    public void Start()
    {
        btm = GameObject.FindWithTag("Manager");
        bt = GameObject.FindWithTag("Manager").GetComponent<BattleManager>();
        // 보스 넘버는 몬스터 넘버로 대체된다!
        boss_number = gameObject.GetComponent<Monster2>().monster_number;

        if (boss_number == 10)
        {
            if (gameObject.GetComponent<Monster2>().anim.GetBool("Visible") == false) 
            {
                gameObject.GetComponent<Monster2>().anim.SetBool("Visible", true);
            }
            Sun_Luna();
            rota_bullet = Rotate_Bullet(0, 90, 180, 270);
            rota_bullet2 = Rotate_Bullet2();
        }
    }

    void Update()
    {
        // 챕터 1 보스
        if (boss_number == 3)
        {
            if (timmer_stop == false)
                timmer += Time.deltaTime;

            if (timmer >= 16f)
            {
                timmer_stop = true;
                timmer = 0f;

                // 랜덤 패턴 생성
                int rnd = Random.Range(0, 3);

                // 발악 패턴 ( 패턴 고정 )
                if (gameObject.GetComponent<Monster2>().mhp <= 650)
                {
                    rnd = 0;
                }

                // 보스 패턴 랜덤 발생
                Boss_Attack(rnd);
            }
        }

        // 챕터 2 보스
        if(boss_number == 10) 
        {
            if (gameObject.GetComponent<Monster2>().anim.GetBool("Visible") == true)
            {
                // 패턴 발생
                if (timmer_stop == false) 
                {
                    timmer += Time.deltaTime;
                }
                if (timmer >= 15f) 
                {
                    timmer_stop = true;
                    timmer = 0f;
                    CancelInvoke();

                    int rnd = Random.Range(0, 4);

                    
                    Boss_Attack(rnd);
                    // 기본 패턴을 3번 한다!
                    pt_cnt[0]++;
                }

                // 태양, 달 문양
                if (pt_time[0] <= 0)
                {
                    pt_time[0] = 120f;
                    CancelInvoke("Invisible_Animation");
                    CancelInvoke("Visible_Animation");
                    CancelInvoke("Visible_On");
                    Sun_Luna();
                }
                // 무력화 타임
                if (pt_bool[2] == true) 
                {
                    pt_time[1] -= Time.deltaTime;

                    if (pt_time[1] <= 0) 
                    {
                        if (pt_1[4].GetComponent<Slider>().value >= 1)
                        {
                            gameObject.GetComponent<Monster2>().anim.SetBool("Heal", false);
                            gameObject.GetComponent<Monster2>().mhp += (int)(9370 * 0.15);
                            if (gameObject.GetComponent<Monster2>().mhp >= 9370) 
                            {
                                gameObject.GetComponent<Monster2>().mhp = 9370;
                            }
                            pt_time[1] = 0f;
                            pt_bool[2] = false;
                            pt_1[4].SetActive(false);
                            timmer_stop = false;
                            Invisible_Animation();
                        }
                    }

                    if (pt_1[4].GetComponent<Slider>().value <= 0)
                    {
                        gameObject.GetComponent<Monster2>().anim.SetBool("Heal", false);
                        pt_time[1] = 0f;
                        pt_bool[2] = false;
                        pt_1[4].SetActive(false);
                        timmer_stop = false;
                        Invisible_Animation();
                    }
                }
            }
            pt_time[0] -= Time.deltaTime;

            // 무력화 패턴 게이지 위치 지정
            if (pt_bool[2] == true) 
            {
                pt_1[4].transform.position = Camera.main.WorldToScreenPoint(transform.position);
            }

            // '태양' 탄막 패턴 중단
            if (pt_bool[3] == true)
            {
                Invisible_Animation();
                pt_bool[3] = false; 
                timmer_stop = false;
                StopCoroutine(rota_bullet);
                StopCoroutine(rota_bullet2);
            }

            // 달 (스턴)
            if (pt_time[2] >= 15f) 
            {
                pt_1[16].GetComponent<Image>().enabled = true;
                pt_1[16].GetComponent<Stage_2_Luna_Effect>().anim.SetBool("Effect", true);
                pt_time[2] = 0;
            }

            // 짤몹 소환
            if (30f >= pt_time[3]) 
            {
                pt_time[3] += Time.deltaTime;
            }
            if (pt_time[3] >= 30f) 
            {
                int rnd = Random.Range(2, 5);
                StartCoroutine(Summon_Snake(rnd));
                pt_time[3] = 0f;
            }
        }

        // 챕터 3 보스
        if (boss_number == 15) 
        {
            // pt_time 배열 변수가 따로 작동함 얘는
            if (60f > timmer)
            {
                timmer += Time.deltaTime;
            }
            else if (timmer > 60f)
            {
                // -29 ~ 7 좌표중 랜덤으로 소환된다. (소환 쫄몹)
                timmer = 0f;
                int rnd = Random.Range(2, 5);
                StartCoroutine(Summon_Monster(rnd));
            }

            // 먹물 패턴
            if (pt_time[3] > 35f)
            {
                pt_time[3] = 0f;
                StartCoroutine(Mist_Summon());
            }
            else if (pt_time[3] < 35f) 
            {
                pt_time[3] += Time.deltaTime;
            }

            // 촉수 패턴
            if (gameObject.GetComponent<Monster2>().mhp > 12375) 
            {
                if (pt_time[0] < 14) 
                {
                    pt_time[0] += Time.deltaTime;
                }
                if (pt_time[0] >= 14f)
                {
                    pt_time[0] = 0f;
                    int rnd = Random.Range(1, 4);
                    StartCoroutine(Summon_Tentacle_1(rnd, 0.6f));
                }
            }
            else if (gameObject.GetComponent<Monster2>().mhp <= 12375 && gameObject.GetComponent<Monster2>().mhp > 6588)
            {
                if (pt_time[1] < 12)
                {
                    pt_time[1] += Time.deltaTime;
                }
                if (pt_time[1] >= 12f)
                {
                    pt_time[1] = 0f;
                    int rnd = Random.Range(4, 8);
                    StartCoroutine(Summon_Tentacle_1(rnd, 0.45f));
                }
            }
            else if (gameObject.GetComponent<Monster2>().mhp <= 6588)
            {
                if (pt_time[2] < 11)
                {
                    pt_time[2] += Time.deltaTime;
                }
                if (pt_time[2] >= 11f)
                {
                    pt_time[2] = 0f;
                    int rnd = Random.Range(5, 10);
                    StartCoroutine(Summon_Tentacle_1(rnd, 0.35f));
                }
            }
        }

        // 챕터 4 보스
        if (boss_number == 19) 
        {
            if (timmer_stop == false) 
            {
                timmer += Time.deltaTime;
            }
            if (timmer >= 11f) 
            {
                timmer = 0f;
                timmer_stop = true;

                int rnd = Random.Range(1, 5);
                pt_cnt[0]++;
                if (pt_cnt[0] == 3)
                {
                    rnd = 5;
                }
                Boss_Attack(rnd);
            }

            // 짤몹 소환
            if (pt_time[0] <= 60f)
            {
                pt_time[0] += Time.deltaTime;
            }
            else if (pt_time[0] >= 60f) 
            {
                StartCoroutine(Summon_Monster_4());   
                pt_time[0] = 0f;
            }

            // 팔한쪽 파괴
            if (gameObject.GetComponent<Monster2>().mhp <= 29570 && gameObject.GetComponent<Monster2>().anim.GetBool("Destroy_1") == false && pt_bool[0] == false)
            {
                pt_bool[0] = true;
                gameObject.GetComponent<Monster2>().anim.SetBool("Destroy_1", true);
                Invoke("Right_Arm", 0.84f);
            }
            // 나머지 팔 한쪽 마저 파괴
            else if (gameObject.GetComponent<Monster2>().mhp <= 18050 && gameObject.GetComponent<Monster2>().anim.GetBool("Destroy_2") == false && pt_bool[1] == false)
            {
                pt_bool[1] = true;
                gameObject.GetComponent<Monster2>().anim.SetBool("Destroy_2", true);
                Invoke("Left_Arm", 0.84f);
            }

            // 차징기
            if (gameObject.GetComponent<Monster2>().anim.GetBool("Change") == true) 
            {
                pt_time[1] += Time.deltaTime;
                if (pt_time[1] > 30)
                {
                    pt_time[1] = 0f;
                    if (gameObject.GetComponent<Monster2>().anim.GetBool("Success") == false)
                    {
                        gameObject.GetComponent<Monster2>().anim.SetBool("End", true);
                        StartCoroutine(Charging_End());
                    }
                }
                else if (gameObject.GetComponent<Monster2>().anim.GetBool("Success") == true)
                {
                    gameObject.GetComponent<Monster2>().anim.SetBool("End", true);
                    StartCoroutine(Charging_End_2());
                }
            }

            // 힐 패턴
            if (gameObject.GetComponent<Monster2>().anim.GetBool("Heal") == true)
            {
                pt_1[15].transform.position = Camera.main.WorldToScreenPoint(transform.position);

                pt_time[2] += Time.deltaTime;
                if (pt_time[2] >= 11)
                {
                    if (pt_1[15].GetComponent<Slider>().value >= 1)
                    {
                        pt_1[15].SetActive(false);
                        int heal = (int)(39750 * 0.15);
                        gameObject.GetComponent<Monster2>().mhp += heal;
                        gameObject.GetComponent<Monster2>().anim.SetBool("Heal", false);
                        if (gameObject.GetComponent<Monster2>().mhp >= 39750) 
                        {
                            gameObject.GetComponent<Monster2>().mhp = 39750;
                        }
                        StartCoroutine(Heal_End());
                    }
                    else 
                    {
                        pt_1[15].SetActive(false);
                        gameObject.GetComponent<Monster2>().anim.SetBool("Heal", false);
                        StartCoroutine(Heal_End());
                    }
                    timmer_stop = false;
                    pt_time[2] = 0;
                } 
                else if (pt_time[2] <= 10)
                {
                    if (pt_1[15].GetComponent<Slider>().value <= 0)
                    {
                        pt_1[15].SetActive(false);
                        pt_time[2] = 0;
                        gameObject.GetComponent<Monster2>().anim.SetBool("Heal", false);
                        StartCoroutine(Heal_End());
                        timmer_stop = false;
                    }
                }
            }
        }
    }

    // 보스패턴
    public void Boss_Attack(int num) 
    {
        // 챕터 1 보스
        if (boss_number == 3) 
        {
            if (num == 0)
            {
                pt_1[6].SetActive(true);
                pt_1[11].SetActive(true);
                Invoke("Trap_On", 5f);
                // 발악패턴
                if (gameObject.GetComponent<Monster2>().mhp <= 650)
                {
                    StartCoroutine(Summon_1(5));
                    Trace_Shots_1();
                    Trace_Shots_2();
                }
            }
            else if (num == 1)
            {
                int rnd = Random.Range(3, 7);
                StartCoroutine(Summon_1(rnd));
            }
            else if (num == 2)
            {
                StartCoroutine(Trace_Shot_1());
            }
        }

        // 챕터 2 보스
        if (boss_number == 10)
        {
            // 특수패턴 ( 기본 패턴 3회 후 특수패턴 진입 )
            if (pt_cnt[0] >= 3)
            {
                // 태양 패턴
                if (pt_bool[0] == true)
                {
                    int ran = Random.Range(0, 3);
                    switch (ran)
                    {
                        case 0:
                            Instantiate(pt_1[12], drop_pos[8]);
                            break;
                        case 1:
                            Instantiate(pt_1[12], drop_pos[9]);
                            break;
                        case 2:
                            Instantiate(pt_1[12], drop_pos[10]);
                            break;
                    }
                    StartCoroutine(rota_bullet);
                }

                // 달 패턴
                else if (pt_bool[1] == true)
                {
                    int ran_Correct = Random.Range(0, 3);
                    int img_ran = Random.Range(0, 6);

                    if (ran_Correct == 0)
                    {
                        pt_1[13].GetComponent<Luna_Altar>().correct = 1;
                    }
                    pt_1[13].GetComponent<Luna_Altar>().img_num = img_ran;
                    Instantiate(pt_1[13], drop_pos[8]);
                    pt_1[13].GetComponent<Luna_Altar>().correct = 0;

                    if (ran_Correct == 1)
                    {
                        pt_1[13].GetComponent<Luna_Altar>().correct = 1;
                    }
                    img_ran = Random.Range(0, 6);
                    pt_1[13].GetComponent<Luna_Altar>().img_num = img_ran;
                    Instantiate(pt_1[13], drop_pos[9]);
                    pt_1[13].GetComponent<Luna_Altar>().correct = 0;

                    if (ran_Correct == 2)
                    {
                        pt_1[13].GetComponent<Luna_Altar>().correct = 1;
                    }
                    img_ran = Random.Range(0, 6);
                    pt_1[13].GetComponent<Luna_Altar>().img_num = img_ran;
                    Instantiate(pt_1[13], drop_pos[10]);
                    pt_1[13].GetComponent<Luna_Altar>().correct = 0;

                    // 방해 탄막
                    StartCoroutine(rota_bullet2);
                }
                pt_cnt[0] = 0;
            }
            // 기본 패턴
            else 
            {
                // 무력화 ( 회복 저지 )
                if (num == 0)
            {
                gameObject.GetComponent<Monster2>().anim.SetBool("Heal", true);
                pt_bool[2] = true;
                // 무력화 게이지 보이기
                pt_1[4].SetActive(true);
                pt_1[4].GetComponent<Slider>().maxValue = 250;
                pt_1[4].GetComponent<Slider>().value = 250;
                pt_time[1] = 10f;
            }

                // 회전하는 칼날 소환
                if (num == 1) 
            {
                pt_1[6].SetActive(true);
                int rnd = Random.Range(8, 15);
                StartCoroutine(Summon_Blade(rnd)); 
                timmer_stop = false;
                Invisible_Animation();
            }

                // 맵 전체 폭발
                if (num == 2) 
            {
                float rnd = Random.Range(-26, 11);
                Instantiate(pt_1[7], new Vector2(rnd, 2.68f), transform.rotation);
                pt_1[8].SetActive(true);
                StartCoroutine(Summon_Explosion());
            }
            
                // 독안개 설치
                if (num == 3) 
            {
                StartCoroutine(Summon_PoisonMist());
            }
            }
        }

        // 챕터 4 보스
        if (boss_number == 19) 
        {
            if (num == 1)
            {
                gameObject.GetComponent<Monster2>().anim.SetInteger("Attack_Num", 1);
                gameObject.GetComponent<Monster2>().anim.SetBool("Attack", true);
                StartCoroutine(Meteor_Summon());
            }
            else if (num == 2)
            {
                gameObject.GetComponent<Monster2>().anim.SetBool("Hide", true);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<Monster2>().image_Decoy.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<Monster2>().rigid.constraints = RigidbodyConstraints2D.FreezePositionY;

                Invoke("DisVisible_Effect", 6f);
            }
            else if (num == 3)
            {
                gameObject.GetComponent<Monster2>().anim.SetInteger("Attack_Num", 2);
                gameObject.GetComponent<Monster2>().anim.SetBool("Attack", true);
                StartCoroutine(Summon_FIre_Pillar());
            }
            else if (num == 4)
            {
                gameObject.GetComponent<Monster2>().anim.SetInteger("Attack_Num", 4);
                gameObject.GetComponent<Monster2>().anim.SetBool("Attack", true);
                pt_bool[1] = true;
                StartCoroutine(Boss_Heal_Pt());
            }
            else if (num == 5)
            {
                gameObject.GetComponent<Monster2>().anim.SetBool("Hide", true);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<Monster2>().image_Decoy.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<Monster2>().rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
                special = true;
                Invoke("DisVisible_Effect", 6f);
            }
        }
    }

    /*----- 스테이지 1보스 패턴 -----*/
    public void Trap_On() 
    {
        pt_1[6].SetActive(false);
        pt_1[7].SetActive(true);
        Throw();
    }

    public void Throw()
    {
        // 개체 투척 (1)
        pt_cnt[1] = Random.Range(0, 6);
        pt_1[pt_cnt[1]].SetActive(true);

        // 개체 투척 (2)
        pt_cnt[2] = Random.Range(0, 6);
        while (pt_cnt[2] == pt_cnt[1])
        {
            pt_cnt[2] = Random.Range(0, 6);
        }
        pt_1[pt_cnt[2]].SetActive(true);

        // 발악 패턴
        if (gameObject.GetComponent<Monster2>().mhp <= 650) 
        {
            // 값 초기 설정
            pt_cnt[3] = Random.Range(0, 6);
            pt_cnt[4] = Random.Range(0, 6);
            pt_cnt[5] = Random.Range(0, 6);
            // 값 검증
            while (pt_cnt[3] == pt_cnt[1] || pt_cnt[3] == pt_cnt[2])
            {
                pt_cnt[3] = Random.Range(0, 6);
                Debug.Log("pt_cnt[3] = " + pt_cnt[3]);
            }
            pt_1[pt_cnt[3]].SetActive(true);
            
            while (pt_cnt[4] == pt_cnt[1] || pt_cnt[4] == pt_cnt[2] || pt_cnt[4] == pt_cnt[3])
            {
                pt_cnt[4] = Random.Range(0, 6);
                Debug.Log("pt_cnt[4] = " + pt_cnt[4]);
            }
            pt_1[pt_cnt[4]].SetActive(true);
            
            while (pt_cnt[5] == pt_cnt[1] || pt_cnt[5] == pt_cnt[2] || pt_cnt[5] == pt_cnt[3] || pt_cnt[5] == pt_cnt[4])
            {
                pt_cnt[5] = Random.Range(0, 6);
                Debug.Log("pt_cnt[5] = " + pt_cnt[5]);
            }
            pt_1[pt_cnt[5]].SetActive(true);
        }
        Invoke("Drop", 2f);
        Invoke("Throw", 7f);
    }

    public void Drop() 
    {
        Instantiate(pt_1[8], new Vector2(drop_pos[pt_cnt[1]].position.x, drop_pos[pt_cnt[1]].position.y), transform.rotation);
        pt_1[pt_cnt[1]].SetActive(false);

        Instantiate(pt_1[8], new Vector2(drop_pos[pt_cnt[2]].position.x, drop_pos[pt_cnt[2]].position.y), transform.rotation);
        pt_1[pt_cnt[2]].SetActive(false);

        // 발악패턴
        if (gameObject.GetComponent<Monster2>().mhp <= 650) 
        {
            // 투척
            Instantiate(pt_1[8], new Vector2(drop_pos[pt_cnt[3]].position.x, drop_pos[pt_cnt[3]].position.y), transform.rotation);
            pt_1[pt_cnt[3]].SetActive(false);
            
            Instantiate(pt_1[8], new Vector2(drop_pos[pt_cnt[4]].position.x, drop_pos[pt_cnt[4]].position.y), transform.rotation);
            pt_1[pt_cnt[4]].SetActive(false);

            Instantiate(pt_1[8], new Vector2(drop_pos[pt_cnt[5]].position.x, drop_pos[pt_cnt[5]].position.y), transform.rotation);
            pt_1[pt_cnt[5]].SetActive(false);
        }

        // 총 3번 떨구기
        pt_cnt[0]++;
        if (pt_cnt[0] > 3)
        {
            CancelInvoke();
            pt_cnt[0] = 0;
            pt_cnt[1] = 0;
            pt_cnt[2] = 0;
            pt_cnt[3] = 0;
            timmer_stop = false;
            pt_1[6].SetActive(false);
            pt_1[7].SetActive(false);
            pt_1[11].SetActive(false);
        }
    }

    IEnumerator Summon_1(int num) 
    {
        for(int i = 0; i < num; i++) 
        {
            Instantiate(pt_1[9], new Vector2(transform.position.x, transform.position.y), transform.rotation);
            yield return new WaitForSeconds(1.5f);
        }
        timmer_stop = false;
    }
    IEnumerator Trace_Shot_1()
    {
        for (int l = 0; l < 5; l++)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(pt_1[10], gameObject.transform.position, transform.rotation);
                yield return new WaitForSeconds(0.15f);
            }
            yield return new WaitForSeconds(1.5f);
        }

        timmer_stop = false;
    }

    // 발악패턴
    public void Trace_Shots_1()
    {
        Instantiate(pt_1[10], gameObject.transform.position, transform.rotation);
        Invoke("Trace_Shots_1", 0.8f);
    }
    public void Trace_Shots_2()
    {
        Instantiate(pt_1[10], gameObject.transform.position, transform.rotation);
        Invoke("Trace_Shots_2", 1.4f);
    }

    /* ----- 스테이지 2보스 패턴 ----- */

    // 태양 / 달 효과
    public void Sun_Luna() 
    {

        gameObject.GetComponent<Monster2>().anim.SetBool("Change", true);
        int ran = Random.Range(0, 2);
        if (ran == 0)
        {
            // '태양' 버프
            pt_1[1].SetActive(true);
            pt_1[2].SetActive(false);
            gameObject.GetComponent<Monster2>().atk = 30;
            gameObject.GetComponent<Monster2>().mdef = 10;
            pt_bool[0] = true;
            pt_bool[1] = false;
            
        }
        else if(ran == 1)
        {
            pt_1[1].SetActive(false);
            pt_1[2].SetActive(true);
            gameObject.GetComponent<Monster2>().atk = 20;
            gameObject.GetComponent<Monster2>().mdef = 15;
            pt_bool[1] = true;
            pt_bool[0] = false;
        }
    }
    public void Change_False() 
    {
        gameObject.GetComponent<Monster2>().anim.SetBool("Change", false);
        Invisible_Animation();
    }


    // 기본 패턴 ( 보이기 안보이기 )
    public void Invisible_Animation()
    {
        gameObject.GetComponent<Monster2>().anim.SetBool("Visible", false);
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        int ran = Random.Range(3, 7);
        Invoke("Visible_Animation", ran);
        
    }
    public void Visible_Animation()
    {
        if (pt_cnt[0] >= 3 && timmer >= 13f)
        {
            transform.position = new Vector2(-7.5f, 10.5f);
            pt_1[0].transform.position = new Vector2(-7.5f, 10.5f);
            pt_1[0].SetActive(true);
        }
        else
        {
            transform.position = GameObject.FindWithTag("Player").transform.position;
            pt_1[0].transform.position = GameObject.FindWithTag("Player").transform.position;
            pt_1[0].SetActive(true);
        }
        Invoke("Visible_On", 0.5f);
    }
    public void Visible_On()
    {
        pt_1[0].SetActive(false);
        gameObject.GetComponent<Monster2>().anim.SetBool("Visible", true);
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        int ran = Random.Range(3, 7);
        Invoke("Invisible_Animation", ran);
    }

    // 문양 안보이게. 보이게.
    public void SunLuna_InVisible()
    {
        pt_1[1].SetActive(false);
        pt_1[2].SetActive(false);
    }
    public void SunLua_Visible() 
    {
        if (pt_bool[0] == true)
        {
            pt_1[1].SetActive(true);
        }
        else 
        {
            pt_1[2].SetActive(true);
        }
    }

    // 회전하는 칼날 소환
    IEnumerator Summon_Blade(int a) 
    {
        for (int i = 0; i < a; i++)
        {
            int rnd = Random.Range(0, drop_pos.Length);
            if (rnd >= 4)
            {
                pt_1[5].GetComponent<Spinning_Blade>().move_set = 1;
                pt_1[5].GetComponent<Spinning_Blade>().speed = 12;
                pt_1[5].GetComponent<Spinning_Blade>().dmg = 20;
                Instantiate(pt_1[5], drop_pos[rnd]);
            }
            else
            {
                pt_1[5].GetComponent<Spinning_Blade>().move_set = -1;
                pt_1[5].GetComponent<Spinning_Blade>().speed = 12;
                pt_1[5].GetComponent<Spinning_Blade>().dmg = 20;
                Instantiate(pt_1[5], drop_pos[rnd]);
            }
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(3f);
        pt_1[6].SetActive(false);
    }

    // 맵 전체 폭발 소환
    IEnumerator Summon_Explosion() 
    {
        yield return new WaitForSeconds(7.5f);
        pt_1[8].SetActive(false);

        for (int i = 0; i < 30; i++)
        {
            int x = Random.Range(-25, 10);
            int y = Random.Range(4, 20);
            Instantiate(pt_1[9], new Vector2(x, y), transform.rotation);

            x = Random.Range(-25, 10);
            y = Random.Range(4, 20);
            Instantiate(pt_1[9], new Vector2(x, y), transform.rotation);

            x = Random.Range(-25, 10);
            y = Random.Range(4, 20);
            Instantiate(pt_1[9], new Vector2(x, y), transform.rotation);

            x = Random.Range(-25, 10);
            y = Random.Range(4, 20);
            Instantiate(pt_1[9], new Vector2(x, y), transform.rotation);

            x = Random.Range(-25, 10);
            y = Random.Range(4, 20);
            Instantiate(pt_1[9], new Vector2(x, y), transform.rotation);
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(2f); 
        timmer_stop = false;
        Invisible_Animation();
        Destroy(GameObject.FindWithTag("Infinity"));
    }

    // 독안개 소환
    IEnumerator Summon_PoisonMist() 
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(3f);
            Vector2 pos = GameObject.FindWithTag("Player").transform.position;
            pt_1[11].transform.position = pos;
            pt_1[11].SetActive(true);
            yield return new WaitForSeconds(2.5f);
            pt_1[10].GetComponent<Poison_Mist>().time = 10f;
            pt_1[10].GetComponent<Poison_Mist>().dmg = 10;
            Instantiate(pt_1[10], pos, transform.rotation);
            pt_1[11].SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        timmer_stop = false;
        Invisible_Animation();
    }

    // 회전탄막
    IEnumerator Rotate_Bullet(int a, int b, int c, int d)
    {
        pt_1[14].GetComponent<Rotation_arrow>().boss = true;
        int fireAngle = a;
        int fireAngle2 = b;
        int fireAngle3 = c;
        int fireAngle4 = d;
        while (true)
        {
            pt_1[14].GetComponent<Rotation_arrow>().dmg = gameObject.GetComponent<Monster2>().atk;
            GameObject tep = Instantiate(pt_1[14],gameObject.GetComponent<Transform>(), true);
            Vector2 dir = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
            tep.transform.right = dir;
            tep.transform.position = transform.position;

            GameObject teb = Instantiate(pt_1[14], gameObject.GetComponent<Transform>(), true);
            Vector2 dirs = new Vector2(Mathf.Cos(fireAngle2 * Mathf.Deg2Rad), Mathf.Sin(fireAngle2 * Mathf.Deg2Rad));
            teb.transform.right = dirs;
            teb.transform.position = transform.position;

            GameObject tem = Instantiate(pt_1[14], gameObject.GetComponent<Transform>(), true);
            Vector2 dird = new Vector2(Mathf.Cos(fireAngle3 * Mathf.Deg2Rad), Mathf.Sin(fireAngle3 * Mathf.Deg2Rad));
            tem.transform.right = dird;
            tem.transform.position = transform.position;

            GameObject tec = Instantiate(pt_1[14], gameObject.GetComponent<Transform>(), true);
            Vector2 dirc = new Vector2(Mathf.Cos(fireAngle4 * Mathf.Deg2Rad), Mathf.Sin(fireAngle4 * Mathf.Deg2Rad));
            tec.transform.right = dirc;
            tec.transform.position = transform.position;


            yield return new WaitForSeconds(0.3f);

            fireAngle += angle_Interval;
            if (fireAngle > 360) fireAngle -= 360;

            fireAngle2 += angle_Interval;
            if (fireAngle2 > 360) fireAngle2 -= 360;

            fireAngle3 += angle_Interval;
            if (fireAngle3 > 360) fireAngle3 -= 360;

            fireAngle4 += angle_Interval;
            if (fireAngle4 > 360) fireAngle4 -= 360;
        }
    }
    IEnumerator Rotate_Bullet2() 
    {
        pt_1[14].GetComponent<Rotation_arrow>().boss = true;
        while (true) 
        {
            for (int fireAngle = start_Angle; fireAngle < end_Angle; fireAngle += angle_Interval)
            {
                pt_1[14].GetComponent<Rotation_arrow>().dmg = gameObject.GetComponent<Monster2>().atk;
                GameObject tempObject = Instantiate(pt_1[14], gameObject.GetComponent<Transform>(), true);
                Vector2 dir = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle* Mathf.Deg2Rad));

                tempObject.transform.right = dir;
                tempObject.transform.position = transform.position;
            }
            yield return new WaitForSeconds(5.5f);
        }
    }

    IEnumerator Summon_Snake(int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            int ran = Random.Range(0, 2);
            if (ran == 0)
            {
                ran = 17;
            }
            else 
            {
                ran = 18;
            }
            int pos_ran = Random.Range(-25, 10);
            pt_1[19].transform.position = new Vector2(pos_ran, 1.6f);
            pt_1[19].SetActive(true);

            yield return new WaitForSeconds(1.5f);
            pt_1[19].SetActive(false);
            Instantiate(pt_1[ran], pt_1[19].transform.position, transform.rotation);
            yield return new WaitForSeconds(1f);
        }
    }

    /* ----- 스테이지 3보스 패턴 ----- */

    // 기본 패턴 ( 짤몹 소환 )
    IEnumerator Summon_Monster(int cnt) 
    {
        for (int i = 0; i < cnt; i++) 
        {
            int ran = Random.Range(0, 3);
            int pos_ran = Random.Range(-20, 10);
            pt_1[3].transform.position = new Vector2(pos_ran, 1.6f);
            pt_1[3].SetActive(true);

            yield return new WaitForSeconds(1.5f);
            pt_1[3].SetActive(false);
            Instantiate(pt_1[ran], pt_1[3].transform.position, transform.rotation);
            yield return new WaitForSeconds(1f);
        }
    }

    // 촉수 소환 ( 패턴 1)
    IEnumerator Summon_Tentacle_1(int cnt, float time)
    {
        Debug.Log(cnt);
        for (int i = 0; i < cnt; i++) 
        {
            float player_pos = GameObject.FindWithTag("Player").transform.position.x;
            pt_1[4].SetActive(true);
            pt_1[4].transform.position = new Vector2(player_pos, 2.9f);
            yield return new WaitForSeconds(time);
            pt_1[4].SetActive(false);
            if (i == cnt - 1)
            {
                pt_1[5].GetComponent<Stage_3_Tentacle>().origin = gameObject;
                pt_1[5].GetComponent<Stage_3_Tentacle>().sw = false;
            }
            else
            {
                pt_1[5].GetComponent<Stage_3_Tentacle>().origin = gameObject;
                pt_1[5].GetComponent<Stage_3_Tentacle>().sw = true;
            }
            pt_1[5].GetComponent<Stage_3_Tentacle>().atk = gameObject.GetComponent<Monster2>().atk;
            GameManager.instance.SFX_Play("Tentacle_Sound", GameManager.instance.attack_Clip[2], 1);
            Instantiate(pt_1[5], new Vector2(pt_1[4].transform.position.x, 0), transform.rotation);
        }
    }

    // 먹물 시야 방해
    IEnumerator Mist_Summon() 
    {
        for (int i = 0; i < 4; i++)
        {
            pt_1[6].SetActive(true);
            pt_1[6].transform.position = new Vector3(GameObject.FindWithTag("Player").transform.position.x, 1.3f, -3);

            yield return new WaitForSeconds(1.5f);
            Instantiate(pt_1[7], pt_1[6].transform.position, Quaternion.Euler(0, 0, 0));
            pt_1[6].SetActive(false);

            yield return new WaitForSeconds(1.5f);
        }

    }

    /* ----- 스테이지 4보스 패턴 ----- */
    
    // 메테오
    IEnumerator Meteor_Summon() 
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<Monster2>().anim.SetBool("Attack", false);
        int rnd = Random.Range(5, 11);
        pt_1[1].GetComponent<Meteor_Object>().dmg = gameObject.GetComponent<Monster2>().atk;
        for (int i = 0; i < rnd; i++) 
        {
            int pos_rnd = Random.Range(-28, 14);
            Vector2 meteor_pos = new Vector2(pos_rnd, 19f);
            if (pos_rnd > -6f)
            {
                pt_1[1].GetComponent<Meteor_Object>().dir = 1;
            }
            else
            {
                pt_1[1].GetComponent<Meteor_Object>().dir = -1;
            }
            Instantiate(pt_1[1], meteor_pos, transform.rotation);
            yield return new WaitForSeconds(0.8f);
        }
        timmer_stop = false;
    }

    // 다시 나타나기
    public void DisVisible_Effect()
    {
        if (pt_cnt[0] != 3)
        {
            pt_1[3].transform.position = new Vector2(GameObject.FindWithTag("Player").transform.position.x, 3.2f);
        }
        else 
        {
            pt_1[3].transform.position = new Vector2(-6.05f, 3.2f);
        }
        pt_1[3].SetActive(true);
        if (special == true)
        {
            pt_1[14].SetActive(true);
            special = false;
        }
        Invoke("DIsVisible_Effect_2", 1f);
    }
    public void DIsVisible_Effect_2()
    {
        pt_1[3].SetActive(false);
        gameObject.GetComponent<Monster2>().image_Decoy.transform.position = new Vector2(pt_1[3].transform.position.x, 3.3f);
        gameObject.GetComponent<Monster2>().anim.SetBool("Visible", true);
        Invoke("DIsVisible_Effect_3", 0.34f);
    }
    public void DIsVisible_Effect_3()
    {
        gameObject.GetComponent<Monster2>().anim.SetBool("Visible", false);
        gameObject.GetComponent<Monster2>().anim.SetBool("Hide", false);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<Monster2>().image_Decoy.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<Monster2>().rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
        if (pt_cnt[0] != 3)
        {
            timmer_stop = false;
        }
        else
        {
            gameObject.GetComponent<Monster2>().anim.SetInteger("Attack_Num", 3);
            gameObject.GetComponent<Monster2>().anim.SetBool("Attack", true);
            StartCoroutine(Charging_Attack());
        }
    }

    // 오른팔 파괴
    public void Right_Arm()
    {
        Destroy(pt_1[4]);
        Destroy(pt_1[5]);
        gameObject.GetComponent<Monster2>().anim.SetBool("Destroy_1", false);
    }
    // 왼팔 파괴
    public void Left_Arm()
    {
        Destroy(pt_1[6]);
        Destroy(pt_1[7]);
        gameObject.GetComponent<Monster2>().anim.SetBool("Destroy_2", false);
    }

    // 짤몹 소환
    IEnumerator Summon_Monster_4() 
    {
        int rnd = Random.Range(2, 5);

        for (int i = 0; i < rnd; i++)
        {
            int type = Random.Range(0, 2);
            if (type == 0)
            {
                type = 9;
            }
            else
            {
                type = 8;
            }
            int pos_rnd = Random.Range(-24, 13);
            pt_1[10].transform.position = new Vector2(pos_rnd, 2.5f);
            pt_1[10].SetActive(true);

            yield return new WaitForSeconds(1.5f);
            Instantiate(pt_1[type], pt_1[10].transform.position, pt_1[10].transform.rotation);
            pt_1[10].SetActive(false);
            yield return new WaitForSeconds(1.5f);
        }
    }

    // 불기둥 소환
    IEnumerator Summon_FIre_Pillar() 
    {
        yield return new WaitForSeconds(1.7f);
        gameObject.GetComponent<Monster2>().anim.SetBool("Attack", false);
        int rnd = Random.Range(3, 6);
        for(int i = 0; i <rnd; i++) 
        {
            pt_1[12].transform.position = new Vector2(GameObject.FindWithTag("Player").transform.position.x, 8.4f);
            pt_1[12].SetActive(true);

            yield return new WaitForSeconds(0.85f);
            Instantiate(pt_1[11], pt_1[12].transform.position, pt_1[12].transform.rotation);
            pt_1[12].SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }
        timmer_stop = false;
    }

    // 기모으는 공격 ( 이건 패턴을 3회 반복시 나타나는 걸로 하자 )
    IEnumerator Charging_Attack() 
    {
        yield return new WaitForSeconds(0.83f);
        gameObject.GetComponent<Monster2>().anim.SetBool("Change", true);
    }

    IEnumerator Charging_End()
    {
        pt_1[14].SetActive(false);
        GameObject.FindWithTag("Player").transform.position = new Vector2(-7.6f, 12.2f);
        yield return new WaitForSeconds(0.42f);
        gameObject.GetComponent<Monster2>().anim.SetBool("Change", false);
        gameObject.GetComponent<Monster2>().anim.SetBool("Attack", false);
        gameObject.GetComponent<Monster2>().anim.SetBool("End", false);
        Instantiate(pt_1[13]);
        pt_cnt[0] = 0;
        timmer_stop = false;
    }
    IEnumerator Charging_End_2()
    {
        pt_1[14].SetActive(false);
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<Monster2>().anim.SetBool("Change", false);
        gameObject.GetComponent<Monster2>().anim.SetBool("Attack", false);
        gameObject.GetComponent<Monster2>().anim.SetBool("End", false);
        gameObject.GetComponent<Monster2>().anim.SetBool("Success", false);
        pt_cnt[0] = 0;
        timmer_stop = false;
    }

    // 힐 패턴
    IEnumerator Boss_Heal_Pt() 
    {
        yield return new WaitForSeconds(1.17f);
        pt_1[15].GetComponent<Slider>().maxValue = 800;
        pt_1[15].GetComponent<Slider>().value = 800;
        pt_1[15].SetActive(true);
        gameObject.GetComponent<Monster2>().anim.SetBool("Heal", true);
    }

    IEnumerator Heal_End() 
    {
        yield return new WaitForSeconds(0.35f);
        gameObject.GetComponent<Monster2>().anim.SetBool("Attack", false);
    }
}
