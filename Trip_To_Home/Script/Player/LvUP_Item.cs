using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LvUP_Item : MonoBehaviour
{
    public GameObject btm;
    BattleManager bt;

    public GameObject chr;

    public Image item_img;
    public int option_num, option_rank;
    public TextMeshProUGUI item_explane;
    int cnt, ary_leng, rock;
    bool calculator = false;

    public Image[] item_rank_sprite;
    public Sprite[] rank_sprite;
    Image self_img;
    // rock = ���ݷ� ���� �̼�

    void Start()
    {
        self_img = GetComponent<Image>();
        btm = GameObject.FindWithTag("Manager");
        bt = GameObject.FindWithTag("Manager").GetComponent<BattleManager>();
        chr = GameObject.FindWithTag("Player");
        cnt = bt.LvUp_Object_List.Count - 1;
        rock = Random.Range(0, 3);
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
        Item_Img();
    }
    void Update()
    {
        item_Option_explane();
        Item_Img();
        bt.paused_now = true;
    }

    public void item_Option_explane()
    {
        // ���� �ɼ� ���� ( ����, ���� �̼� ) 0.01 = 1%

        /*------ �븻 ��� ------*/
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
            if (option_num == 12)
            {
                item_explane.text = "���� Ƚ�� ����\n���� ���� Ƚ���� 1 ������ŵ�ϴ�.";
            }

            // ȸ���� ź��
            if (option_num == 13 && GameManager.instance.attack_object[0] == false)
            {
                item_explane.text = "ȸ���� ���±�ü\n13�ʸ��� 5�ʵ��� �÷��̾� ĳ������ �ֺ��� ȸ���ϴ� ��ü�� �����մϴ�. �ߺ� ���ý� �� ��ü�� ������ �þ�ϴ�.\n<size=14>�÷��̾� ���ݷ��� 75% ��ŭ�� ������.</size>";
            }
            else if (option_num == 13 && GameManager.instance.attack_object[0] == true)
            {
                item_explane.text = "ȸ���� ���±�ü\n���� ��ü�� ���ڸ� �Ѱ� �� �ø��ϴ�. \n���� ���±�ü ���� : " + GameManager.instance.attack_object_cnt[0];
            }

            // ����� �ѹ�
            if (option_num == 14)
            {
                item_explane.text = "����� �ѹ�\n���ݽ� 40% Ȯ���� ���ݷ���" + (int)(GameManager.instance.attack_object_cnt[1] + 15) + "% ��ŭ�� ������������ �ִ� źȯ�� �߰��� �ϳ� �� �߻��մϴ�.";
            }

            // ������ ���ڰ� ( ��Ƽ�� )
            if (option_num == 15)
            {
                item_explane.text = "(��Ƽ��) ������ ���ڰ�\n������ ����� �����ִ� �ż��� ���ڰ��� ��ȯ�Ͽ� ��ġ�մϴ�. ���ڰ��� 10�ʰ� ���� �Ǹ� 3�ʸ��� ���ڰ� ������ �÷��̾�� �ִ�ü���� 7% ��ŭ ȸ���ǰ�" +
                    "\n�ֺ���� �����Դ� 10�� �������ظ� �ݴϴ�." +
                    "<size=16><color=#969696>??? : ������ ���� ���Ⱑ ���ƿ´�!</color></size>";
            }

            // ��ȣ�� �帷
            if (option_num == 16)
            {
                int an = (int)GameManager.instance.attack_object_cnt[3] - 1;
                if (an < 5)
                {
                    an = 5;
                }
                item_explane.text = "��ȣ�� �帷\n" + an + "�ʸ��� ���� ������ 1ȸ �����ִ� ��ȣ���� �����մϴ�.";
            }

            // �Ű浶
            if (option_num == 17)
            {
                item_explane.text = "�Ű浶\n���ݽ� 40% Ȯ���� ���ݷ��� 65% ��ŭ�� ���ظ� �ִ� �Ű浶ź�� �߻��մϴ�.\n�ǰ� ���� ���ʹ�" + (float)(GameManager.instance.attack_object_cnt[4] + 0.2) + " �� ���� �̵��Ҵɻ��°� �˴ϴ�."
                    + "\n<size=17><color=#969696>??? : ���ڵ����� ����� �̾߱Ⱑ ���� ����, ���� �� ���� ������.</color></size>";
            }

            // ���� �Ҵ�Ʈ(��Ƽ��)
            if (option_num == 18)
            {
                item_explane.text = "���� �Ҵ�Ʈ(��Ƽ��)\n���� �÷��̾��� ü���� 15 ȸ����ŵ�ϴ�.\n�� ���������� 5ȸ �̻� ���� ���Ǳ���� ������ ����� �Ͻ������� �Ұ����մϴ�";
            }

            // ������ �Ҵ�Ʈ(��Ƽ��)
            if (option_num == 19)
            {
                item_explane.text = "������ �Ҵ�Ʈ(��Ƽ��)\n���� �÷��̾ �������� �ֺ� ���鿡�� ���ݷ��� 25% ���ظ� �����ϴ�.";
            }

            // ����ź
            if (option_num == 20)
            {
                item_explane.text = "����ź\n���ݽ� 30% Ȯ���� ������ �޶�ٴ� źȯ�� �߻��� �ǰݽ� ���ݷ��� 100% ���ظ� ���� ��, �޶���� źȯ�� 3���� �����Ͽ�" +
                    " ���ݷ���" + (int)(GameManager.instance.attack_object_cnt[5] + 10) + "% ��ŭ���ظ� �ݴϴ�\n<size=17><color=#969696>??? : ������ �����̴�!</color></size>";
            }

            // ��ȭ�� ����
            if (option_num == 21)
            {
                item_explane.text = "��ȭ�� ����\n����" + (int)(GameManager.instance.attack_object_cnt[7] - 1) + "ȸ óġ�ϸ� �ִ�ü���� 3~5 ��ŭ ����ϴ�. �ߺ� ���ý� ��ǥ������ 1ȸ ���ҽ�ŵ�ϴ�.";
            }

            // ���� ¡ǥ1
            if (option_num == 22 && GameManager.instance.attack_object[9] == false)
            {
                item_explane.text = "���� ¡ǥ��\n���� ȿ���� �����Ͱ����� ���� �ƹ��� �𸨴ϴ�.";
            }
            if (option_num == 22 && GameManager.instance.attack_object[9] == true)
            {
                item_explane.text = "���� ¡ǥ�� - ����\n �ִ�ü���� 15 ������ŵ�ϴ�.";
            }

            // ���� ¡ǥ2
            if (option_num == 23 && GameManager.instance.attack_object[10] == false)
            {
                item_explane.text = "���� ¡ǥ��\n���� ȿ���� �����Ͱ����� ���� �ƹ��� �𸨴ϴ�.";
            }
            if (option_num == 23 && GameManager.instance.attack_object[10] == true)
            {
                item_explane.text = "���� ¡ǥ�� - ��\n���ݷ��� 10 ������ŵ�ϴ�.";
            }

            // ���� ¡ǥ3
            if (option_num == 24 && GameManager.instance.attack_object[11] == false)
            {
                item_explane.text = "���� ¡ǥ��\n���� ȿ���� �����Ͱ����� ���� �ƹ��� �𸨴ϴ�.";
            }
            if (option_num == 24 && GameManager.instance.attack_object[11] == true)
            {
                item_explane.text = "���� ¡ǥ�� - �ӵ�\n���ݼӵ��� 5 ����ü �ӵ��� 2 ������ŵ�ϴ�.";
            }

            // ������ �˾॰
            if (option_num == 25)
            {
                item_explane.text = "������ �˾॰\n�÷��̾��� ũ�Ⱑ Ŀ���ϴ�. �װ� ��ϴ�.\n<size=17><color=#969696>���� ũ�⸦ �޼��Ѵٸ� ���� ���� �Ͼ� �� ����...?</color></size>";
            }

            // ������ �˾ॱ
            if (option_num == 26)
            {
                item_explane.text = "������ �˾ॱ\n�÷��̾��� ũ�Ⱑ �۾����ϴ�. �װ� ��ϴ�.\n<size=17><color=#969696>���� ũ�⸦ �޼��Ѵٸ� ���� ���� �Ͼ� �� ����...?</color></size>";
            }

            // ���߷� ������
            if (option_num == 27 && GameManager.instance.attack_object[13] == false)
            {
                item_explane.text = "���߷� ������\n���� �߷��� ��Ʋ�� ������ ������ ���ϴ�. �˹� ������ �ݴ�� �ۿ��ŵ�ϴ�.\n<size=17><color=#969696>??? : ���� ��ǥ�踦 ��� �����Ų �̴ϱ�?</color></size>";
            }

            // ���ݸ�
            if (option_num == 27 && GameManager.instance.attack_object[13] == true)
            {
                item_explane.text = "���ݸ�\nü���� 15 ȸ���ϰ� ���ݷ��� 1 �����մϴ�. ���� �𸣰� ���� ���� �Ƹ��ٿ� ���Դϴ�!\n<size=17><color=#969696>?? : ��ƾƾƶ��ؿ�~</color></size>";
            }

            // 777 ����
            if (option_num == 28 && GameManager.instance.attack_object[19] == false)
            {
                item_explane.text = "777\n������ ������ 2��� ����ϴ�.";
            }
            if (option_num == 28 && GameManager.instance.attack_object[19] == true)
            {
                item_explane.text = "����� ���\n25 ������ ȹ���մϴ�.";
            }

            // ��� ������
            if (option_num == 29 && GameManager.instance.attack_object[20] == false)
            {
                item_explane.text = "��� ������\n������ ��� Ȯ���� 15% �����մϴ�.";
            }

            // ��ī��(��Ƽ��)
            if (option_num == 29 && GameManager.instance.attack_object[20] == true)
            {
                item_explane.text = "��ī��\n25������ �Ҹ��Ͽ� ������ ������ ��ȯ�մϴ�.\n<size=17><color=#969696>���� ����. - �ڰ� �ִ� ��ȯ�� ������ ���� -</color></size>";
            }

            // �ź��� ��
            if (option_num == 30)
            {
                item_explane.text = "�ź��� ��\n" + GameManager.instance.attack_object_cnt[14] + "%Ȯ���� ���ݷ�, ���ݼӵ�, �̵��ӵ� 3������ �ϳ��� 5 ������ŵ�ϴ�. ������ ����Ȯ���� 10% �����ǰ� ���н� 10% �����ϰ� ��� ȿ���� ���� �� �����ϴ�. \n(�ִ� 75% ���� 25%)"
                    + "<size=17><color=#969696>??? : �̰�... Ȯ���� �� �̻���!</color></size>";
            }

            // ���߼��� ����(��Ƽ��)
            if (option_num == 31)
            {
                item_explane.text = "<size=16>���߼��� ����(��Ƽ��)\n��Ƽ�� ������ ���Ű�� '���߼� - �׸���', '���߼� - ��'��ȯ�� �����մϴ�. '�׸���' ������ ��� ���� 3Ÿ���� �ִ�ü���� 1.5% ����Ͽ� ���ݷ��� 235% �������ظ� �ִ� ź�� �߻��մϴ�." +
                    "\n'��' ������ ��� 3Ÿ���� ���߽� �ִ�ü���� 1.5% �� ȸ���ϸ� ���ݷ��� 100% �������� �ִ� ź�� �߻��մϴ�.</size>";
            } 

            // ��������
            if (option_num == 32 && GameManager.instance.attack_object[29] == false) 
            {
                item_explane.text = "��������\n���ϴ� ������ ����Ű�� ������ 2�� �Է½� ������ ����� ����Ͽ� �̵��ӵ���" + GameManager.instance.attack_object_cnt[29]*100 + "% �� �ӵ��� �����մϴ�." +
                    "\n'��������'�� 2���� ���� ���ð��� �ֽ��ϴ�." +
                    "\n<size=17><color=#969696>??? : ����.</color></size>";
            }
            if (option_num == 32 && GameManager.instance.attack_object[29] == true)
            {
                item_explane.text = "��������+\n'��������'�� �̵��ӵ� ����� 5% ������ŵ�ϴ� ���� ��� : " + GameManager.instance.attack_object_cnt[29] * 100 +"%";
            }
        }

        /*------ ���� ��� ------*/
        if (option_rank == 2)
        {
            // ���� �̽���
            if (option_num == 0 && GameManager.instance.double_shot == false)
            {
                item_explane.text = "����\n���ݽ� �߻�Ǵ� źȯ�� 2���� ����˴ϴ�. \n<size=17><color=#969696>-??? : �Ѿ��� �ΰ�����?!-</color></size>";
            }

            // ���� ����
            if (option_num == 0 && GameManager.instance.double_shot == true)
            {
                item_explane.text = "����Ƣ��\n�ٻ��ϰ� ����� ���ִ� ����Ƣ���Դϴ�! ���ݷ�, ���ݼӵ��� 2 �����մϴ�.\n<size=17><color=#969696>���� ���� �����մϴ�.</color></size>";
            }

            // ������ ����
            if (option_num == 1)
            {
                item_explane.text = "������ ����\n��� �ɷ�ġ�� 5 ���� ��ŵ�ϴ�. ( ���°� �������� 2 ���� )";
            }

            // ��������
            if (option_num == 2 && GameManager.instance.attack_object[6] == false)
            {
                item_explane.text = "�����ο� : ��������\n������ �⺻���� ���߽ø��� 0.7�ʸ��� ������ ���ݷ���" + (int)((GameManager.instance.attack_object_cnt[6]) * 100) + "% �������ظ� ������ '�ߵ�' �����̻��� �ο��մϴ�. '�ߵ�' �����̻��� 5�ʵ��� �����˴ϴ�.";
            }

            // �������� ��ȭ
            if (option_num == 2 && GameManager.instance.attack_object[6] == true)
            {
                item_explane.text = "�����ο� : ��������+\n'�ߵ�'�����̻��� ���ݷ��� 1% ������ŵ�ϴ�.";
            }

            // ���߸���
            if (option_num == 3 && GameManager.instance.attack_object[8] == false)
            {
                item_explane.text = "�����ο� : ���߸���\n������ ���ϴ� �⺻���ݿ� ���߸����� �ο��մϴ�. ������ �⺻������ ���߽� ���� ������ ����Ű�� ��ü�� ������ ������ŵ�ϴ�\n���� �������� ���ݷ��� 140% �� �ش��մϴ�.";
            }

            // ���߸��� ��ȭ
            if (option_num == 3 && GameManager.instance.attack_object[8] == true)
            {
                item_explane.text = "�����ο� : ���߸���+\n���߸����� ���� �������� 10% ������ŵ�ϴ�.";
            }

            // ������ ��â��
            if (option_num == 4)
            {
                item_explane.text = "������ ��â��\n�ִ�ü���� 40 ������ 1 ������Ų �� ü���� ���� ȸ���մϴ�.\n<size=17><color=#969696>-�Ϳ��� ��� ĳ���Ͱ� �׷����ֽ��ϴ�!-</color></size>";
            }

            // �� ���� �佺Ʈ
            if (option_num == 5)
            {
                item_explane.text = "�� ���� �佺Ʈ\n�̵��ӵ��� 2  �������� 2 ������ŵ�ϴ�.\n<size=17><color=#969696>-�����̾�! ����!-</color></size>";
            }

            // ���� ����
            if (option_num == 6)
            {
                item_explane.text = "���� ����\n�������� 1 �ִ����� Ƚ���� 1 ������ŵ�ϴ�.\n<size=17><color=#969696>-??? : -</color></size>";
            }

            // ��ȣ�� ¡ǥ
            if (option_num == 7)
            {
                item_explane.text = "��ȣ�� ¡ǥ\n�ִ�ü���� 10 ������ 5 ����մϴ�.";
            }

            // ���� �۷���
            if (option_num == 8)
            {
                item_explane.text = "���� �۷���\n���ݷ��� 3 �˹��� 2 ������ŵ�ϴ�.\n<size=17><color=#969696>-??? : ����! ��õ�� ���!-</color></size>";
            }

            // ���� ¡ǥ4
            if (option_num == 9 && GameManager.instance.attack_object[15] == false)
            {
                item_explane.text = "���� ¡ǥ��\n���� ȿ���� �����Ͱ����� ���� �ƹ��� �𸨴ϴ�.";
            }
            if (option_num == 9 && GameManager.instance.attack_object[15] == true)
            {
                item_explane.text = "���� ¡ǥ�� - ����\n�������� 3 ����Ƚ���� 1 �����մϴ�.";
            }

            // ���� ����(��Ʈ���)
            if (option_num == 10 && GameManager.instance.attack_object[17] == false)
            {
                item_explane.text = "���� ����\n������ ������� " + (GameManager.instance.attack_object_cnt[17] - 0.1f) + "�ʸ��� �ִ�ü���� 3% �� ȸ���մϴ�.";
            }
            if (option_num == 10 && GameManager.instance.attack_object[17] == true)
            {
                item_explane.text = "�ܹ���\nü���� 30 ȸ���մϴ�. ���ݷ��� 1 ������ŵ�ϴ�.\n<size=17><color=#969696>������ ���� ������....</color></size>";
            }

            // �´���
            if (option_num == 11 && GameManager.instance.attack_object[16] == false)
            {
                item_explane.text = "�´���\n�� �⺻���ݿ� ���� źȯ�� ���Ǵ� ������ �����Ͽ� ���� ����ü ������ ����Ͽ� ������� ����ϴ�.";
            }
            if (option_num == 11 && GameManager.instance.attack_object[16] == true)
            {
                item_explane.text = "�ٻ��� ġŲ\nü���� 30 ȸ���ϰ� ���ݷ��� 2 �����մϴ�.";
            }

            // ���� ģ�� �׷�
            if (option_num == 12 && GameManager.instance.attack_object[21] == false)
            {
                item_explane.text = "����ģ�� �׷�\n'����'ȿ�� �� �ߵ��ϴ� �йи��� ���� ����� �׷ο� ����մϴ�. \n<size=17>���� : �����Ÿ����� ���� ���ٽ� �ڵ� ���� ź���� 11.7�ʸ��� �����մϴ�. ź���� ���ݷ��� �÷��̾� ���ݷ��� 1.2�� �Դϴ�.</size>";
            }
            if (option_num == 12 && GameManager.instance.attack_object[21] == true)
            {
                item_explane.text = "����ģ�� �׷�+\nź�� ���� �ð��� 0.4�� �����մϴ�.";
            }

            // ���������� ����
            if (option_num == 13 && GameManager.instance.attack_object[22] == false)
            {
                item_explane.text = "���������� ����\n10�ʸ��� �÷��̾� ���ݷ��� 135% �������� �ִ� ������ ������ ���� ���� �������� �߻��ϴ� ������ �йи����� ���������� ����մϴ�.";
            }
            if (option_num == 13 && GameManager.instance.attack_object[22] == true)
            {
                item_explane.text = "���������� ����\n������ ������ ���ݷ��� 10% ��½�ŵ�ϴ�. �������� ���� �ҷ� �����˴ϴ�.";
            }

            // �Ϻ��� ��Ʈ�� 
            if (option_num == 14 && GameManager.instance.attack_object[23] == false)
            {
                item_explane.text = "��ȥ�� ��Ʈ��\n�йи��� �迭 ������ ���ݷ��� 2�� ������ŵ�ϴ�.";
            }
            if (option_num == 14 && GameManager.instance.attack_object[23] == true)
            {
                item_explane.text = "�йи����� �ູ\n���� Ƚ���� ������ ��� ������ 3 �����մϴ�. (ü���� 5, ������ 1 ����)";
            }

            // ��ø����
            if (option_num == 15 && GameManager.instance.attack_object[24] == false)
            {
                item_explane.text = "��ø�� ����\n�⺻���� �߻�� ź�˿� ����ִ� ������ ������ ���Խ��� ��ø��ŵ�ϴ�. 3�� �� ��ø�� ������ �� �ֺ��� ���ݷ��� 115%�� ������ü�� ��ø �� �� ��ŭ �����Ǿ� ���� ���� Ÿ���մϴ�." +
                    " (�ִ� ��ø �� 5ȸ)";
            }
            else if (option_num == 15 && GameManager.instance.attack_object[24] == true)
            {
                item_explane.text = "��ø�� ����+\n��ø�� ���� ���ݷ� ����� 5% �����մϴ�.\n������ : " + GameManager.instance.attack_object_cnt[24] * 100 + "%";
            }

            // ���Ǻ����� ģ�� ����
            if (option_num == 16 && GameManager.instance.attack_object[12] == false)
            {
                item_explane.text = "���Ǻ����� ģ�� ����\n������ ���Ǻ����ϰ� �Ϳ����� ����� ��и��� �����̿� ����մϴ�. �����̴� �� �߽߰� �޷���� �÷��̾� ���ݷ� 85% �������� ������ �����մϴ�.";
            }
            else if (option_num == 16 && GameManager.instance.attack_object[12] == true) 
            {
                item_explane.text = "���Ǻ����� ģ�� ����+\n�������� ���ݷ°���� 10% ������ŵ�ϴ�. ���� ��� : " + GameManager.instance.attack_object_cnt[12] * 100 + "%";
            }

            // ������ ����(��Ƽ��)
            if (option_num == 17)
            {
                item_explane.text = "������ ����(��Ƽ��)\n���� 7�ʵ��� ������ �Ǹ�, ����� �ε� �� ��� ���ݷ��� 100% ��ŭ�� ���� �������� �����ϴ�.";
            }

            // ��üȭ(��Ƽ��)
            if (option_num == 18)
            {
                item_explane.text = "��üȭ(��Ƽ��)\n���� �Ͻ������� ��üȭ ����, ���� ��� �������� ���� 10�ʰ� �����ο����ϴ�.";
            }

            // �⺻���� ����( ����Ʈ��! )
            if (option_num == 19 && GameManager.instance.attack_object[25] == false)
            {
                item_explane.text = "����Ʈ��!\n<size=18>�⺻ ������ ��÷� ������ ������ ���⸦ �������� �����ϴ� ���·� ��ȭ�ϸ� ���ݷ��� 10 �����մϴ�.</size>\n<size=17>������ ���� ������ �� ������ ������ �⺻���� ��ȭ ȿ���� ���� �� �����ϴ�.</size>";
            }
            else if (option_num == 19 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] == 0)
            {
                item_explane.text = "����Ʈ�� ��\n�⺻������ ���� �������� �����մϴ�. ������ �Ϸ�� ���¿��� ���ݽ� ������ ������ ������ ���濡 �߻��մϴ�. ������ ���� ���� �� �� �ֽ��ϴ�.\n" +
                    "'���߸���'���� �⺻���� ��ȭ ȿ���� ���� �����մϴ�.";
            }
            else if (option_num == 19 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] == 1)
            {
                item_explane.text = "��������\n20% Ȯ���� '����Ʈ�� ��'�� �ǰ��� ������ '����' ȿ���� �߻���ŵ�ϴ�." +
                    "\n<size='16'>���� : �ǰݴ��� ����� �߽����� ������ ������ �帣�� ������ ���� ���� ��� ��� ������ ���ݷ��� 100%�� ���ϴ� ���ظ� �ָ� 45% Ȯ���� �����ŵ�ϴ�.</size>";
            }
            else if (option_num == 19 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] >= 2)
            {
                item_explane.text = "��������+\n'����'ȿ���� ���ݷ��� 5% ������ŵ�ϴ�. ���� �߰� ���ݷ�" + GameManager.instance.attack_object_cnt[25] * 5+"%";
            }
            // ������ �ʴ� ����
            if (option_num == 20 && GameManager.instance.attack_object[18] == false)
            {
                item_explane.text = "������ �ʴ� ����\n�и� ������ �ֽ��ϴ�! ������ ���� ������. �ִ�ü�� 15�� ������Ű�� ������ 3 ������ŵ�ϴ�.\n40% Ȯ���� ���ظ� ������ �޴� ���ظ� 50% �����մϴ�.";
            }
            if (option_num == 20 && GameManager.instance.attack_object[18] == true)
            {
                item_explane.text = "�� ����\n���������ϰ� �� ���� �� �����Դϴ�. ü���� 25 ȸ���մϴ�.\n<size=17><color=#969696>??? : �� ���ڴ� ���ó�?</color></size>";
            }
            // ������ ��
            if (option_num == 21) 
            {
                item_explane.text = "������ ��\n�ִ�ü�°� ������ �������� �����մϴ�. ��� ���ݷ��� 1.5�������մϴ�.\n<size=17><color=#969696>�밡�� ����� .... �������� ���� �� ����.</color></size>";
            }
            // �Ӵ� ��(��Ƽ��)
            if (option_num == 22) 
            {
                item_explane.text = "�Ӵ� ��(��Ƽ��)\n10�ʵ��� �⺻ ���� �� 1 ������ �Ҹ��Ͽ� ���� źȯ�� �߰��� �߻��մϴ�. ���� źȯ�� ���ݷ��� 100% ���ظ� ������\nź���� ���� �����ϴ�. " +
                    "���� źȯ���� óġ�� 10% Ȯ���� ������ ����մϴ�.";
            }
            // �ٺ�ť ��Ƽ!(��Ƽ��)
            if (option_num == 23)
            {
                item_explane.text = "�ٺ�ť ��Ƽ!(��Ƽ��)\n������ ���־�̴� �ٺ�ť�� ��ġ�մϴ�. �ٺ�ť�� ������ ü���� 30 + " + (int)(GameManager.instance.maxhp * 0.2)+ "�ִ�ü���� 20% �� ȸ���մϴ�.\n" +
                    "�ٺ�ť�� ������ �ͱ� �� ������ ���� �� ������ ���� ���ݿ� �ǰݽ� �ı��˴ϴ�.";
            }
            // �����Ʈ(��Ƽ��)
            if (option_num == 24)
            {
                item_explane.text = "�����Ʈ(��Ƽ��)\nĳ���͸� �߽����� �� �ڷ� ������ 4�� ����߸��ϴ�. �� ������ ���ݷ��� 120% ��ŭ�� ���ظ� �ְ� 30% Ȯ���� ���� �����̻��� �ο��մϴ�.";
            }
            // Ǫ�� ������ ����
            if (option_num == 25 && GameManager.instance.attack_object[30] == false) 
            {
                item_explane.text = "Ǫ�� ������ ����\n8�ʸ��� �÷��̾� ���ݷ��� 100% �������� ������ ź�� �߻��մϴ�. ������ �ǰݽ� '��������' �ɼ��� �ߵ��մϴ�. \n" +
                    "'Ǫ�� ������ ������' '��������' ���ݷ��� �÷��̾� ���ݷ��� 135%��ŭ�� �������� �����ϴ�.";
            }
            else if (option_num == 25 && GameManager.instance.attack_object[30] == true)
            {
                item_explane.text = "Ǫ�� ������ ����+\n;Ǫ�� ������ ����'�� '��������' �ɼ��� ���ݷ��� 10% ������ŵ�ϴ�. ���� ���ݷ�" + GameManager.instance.attack_object_cnt[30] * 100 +"%";
            }
        }

        /*------ ����ũ ��� ------*/
        if (option_rank == 3)
        {
            // Ʈ���� ��
            if (option_num == 0 && GameManager.instance.tripple_shpt == false)
            {
                item_explane.text = "Ʈ���� ��\n���ݽ� �߻�Ǵ� źȯ�� ���� 3���� ����˴ϴ�.";
            }
            // Ʈ���� ��
            if (option_num == 0 && GameManager.instance.tripple_shpt == true)
            {
                item_explane.text = "���� ����\n���ݷ��� 10 �ִ�ü���� 50 ������Ų �� ü���� ȸ���մϴ�.";
            }

            // ����� ����
            if (option_num == 1)
            {
                item_explane.text = "����� ����\n��뿡 ���ʵ� ������ �Դϴ� ������ ������ ��ϵǾ������� �밡�� �ִٰ� �մϴ�. ���ݷ��� 25 ������Ű���� ���ݼӵ��� 40 �����մϴ�. ���� źȯ�� ũ�Ⱑ ���� �����մϴ�.";
            }

            // ������ ���±�
            if (option_num == 2)
            {
                item_explane.text = "������ ���±�(��Ƽ��)\n���� ���� ���ݷ��� 100% ��ŭ�� ���ظ� �ִ� źȯ�� �����ӵ��� 15�� �����մϴ�. ���� �� 5�ʵ��� ������ �Ұ��� �մϴ�.";
            }

            // ������ ��
            if (option_num == 3 && GameManager.instance.attack_object[26] == false)
            {
                item_explane.text = "������ ��\n���ݼӵ��� �ִ�ġ�� �����Ǹ� ź���� ����� ���� �۾����ϴ�! ��� ���ݷ��� 70% �����մϴ�.";
            }
            // õ���� �ູ
            else if ((option_num == 3 && GameManager.instance.attack_object[26] == true) || option_num == 4) 
            {
                item_explane.text = "õ���� �ູ\n�ִ�ü�� 20 ���ݷ� 15 ��Ÿ� 15 ���� 3 �̵��ӵ� 3 ����Ƚ�� 1 �� ������ŵ�ϴ�.";
            }

            // �����ο� : ��
            if (option_num == 5 && GameManager.instance.attack_object[27] == false)
            {
                item_explane.text = "�����ο� : ��\n�⺻������ ������ ���߽� ������ ���� ����� ��ø ��ŵ�ϴ�. 3�� �� ��ø�� ����Ͽ� ȿ���� �ߵ��մϴ�.\n" +
                    "<size=17>2 ��ø ���� : ���� ������ ������ ���ݷ��� 145% ��ŭ�� ���ظ������ϴ�.\n3 - 4 ��ø : ���� ���� ��ȯ�Ͽ� ���ݷ��� 185% ��ŭ�� ���ظ� �����ϴ�." +
                    "\n5 ��ø �̻� :���� ���� �� 3���� ��ȯ�Ͽ� ���� 75% ��ŭ�� ���ظ� ������ ���� ������ ������ ���ݷ��� 215% ��ŭ�� ���ظ� �ݴϴ�." +
                    "���� �� ȿ���� ���ظ� �� ��� ���� ������ 25% ��ŭ ü���� ȸ���մϴ�.</size>";
            }
            else if (option_num == 5 && GameManager.instance.attack_object[27] == true) 
            {
                item_explane.text = "�����ο� : ��+\n'�����ο� : ��' �� ��� ���� ������� 15% ������ŵ�ϴ�";
            }
        }
    }

    public void Item_Select()
    {
        // �븻 ( 33�� )
        if (option_rank == 1)
        {
            if (option_num == 0)
            {
                //  ���� �Ͻ���������! �ɷ�����!
                GameManager.instance.atk += 3;
                calculator = true;
            }
            if (option_num == 1)
            {
                GameManager.instance.atk_cool -= (float)0.01;

            }
            if (option_num == 2)
            {
                GameManager.instance.hp = GameManager.instance.maxhp;
                bt.heal_Alert.text = GameManager.instance.maxhp.ToString();
                Instantiate(bt.heal_Alert);

            }
            if (option_num == 3)
            {
                GameManager.instance.speed += (float)0.1;

            }
            if (option_num == 4)
            {
                GameManager.instance.maxhp += 3;
                bt.heal_Alert.text = "+3";
                Instantiate(bt.heal_Alert);
            }
            if (option_num == 5)
            {
                GameManager.instance.arrow_speed += (float)0.1;

            }
            if (option_num == 6)
            {
                GameManager.instance.atk += 5;
                calculator = true;
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
                bt.heal_Alert.text = "+5";
                Instantiate(bt.heal_Alert);
            }
            if (option_num == 11)
            {
                GameManager.instance.arrow_speed += (float)0.15;

            }
            if (option_num == 12)
            {
                GameManager.instance.jump_cnt++;

            }
            // ȸ���� ź��
            if (option_num == 13 && GameManager.instance.attack_object[0] == false)
            {
                GameManager.instance.attack_object[0] = true;
                GameManager.instance.attack_object_cnt[0] = 3;
            }
            else if (option_num == 13 && GameManager.instance.attack_object[0] == true)
            {
                GameManager.instance.attack_object_cnt[0]++;
            }
            // ����� �ѹ�
            if (option_num == 14)
            {
                GameManager.instance.attack_object[1] = true;
                GameManager.instance.attack_object_cnt[1] += 15;

            }
            // ������ ���ڰ�
            if (option_num == 15)
            {
                Active_Item_Setting("������ ���ڰ�", 15);
            }
            // ��ȣ�� �帷
            if (option_num == 16)
            {
                GameManager.instance.attack_object[3] = true;
                if (GameManager.instance.attack_object_cnt[3] > 5)
                {
                    GameManager.instance.attack_object_cnt[3]--;
                }
            }
            // �Ű浶
            if (option_num == 17)
            {
                GameManager.instance.attack_object[4] = true;
                GameManager.instance.attack_object_cnt[4] += 0.2f;

            }
            // ���� �Ҵ�Ʈ ( ��Ƽ�� )
            if (option_num == 18)
            {
                Active_Item_Setting("���� �Ҵ�Ʈ", 18);
            }
            // ������ �Ҵ�Ʈ ( ��Ƽ�� )
            if (option_num == 19)
            {
                Active_Item_Setting("������ �Ҵ�Ʈ", 19);

            }
            // ����ź
            if (option_num == 20)
            {
                GameManager.instance.attack_object[5] = true;
                GameManager.instance.attack_object_cnt[5] += 10;

            }
            // ��ȭ�� ����
            if (option_num == 21)
            {
                GameManager.instance.attack_object[7] = true;
                GameManager.instance.attack_object_cnt[7]--;
            }
            // ���� ¡ǥ 1
            if (option_num == 22 && GameManager.instance.attack_object[9] == false)
            {
                GameManager.instance.attack_object[9] = true;
                if (GameManager.instance.attack_object[15] == true && GameManager.instance.attack_object[9] == true && GameManager.instance.attack_object[10] == true && GameManager.instance.attack_object[11] == true)
                {
                    bt.player_Alert.GetComponent<TextMeshPro>().text = "���� ¡ǥ�� ��� ��ҽ��ϴ�!";
                    GameManager.instance.luna = true;
                    GameManager.instance.arrow_size *= 4;
                    Instantiate(bt.player_Alert);
                }
            }
            else if (option_num == 22 && GameManager.instance.attack_object[9] == true)
            {
                GameManager.instance.maxhp += 15;
                bt.heal_Alert.text = "+15";
                Instantiate(bt.heal_Alert);
            }
            // ���� ¡ǥ 2
            if (option_num == 23 && GameManager.instance.attack_object[10] == false)
            {
                GameManager.instance.attack_object[10] = true;
                if (GameManager.instance.attack_object[15] == true && GameManager.instance.attack_object[9] == true && GameManager.instance.attack_object[10] == true && GameManager.instance.attack_object[11] == true)
                {
                    bt.player_Alert.GetComponent<TextMeshPro>().text = "���� ¡ǥ�� ��� ��ҽ��ϴ�!";
                    GameManager.instance.luna = true;
                    GameManager.instance.arrow_size *= 4;
                    Instantiate(bt.player_Alert);
                }
            }
            else if (option_num == 23 && GameManager.instance.attack_object[10] == true)
            {
                GameManager.instance.atk += 10;
                calculator = true;
            }
            // ���� ¡ǥ 3
            if (option_num == 24 && GameManager.instance.attack_object[11] == false)
            {
                GameManager.instance.attack_object[11] = true;
                if (GameManager.instance.attack_object[15] == true && GameManager.instance.attack_object[9] == true && GameManager.instance.attack_object[10] == true && GameManager.instance.attack_object[11] == true)
                {
                    bt.player_Alert.GetComponent<TextMeshPro>().text = "���� ¡ǥ�� ��� ��ҽ��ϴ�!";
                    GameManager.instance.luna = true;
                    GameManager.instance.arrow_size *= 4;
                    Instantiate(bt.player_Alert);
                }
            }
            else if (option_num == 24 && GameManager.instance.attack_object[11] == true)
            {
                GameManager.instance.arrow_speed += 2f;
                GameManager.instance.atk_cool -= 0.05f;

            }
            // �˾� 1
            if (option_num == 25)
            {
                GameManager.instance.player_size += 0.1f;
                if (GameManager.instance.player_size >= 1.6f)
                {
                    // �˾� ȿ��
                    if (GameManager.instance.giant == false)
                    {
                        GameManager.instance.maxhp += 50;
                        GameManager.instance.atk += 20;
                        GameManager.instance.arrow_size = 0.625f;
                        GameManager.instance.giant = true;
                    }
                    GameManager.instance.player_size = 1.6f;
                    // ������ ����
                    if (GameManager.instance.playerdata.Achievements[12] == false && GameManager.instance.achievement_bool == false)
                    {
                        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                        GameManager.instance.achievement_bool = true;
                        GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n�Ŵ�ȭ!";
                        GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Lv_Up_item_Img_n[25];
                        Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                        GameManager.instance.playerdata.Achievements[12] = true;
                        GameManager.instance.Save_PlayerData_ToJson();
                    }
                }

            }
            // �˾� 2
            if (option_num == 26)
            {
                GameManager.instance.player_size -= 0.1f;
                if (GameManager.instance.player_size <= 1f)
                {
                    // �˾� ȿ��
                    if (GameManager.instance.sweep == false)
                    {
                        GameManager.instance.speed += 3.5f;
                        GameManager.instance.arrow_speed += 4.5f;
                        GameManager.instance.atk_cool -= 0.2f;
                        GameManager.instance.arrow_size -= 2f;
                        GameManager.instance.sweep = true;
                    }
                    GameManager.instance.player_size = 1f;
                    // ������ ����
                    if (GameManager.instance.playerdata.Achievements[13] == false && GameManager.instance.achievement_bool == false)
                    {
                        GameManager.instance.SFX_Play("ach_sound", GameManager.instance.ach_clip, 1);
                        GameManager.instance.achievement_bool = true;
                        GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_text.text = "<color=yellow>�������� �޼�!</color>\n���ȭ!";
                        GameManager.instance.Achievement_Alert.GetComponent<Achievement>().Achievements_img.sprite = GameManager.instance.Lv_Up_item_Img_n[26];
                        Instantiate(GameManager.instance.Achievement_Alert, GameObject.FindWithTag("Canvas").transform);
                        GameManager.instance.playerdata.Achievements[13] = true;
                        GameManager.instance.Save_PlayerData_ToJson();
                    }
                }

            }
            // ���߷� ������
            if (option_num == 27 && GameManager.instance.attack_object[13] == false)
            {
                GameManager.instance.attack_object[13] = true;
            }
            // ���ݸ�
            if (option_num == 27 && GameManager.instance.attack_object[13] == true)
            {
                GameManager.instance.atk += 3;
            }
            /// 777 ����
            if (option_num == 28 && GameManager.instance.attack_object[19] == false)
            {
                GameManager.instance.attack_object[19] = true;
            }
            else if (option_num == 28 && GameManager.instance.attack_object[19] == true)
            {
                GameManager.instance.coin += 25;
            }
            // ��� ������
            if (option_num == 29 && GameManager.instance.attack_object[20] == false)
            {
                GameManager.instance.attack_object[20] = true;
            }
            // ��ī��(��Ƽ��)
            else if (option_num == 29 && GameManager.instance.attack_object[20] == true)
            {
                Active_Item_Setting("��ī��", 29);
            }
            // �ź��� ��
            if (option_num == 30)
            {
                int faill = 100 - (int)GameManager.instance.attack_object_cnt[14];
                int total = 100;
                int pivot = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

                // ����
                if (pivot >= faill)
                {
                    switch (rock)
                    {
                        case 0:
                            GameManager.instance.atk += 5;
                            calculator = true;
                            break;
                        case 1:
                            GameManager.instance.atk_cool -= 0.05f;
                            break;
                        case 2:
                            GameManager.instance.speed += 0.05f;
                            break;
                    }
                    GameManager.instance.attack_object_cnt[14] -= 10;
                    if (GameManager.instance.attack_object_cnt[14] <= 25)
                    {
                        GameManager.instance.attack_object_cnt[14] = 25;
                    }
                }

                // ����
                else if (pivot < faill)
                {
                    GameManager.instance.attack_object_cnt[14] += 10;
                    if (GameManager.instance.attack_object_cnt[14] >= 75)
                    {
                        GameManager.instance.attack_object_cnt[14] = 75;
                    }
                }
            }
            // ���߼��� ���� ( ��Ƽ�� )
            if (option_num == 31)
            {
                Active_Item_Setting("���߼��� ����", 31);
            }
            // ��������
            if (option_num == 32 && GameManager.instance.attack_object[29] == false)
            {
                GameManager.instance.attack_object[29] = true;
            }
            else if (option_num == 32 && GameManager.instance.attack_object[29] == true) 
            {
                GameManager.instance.attack_object_cnt[29] += 0.05f;
            }
        }
        // ���� ( 25�� )
        if (option_rank == 2)
        {
            // ���� �̽���
            if (option_num == 0 && GameManager.instance.double_shot == false)
            {
                GameManager.instance.double_shot = true;
            }
            // ���� �����
            else if (option_num == 0 && GameManager.instance.double_shot == true)
            {
                GameManager.instance.atk += 2;
                GameManager.instance.atk_cool -= (float)0.02;
                calculator = true;
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
                GameManager.instance.jump_power += 0.2f;
                calculator = true;
            }
            // ��������
            if (option_num == 2 && GameManager.instance.attack_object[6] == false)
            {
                GameManager.instance.attack_object[6] = true;
                GameManager.instance.attack_object_cnt[6] = 0.03f;
            }
            // �������� ��ȭ
            else if (option_num == 2 && GameManager.instance.attack_object[6] == true)
            {
                GameManager.instance.attack_object_cnt[6] += 0.01f;
            }
            // ���߸���
            if (option_num == 3 && GameManager.instance.attack_object[8] == false)
            {
                GameManager.instance.attack_object[8] = true;
            }
            // ���߸��� ��ȭ
            else if (option_num == 3 && GameManager.instance.attack_object[8] == true)
            {
                GameManager.instance.attack_object_cnt[8] += 0.1f;
            }
            // ������ ��â��
            if (option_num == 4)
            {
                GameManager.instance.maxhp += 40;
                GameManager.instance.hp = GameManager.instance.maxhp;
                GameManager.instance.def += 1;
                // �ִ�ü�� ����
                bt.heal_Alert.text = "+40";
                Instantiate(bt.heal_Alert);
                // ȸ��
                bt.heal_Alert.text = GameManager.instance.maxhp.ToString();
                Instantiate(bt.heal_Alert);
            }
            // �� ���� �佺Ʈ
            if (option_num == 5)
            {
                GameManager.instance.speed += 0.2f;
                GameManager.instance.jump_power += 0.2f;
            }
            // ���� ����
            if (option_num == 6)
            {
                GameManager.instance.jump_power += 0.1f;
                GameManager.instance.jump_cnt++;
            }
            // ��ȣ�� ¡ǥ
            if (option_num == 7)
            {
                GameManager.instance.maxhp += 10;
                GameManager.instance.def += 5;
            }
            // ���� �۷���
            if (option_num == 8)
            {
                GameManager.instance.atk += 3;
                GameManager.instance.mob_knockback += 0.2f;
                calculator = true;
            }
            // ���� ¡ǥ 4
            if (option_num == 9 && GameManager.instance.attack_object[15] == false)
            {
                GameManager.instance.attack_object[15] = true;
                if (GameManager.instance.attack_object[15] == true && GameManager.instance.attack_object[9] == true && GameManager.instance.attack_object[10] == true && GameManager.instance.attack_object[11] == true)
                {
                    bt.player_Alert.GetComponent<TextMeshPro>().text = "���� ¡ǥ�� ��� ��ҽ��ϴ�!";
                    GameManager.instance.luna = true;
                    GameManager.instance.arrow_size *= 4;
                    Instantiate(bt.player_Alert);
                }
            }
            else if (option_num == 9 && GameManager.instance.attack_object[15] == true)
            {
                GameManager.instance.jump_power += 0.03f;
                GameManager.instance.jump_cnt++;
            }
            // ���� ����
            if (option_num == 10 && GameManager.instance.attack_object[17] == false)
            {
                GameManager.instance.attack_object[17] = true;
                GameManager.instance.attack_object_cnt[17] -= 0.1f;
            }
            else if (option_num == 10 && GameManager.instance.attack_object[17] == true)
            {
                GameManager.instance.hp += 30;
                GameManager.instance.atk++;
                bt.heal_Alert.text = "30";
                Instantiate(bt.heal_Alert);
                calculator = true;
            }
            // �´���
            if (option_num == 11 && GameManager.instance.attack_object[16] == false)
            {
                GameManager.instance.attack_object[16] = true;
            }
            else if (option_num == 11 && GameManager.instance.attack_object[16] == true)
            {
                GameManager.instance.hp += 30;
                GameManager.instance.atk += 2;
                bt.heal_Alert.text = "30";
                Instantiate(bt.heal_Alert);
                calculator = true;
            }
            // ���� ģ�� �׷�
            if (option_num == 12 && GameManager.instance.attack_object[21] == false)
            {
                Instantiate(bt.attack_objects[1]);
                GameManager.instance.attack_object[21] = true;
                GameManager.instance.attack_object_cnt[21] = 11.7f;
            }
            else if (option_num == 12 && GameManager.instance.attack_object[21] == true)
            {
                GameManager.instance.attack_object_cnt[21] -= 0.4f;
            }
            // ���� ������ ����
            if (option_num == 13 && GameManager.instance.attack_object[22] == false)
            {
                Instantiate(bt.attack_objects[2]);
                GameManager.instance.attack_object[22] = true;
                GameManager.instance.attack_object_cnt[22] = 1.35f;
            }
            else if (option_num == 13 && GameManager.instance.attack_object[22] == true)
            {
                GameManager.instance.attack_object_cnt[22] += 0.1f;
            }
            // �Ϻ��� ��Ʈ��
            if (option_num == 14 && GameManager.instance.attack_object[23] == false)
            {
                GameManager.instance.attack_object[23] = true;
            }
            else if (option_num == 14 && GameManager.instance.attack_object[23] == true)
            {
                GameManager.instance.atk += 3;
                GameManager.instance.atk_cool -= 0.03f;
                GameManager.instance.arrow_speed += 0.03f;
                GameManager.instance.speed += 0.03f;
                GameManager.instance.jump_power += 0.03f;
                GameManager.instance.maxhp += 5;
                GameManager.instance.hp += 5;
                GameManager.instance.def += 1;
                calculator = true;
            }
            // ��ø�� ����
            if (option_num == 15 && GameManager.instance.attack_object[24] == false)
            {
                GameManager.instance.attack_object[24] = true;
                GameManager.instance.attack_object_cnt[24] = 1.15f;
            }
            else if (option_num == 15 && GameManager.instance.attack_object[24] == true)
            {
                GameManager.instance.attack_object_cnt[24] += 0.05f;
            }
            // ������
            if (option_num == 16 && GameManager.instance.attack_object[12] == false)
            {
                Instantiate(bt.attack_objects[4]);
                GameManager.instance.attack_object[12] = true;
                GameManager.instance.attack_object_cnt[12] = 0.85f;
            }
            else if (option_num == 16 && GameManager.instance.attack_object[12] == true) 
            {
                GameManager.instance.attack_object_cnt[12] += 0.1f;
            }
            // ������ ���� ( ��Ƽ�� )
            if (option_num == 17)
            {
                Active_Item_Setting("������ ����", 17);
            }
            // ��üȭ ( ��Ƽ�� )
            if (option_num == 18)
            {
                Active_Item_Setting("��üȭ", 18);
            }
            // ����Ʈ��
            if (option_num == 19 && GameManager.instance.attack_object[25] == false)
            {
                GameManager.instance.attack_object[25] = true;
                Instantiate(bt.attack_objects[3]);
            }
            else if (option_num == 19 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] <= 0)
            {
                Destroy(GameObject.FindWithTag("Lightning"));
                GameManager.instance.attack_object_cnt[25] = 1;
                if (GameManager.instance.arrow_size <= 0.8f)
                {
                    GameManager.instance.arrow_size = 0.8f;
                }
                else
                {
                    GameManager.instance.arrow_size += 2f;
                }
            }
            else if (option_num == 19 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] >= 1)
            {
                GameManager.instance.attack_object_cnt[25]++;
            }
            // ������ �ʴ� ����
            if (option_num == 20 && GameManager.instance.attack_object[18] == false)
            {
                GameManager.instance.attack_object[18] = true;
                GameManager.instance.maxhp += 15;
                GameManager.instance.hp += 15;
                GameManager.instance.def += 3;
                bt.heal_Alert.text = "+15";
                Instantiate(bt.heal_Alert);
            }
            else if (option_num == 20 && GameManager.instance.attack_object[18] == true)
            {
                GameManager.instance.hp += 25;
                bt.heal_Alert.text = "25";
                Instantiate(bt.heal_Alert);
            }
            // ������ ��
            if (option_num == 21) 
            {
                GameManager.instance.maxhp /= 2;
                GameManager.instance.def /= 2;
                GameManager.instance.atk = (int)(GameManager.instance.atk * 1.5f);
                calculator = true;
            }
            // �Ӵ� ��
            if (option_num == 22) 
            {
                Active_Item_Setting("�Ӵ� ��", 26);
            }
            // �ٺ�ť ��Ƽ!
            if (option_num == 23)
            {
                Active_Item_Setting("�ٺ�ť ��Ƽ!", 27);
            }
            // �����Ʈ
            if (option_num == 24)
            {
                Active_Item_Setting("�����Ʈ", 28);
            }
            // Ǫ�� ������ ����
            if (option_num == 25 && GameManager.instance.attack_object[30] == false)
            {
                Instantiate(GameManager.instance.summon_Object[3]);
                GameManager.instance.attack_object[30] = true;
                GameManager.instance.attack_object_cnt[30] = 1.35f;
            }
            else if (option_num == 25 && GameManager.instance.attack_object[30] == true)
            {
                GameManager.instance.attack_object_cnt[30] += 0.1f;
            }

        }
        // ����ũ ( 6�� )
        if (option_rank == 3)
        {
            // Ʈ���� ��
            if (option_num == 0 && GameManager.instance.tripple_shpt == false)
            {
                GameManager.instance.tripple_shpt = true;

            }
            else if (option_num == 0 && GameManager.instance.tripple_shpt == true)
            {
                GameManager.instance.atk += 10;
                GameManager.instance.maxhp += 50;
                GameManager.instance.hp = GameManager.instance.maxhp;
                calculator = true;
            }
            // ����� ����
            if (option_num == 1)
            {
                GameManager.instance.atk += 25;
                GameManager.instance.atk_cool += 0.4f;
                GameManager.instance.arrow_size += 0.1f;
                calculator = true;
            }
            // ������ ���±�
            if (option_num == 2)
            {
                Active_Item_Setting("������ ���±�", 2);
            }
            // ������ ��
            if (option_num == 3 && GameManager.instance.attack_object[26] == false)
            {
                GameManager.instance.attack_object[26] = true;
                GameManager.instance.arrow_size -= 0.25f;
                GameManager.instance.real_atk = GameManager.instance.atk;
                GameManager.instance.atk -= (int)(GameManager.instance.atk * 0.7);
                GameManager.instance.calcul = GameManager.instance.real_atk - GameManager.instance.atk;
                GameManager.instance.atk_cool = 0.13f;
            }
            // õ���� �ູ
            else if ((option_num == 3 && GameManager.instance.attack_object[26] == true) || option_num == 4) 
            {
                GameManager.instance.maxhp += 20;
                GameManager.instance.atk += 15;
                GameManager.instance.def += 3;
                GameManager.instance.speed += 0.03f;
                GameManager.instance.jump_cnt++;
                GameManager.instance.arrow_speed += 0.15f;
                calculator = true;
            }
            // �����ο� : ��
            if (option_num == 5 && GameManager.instance.attack_object[27] == false)
            {
                GameManager.instance.attack_object[27] = true;
                GameManager.instance.attack_object_cnt[27] = 1;
            }
            else if (option_num == 5 && GameManager.instance.attack_object[27] == true)
            {
                GameManager.instance.attack_object_cnt[27]++;
            }
        }

        // ������ �� �������
        if (GameManager.instance.attack_object[26] == true && calculator == true) 
        {
            int cal = GameManager.instance.real_atk - GameManager.instance.atk;
            cal = GameManager.instance.calcul - cal;
            GameManager.instance.real_atk += cal;
            GameManager.instance.atk = GameManager.instance.real_atk - (int)(GameManager.instance.real_atk * 0.7);
            GameManager.instance.calcul = GameManager.instance.real_atk - GameManager.instance.atk;

            GameManager.instance.atk_cool = 0.13f;
        }

        // ����Ʈ�� ������ ������ ����
        if (GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] >= 1) 
        {
            GameManager.instance.arrow_size = 0.8f;
        }

        // ������ ������Ʈ ����
        for (int i = 0; i < 4; i++)
        {
            Destroy(bt.LvUp_Object_List[cnt]);
            cnt--;
        }
        bt.lvup_cnt--;

        bt.List_Clean();
        bt.Using_Item_Create_List();

        // ������ ��!
        if (bt.lvup_cnt <= 0)
        {
            ary_leng = bt.LvUp_Object_List.Count;
            for (int i = 0; i < ary_leng; i++)
            {
                bt.LvUp_Object_List.RemoveAt(0);
            }
            bt.paused_now = false;
            bt.List_Clean();
            bt.LvUp_Object.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void Item_Img()
    {
        if (option_rank == 1)
        {
            item_img.sprite = GameManager.instance.Lv_Up_item_Img_n[option_num];

            /* ��ü������ �̹��� ���� */
            // ��ī��
            if (option_num == 29 && GameManager.instance.attack_object[20] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_n[32];
            }
            // ���ݸ�
            if (option_num == 27 && GameManager.instance.attack_object[13] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_n[33];
            }
            // ����� ���
            if (option_num == 28 && GameManager.instance.attack_object[19] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_n[34];
            }
            if (option_num == 32)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_n[35];
            }
        }

        else if (option_rank == 2)
        {
            item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[option_num];

            if (option_num == 10 && GameManager.instance.attack_object[17] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[21];
            }
            else if (option_num == 11 && GameManager.instance.attack_object[16] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[22];
            }
            else if (option_num == 20 && GameManager.instance.attack_object[18] == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[23];
            }
            else if (option_num == 0 && GameManager.instance.double_shot == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[24];
            }
            else if (option_num == 21)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[25];
            }
            else if (option_num == 22)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[26];
            }
            else if (option_num == 23)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[27];
            }
            else if (option_num == 24)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[28];
            }
            else if (option_num == 25)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_r[29];
            }
        }

        else if (option_rank == 3)
        {
            item_img.sprite = GameManager.instance.Lv_Up_item_Img_u[option_num];
            if (option_num == 3 && GameManager.instance.attack_object[26] == true) 
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_u[4];
            }
            // ����
            if (option_num == 0 && GameManager.instance.tripple_shpt == true)
            {
                item_img.sprite = GameManager.instance.Lv_Up_item_Img_u[5];
            }
        }
    }

    public void Active_Item_Setting(string name, int num)
    {
        if (option_rank == 1)
        {
            GameManager.instance.active_item_img = GameManager.instance.Lv_Up_item_Img_n[num];
            // ��ī��
            if (option_num == 29 && GameManager.instance.attack_object[20] == true)
            {
                GameManager.instance.active_item_img = GameManager.instance.Lv_Up_item_Img_n[32];
            }
        }
        else if (option_rank == 2)
        {
            GameManager.instance.active_item_img = GameManager.instance.Lv_Up_item_Img_r[num];
        }
        else if (option_rank == 3)
        {
            GameManager.instance.active_item_img = GameManager.instance.Lv_Up_item_Img_u[num];
        }

        if (name == "������ ���ڰ�")
        {
            GameManager.instance.active_item_cool = 50f;
            GameManager.instance.active_timmer = 50f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "������ ���ڰ�";
            bt.active_Item.SetActive(true);
        }
        if (name == "���� �Ҵ�Ʈ")
        {
            GameManager.instance.active_item_cool = 40f;
            GameManager.instance.active_timmer = 40f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "���� �Ҵ�Ʈ";
            bt.active_Item.SetActive(true);
        }
        if (name == "������ �Ҵ�Ʈ")
        {
            GameManager.instance.active_item_cool = 25f;
            GameManager.instance.active_timmer = 25f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "������ �Ҵ�Ʈ";
            bt.active_Item.SetActive(true);
        }
        if (name == "������ ���±�")
        {
            GameManager.instance.active_item_cool = 20f;
            GameManager.instance.active_timmer = 20f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "������ ���±�";
            bt.active_Item.SetActive(true);
        }
        if (name == "������ ����")
        {
            GameManager.instance.active_item_cool = 35f;
            GameManager.instance.active_timmer = 35f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "������ ����";
            bt.active_Item.SetActive(true);
        }
        if (name == "��üȭ")
        {
            GameManager.instance.active_item_cool = 25f;
            GameManager.instance.active_timmer = 25f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "��üȭ";
            bt.active_Item.SetActive(true);
        }
        if (name == "���߼��� ����")
        {
            GameManager.instance.active_item_cool = 2.5f;
            GameManager.instance.active_timmer = 2.5f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "���߼��� ����";
            bt.active_Item.SetActive(true);
        }
        if (name == "��ī��")
        {
            GameManager.instance.active_item_cool = 10f;
            GameManager.instance.active_timmer = 10f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "��ī��";
            bt.active_Item.SetActive(true);
        }
        if (name == "�Ӵ� ��")
        {
            GameManager.instance.active_item_cool = 15f;
            GameManager.instance.active_timmer = 15f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "�Ӵ� ��";
            bt.active_Item.SetActive(true);
        }
        if (name == "�ٺ�ť ��Ƽ!")
        {
            GameManager.instance.active_item_cool = 97f;
            GameManager.instance.active_timmer = 97f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "�ٺ�ť ��Ƽ!";
            bt.active_Item.SetActive(true);
        }
        if (name == "�����Ʈ")
        {
            GameManager.instance.active_item_cool = 23f;
            GameManager.instance.active_timmer = 23f;
            GameManager.instance.active_item_bool = true;
            GameManager.instance.active_item_name = "�����Ʈ";
            bt.active_Item.SetActive(true);
        }
    }
}
