using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Item : MonoBehaviour
{
    public int item_num, cost, type, rank;
    public TextMeshProUGUI item_explane;
    public Image img;
    public string it_name;

    GameObject btm;
    BattleManager bt;
    
    void Start()
    {
        btm = GameObject.FindWithTag("Manager");
        bt = GameObject.FindWithTag("Manager").GetComponent<BattleManager>();
    }

    void Update()
    {
        Item_option();
    }

    public void Item_option() 
    {
        // type �� 0 �̸� ���� , 1�̸� ������ ������
        if (type == 0)
        {
            if (item_num == 0)
            {
                item_explane.text = "������ ���� [10 ����]\n ü���� 10��ŭ ȸ�������ݴϴ�. ������ ������ �����ϰ� �������ϴ�.";
                img.sprite = GameManager.instance.shop_Food_img[0];
                cost = 10;
            }
            else if (item_num == 1)
            {
                item_explane.text = "ȣ�л� [15 ����]\nü���� 20��ŭ ȸ�� ��ŵ�ϴ�. �� ���迡���� ���� ���� �� �ִ� ���Դϴ�.";
                img.sprite = GameManager.instance.shop_Food_img[1];
                cost = 15;
            }
            else if (item_num == 2)
            {
                item_explane.text = "��ī�� [20 ����]\nü���� 25��ŭ ȸ����ŵ�ϴ�. �޴��� ���� �������ϴ�!";
                img.sprite = GameManager.instance.shop_Food_img[2];
                cost = 20;
            }
            else if (item_num == 3)
            {
                item_explane.text = "����Ƣ�� [25 ����]\nü���� 25��ŭ ȸ���ϰ� ���ݷ��� 1 ������ŵ�ϴ�. ������ �ٻ��ϰ� ����մϴ�!\n<size=17><color=#969696>??? : �� ���״� ������~</color></size>";
                img.sprite = GameManager.instance.shop_Food_img[3];
                cost = 25;
            }
            else if (item_num == 4)
            {
                item_explane.text = "���ݸ� ���� [30 ����]\nü���� 20��ŭ ȸ���ϰ� ���ݷ� 2 �̵��ӵ��� 1 ������ŵ�ϴ�. ������ ������ ���� ���� �����Դϴ�!";
                img.sprite = GameManager.instance.shop_Food_img[4];
                cost = 30;
            }
            else if (item_num == 5)
            {
                item_explane.text = "���� [40 ����]\nü���� 30��ŭ ȸ���ϰ� ���ݷ� 2 ���ݼӵ� 2 ������ŵ�ϴ�.";
                img.sprite = GameManager.instance.shop_Food_img[5];
                cost = 40;
            }
            else if (item_num == 6)
            {
                item_explane.text = "���� ��� [5 ����]\nü���� 5��ŭ ȸ���մϴ�. ����� ���� ��ǰ������ ���� ���� ������ ���� ������ �����ɴϴ�.";
                img.sprite = GameManager.instance.shop_Food_img[6];
                cost = 5;
            }
            else if (item_num == 7)
            {
                item_explane.text = "�Ҽ��� [20 ����]\nü���� 20��ŭ ȸ���ϰ� �̵��ӵ��� 2 ������ŵ�ϴ�. �������ϸ� �븩�븩�ϰ� ���ִ� ������ ���ϴ�.";
                img.sprite = GameManager.instance.shop_Food_img[7];
                cost = 20;
            }
            else if (item_num == 8)
            {
                item_explane.text = "��Ʈ���� [1 ����]\nü���� 1��ŭ ȸ���մϴ�. ������ �̻��� �İ��� �������ϴ�.";
                img.sprite = GameManager.instance.shop_Food_img[8];
                cost = 1;
            }
        }
        else if(type == 1)
        {
            // ���� ����
            if (item_num == 0 && GameManager.instance.attack_object[6] == false)
            {
                item_explane.text = "�����ο� : �������� [60 ����]\n������ �⺻���� ���߽ø��� 0.7�ʸ��� ������ ���ݷ���" + (int)((GameManager.instance.attack_object_cnt[6]) * 100) + "% �������ظ� ������ '�ߵ�' �����̻��� �ο��մϴ�. '�ߵ�' �����̻��� 5�ʵ��� �����˴ϴ�.";
                img.sprite = GameManager.instance.shop_Item_img[0];
                cost = 60;
            }
            else if (item_num == 0 && GameManager.instance.attack_object[6] == true)
            {
                item_explane.text = "�����ο� : ��������+ [60 ����]\n'�ߵ�'�����̻��� ���ݷ��� 1% ������ŵ�ϴ�.";
                img.sprite = GameManager.instance.shop_Item_img[0];
                cost = 60;
            }

            // ���� ����
            if (item_num == 1 && GameManager.instance.attack_object[8] == false)
            {
                item_explane.text = "�����ο� : ���߸��� [65 ����]\n������ ���ϴ� �⺻���ݿ� ���߸����� �ο��մϴ�. ������ �⺻������ ���߽� ���� ������ ����Ű�� ��ü�� ������ ������ŵ�ϴ�\n���� �������� ���ݷ��� 165% �� �ش��մϴ�.";
                img.sprite = GameManager.instance.shop_Item_img[1];
                cost = 65;
            }
            if (item_num == 1 && GameManager.instance.attack_object[8] == true)
            {
                item_explane.text = "�����ο� : ���߸���+\n���߸����� ���� �������� 10% ������ŵ�ϴ�.";
                img.sprite = GameManager.instance.shop_Item_img[1];
                cost = 65;
            }

            // ����Ʈ��
            if (item_num == 2 && GameManager.instance.attack_object[25] == false)
            {
                item_explane.text = "����Ʈ��! [75 ����]\n<size=18>�⺻ ������ ��÷� ������ ������ ���⸦ �������� �����ϴ� ���·� ��ȭ�ϸ� ���ݷ��� 10 �����մϴ�.</size>\n<size=17>������ ���� ������ �� ������ ������ �⺻���� ��ȭ ȿ���� ���� �� �����ϴ�.</size>";
                img.sprite = GameManager.instance.shop_Item_img[2];
                cost = 75;
            }
            else if (item_num == 2 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] == 0)
            {
                item_explane.text = "����Ʈ�� �� [75 ����]\n�⺻������ ���� �������� �����մϴ�. ������ �Ϸ�� ���¿��� ���ݽ� ������ ������ ������ ���濡 �߻��մϴ�. ������ ���� ���� �� �� �ֽ��ϴ�.\n" +
                    "'���߸���'���� �⺻���� ��ȭ ȿ���� ���� �����մϴ�.";
                img.sprite = GameManager.instance.shop_Item_img[2];
                cost = 75;
            }
            else if (item_num == 2 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] == 1)
            {
                item_explane.text = "�������� [75 ����]\n20% Ȯ���� '����Ʈ�� ��'�� �ǰ��� ������ '����' ȿ���� �߻���ŵ�ϴ�." +
                    "\n<size='16'>���� : �ǰݴ��� ����� �߽����� ������ ������ �帣�� ������ ���� ���� ��� ��� ������ ���ݷ��� 100%�� ���ϴ� ���ظ� �ָ� 45% Ȯ���� �����ŵ�ϴ�.</size>";
                img.sprite = GameManager.instance.shop_Item_img[2];
                cost = 75;
            }
            else if (item_num == 2 && GameManager.instance.attack_object[25] == true && GameManager.instance.attack_object_cnt[25] >= 2)
            {
                item_explane.text = "��������+[75 ����]\n'����'ȿ���� ���ݷ��� 5% ������ŵ�ϴ�. ���� �߰� ���ݷ�" + GameManager.instance.attack_object_cnt[25] * 5+"%";
                img.sprite = GameManager.instance.shop_Item_img[2];
                cost = 75;
            }

            // ȸ����ü
            if (item_num == 3 && GameManager.instance.attack_object[0] == false)
            {
                item_explane.text = "ȸ���� ���±�ü [55 ����]\n13�ʸ��� 5�ʵ��� �÷��̾� ĳ������ �ֺ��� ȸ���ϴ� ��ü�� �����մϴ�. �ߺ� ���ý� �� ��ü�� ������ �þ�ϴ�.\n<size=14>�÷��̾� ���ݷ��� 75% ��ŭ�� ������.</size>";
                img.sprite = GameManager.instance.shop_Item_img[3];
                cost = 55;
            }
            if (item_num == 3 && GameManager.instance.attack_object[0] == true)
            {
                item_explane.text = "ȸ���� ���±�ü [55 ����]\n���� ��ü�� ���ڸ� �Ѱ� �� �ø��ϴ�. \n���� ���±�ü ���� : " + GameManager.instance.attack_object_cnt[0];
                img.sprite = GameManager.instance.shop_Item_img[3];
                cost = 55;
            }

            // ��ȣ�� �帷
            if (item_num == 4)
            {
                int an = (int)GameManager.instance.attack_object_cnt[3] - 1;
                if (an < 5)
                {
                    an = 5;
                }
                item_explane.text = "��ȣ�� �帷[70 ����]\n" + an + "�ʸ��� ���� ������ 1ȸ �����ִ� ��ȣ���� �����մϴ�.";
                img.sprite = GameManager.instance.shop_Item_img[4];
                cost = 70;
            }

            // �ź��� ��
            if (item_num == 5)
            {
                item_explane.text = "�ź��� �� [25 ����]\n" + GameManager.instance.attack_object_cnt[14] + "%Ȯ���� ���ݷ�, ���ݼӵ�, �̵��ӵ� 3������ �ϳ��� 5 ������ŵ�ϴ�. ������ ����Ȯ���� 10% �����ǰ� ���н� 10% �����ϰ� ��� ȿ���� ���� �� �����ϴ�. \n(�ִ� 75% ���� 25%)"
                    + "<size=17><color=#969696>??? : �̰�... Ȯ���� �� �̻���!</color></size>";
                img.sprite = GameManager.instance.shop_Item_img[5];
                cost = 25;
            }

            // ��ø ����
            if (item_num == 6 && GameManager.instance.attack_object[24] == false)
            {
                item_explane.text = "��ø�� ���� [65 ����]\n�⺻���� �߻�� ź�˿� ����ִ� ������ ������ ���Խ��� ��ø��ŵ�ϴ�. 3�� �� ��ø�� ������ �� �ֺ��� ���ݷ��� 115%�� ������ü�� ��ø �� �� ��ŭ �����Ǿ� ���� ���� Ÿ���մϴ�." +
                        " (�ִ� ��ø �� 5ȸ)";
                img.sprite = GameManager.instance.shop_Item_img[6];
                cost = 65;
            }
            else if (item_num == 6 && GameManager.instance.attack_object[24] == true)
            {
                item_explane.text = "��ø�� ����+ [65 ����]\n��ø�� ���� ���ݷ� ����� 5% �����մϴ�.\n������ : " + GameManager.instance.attack_object_cnt[24] * 100 + "%";
                img.sprite = GameManager.instance.shop_Item_img[6];
                cost = 65;
            }

        }
        
    }

    public void Buy() 
    {
        bt.buy_alert.SetActive(true);
        bt.buy_number = item_num;
        bt.buy_cost = cost;
        bt.buy_type = type;
        bt.buy_alery_text.text = "������ �����Ͻðڽ��ϱ�?\n" + cost + "������ �Ҹ� �˴ϴ�.";
    }
}
