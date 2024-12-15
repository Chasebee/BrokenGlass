using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Story_Doal : MonoBehaviour
{
    public int now;
    public int end;
    public bool clear_game;
    public TextMeshProUGUI text;
    public GameObject game_end;
    public TextMeshProUGUI score_text;
    public TextMeshProUGUI end_text;

    public GameObject btm;
    BattleManager bt;

    void Start()
    {
        btm = GameObject.FindWithTag("Manager");
        bt = btm.GetComponent<BattleManager>();
        Time.timeScale = 0f;
    }
    void Update()
    {
        if (clear_game == false)
        {
            // é�� 1
            if (GameManager.instance.area_num == 1)
            {
                if (GameManager.instance.stage_num == 2)
                {
                    if (now == 0)
                    {
                        text.text = "�̼���\n<size=42>�ƾ߾�... �̰� ��ü �������̾�...</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "�̼���\n<size=42>�и� ������ ���³��̶� ������ ������ �غ��� �߾��µ� �̰� ����ü �������ΰ���?</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "�̼���\n<size=42>�׷��� ���� ����� ����ü ����..? �ֺ��� ���� �ϴ� ���� �� ���迡�� �� ���� �����ε�...\n���� ���Ӽӿ� ���� ���°ž�..?</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "�̼���\n<size=42>ħ������ ���� ���Ⱑ ���� �˴� ���� �� �̶�� ������ �����Ұž�.</size>";
                    }
                    else if (now == 4)
                    {
                        text.text = "�̼���\n<size=42>������ �����Ѱ� ���� ������ �������°� ������...?</size>";
                    }
                    else if (now == 5)
                    {
                        text.text = "�̼���\n<size=42>�ϴ� ���⼭ �������� ������ ������ �� �� ������... �и� �� ������ 4���� '���� ����'�� ��� ��ƾ߸� �ϴ� �����̿���..?</size>";
                    }
                    else if (now == 6)
                    {
                        text.text = "�̼���\n<size=42>��Ե� ��� '���� ����'�� ��Ƽ� ���� ����� ���ư��� ���ھ�!</size>";
                    }
                }
                if (GameManager.instance.stage_num == 7)
                {
                    if (now == 0)
                    {
                        text.text = "�̼���\n<size=42>�������� �ݹ� ��ġ�µ�..?</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "�̼���\n<size=42>��ø� ���⼭ �����ٰ� �ٽ� ���߰ھ�.</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "???\n<size=42>(�ν��� �ν���)</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "�̼���\n<size=42>... �̰� ���� �Ҹ���?</size>";
                    }
                    else if (now == 4)
                    {
                        text.text = "???\n<size=42>(�ν��� �ν��� �ν��� �ν��� �ν���)</size>";
                    }
                    else if (now == 5)
                    {
                        text.text = "�̼���\n<size=42>���� Ŀ�ٶ��� �������� ���°Ű�����..? ���� �غ��ؾ߰ھ�!</size>";
                    }
                }
            }
            // é�� 2
            else if (GameManager.instance.area_num == 2)
            {
                if (GameManager.instance.stage_num == 2)
                {
                    if (now == 0)
                    {
                        text.text = "�̼���\n<size=42>���ڱ� �׷��� Ŀ�ٶ� �༮�� ���� �� ���� �޿��� �����µ� ���� ���� ������ �����̴�..</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "�̼���\n<size=42>�׷��� �� Ŀ�ٶ� �༮�� '���� ����'�� ������ �־ ������ ã�ƾ� �ϴ� ����� ��� ���ٰ� �ؾ��Ϸ���...</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "�̼���\n<size=42>�׳����� ���� �縷�̶� �׷��� ��û���� ����...</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "�̼���\n<size=42>�� ã�Ƽ� ��������� ���ư��߰ھ�...</size>";
                    }
                }
                if (GameManager.instance.stage_num == 7)
                {
                    if (now == 0)
                    {
                        text.text = "�̼���\n<size=42>����� �縷�� �ƴ� ���� �����ΰ� ������..?</size>";
                    }
                    if (now == 1)
                    {
                        text.text = "???\n<size=42>ħ����...</size>";
                    }
                    if (now == 2)
                    {
                        text.text = "�̼���\n<size=42>?... ���� �̷����� �ִ°ǰ�?</size>";
                    }
                    if (now == 3)
                    {
                        text.text = "???\n<size=42>���� �Ƚ��� �����ϴ� �ڴ� ��� ������ �ʰڴ�!</size>";
                    }
                    if (now == 4)
                    {
                        text.text = "�̼���\n<size=42>�̰�.. ��Ȳ�� �����Ը� �귯���°� ������..?</size>";
                    }
                }
            }
            // é�� 3
            else if (GameManager.instance.area_num == 3)
            {
                if (GameManager.instance.stage_num == 2)
                {
                    if (now == 0)
                    {
                        text.text = "�̼���\n<size=42>���ڱ� �̻��� ������ ���� �� �� ���̾�...</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "�̼���\n<size=42>�׷��� �� ������'���� ����'�� ������ �־ �����̾�</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "�̼���\n<size=42>�׳����� �ű��ϴ�... �ٴ���ε� ���� �������ְ� ���󿡼� �ȴ� �� ó�� ������ �� �� �־�.</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "�̼���\n<size=42>�̰͵� ������ ���̶� ������ �ɱ�?</size>";
                    }
                    else if (now == 4)
                    {
                        text.text = "�̼���\n<size=42>�ϴ��� � ��������!</size>";
                    }
                }
                if (GameManager.instance.stage_num == 7)
                {
                    if (now == 0)
                    {
                        text.text = "�̼���\n<size=42>�������� �ʹ� �ݹ� ��ġ��... ���� �����ٰ� �������߰ڴ�.</size>";
                    }
                    if (now == 1)
                    {
                        text.text = "�̼���\n<size=42>���? �� ���� ��ü�� ����? ������ ������ �����̴°� ������...</size>";
                    }
                    if (now == 2)
                    {
                        text.text = "�̼���\n<size=42>��...����! �� ���ڱ� �������� ���°ž�?!</size>";
                    }
                    if (now == 3)
                    {
                        text.text = "�̼���\n<size=42>��...��û Ŀ�ٶ� �����ݾ�!? ���� �ٷ� ũ�����ΰǰ�???</size>";
                    }
                    if (now == 4)
                    {
                        text.text = "�̼���\n<size=42>����...? ���ڱ� ������ �̼��ϰ� ������ �������µ�?</size>";
                    }
                    if (now == 5)
                    {
                        text.text = "�̼���\n<size=42>�ٷ� �� �ؿ��� ���� �ö���� �� ������? ���ؾ߰ھ�!</size>";
                    }
                    if (now == 6)
                    {
                        text.text = "<size=42>(�Ŵ��� ũ������ �˼��� ���� �հ� �ö�´�)</size>";
                    }
                    if (now == 7)
                    {
                        text.text = "�̼���\n<size=42>ũ..ū�ϳ����ߴ�...</size>";
                    }
                    if (now == 8)
                    {
                        text.text = "�̼���\n<size=42>�̳༮�� ó������ ������ ���� ���� ���ҰŰ�����...?</size>";
                    }
                }
            }
            // é�� 4
            else if (GameManager.instance.area_num == 4)
            {
                if (GameManager.instance.stage_num == 2)
                {
                    if (now == 0)
                    {
                        text.text = "�̼���\n<size=42>���... ���� �縷���� �ξ� �̴߰�...</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "�̼���\n<size=42>�׷��� �Ʊ� �� �Ŵ��� ����� ����ü �� ���ڱ� �� ���� �����Ѱɱ�?</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "�̼���\n<size=42>�׷��� �׳༮�� ������ ������ �־ ������ ��������...</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "�̼���\n<size=42>�ϴ� �̰��� ���� �������� ���ڴ�...</size>";
                    }
                    else if (now == 4)
                    {
                        text.text = "�̼���\n<size=42>�� ��ü���� ���� � ������ ��������!</size>";
                    }
                }
                if (GameManager.instance.stage_num == 7)
                {
                    if (now == 0)
                    {
                        text.text = "�̼���\n<size=42>��...��... �ʹ� �̰߰� ��ģ��...</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "�̼���\n<size=42>���� �� �������� �ִµ�....</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "�̼���\n<size=42>������� �� �����̶�� �ű��ϴ�...</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "�̼���\n<size=42>��..��..? �̰� �� �����̴°� ����?</size>";
                    }
                    else if (now == 4)
                    {
                        text.text = "�̼���\n<size=42>������ �������ݾ�! �ٷ� �غ��ؾ߰ھ�!</size>";
                    }
                }
            }
        }
        else
        {
            if (GameManager.instance.area_num == 4) 
            {
                if (GameManager.instance.stage_num == 7) 
                {
                    if (now == 0)
                    {
                        text.text = "�̼���\n<size=42>������ ������ ��� ���� �� ���� �����ϰ� �����ߴ��Ű���...</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "�̼���\n<size=42>������ ������ �̳༮�� �����־������̾�..</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "�̼���\n<size=42>�׷��� ���� ��� ���� ������ ��Ҿ�!</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "�̼���\n<size=42>���� �� ���� ����� ���� �� �� �ְ� �Ǿ�����...</size>";
                    }
                    else if (now == 4)
                    {
                        text.text = "�̼���\n<size=42>���� �����̿�! ���� �ٶ��� ���� ���� �ִ� ����� ���ư��� ���̴�!</size>";
                    }
                    else if (now == 5)
                    {
                        text.text = "(���� ������ �ϳ��� ��ģ �� �ҿ��� ����, ���� �����鿡�� ���� ���� �����Ѵ�.)";
                    }
                    else if (now == 6)
                    {
                        text.text = "(���� �������� �վ��� ������ ���� ���� �̼����� ���հ� �̼����� �� �ڸ����� �������.)";
                    }
                    else if (now == 7)
                    {
                        text.text = "(���� �������� ������ ���� ��׶� ���� �� �ڸ��� ������ ���� �������� �ٽ� ������� �������. ���� �������� ������ �� �ٽ� �������� ���� ���� ���� ��ٸ����̴�.)";
                    }
                    else if (now == 8)
                    {
                        text.text = "(END)\n���� ��尡 �رݵǾ����ϴ�.";
                        GameManager.instance.playerdata.story_Mod_Clear = true;
                    }
                    else if (now == 9)
                    {
                        gameObject.SetActive(false);
                        game_end.SetActive(true);
                        end_text.text = "GAME CLEAR!!!";
                        if (GameManager.instance.score < GameManager.instance.playerdata.max_Scroe)
                        {
                            score_text.text = "���� ���� : " + GameManager.instance.score + "\n<size=24>�ְ� ���� : " + GameManager.instance.playerdata.max_Scroe + "</size>";
                        }
                        else if (GameManager.instance.score > GameManager.instance.playerdata.max_Scroe)
                        {
                            score_text.text = "���� ���� : " + GameManager.instance.score + "\n�ű��!";
                            GameManager.instance.playerdata.max_Scroe = GameManager.instance.score;
                            GameManager.instance.Save_PlayerData_ToJson();
                        }
                        GameManager.instance.bgm_audioSource.Stop();
                    }
                }
            }
        }
        bt.paused_now = true;
    }
    public void Story() 
    {
        if(now >= end)
        {
            bt.paused_now = false;
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
        now++;
    }
}
