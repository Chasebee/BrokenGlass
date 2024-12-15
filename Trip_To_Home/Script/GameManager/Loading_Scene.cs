using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Loading_Scene : MonoBehaviour
{
    [SerializeField]
    Image loadging_bar;
    [SerializeField]
    TextMeshProUGUI tmi_text;
    static string next_Scene;
    int rnd;
    string[] tmi = {"Tmi 1. �׷δ� �����߿��� ���� ���̸� ���� �����Ѵ�ϴ�!", "Tmi 2. ���� ������ ������ ���մ� ����� �޴����� �����Ϸ��� 5�ʸ� �ȴٰ� �մϴ�!", "Tmi 3. ���ΰ��� ���̴� ����ϱ��?", 
        "Tmi 4. �� ���迡���� �������� ����� ���� �� �ִٰ� �մϴ�. �����ο�!", "Tmi 5. ��������� ���� �ձñ�翴�ٴ� �ҹ��� �ֽ��ϴ�.", "Tmi 6. �÷��� �� �ּż� �����մϴ�.",
    "Tmi 7. '������ ��â��'�� �� ���迡���� ���� ���� �� �� ���� �����̸� ������ ��� ������ �ϰŷ� �ǰ� �ִٰ� �մϴ�.", "Tmi 8. �׷δ� �޺��� �� ��� ��Ҹ� �����մϴ�.",
    "Tmi 9. �����̴� ������ õ���������� ���� �ʴ´ٰ� �մϴ�!", "Tmi 10. ������ �����ٴ� ���ڰ��� ��ü ��� ��ȯ�Ǵ°ɱ��?", "Tmi 11. ���ΰ��� �����纸�� ���簡 �ǰ�;��ٰ� �մϴ�.", "Tmi 12. �����̴� ������ �����Ѵٰ� �մϴ�!",
    "Tmi 13. ���ΰ��� ��� ������ �ٷ� �� �ְ� �Ȱɱ��?", "Tmi 14. ������ Ŀ�ǵ尡 �ִٰ� �ϳ׿�...? � ȿ���� �������?", "Tmi 15. ��� �ؾ� ���Ӽ����� �������°ɱ��?"};
    public static void LoadScene(string sceneName)
    {
        next_Scene= sceneName;
        SceneManager.LoadScene("Loading");
    }
    void Start()
    {
        GameManager.instance.achievement_bool = false;
        rnd = Random.Range(0, tmi.Length);
        tmi_text.text = tmi[rnd];
        StartCoroutine(LoadSceneProcess());
    }
    IEnumerator LoadSceneProcess() 
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(next_Scene);
        op.allowSceneActivation= false;
        float timmer = 0f;
        while (!op.isDone) 
        {
            yield return null;
            if (op.progress < 0.9f)
            {
                loadging_bar.fillAmount = op.progress;
            }
            else 
            {
                timmer += Time.unscaledDeltaTime;
                loadging_bar.fillAmount = Mathf.Lerp(0.9f, 1f, timmer);
                if (loadging_bar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
