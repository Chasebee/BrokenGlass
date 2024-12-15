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
        // attack_object, s_attack_object�� ������� Decoy�� ����
        if (GameManager.instance.survival_Mod == false)
        {
            // ���⼭ �ϴ� �ѹ� ����
            if (db == false && trp == false)
            {
                switch (number)
                {
                    // ȸ�� ź��
                    case 0:
                        rank = 1;
                        number = 13;
                        break;
                    // ����� �ѹ�
                    case 1:
                        rank = 1;
                        number = 14;
                        break;
                    // ��ȣ�� �帷
                    case 3:
                        rank = 1;
                        number = 16;
                        break;
                    // �Ű浶
                    case 4:
                        rank = 1;
                        number = 17;
                        break;
                    // ����ź
                    case 5:
                        rank = 1;
                        number = 20;
                        break;
                    // �����ο� : ���� ����
                    case 6:
                        rank = 2;
                        number = 2;
                        break;
                    // ����� ����
                    case 7:
                        rank = 1;
                        number = 21;
                        break;
                    // �����ο� : ���� ����
                    case 8:
                        rank = 2;
                        number = 3;
                        break;
                    // ���� ¡ǥ 1
                    case 9:
                        rank = 1;
                        number = 22;
                        break;
                    // ���� ¡ǥ 2
                    case 10:
                        rank = 1;
                        number = 23;
                        break;
                    // ���� ¡ǥ 3
                    case 11:
                        rank = 1;
                        number = 24;
                        break;
                    // ������
                    case 12:
                        rank = 2;
                        number = 16;
                        break;
                    // ���߷� ��ġ
                    case 13:
                        rank = 1;
                        number = 27;
                        break;
                    // ���� ¡ǥ 4
                    case 15:
                        rank = 2;
                        number = 9;
                        break;
                    // �´���
                    case 16:
                        rank = 2;
                        number = 11;
                        break;
                    // ���� ����
                    case 17:
                        rank = 2;
                        number = 10;
                        break;
                    // ������ �ʴ� ����
                    case 18:
                        rank = 2;
                        number = 20;
                        break;
                    // 777 ����
                    case 19:
                        rank = 1;
                        number = 28;
                        break;
                    // �̴ٽ��� ��
                    case 20:
                        rank = 1;
                        number = 29;
                        break;
                    // ���� ģ�� �׷�
                    case 21:
                        rank = 2;
                        number = 12;
                        break;
                    // ���� ����
                    case 22:
                        rank = 2;
                        number = 13;
                        break;
                    // �Ϻ��� ��Ʈ��
                    case 23:
                        rank = 2;
                        number = 14;
                        break;
                    // ��ø����
                    case 24:
                        rank = 2;
                        number = 15;
                        break;
                    // ����Ʈ��!
                    case 25:
                        rank = 2;
                        number = 19;
                        break;
                    // ������ ��
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
                    // ��ȣ�� �帷 ( ���� ��忡�� ��ũ U )
                    if (number == 16)
                    {
                        frame_Image.sprite = rank_frame[2];
                        rank = 3;
                    }
                    // ��ȭ�� ���� ( ���� ��忡�� ��ũ R )
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
