using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Survival_Mod_LvUp_Item : MonoBehaviour
{
    public GameObject svm;
    public GameObject player;
    Survival_Mod_Manager sm;

    public Image item_img;
    public int option_num, option_rank;
    int cnt, ary_leng;
    public TextMeshProUGUI item_explane;
    public bool trigger;

    public Image[] item_rank_sprite;
    public Sprite[] rank_sprite;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        svm = GameObject.FindWithTag("Manager");
        sm = GameObject.FindWithTag("Manager").GetComponent<Survival_Mod_Manager>();
        cnt = sm.LvUp_Object_List.Count - 1;

    }

    void Update()
    {
        Item_Explane();
        Item_Img();
        sm.paused_now = true;
    }

    public void Item_Explane() 
    {
        // �븻
        if (option_rank == 1)
        {
            if (option_num == 0)
            {
                item_explane.text = "���ݷ� ������\n���ݷ��� 3 ������ŵ�ϴ�.";
            }
            if (option_num == 1)
            {
                item_explane.text = "���ݼӵ� ������\n���ݼӵ��� 1 ������ŵ�ϴ�.";
            }
            if (option_num == 2)
            {
                item_explane.text = "ü�� ȸ��\nü���� ���� ȸ���մϴ�.";
            }
            if (option_num == 3)
            {
                item_explane.text = "�̵��ӵ� ������\n�̵� �ӵ��� 1 ������ŵ�ϴ�.";
            }
            if (option_num == 4)
            {
                item_explane.text = "�ִ�ü�� ������\n�ִ�ü���� 3 ������ŵ�ϴ�.";
            }
            if (option_num == 5)
            {
                item_explane.text = "ź�� ������\n����ü�� �ӵ��� 10 ������ŵ�ϴ�.";
            }
            if (option_num == 6)
            {
                item_explane.text = "���ݷ� ������\n���ݷ��� 5 ������ŵ�ϴ�.";
            }
            if (option_num == 7)
            {
                item_explane.text = "���ݼӵ� ������\n���ݼӵ��� 3 ������ŵ�ϴ�.";
            }
            if (option_num == 8)
            {
                item_explane.text = "���� ������\n������ 1 ������ŵ�ϴ�.";
            }
            if (option_num == 9)
            {
                item_explane.text = "�̵��ӵ� ������\n�̵� �ӵ��� 3 ������ŵ�ϴ�.";
            }
            if (option_num == 10)
            {
                item_explane.text = "�ִ�ü�� ������\n�ִ�ü���� 5 ������ŵ�ϴ�.";
            }
            if (option_num == 11)
            {
                item_explane.text = "ź�� ������\n����ü�� �ӵ��� 15 ������ŵ�ϴ�.";
            }
            // ȸ���� ź��
            if (option_num == 12 && GameManager.instance.attack_object[0] == false)
            {
                item_explane.text = "ȸ���� ���±�ü\n13�ʸ��� 5�ʵ��� �÷��̾� ĳ������ �ֺ��� ȸ���ϴ� ��ü�� �����մϴ�. �ߺ� ���ý� �� ��ü�� ������ �þ�ϴ�.\n<size=14>�� ��ü�� �÷��̾� ���ݷ��� 75% ��ŭ�� �������� �����ϴ�.</size>";
            }
            else if (option_num == 12 && GameManager.instance.attack_object[0] == true)
            {
                item_explane.text = "ȸ���� ���±�ü\n���� ��ü�� ���ڸ� �Ѱ� �� �ø��ϴ�. \n���� ���±�ü ���� : " + GameManager.instance.attack_object_cnt[0];
            }
            // ��¥ �������� �Ȱ�
            if (option_num == 13 && GameManager.instance.s_attack_object[2] == false)
            {
                item_explane.text = "��¥ �������� �Ȱ�\n���� óġ�� ����ġ�� 5% �߰��� ȹ���մϴ�. ���������� ���������� ���� �ȵǴ� ���̵����� �������ϴ�!";
            }
            else if (option_num == 13 && GameManager.instance.s_attack_object[2] == true)
            {
                item_explane.text = "��¥ �������� �Ȱ�+\n�� óġ�� ��� �߰� ����ġ�� ����� ������ŵ�ϴ�. ���� " + GameManager.instance.s_attack_object_cnt[2] + "% => " + (GameManager.instance.s_attack_object_cnt[2] + 5) + "%";
            }
            // ƨ��� ���� ��ü
            if (option_num == 14 && GameManager.instance.s_attack_object[7] == false)
            {
                item_explane.text = "ƨ��� ���� ��ü\n�̸� ���� ƨ��� ���鿡�� ���ظ� ������ ���� ��ü�� 25�ʸ��� ��ȯ�մϴ�. ���� ��ü�� �÷��̾� ���ݷ��� 80% ��ŭ�� ���ظ� ������ 10�ʵ��� �����˴ϴ�.";
            }
            else if (option_num == 14 && GameManager.instance.s_attack_object[7] == true)
            {
                item_explane.text = "ƨ��� ���� ��ü+\n��ȯ�Ǵ� ���� ��ü�� ������ ������ŵ�ϴ�. ���� : " + GameManager.instance.s_attack_object_cnt[7] + "�� => " + (GameManager.instance.s_attack_object_cnt[7] + 1) + "��";
            }
            // ���Ȱ�
            if (option_num == 15 && GameManager.instance.s_attack_object[8] == false)
            {
                item_explane.text = "���Ȱ�\n18�ʸ��� �÷��̾��� �ֺ��� 7.4�ʵ��� �����Ǵ� ���Ȱ������� �����մϴ�. ���Ͱ� ���Ȱ� �������ִ� ��� 0.3�ʸ��� �÷��̾� ���ݷ��� 75% ��ŭ�� �������� �����ϴ�.";
            }
            else if (option_num == 15 && GameManager.instance.s_attack_object[8] == true)
            {
                item_explane.text = "���Ȱ�+\n�����Ǵ� ���Ȱ� ������ ���� 1�� ���Ȱ��� ������ 10% �������� 5% ������ŵ�ϴ�." +
                    "\n����(���� / ���� / ������) : " + (GameManager.instance.s_attack_object_cnt[8]*10) + "�� / " + (int)(GameManager.instance.s_attack_object_cnt[8] * 100) + "% / " + (int)(GameManager.instance.s_attack_object_cnt[9] * 100) + "% => " +
                    (int)(((GameManager.instance.s_attack_object_cnt[8] + 0.1) * 10)) + "�� / " + (int)((GameManager.instance.s_attack_object_cnt[8] + 0.1) * 100) +"% / " + (int)((GameManager.instance.s_attack_object_cnt[9] + 0.05) * 100) + "%";
            }
        }

        // ����
        else if (option_rank == 2)
        {
            // ��ȭ�� ����
            if (option_num == 0)
            {
                item_explane.text = "��ȭ�� ����\n����" + (int)(GameManager.instance.attack_object_cnt[7] - 1) + "ȸ óġ�ϸ� �ִ�ü���� 1~2 ��ŭ ����ϴ�. �ߺ� ���ý� ��ǥ������ 1ȸ ���ҽ�ŵ�ϴ�.";
            }

            // ������ ����
            if (option_num == 1)
            {
                item_explane.text = "������ ����\n��� �ɷ�ġ�� 5 ���� ��ŵ�ϴ�. ( ������ 2 ���� )";
            }

            // ������ ��â��
            if (option_num == 2)
            {
                item_explane.text = "������ ��â��\n�ִ�ü���� 40 ������ 1 ������Ų �� ü���� ���� ȸ���մϴ�.\n<size=17><color=#969696>-�Ϳ��� ��� ĳ���Ͱ� �׷����ֽ��ϴ�!-</color></size>";
            }

            // ���� �Ǹ��� ��
            if (option_num == 3 && GameManager.instance.s_attack_object[1] == false)
            {
                item_explane.text = "���� �Ǹ��� ��\n������ ������ ���� ����� ���ĳ����� �Ǹ��� �� �Դϴ�. �� óġ �� ���� ������� 25% Ȯ���� �����Ͽ� �ִ� ü����" + GameManager.instance.s_attack_object_cnt[1] + "% ��ŭ ü���� ȸ���մϴ�.";
            }
            else if (option_num == 3 && GameManager.instance.s_attack_object[1] == true)
            {
                item_explane.text = "���� �Ǹ��� ��+\n���� ������� ���� �����Ͽ� �� ���� ü���� ȸ���մϴ�. ���� ȸ�� ���� " + GameManager.instance.s_attack_object_cnt[1] + "% ���ý� ���� " + (GameManager.instance.s_attack_object_cnt[1] + 0.3f) + "%";
            }

            // ġ��Ÿ Ȯ�� ����
            if (option_num == 4)
            {
                item_explane.text = "ġ��Ÿ Ȯ�� ������\n���� ������ �����մϴ�. ġ��Ÿ Ȯ���� 5% ������ŵ�ϴ�.";
            }

            // ���� ����
            if (option_num == 5 && GameManager.instance.s_attack_object[3] == false)
            {
                item_explane.text = "���� ����\n4.8�ʸ��� �÷��̾� ��ġ�� �ڵ����� ���ڰ� ��ġ�˴ϴ�. ���ڿ� ���� ������� �ڵ����� �����ϸ� ���ݷ��� 130% ��ŭ�� ���ظ� ��� ������ �ݴϴ�.";
            }
            else if (option_num == 5 && GameManager.instance.s_attack_object[3] == true)
            {
                item_explane.text = "���� ����+\n'���� ����'�� ���� �������� �����մϴ�. ���� : " + GameManager.instance.s_attack_object_cnt[3] + "% => " + (GameManager.instance.s_attack_object_cnt[3] + 10) + "%";
            }

            // ������ ȭ��
            if (option_num == 6 && GameManager.instance.s_attack_object[4] == false)
            {
                item_explane.text = "������ ȭ��\n5�ʸ��� �� �ϳ��� ���� �����Ͽ� ���ݷ��� 85% ���ظ� �ִ� ����ȭ�� 3���� ��ȯ�մϴ�." +
                    " ���� ȭ���� ���� ���� �� �� ������ ��ǥ�� ������ ���� �ƴ� �ٸ� �����Դ� ������ ���ظ� �����ϴ�.\n���� ���� ��� ����ȭ���� �������� �ʽ��ϴ�.";
            }
            else if (option_num == 6 && GameManager.instance.s_attack_object[4] == true)
            {
                item_explane.text = "������ ȭ��+\n�����ϴ� ����ȭ���� ������ �ϳ� �� �߰��ϸ� ���ݷ��� 5% ������ŵ�ϴ�.\n" +
                    "���� : " + GameManager.instance.s_attack_object_cnt[5] + "�� / " + (GameManager.instance.s_attack_object_cnt[4] * 100) + "%";
            }

            // ����Ʈ�� ��
            if (option_num == 7 && GameManager.instance.s_attack_object[6] == false)
            {
                item_explane.text = "����Ʈ�� ��\n7.4�ʸ��� �÷��̾��� ���� ���⿡�� ������ ������ �߻�Ǹ� ���� �����ϴ� ����Ʈ�� ���� �߻��մϴ�." +
                    " ����Ʈ�� ���� �÷��̾��� ���ݷ��� 55% �� ���ظ� ������. ���� ���� �� �� ���� �������� 10% �� �����մϴ�.\n������ ���� ��ø���� �ִ� 5ȸ�̸� ���� Ȯ���� ���� ������� �̵��Ұ� ��ų �� �ֽ��ϴ�.";
            }
            else if (option_num == 7 && GameManager.instance.s_attack_object[6] == true)
            {
                item_explane.text = "����Ʈ�� ��+\n'����Ʈ�� ��'�� ���ݷ� ����� 5% ������ŵ�ϴ�. ���� : " + (GameManager.instance.s_attack_object_cnt[6] * 100) + "% => " + (int)((GameManager.instance.s_attack_object_cnt[6] + 0.05) * 100) + "%";
            }

            // ��ȭ
            if (option_num == 8 && GameManager.instance.s_attack_object[10] == false)
            {
                item_explane.text = "��ȭ\n�÷��̾��� �߽ɿ� ���� �����˴ϴ�. �÷��̾ ü���� ȸ�� �� �� ���ο� �ִ� ��� ������ ���ݷ��� 15% + ȸ���� X 1.3 ��ŭ�� �������� �����ϴ�.";
            }
            if (option_num == 8 && GameManager.instance.s_attack_object[10] == true)
            {
                item_explane.text = "��ȭ+\n�÷��̾��� �߽��� ���� ũ�Ⱑ 5% �����մϴ�. ���� �߰� ũ�� : " + GameManager.instance.s_attack_object_cnt[10] + "% => " + (int)((GameManager.instance.s_attack_object_cnt[10] + 0.05f) * 100)+"%";
            }    
        }

        // ����ũ
        else if (option_rank == 3)
        {
            // �Ϲݰ��� ���� ����
            if (option_num == 0)
            {
                item_explane.text = "��ī�ο� ����\n�⺻������ ��� ������ �����մϴ�.";
            }

            // õ���� �ູ
            if (option_num == 1)
            {
                item_explane.text = "õ���� �ູ\n�ִ�ü�� 25 ���ݷ� 20 ��Ÿ� 10 ���� 3 �̵��ӵ� 3 ��ŭ ������ŵ�ϴ�.";
            }

            // (��Ʈ������ - ġ��Ÿ ���� ( ������ ġ��Ÿ Ȯ��, �ٸ����� ġ��Ÿ ���� ) �Ѵ� �Ծ��� ��� ġ��Ÿ ������ �߻��� ġ��Ÿ ������ �ѹ� �� ���� / ġ��Ÿ ���ؽ� ü�� ȸ��
            if (option_num == 2 && GameManager.instance.s_attack_object[11] == false)
            {
                item_explane.text = "�ҿ����� �ı��� �帲ĳ��\n����� �������ִ� �� �� �帲ĳ���Դϴ�. ������ ���� ���������� ������ ���� ������ ���Դϴ�." +
                    "\nġ��Ÿ ���ݷ��� ����� 80% ġ��Ÿ Ȯ���� 45% ������ŵ�ϴ�. '�ҿ����� ����� �帲ĳ��' �� ������ ��쿡 ġ��Ÿ�� �ߵ��� ��� ġ��Ÿ �������� ������ ġ��Ÿ �������� 23% ��ŭ ü���� ȸ���ϸ�, �ѹ��� ġ��Ÿ ���ظ� �����ϴ�.";
            }
            if (option_num == 3 && GameManager.instance.s_attack_object[12] == false) 
            {
                item_explane.text = "�ҿ����� ����� �帲ĳ��\n����� �������ִ� �� �� �帲ĳ���Դϴ�. ������ ���� ���������� ������ ���� ������ ���Դϴ�.\n3�ʸ��� �ִ�ü���� 3.5% ��ŭ ü���� ȸ���մϴ�." +
                    "\n'�ҿ����� �ı��� �帲ĳ��' �� ������ ��쿡 ġ��Ÿ�� �ߵ��� ��� ġ��Ÿ �������� ������ ġ��Ÿ �������� 23% ��ŭ ü���� ȸ���ϸ�, �ѹ��� ġ��Ÿ ���ظ� �����ϴ�.";
            }

            // ��ȣ�� �帷
            if ((option_num == 4 && GameManager.instance.attack_object[3] == false) || (option_num == 2 && GameManager.instance.s_attack_object[11] == true))
            {
                item_explane.text = "��ȣ�� �帷\n8�ʸ��� ���� ������ 1ȸ �����ִ� ��ȣ���� �����մϴ�. ���� ���� �� 3�ʵ��� ����ȿ���� ������ ��� ���� ����Ͽ� �ٴ� �� �ֽ��ϴ�.";
            }
            else if ((option_num == 4 && GameManager.instance.attack_object[3] == true) || (option_num == 2 && GameManager.instance.s_attack_object[11] == true)) 
            {
                item_explane.text = "��ȣ�� �帷+\n��ȣ���� ����� �ֱ⸦ 0.5�� ���ҽ�ŵ�ϴ�. �ּҴ��� (3.5��)\n���� ����� �ֱ� :" + GameManager.instance.attack_object_cnt[3] + "��";
            }
            
            // ����� ����
            if (option_num == 5 || (option_num == 3 && GameManager.instance.s_attack_object[12] == true))
            {
                item_explane.text = "����� ����\n��뿡 ���ʵ� ������ �Դϴ� ������ ������ ��ϵǾ������� �밡�� �ִٰ� �մϴ�. ���ݷ��� 25 ������Ű���� ���ݼӵ��� 40 �����մϴ�. ���� źȯ�� ũ�Ⱑ ���� �����մϴ�.";
            }
        }
    }
    public void Item_Select()
    {
        // �븻
        if (option_rank == 1) 
        {
            if (option_num == 0)
            {
                GameManager.instance.atk += 3;
            }
            if (option_num == 1)
            {
                GameManager.instance.atk_cool -= (float)0.01;

            }
            if (option_num == 2)
            {
                int heal = GameManager.instance.maxhp - GameManager.instance.hp;
                GameManager.instance.hp = GameManager.instance.maxhp;
                if (GameManager.instance.s_attack_object[10] == true) 
                {
                    player.GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_val = heal;
                    player.GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_on = true;
                }
            }
            if (option_num == 3)
            {
                GameManager.instance.speed += (float)0.1;

            }
            if (option_num == 4)
            {
                GameManager.instance.maxhp += 3;
            }
            if (option_num == 5)
            {
                GameManager.instance.arrow_speed += (float)0.1;
            }
            if (option_num == 6)
            {
                GameManager.instance.atk += 5;
            }
            if (option_num == 7)
            {
                GameManager.instance.atk_cool -= (float)0.03;
            }
            if (option_num == 8)
            {
                GameManager.instance.def++;
            }
            if (option_num == 9)
            {
                GameManager.instance.speed += (float)0.3;
            }
            if (option_num == 10)
            {
                GameManager.instance.maxhp += 5;
            }
            if (option_num == 11)
            {
                GameManager.instance.arrow_speed += (float)0.15;
            }
            // ȸ���� ź��
            if (option_num == 12 && GameManager.instance.attack_object[0] == false)
            {
                GameManager.instance.attack_object[0] = true;
                GameManager.instance.attack_object_cnt[0] = 3;
            }
            else if (option_num == 12 && GameManager.instance.attack_object[0] == true)
            {
                GameManager.instance.attack_object_cnt[0]++;
            }
            // ��¥ �������� �Ȱ�
            if (option_num == 13 && GameManager.instance.s_attack_object[2] == false)
            {
                GameManager.instance.s_attack_object[2] = true;
                GameManager.instance.s_attack_object_cnt[2] = 5;
            }
            else if (GameManager.instance.s_attack_object[2] == true)
            {
                GameManager.instance.s_attack_object_cnt[2] += 5f;
            }
            // ƨ��� ���� ��ü
            if (option_num == 14 && GameManager.instance.s_attack_object[7] == false)
            {
                GameManager.instance.s_attack_object[7] = true;
                GameManager.instance.s_attack_object_cnt[7] = 2;
            }
            else if (option_num == 14 && GameManager.instance.s_attack_object[7] == true)
            {

                GameManager.instance.s_attack_object_cnt[7]++;
            }
            // ���Ȱ�
            if (option_num == 15 && GameManager.instance.s_attack_object[8] == false)
            {
                GameManager.instance.s_attack_object[8] = true;
                GameManager.instance.s_attack_object_cnt[8] = 0.1f;
                GameManager.instance.s_attack_object_cnt[9] = 0.75f;
            }
            else if (option_num == 15 && GameManager.instance.s_attack_object[8] == true)
            {
                GameManager.instance.s_attack_object_cnt[8] += 0.1f;
                GameManager.instance.s_attack_object_cnt[9] += 0.05f;
            }
        }

        // ����
        else if (option_rank == 2)
        {
            // ��ȭ�� ����
            if (option_num == 0)
            {
                GameManager.instance.attack_object[7] = true;
                GameManager.instance.attack_object_cnt[7]--;
            }
            // ������ ����
            if (option_num == 1)
            {
                GameManager.instance.atk += 5;
                GameManager.instance.maxhp += 5;
                GameManager.instance.speed += 0.5f;
                GameManager.instance.atk_cool -= 0.05f;
                GameManager.instance.arrow_speed += 0.05f;
                GameManager.instance.def += 2;
            }
            // ������ ��â��
            if (option_num == 2)
            {
                GameManager.instance.maxhp += 40;
                int heal = GameManager.instance.maxhp - GameManager.instance.hp;
                GameManager.instance.hp = GameManager.instance.maxhp;
                GameManager.instance.def += 1;
                if (GameManager.instance.s_attack_object[10] == true)
                {
                    player.GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_val = heal;
                    player.GetComponent<Survival_Mod_Player_Move>().heal_area.GetComponent<Heal_Area>().heal_on = true;
                }
            }
            // ���� �Ǹ��� ��
            if (option_num == 3 && GameManager.instance.s_attack_object[1] == false)
            {
                GameManager.instance.s_attack_object[1] = true;
            }
            else if (option_num == 3 && GameManager.instance.s_attack_object[1] == true)
            {
                GameManager.instance.s_attack_object_cnt[1] += 0.3f;
            }
            // ġȮ ����
            if (option_num == 4) 
            {
                GameManager.instance.crit_per += 5f;
            }
            // ���� ����
            if (option_num == 5 && GameManager.instance.s_attack_object[3] == false)
            {
                GameManager.instance.s_attack_object[3] = true;
                GameManager.instance.s_attack_object_cnt[3] = 130;
            }
            else if (option_num == 5 && GameManager.instance.s_attack_object[3] == true)
            {
                GameManager.instance.s_attack_object_cnt[3] += 10;
            }
            // ������ ȭ��
            if (option_num == 6 && GameManager.instance.s_attack_object[4] == false)
            {
                GameManager.instance.s_attack_object[4] = true;
                GameManager.instance.s_attack_object_cnt[4] = 0.85f;
                GameManager.instance.s_attack_object_cnt[5] = 3;
            }
            else if (option_num == 6 && GameManager.instance.s_attack_object[4] == true)
            {
                GameManager.instance.s_attack_object_cnt[4] += 0.05f;
                GameManager.instance.s_attack_object_cnt[5]++;
            }
            // ����Ʈ�� ��
            if (option_num == 7 && GameManager.instance.s_attack_object[6] == false)
            {
                GameManager.instance.s_attack_object[6] = true;
                GameManager.instance.s_attack_object_cnt[6] = 0.55f;
            }
            else if (option_num == 7 && GameManager.instance.s_attack_object[6] == true)
            {
                GameManager.instance.s_attack_object_cnt[6] += 0.05f;
            }
            // ��ȭ
            if (option_num == 8 && GameManager.instance.s_attack_object[10] == false)
            {
                player.GetComponent<Survival_Mod_Player_Move>().heal_area.SetActive(true);
                GameManager.instance.s_attack_object[10] = true;
            }
            else if (option_num == 8 && GameManager.instance.s_attack_object[10] == true)
            {
                GameManager.instance.s_attack_object_cnt[10] += 0.05f;
                float bonus_x = player.GetComponent<Survival_Mod_Player_Move>().heal_area.transform.localScale.x * GameManager.instance.s_attack_object_cnt[10];
                float bonus_y = player.GetComponent<Survival_Mod_Player_Move>().heal_area.transform.localScale.y * GameManager.instance.s_attack_object_cnt[10];
                player.GetComponent<Survival_Mod_Player_Move>().heal_area.transform.localScale =
                    new Vector2(player.GetComponent<Survival_Mod_Player_Move>().heal_area.transform.localScale.x + bonus_x, player.GetComponent<Survival_Mod_Player_Move>().heal_area.transform.localScale.y + bonus_y); 
            }
        }

        // ����ũ
        else if (option_rank == 3)
        {
            // ��ī�ο� ����
            if (option_num == 0)
            {
                GameManager.instance.s_attack_object[0] = true;
            }

            // õ���� �ູ
            if (option_num == 1)
            {
                GameManager.instance.maxhp += 25;
                GameManager.instance.atk += 20;
                GameManager.instance.def += 3;
                GameManager.instance.speed += 0.03f;
                GameManager.instance.arrow_speed += 0.1f;
            }

            // ��Ʈ������ - �帲ĳ��
            if (option_num == 2 && GameManager.instance.s_attack_object[11] == false) 
            {
                GameManager.instance.crit_dmg += 0.8f;
                GameManager.instance.crit_per += 45;
                GameManager.instance.s_attack_object[11] = true;
                trigger = true;
            }
            if (option_num == 3 && GameManager.instance.s_attack_object[12] == false)
            {
                GameManager.instance.s_attack_object[12] = true;
                trigger = true;
            }
            
            // ��ȣ�� �帷
            if (((option_num == 4 && GameManager.instance.attack_object[3] == false) || (option_num == 2 && GameManager.instance.s_attack_object[11] == true)) && trigger == false)
            {
                GameManager.instance.attack_object[3] = true;
                GameManager.instance.attack_object_cnt[3] = 8;
            }
            if (((option_num == 4 && GameManager.instance.attack_object[3] == true) || (option_num == 2 && GameManager.instance.s_attack_object[11] == true)) && trigger == false)
            {
                GameManager.instance.attack_object_cnt[3]-= 0.5f;
            }

            // ����� ����
            if ((option_num == 5 || (option_num == 3 && GameManager.instance.s_attack_object[12] == true)) && trigger == false)
            {
                GameManager.instance.atk += 25;
                GameManager.instance.atk_cool += 0.4f;
                GameManager.instance.arrow_size += 0.1f;
            }
        }

        // ������ ������Ʈ ����
        for (int i = 0; i < 4; i++)
        {
            Destroy(sm.LvUp_Object_List[cnt]);
            cnt--;
        }
        sm.lvup_cnt--;

        sm.List_Clean();
        sm.Using_Item_Create_List();

        // ������ ��!
        if (sm.lvup_cnt <= 0)
        {
            ary_leng = sm.LvUp_Object_List.Count;
            for (int i = 0; i < ary_leng; i++)
            {
                sm.LvUp_Object_List.RemoveAt(0);
            }
            sm.paused_now = false;
            sm.List_Clean();
            sm.LvUp_Object.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
    public void Item_Img()
    {
        switch (option_rank)
        {
            case 1:
                item_rank_sprite[0].sprite = rank_sprite[0];
                item_rank_sprite[1].sprite = rank_sprite[3];
                item_rank_sprite[2].sprite = rank_sprite[6];
                break;
            case 2:
                item_rank_sprite[0].sprite = rank_sprite[1];
                item_rank_sprite[1].sprite = rank_sprite[4];
                item_rank_sprite[2].sprite = rank_sprite[7];
                break;
            case 3:
                item_rank_sprite[0].sprite = rank_sprite[2];
                item_rank_sprite[1].sprite = rank_sprite[5];
                item_rank_sprite[2].sprite = rank_sprite[8];
                break;
        }
        if (option_rank == 1)
        {
            item_img.sprite = GameManager.instance.s_Lv_Up_item_Img_n[option_num];

            /*if (option_num == 29 && GameManager.instance.attack_object[20] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_n[32];
            }*/
        }

        else if (option_rank == 2)
        {
            item_img.sprite = GameManager.instance.s_Lv_Up_item_Img_r[option_num];
        }

        else if (option_rank == 3)
        {
            item_img.sprite = GameManager.instance.s_Lv_Up_item_Img_u[option_num];
        }
    }
}
