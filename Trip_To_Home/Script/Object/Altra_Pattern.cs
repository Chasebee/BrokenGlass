using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Altra_Pattern : MonoBehaviour
{
    public float timeer, time;
    public int cnt, random, input_cnt;
    public GameObject mini_Game;

    public string[,] key = { {"a", "s", "d", "f"},{ "q", "w", "e", "r"}, {"a", "q", "r", "d"} , {"q", "s", "f", "d"}, {"w", "r", "f", "a"}, {"f", "a", "q", "e" } };
    public GameObject[] keyboard;
    public Sprite[] key_img;

    Animator anim;

    void Start() 
    {
        anim = GetComponent<Animator>();
        Key_Setting();
    }
    void Update() 
    {
        // 타이머
        if (timeer <= time)
        {
            timeer += Time.deltaTime;
        }
        else if (timeer >= time) 
        {
            if(cnt >= 1)
            {
                cnt = 0;
                anim.SetBool("Destroy", true);
                GameObject.Find("Stage_2_Boss 1").GetComponent<Boss_Pattern>().pt_bool[3] = true;
                GameObject.Find("Stage_2_Boss 1").GetComponent<Boss_Pattern>().pt_1[15].GetComponent<Image>().enabled = true;
                GameObject.Find("Stage_2_Boss 1").GetComponent<Boss_Pattern>().pt_1[15].GetComponent<Stage_2_Sun_Effect>().anim.SetBool("Effect", true);
            }
        }

        // 키 입력
        if (cnt >= 1) 
        {
            if (random == 0)
            {
                switch (input_cnt)
                {
                    case 0:
                        if (Input.GetKeyDown(KeyCode.A))
                        {
                            keyboard[0].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 1:
                        if (Input.GetKeyDown(KeyCode.S))
                        {
                            keyboard[1].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.D))
                        {
                            keyboard[2].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 3:
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            keyboard[3].SetActive(false);
                            input_cnt = 0;
                            cnt--;
                            Key_Setting();
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                }
            }
            else if (random == 1)
            {
                switch (input_cnt)
                {
                    case 0:
                        if (Input.GetKeyDown(KeyCode.Q))
                        {
                            keyboard[0].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 1:
                        if (Input.GetKeyDown(KeyCode.W))
                        {
                            keyboard[1].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            keyboard[2].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 3:
                        if (Input.GetKeyDown(KeyCode.R))
                        {
                            keyboard[3].SetActive(false);
                            input_cnt = 0;
                            cnt--;
                            Key_Setting();
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                }
            }
            else if (random == 2)
            {
                switch (input_cnt)
                {
                    case 0:
                        if (Input.GetKeyDown(KeyCode.A))
                        {
                            keyboard[0].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 1:
                        if (Input.GetKeyDown(KeyCode.Q))
                        {
                            keyboard[1].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.R))
                        {
                            keyboard[2].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.D))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 3:
                        if (Input.GetKeyDown(KeyCode.D))
                        {
                            keyboard[3].SetActive(false);
                            input_cnt = 0;
                            cnt--;
                            Key_Setting();
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                }
            }
            else if (random == 3)
            {
                switch (input_cnt)
                {
                    case 0:
                        if (Input.GetKeyDown(KeyCode.Q))
                        {
                            keyboard[0].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 1:
                        if (Input.GetKeyDown(KeyCode.S))
                        {
                            keyboard[1].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            keyboard[2].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 3:
                        if (Input.GetKeyDown(KeyCode.D))
                        {
                            keyboard[3].SetActive(false);
                            input_cnt = 0;
                            cnt--;
                            Key_Setting();
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                }
            }
            else if (random == 4)
            {
                switch (input_cnt)
                {
                    case 0:
                        if (Input.GetKeyDown(KeyCode.W))
                        {
                            keyboard[0].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 1:
                        if (Input.GetKeyDown(KeyCode.R))
                        {
                            keyboard[1].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            keyboard[2].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 3:
                        if (Input.GetKeyDown(KeyCode.A))
                        {
                            keyboard[3].SetActive(false);
                            input_cnt = 0;
                            cnt--;
                            Key_Setting();
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                }
            }
            else if (random == 5)
            {
                switch (input_cnt)
                {
                    case 0:
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            keyboard[0].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 1:
                        if (Input.GetKeyDown(KeyCode.A))
                        {
                            keyboard[1].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.Q))
                        {
                            keyboard[2].SetActive(false);
                            input_cnt++;
                        }
                        else if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                    case 3:
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            keyboard[3].SetActive(false);
                            input_cnt = 0;
                            cnt--;
                            Key_Setting();
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.R))
                        {
                            for (int i = 0; i < keyboard.Length; i++)
                            {
                                keyboard[i].SetActive(true);
                                input_cnt = 0;
                            }
                        }
                        break;
                }
            }
        }
        else
        {
            for (int i = 0; i < keyboard.Length; i++)
            {
                keyboard[i].SetActive(false);
            }
        }
    }

    public void Key_Setting() 
    {
        // 패턴 끝난 경우
        if (cnt == 0) 
        {
            anim.SetBool("Destroy", true);
            timeer = 0f;
            GameObject.Find("Stage_2_Boss 1").GetComponent<Boss_Pattern>().pt_bool[3] = true;
        }

        // 패턴 진행
        random = Random.Range(0, 6);
        if (random == 0)
        {
            keyboard[0].GetComponent<SpriteRenderer>().sprite = key_img[0];
            keyboard[1].GetComponent<SpriteRenderer>().sprite = key_img[1];
            keyboard[2].GetComponent<SpriteRenderer>().sprite = key_img[2];
            keyboard[3].GetComponent<SpriteRenderer>().sprite = key_img[3];
        }
        else if (random == 1)
        {
            keyboard[0].GetComponent<SpriteRenderer>().sprite = key_img[4];
            keyboard[1].GetComponent<SpriteRenderer>().sprite = key_img[5];
            keyboard[2].GetComponent<SpriteRenderer>().sprite = key_img[6];
            keyboard[3].GetComponent<SpriteRenderer>().sprite = key_img[7];
        }
        else if (random == 2)
        {
            keyboard[0].GetComponent<SpriteRenderer>().sprite = key_img[0];
            keyboard[1].GetComponent<SpriteRenderer>().sprite = key_img[4];
            keyboard[2].GetComponent<SpriteRenderer>().sprite = key_img[7];
            keyboard[3].GetComponent<SpriteRenderer>().sprite = key_img[2];
        }
        else if (random == 3)
        {
            keyboard[0].GetComponent<SpriteRenderer>().sprite = key_img[4];
            keyboard[1].GetComponent<SpriteRenderer>().sprite = key_img[1];
            keyboard[2].GetComponent<SpriteRenderer>().sprite = key_img[3];
            keyboard[3].GetComponent<SpriteRenderer>().sprite = key_img[2];
        }
        else if (random == 4)
        {
            keyboard[0].GetComponent<SpriteRenderer>().sprite = key_img[5];
            keyboard[1].GetComponent<SpriteRenderer>().sprite = key_img[7];
            keyboard[2].GetComponent<SpriteRenderer>().sprite = key_img[3];
            keyboard[3].GetComponent<SpriteRenderer>().sprite = key_img[0];
        }
        else if (random == 5)
        {
            keyboard[0].GetComponent<SpriteRenderer>().sprite = key_img[3];
            keyboard[1].GetComponent<SpriteRenderer>().sprite = key_img[0];
            keyboard[2].GetComponent<SpriteRenderer>().sprite = key_img[4];
            keyboard[3].GetComponent<SpriteRenderer>().sprite = key_img[6];
        }
    }

    public void Destroy_Self()
    {
       Destroy(gameObject);
    }
}
