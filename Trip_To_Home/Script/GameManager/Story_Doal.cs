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
            // 챕터 1
            if (GameManager.instance.area_num == 1)
            {
                if (GameManager.instance.stage_num == 2)
                {
                    if (now == 0)
                    {
                        text.text = "이소현\n<size=42>아야야... 이게 대체 무슨일이야...</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "이소현\n<size=42>분명 오늘은 쉬는날이라서 집에서 게임좀 해볼까 했었는데 이게 도대체 무슨일인거지?</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "이소현\n<size=42>그러고 보니 여기는 도대체 어디야..? 주변이 내가 하던 게임 속 세계에서 볼 법한 느낌인데...\n설마 게임속에 내가 들어온거야..?</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "이소현\n<size=42>침착하자 만약 여기가 내가 알던 게임 속 이라면 마법이 가능할거야.</size>";
                    }
                    else if (now == 4)
                    {
                        text.text = "이소현\n<size=42>마법이 가능한걸 보니 정말로 빨려들어온거 같은데...?</size>";
                    }
                    else if (now == 5)
                    {
                        text.text = "이소현\n<size=42>일단 여기서 나가려면 게임을 끝내야 할 것 같은데... 분명 이 게임은 4개의 '별의 조각'을 모두 모아야만 하는 게임이였지..?</size>";
                    }
                    else if (now == 6)
                    {
                        text.text = "이소현\n<size=42>어떻게든 모든 '별의 조각'을 모아서 원래 세계로 돌아가고 말겠어!</size>";
                    }
                }
                if (GameManager.instance.stage_num == 7)
                {
                    if (now == 0)
                    {
                        text.text = "이소현\n<size=42>생각보다 금방 지치는데..?</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "이소현\n<size=42>잠시만 여기서 쉬었다가 다시 가야겠어.</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "???\n<size=42>(부스럭 부스럭)</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "이소현\n<size=42>... 이게 무슨 소리지?</size>";
                    }
                    else if (now == 4)
                    {
                        text.text = "???\n<size=42>(부스럭 부스럭 부스럭 부스럭 부스럭)</size>";
                    }
                    else if (now == 5)
                    {
                        text.text = "이소현\n<size=42>뭔가 커다란게 이쪽으로 오는거같은데..? 당장 준비해야겠어!</size>";
                    }
                }
            }
            // 챕터 2
            else if (GameManager.instance.area_num == 2)
            {
                if (GameManager.instance.stage_num == 2)
                {
                    if (now == 0)
                    {
                        text.text = "이소현\n<size=42>갑자기 그렇게 커다란 녀석이 덤벼 들 줄은 꿈에도 몰랐는데 별일 없이 끝나서 다행이다..</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "이소현\n<size=42>그래도 그 커다란 녀석이 '별의 조각'을 가지고 있어서 오히려 찾아야 하는 수고는 덜어서 좋다고 해야하려나...</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "이소현\n<size=42>그나저나 여긴 사막이라 그런지 엄청나게 덥네...</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "이소현\n<size=42>얼른 찾아서 원래세계로 돌아가야겠어...</size>";
                    }
                }
                if (GameManager.instance.stage_num == 7)
                {
                    if (now == 0)
                    {
                        text.text = "이소현\n<size=42>여기는 사막이 아닌 무슨 유적인거 같은데..?</size>";
                    }
                    if (now == 1)
                    {
                        text.text = "???\n<size=42>침입자...</size>";
                    }
                    if (now == 2)
                    {
                        text.text = "이소현\n<size=42>?... 누가 이런곳에 있는건가?</size>";
                    }
                    if (now == 3)
                    {
                        text.text = "???\n<size=42>왕의 안식을 방해하는 자는 살려 보내지 않겠다!</size>";
                    }
                    if (now == 4)
                    {
                        text.text = "이소현\n<size=42>이거.. 상황이 안좋게만 흘러가는거 같은데..?</size>";
                    }
                }
            }
            // 챕터 3
            else if (GameManager.instance.area_num == 3)
            {
                if (GameManager.instance.stage_num == 2)
                {
                    if (now == 0)
                    {
                        text.text = "이소현\n<size=42>갑자기 이상한 가면이 공격 해 올 줄이야...</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "이소현\n<size=42>그래도 그 가면이'별의 조각'을 가지고 있어서 다행이야</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "이소현\n<size=42>그나저나 신기하다... 바닷속인데 숨을 쉴수가있고 지상에서 걷는 것 처럼 움직일 수 가 있어.</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "이소현\n<size=42>이것도 마법의 힘이라 가능한 걸까?</size>";
                    }
                    else if (now == 4)
                    {
                        text.text = "이소현\n<size=42>일단은 어서 움직이자!</size>";
                    }
                }
                if (GameManager.instance.stage_num == 7)
                {
                    if (now == 0)
                    {
                        text.text = "이소현\n<size=42>생각보다 너무 금방 지치네... 좀만 쉬었다가 움직여야겠다.</size>";
                    }
                    if (now == 1)
                    {
                        text.text = "이소현\n<size=42>어라? 저 검은 물체는 뭐지? 굉장히 빠르게 움직이는거 같은데...</size>";
                    }
                    if (now == 2)
                    {
                        text.text = "이소현\n<size=42>뭐...뭐야! 왜 갑자기 이쪽으로 오는거야?!</size>";
                    }
                    if (now == 3)
                    {
                        text.text = "이소현\n<size=42>어...엄청 커다란 문어잖아!? 저게 바로 크라켄인건가???</size>";
                    }
                    if (now == 4)
                    {
                        text.text = "이소현\n<size=42>뭐지...? 갑자기 땅에서 미세하게 진동이 느껴지는데?</size>";
                    }
                    if (now == 5)
                    {
                        text.text = "이소현\n<size=42>바로 발 밑에서 무언가 올라오는 거 같은데? 피해야겠어!</size>";
                    }
                    if (now == 6)
                    {
                        text.text = "<size=42>(거대한 크라켄의 촉수가 땅을 뚫고 올라온다)</size>";
                    }
                    if (now == 7)
                    {
                        text.text = "이소현\n<size=42>크..큰일날뻔했다...</size>";
                    }
                    if (now == 8)
                    {
                        text.text = "이소현\n<size=42>이녀석을 처리하지 않으면 내가 먼저 당할거같은데...?</size>";
                    }
                }
            }
            // 챕터 4
            else if (GameManager.instance.area_num == 4)
            {
                if (GameManager.instance.stage_num == 2)
                {
                    if (now == 0)
                    {
                        text.text = "이소현\n<size=42>우와... 전의 사막보다 훨씬 뜨겁다...</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "이소현\n<size=42>그래도 아까 그 거대한 문어는 도대체 왜 갑자기 날 먼저 공격한걸까?</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "이소현\n<size=42>그래도 그녀석이 조각을 가지고 있어서 오히려 다행이지...</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "이소현\n<size=42>일단 이곳에 오래 있을수는 없겠다...</size>";
                    }
                    else if (now == 4)
                    {
                        text.text = "이소현\n<size=42>더 지체하지 말고 어서 빠르게 움직이자!</size>";
                    }
                }
                if (GameManager.instance.stage_num == 7)
                {
                    if (now == 0)
                    {
                        text.text = "이소현\n<size=42>헉...헉... 너무 뜨겁고 지친다...</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "이소현\n<size=42>저기 웬 석상같은게 있는데....</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "이소현\n<size=42>용암으로 된 석상이라니 신기하다...</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "이소현\n<size=42>어..어..? 이게 왜 움직이는거 같지?</size>";
                    }
                    else if (now == 4)
                    {
                        text.text = "이소현\n<size=42>정말로 움직이잖아! 바로 준비해야겠어!</size>";
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
                        text.text = "이소현\n<size=42>여지껏 만났던 모든 몬스터 중 가장 위험하고 강력했던거같아...</size>";
                    }
                    else if (now == 1)
                    {
                        text.text = "이소현\n<size=42>마지막 조각은 이녀석이 갖고있었을줄이야..</size>";
                    }
                    else if (now == 2)
                    {
                        text.text = "이소현\n<size=42>그래도 이제 모든 별의 조각을 모았어!</size>";
                    }
                    else if (now == 3)
                    {
                        text.text = "이소현\n<size=42>이젠 내 원래 세계로 돌아 갈 수 있게 되었으니...</size>";
                    }
                    else if (now == 4)
                    {
                        text.text = "이소현\n<size=42>별의 조각이여! 나의 바람은 내가 원래 있던 세계로 돌아가는 것이다!</size>";
                    }
                    else if (now == 5)
                    {
                        text.text = "(별의 조각을 하나로 합친 후 소원을 빌자, 별의 조각들에서 빛이 나기 시작한다.)";
                    }
                    else if (now == 6)
                    {
                        text.text = "(별의 조각에서 뿜어져 나오는 밝은 빛은 이소현을 감쌌고 이소현은 그 자리에서 사라졌다.)";
                    }
                    else if (now == 7)
                    {
                        text.text = "(별의 조각에서 나오는 빛이 사그라 들자 그 자리에 남겨진 별의 조각들은 다시 사방으로 흩어졌다. 별의 조각들은 언젠가 또 다시 누군가에 의해 모일 날을 기다릴것이다.)";
                    }
                    else if (now == 8)
                    {
                        text.text = "(END)\n생존 모드가 해금되었습니다.";
                        GameManager.instance.playerdata.story_Mod_Clear = true;
                    }
                    else if (now == 9)
                    {
                        gameObject.SetActive(false);
                        game_end.SetActive(true);
                        end_text.text = "GAME CLEAR!!!";
                        if (GameManager.instance.score < GameManager.instance.playerdata.max_Scroe)
                        {
                            score_text.text = "최종 점수 : " + GameManager.instance.score + "\n<size=24>최고 점수 : " + GameManager.instance.playerdata.max_Scroe + "</size>";
                        }
                        else if (GameManager.instance.score > GameManager.instance.playerdata.max_Scroe)
                        {
                            score_text.text = "최종 점수 : " + GameManager.instance.score + "\n신기록!";
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
