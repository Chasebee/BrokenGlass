using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Achievements_Objects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image[] img;
    public Image alert_img;
    public GameObject trophy, alert, tool_tip;
    public TextMeshProUGUI explane, clear_text;
    public int num;

    void Update()
    {
        if (GameManager.instance.playerdata.Achievements[num] == true)
        {
            gameObject.GetComponent<Button>().interactable = true;
            img[0].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            img[1].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            trophy.SetActive(true);
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
            img[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            img[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            trophy.SetActive(false);
        }
    }

    public void Click() 
    {
        alert_img.sprite = img[1].sprite;
        alert.SetActive(true);
        switch (num)
        {
            case 0:
                explane.text = "����� �ְ��\n�÷��� ���� ����� �ְ��Դϴ�! ���� ����帳�ϴ�!";
                break;
            case 1:
                explane.text = "��������!!\n���� ����ü ����ϱ��? Ȳ�ݺ��� ����簡 �ݰ������� �ʴ´ٸ� ���ڳ׿�....";
                break;
            case 2:
                explane.text = "�ĳĳĿ� �Ŀ�!\n������ ���������ϸ� ���Ӽ� ���� ����� �йи��� �׷ΰ� ������ ���ϰ� ���� �ɱ��?";
                break;
            case 3:
                explane.text = "������ ��!\nȰ���ϸ� ������ ��ġ�� ������ �йи��� �����̰� ������ ���ϰ� ���� �ɱ��?";
                break;
            case 4:
                explane.text = "���Ǻ����� ģ����\n������ ���Ǻ����� ������� �� �͸� �����ϴ�! �ܷ����� �ʰڳ׿�.";
                break;
            case 5:
                explane.text = "�׷����� ������\né�� 1 ���� ���� �Ŵ� ���������� ����� ��밡 ���� �ʴ±���!";
                break;
            case 6:
                explane.text = "����͸�\né�� 2 �縷�� ���� ���ֹ��� ������ ��� ������ ���߳� �¸��ϼ̽��ϴ�!";
                break;
            case 7:
                explane.text = "������ ��\n������ ������ ���� é�� 3�� ���� ũ�������� ����� ����Ʈ���� ���մϴ�! ����� ������ �����Դϴ�!";
                break;
            case 8:
                explane.text = "ȭ���� ����\né�� 4�� ������ ��� �񷽵� ����� ����Ʈ�� �� �������ϴ�. ����� �� ȭ���� �����Դϴ�!";
                break;
            case 9:
                explane.text = "�� �������� ���� �մ� ��������!\n������ �������� ���� ���ϴ°� �ƹ��͵� �����ϴ�!";
                break;
            case 10:
                explane.text = "�̰� ��ź������!\n���� �ѹ� ���� ���� ����ü�� �����ϼ���!";
                break;
            case 11:
                explane.text = "������ HIGH�� ���!\n������ �����ӵ��� ���� ���� �� �� �ְԵǾ����ϴ�! ����� ������ ���׿�!";
                break;
            case 12:
                explane.text = "�Ŵ�ȭ!\n�Ŵ��������ϴ�! �װ� ��ϴ�! �Ƹ�����.";
                break;
            case 13:
                explane.text = "���ȭ!\n�۾������ϴ�! �װ� ��ϴ�! �Ƹ�����.";
                break;
            case 14:
                explane.text = "����?\n������ �����Դϴ�! �������� �׷��ٰ� �ϳ׿�. �����ѵ� ������.";
                break;
            case 15:
                explane.text = "�̰ɻ��\n�̰ɻ��. ��� ��Ƴ����̾��?";
                break;
            case 16:
                explane.text = "�ൿ�� �����մϴ�.\n�����Ǿ����ϴ�! ������ �� �� �����ϴ�!";
                break;
            case 17:
                explane.text = "���� �ŷ�����.\n���� �ŷ������ϴ�! ���� ��� ����ΰ� ��������....";
                break;
            case 18:
                explane.text = "©�׶�!\n���� �� ��� ���� �Ҹ��ΰŰ����ϴ�. ���� �鸱���� ������ ���׿�.";
                break;
            case 19:
                explane.text = "�����غ�\n������ �̸� �غ��صθ� ������� �����ϴ�!";
                break;
            case 20:
                explane.text = "�������ּ���\n��ȣ�� �帷���ε� ����� ��ȣ�ϱ� ����� ���̳׿�.";
                break;
            case 21:
                explane.text = "��Ƽ ����\n���� óġ���� ���ߴٸ� ź���� ���ڶ��� �ʾҴ��� �����غ��°� ����?";
                break;
            case 22:
                explane.text = "ã�ƶ� ����� ����\n��Ű���? �� ��ܺ��̳���?";
                break;
            case 23:
                explane.text = "�װ��� �� �ܻ��Դϴ�.\n�ʹ� ���� ���� �������� �ʳ׿�!";
                break;
            case 24:
                explane.text = "��!\n���⼼ ������ ������ ���׿�.";
                break;
            case 25:
                explane.text = "��.. �̸���!\n������ �� �丮�� �ٺ�ť�����ϴ�! ȲȦ�� ���̿��ٱ���!";
                break;
            case 26:
                explane.text = "�����Ʒå�\n�⺻�� ���� �߿��մϴ�.";
                break;
            case 27:
                explane.text = "�����Ʒå�\n�����Դϴ�. ���� �⺻�� �� �ٵ����մϴ�.";
                break;
            case 28:
                explane.text = "�����Ʒå�\n����������! �⺻�� �߿��մϴ�!";
                break;
            case 29:
                explane.text = "����� �� ����?\n�и� ����� ���ư��� ������... �ƴѰ� ���׿�!";
                break;
        }
    }

    public void Clear_Requirements() 
    {
        switch (num)
        {
            case 0:
                clear_text.text = "�޼� ���� : ������ ó�� �����ϱ�";
                break;
            case 1:
                clear_text.text = "�޼� ���� :  �Ϲݸ��� ������ �����ϼ���.";
                break;
            case 2:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� '����ģ�� �׷�' ������ �ɼ��� �����ϱ�.";
                break;
            case 3:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� '���Ǻ����� ģ�� ����' ������ �ɼ��� �����ϱ�.";
                break;
            case 4:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� '����ģ�� �׷�', '���Ǻ����� ģ�� ����' �������� ���ÿ� �����ϼ���.";
                break;
            case 5:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� é�� 1�� ������ óġ�ϼ���.";
                break;
            case 6:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� é�� 2�� ������ óġ�ϼ���.";
                break;
            case 7:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� é�� 3�� ������ óġ�ϼ���.";
                break;
            case 8:
                clear_text.text = "�޼� ����  �Ϲݸ�忡��: é�� 4�� ������ óġ�ϼ���.";
                break;
            case 9:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� '����Ʈ�� ��' �ɼ����� �ѹ��� �� 3���� �����ϼ���.";
                break;
            case 10:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� '��ȣ�� �帷' �ɼ����� ������ 1ȸ ��ȿȭ �ϼ���.";
                break;
            case 11:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� ���ݼӵ��� �ִ��ġ�� �޼��ϼ���.";
                break;
            case 12:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� ĳ������ ũ�⸦ �ִ�ġ�� �޼��ϼ���.";
                break;
            case 13:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� ĳ������ ũ�⸦ �ּ�ġ�� �޼��ϼ���.";
                break;
            case 14:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� '����ź' Ȥ�� '�����ο� : ���߸���'���� ������ ���߷�\n�ѹ��� �� 3���� �����ϼ���.";
                break;
            case 15:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� ���� ü���� 1�̿��� �մϴ�.";
                break;
            case 16:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� ���� �����̻� �ɸ�����.";
                break;
            case 17:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� ������ �����ϴ� ������ΰ� �ŷ��� 1ȸ�� �����Ű����.";
                break;
            case 18:
                clear_text.text = "�޼� ���� : ������ ó������ ������ �ֿ�������.";
                break;
            case 19:
                clear_text.text = "�޼� ���� : �������� �������� �ƹ� �����̳� �����ϼ���.";
                break;
            case 20:
                clear_text.text = "�޼� ���� : �Ϲݸ���� �� ������������ '��ȣ�� �帷' ȿ���� 10�� �̻� �ߵ���Ű����.";
                break;
            case 21:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� '���� ��' �� 'Ʈ���� ��' �������� ���ÿ� �����ϼ���.";
                break;
            case 22:
                clear_text.text = "�޼� ���� : Ư�� Ŀ�ǵ带 �Է��ϼ���.";
                break;
            case 23:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� '��������' �ɼ��� ����ϼ���.";
                break;
            case 24:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� '���� ������ ����' �� 'Ǫ�� ������ ����' �������� ���ÿ� �����ϼ���.";
                break;
            case 25:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� ��Ƽ�� ������ '�ٺ�ť ��Ƽ!' �� ��� ȿ���� �丮�� �ٺ�ť�� �Ծ���մϴ�.";
                break;
            case 26:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� �Ϲݰ��� Ƚ���� �� 10ȸ �̻��� �޼��ϼ���.";
                break;
            case 27:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� �Ϲݰ��� Ƚ���� �� 100ȸ �̻��� �޼��ϼ���.";
                break;
            case 28:
                clear_text.text = "�޼� ���� : �Ϲݸ�忡�� �Ϲݰ��� Ƚ���� �� 1000ȸ �̻��� �޼��ϼ���.";
                break;
            case 29:
                clear_text.text = "�޼� ���� : ������带 ó������ �÷��� �ϼ���.";
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        Clear_Requirements();
        tool_tip.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData) 
    {
        clear_text.text = null;
        tool_tip.SetActive(false);
    }
}
