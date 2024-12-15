using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Item_Decoy : MonoBehaviour
{
    public Image frame_Image;
    public Image self_img;
    public Sprite[] rank_frame;
    public int number, rank;
    public bool s_mod;
    public bool db, trp;
    void Start() 
    {
        // attack_object, s_attack_object의 순서대로 Decoy가 생성
        if (GameManager.instance.survival_Mod == false)
        {
            // 여기서 일단 한번 필터
            if (db == false && trp == false)
            {
                switch (number)
                {
                    // 회전 탄막
                    case 0:
                        rank = 1;
                        number = 13;
                        break;
                    // 행운의 한발
                    case 1:
                        rank = 1;
                        number = 14;
                        break;
                    // 보호의 장막
                    case 3:
                        rank = 1;
                        number = 16;
                        break;
                    // 신경독
                    case 4:
                        rank = 1;
                        number = 17;
                        break;
                    // 점착탄
                    case 5:
                        rank = 1;
                        number = 20;
                        break;
                    // 마법부여 : 독성 마법
                    case 6:
                        rank = 2;
                        number = 2;
                        break;
                    // 흡수의 부적
                    case 7:
                        rank = 1;
                        number = 21;
                        break;
                    // 마법부여 : 폭발 마법
                    case 8:
                        rank = 2;
                        number = 3;
                        break;
                    // 달의 징표 1
                    case 9:
                        rank = 1;
                        number = 22;
                        break;
                    // 달의 징표 2
                    case 10:
                        rank = 1;
                        number = 23;
                        break;
                    // 달의 징표 3
                    case 11:
                        rank = 1;
                        number = 24;
                        break;
                    // 몽몽이
                    case 12:
                        rank = 2;
                        number = 16;
                        break;
                    // 항중력 장치
                    case 13:
                        rank = 1;
                        number = 27;
                        break;
                    // 달의 징표 4
                    case 15:
                        rank = 2;
                        number = 9;
                        break;
                    // 맞대응
                    case 16:
                        rank = 2;
                        number = 11;
                        break;
                    // 붉은 수정
                    case 17:
                        rank = 2;
                        number = 10;
                        break;
                    // 보이지 않는 갑옷
                    case 18:
                        rank = 2;
                        number = 20;
                        break;
                    // 777 잭팟
                    case 19:
                        rank = 1;
                        number = 28;
                        break;
                    // 미다스의 손
                    case 20:
                        rank = 1;
                        number = 29;
                        break;
                    // 작은 친구 네로
                    case 21:
                        rank = 2;
                        number = 12;
                        break;
                    // 붉은 위습
                    case 22:
                        rank = 2;
                        number = 13;
                        break;
                    // 완벽한 파트너
                    case 23:
                        rank = 2;
                        number = 14;
                        break;
                    // 중첩마력
                    case 24:
                        rank = 2;
                        number = 15;
                        break;
                    // 라이트닝!
                    case 25:
                        rank = 2;
                        number = 19;
                        break;
                    // 질보다 양
                    case 26:
                        rank = 3;
                        number = 3;
                        break;
                    case 27:
                        rank = 3;
                        number = 5;
                        break;
                    case 29:
                        rank = 1;
                        number = 35;
                        break;
                    case 30:
                        rank = 2;
                        number = 29;
                        break;
                }
            }
            if (rank == 1)
            {
                frame_Image.sprite = rank_frame[0];
                self_img.sprite = GameManager.instance.Lv_Up_item_Img_n[number];
            }
            else if (rank == 2)
            {
                frame_Image.sprite = rank_frame[1];
                self_img.sprite = GameManager.instance.Lv_Up_item_Img_r[number];
            }
            else if (rank == 3)
            {
                frame_Image.sprite = rank_frame[2];
                self_img.sprite = GameManager.instance.Lv_Up_item_Img_u[number];
            }
        }
        else if (GameManager.instance.survival_Mod == true) 
        {
            if (s_mod == true)
            {
                switch (number)
                {
                    case 0:
                        rank = 3;
                        number = 0;
                        break;
                    case 1:
                        rank = 2;
                        number = 3;
                        break;
                    case 2:
                        rank = 1;
                        number = 13;
                        break;
                    case 3:
                        rank = 2;
                        number = 5;
                        break;
                    case 4:
                        rank = 2;
                        number = 6;
                        break;
                    case 6:
                        rank = 2;
                        number = 7;
                        break;
                    case 7:
                        rank = 1;
                        number = 14;
                        break;
                    case 8:
                        rank = 1;
                        number = 15;
                        break;
                    case 10:
                        rank = 2;
                        number = 8;
                        break;
                    case 11:
                        rank = 3;
                        number = 2;
                        break;
                    case 12:
                        rank = 3;
                        number = 3;
                        break;
                }
                if (rank == 1)
                {
                    frame_Image.sprite = rank_frame[0];
                    self_img.sprite = GameManager.instance.s_Lv_Up_item_Img_n[number];
                }
                else if (rank == 2)
                {
                    frame_Image.sprite = rank_frame[1];
                    self_img.sprite = GameManager.instance.s_Lv_Up_item_Img_r[number];
                }
                else if (rank == 3)
                {
                    frame_Image.sprite = rank_frame[2];
                    self_img.sprite = GameManager.instance.s_Lv_Up_item_Img_u[number];
                }
            }
            else if (s_mod == false) 
            {
                switch (number)
                {
                    case 0:
                        rank = 1;
                        number = 13;
                        break;
                    case 3:
                        rank = 1;
                        number = 16;
                        break;
                    case 7:
                        rank = 1;
                        number = 21;
                        break;
                }
                if (rank == 1)
                {
                    frame_Image.sprite = rank_frame[0];
                    self_img.sprite = GameManager.instance.Lv_Up_item_Img_n[number];
                    // 보호의 장막 ( 생존 모드에선 랭크 U )
                    if (number == 16)
                    {
                        frame_Image.sprite = rank_frame[2];
                        rank = 3;
                    }
                    // 정화의 부적 ( 생존 모드에선 랭크 R )
                    if (number == 21)
                    {
                        frame_Image.sprite = rank_frame[1];
                        rank = 2;
                    }
                }
                else if (rank == 2)
                {
                    frame_Image.sprite = rank_frame[1];
                    self_img.sprite = GameManager.instance.Lv_Up_item_Img_r[number];
                }
                else if (rank == 3)
                {
                    frame_Image.sprite = rank_frame[2];
                    self_img.sprite = GameManager.instance.Lv_Up_item_Img_u[number];
                }
            }
        }
    }
}
