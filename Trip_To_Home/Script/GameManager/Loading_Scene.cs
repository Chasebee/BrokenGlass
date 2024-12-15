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
    string[] tmi = {"Tmi 1. 네로는 생선중에서 고등어 구이를 제일 좋아한답니다!", "Tmi 2. 붉은 전기의 위습이 내뿜는 전기로 휴대폰을 완충하려면 5초면 된다고 합니다!", "Tmi 3. 주인공의 나이는 몇살일까요?", 
        "Tmi 4. 이 세계에서는 슬라임을 얼려서 먹을 수 있다고 합니다. 정말로요!", "Tmi 5. 방랑상인은 전직 왕궁기사였다는 소문이 있습니다.", "Tmi 6. 플레이 해 주셔서 감사합니다.",
    "Tmi 7. '마법의 반창고'는 이 세계에서는 원래 존재 할 수 없는 물건이며 굉장히 비싼 값으로 암거래 되고 있다고 합니다.", "Tmi 8. 네로는 햇볕이 잘 드는 장소를 좋아합니다.",
    "Tmi 9. 몽몽이는 언제나 천진난만함을 잃지 않는다고 합니다!", "Tmi 10. 성령이 깃들었다는 십자가는 대체 어디서 소환되는걸까요?", "Tmi 11. 주인공은 마법사보단 전사가 되고싶었다고 합니다.", "Tmi 12. 몽몽이는 개껌을 좋아한다고 합니다!",
    "Tmi 13. 주인공은 어떻게 마력을 다룰 수 있게 된걸까요?", "Tmi 14. 숨겨진 커맨드가 있다고 하네요...? 어떤 효과가 있을까요?", "Tmi 15. 어떻게 해야 게임속으로 빨려들어가는걸까요?"};
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
