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
                explane.text = "당신이 최고야\n플레이 해준 당신이 최고입니다! 정말 감사드립니다!";
                break;
            case 1:
                explane.text = "빨려들어간다!!\n여긴 도대체 어디일까요? 황금빛의 성기사가 반겨주지만 않는다면 좋겠네요....";
                break;
            case 2:
                explane.text = "냐냐냐옹 냐옹!\n굉장히 복슬복슬하며 붙임성 좋은 고양이 패밀리어 네로가 무엇을 말하고 싶은 걸까요?";
                break;
            case 3:
                explane.text = "으르렁 멍!\n활발하며 에너지 넘치는 강아지 패밀리어 몽몽이가 무엇을 말하고 싶은 걸까요?";
                break;
            case 4:
                explane.text = "복실복실한 친구들\n굉장히 복실복실한 여행길이 될 것만 같습니다! 외롭지도 않겠네요.";
                break;
            case 5:
                explane.text = "그래봤자 슬라임\n챕터 1 숲의 보스 거대 슬라임조차 당신의 상대가 되질 않는군요!";
                break;
            case 6:
                explane.text = "신출귀몰\n챕터 2 사막의 보스 저주받은 가면의 모든 공격을 버텨내 승리하셨습니다!";
                break;
            case 7:
                explane.text = "심해의 왕\n심해의 포악한 괴수 챕터 3의 보스 크라켄조차 당신을 쓰러트리지 못합니다! 당신이 심해의 군주입니다!";
                break;
            case 8:
                explane.text = "화산의 심장\n챕터 4의 강력한 용암 골렘도 당신을 쓰러트릴 수 없었습니다. 당신이 곧 화산의 심장입니다!";
                break;
            case 9:
                explane.text = "내 레이저는 모든걸 뚫는 레이저다!\n강력한 레이저로 뚫지 못하는건 아무것도 없습니다!";
                break;
            case 10:
                explane.text = "이거 방탄유리야!\n아직 한발 남은 적의 투사체를 조심하세요!";
                break;
            case 11:
                explane.text = "굉장히 HIGH한 기분!\n굉장히 빠른속도로 적을 공격 할 수 있게되었습니다! 기분이 굉장히 좋네요!";
                break;
            case 12:
                explane.text = "거대화!\n거대해졌습니다! 그게 답니다! 아마도요.";
                break;
            case 13:
                explane.text = "축소화!\n작아졌습니다! 그게 답니다! 아마도요.";
                break;
            case 14:
                explane.text = "예술?\n예술은 폭발입니다! 누군가가 그렇다고 하네요. 위험한데 말이죠.";
                break;
            case 15:
                explane.text = "이걸사네\n이걸사네. 어떻게 살아남으셨어요?";
                break;
            case 16:
                explane.text = "행동을 정지합니다.\n정지되었습니다! 움직일 수 가 없습니다!";
                break;
            case 17:
                explane.text = "좋은 거래였다.\n좋은 거래였습니다! 조금 비싼 기분인것 같지만요....";
                break;
            case 18:
                explane.text = "짤그랑!\n언제 들어도 기분 좋은 소리인거같습니다. 많이 들릴수록 좋을거 같네요.";
                break;
            case 19:
                explane.text = "사전준비\n뭐든지 미리 준비해두면 무서울게 없습니다!";
                break;
            case 20:
                explane.text = "리필해주세요\n보호의 장막으로도 당신을 보호하긴 어려워 보이네요.";
                break;
            case 21:
                explane.text = "멀티 슈터\n적을 처치하지 못했다면 탄알이 모자라지 않았는지 생각해보는건 어떨까요?";
                break;
            case 22:
                explane.text = "찾아라 비밀의 열쇠\n어떠신가요? 잘 즐겨보셨나요?";
                break;
            case 23:
                explane.text = "그것은 제 잔상입니다.\n너무 빨라서 눈에 들어오지가 않네요!";
                break;
            case 24:
                explane.text = "찌릿찌릿!\n전기세 걱정은 없을것 같네요.";
                break;
            case 25:
                explane.text = "이.. 이맛은!\n굉장히 잘 요리한 바베큐였습니다! 황홀한 맛이였다구요!";
                break;
            case 26:
                explane.text = "기초훈련Ⅰ\n기본이 가장 중요합니다.";
                break;
            case 27:
                explane.text = "기초훈련Ⅱ\n아직입니다. 아직 기본을 더 다듬어야합니다.";
                break;
            case 28:
                explane.text = "기초훈련Ⅲ\n잊지마세요! 기본이 중요합니다!";
                break;
            case 29:
                explane.text = "여기는 또 어디야?\n분명 제대로 돌아간것 같은데... 아닌것 같네요!";
                break;
        }
    }

    public void Clear_Requirements() 
    {
        switch (num)
        {
            case 0:
                clear_text.text = "달성 조건 : 게임을 처음 구동하기";
                break;
            case 1:
                clear_text.text = "달성 조건 :  일반모드로 게임을 시작하세요.";
                break;
            case 2:
                clear_text.text = "달성 조건 : 일반모드에서 '작은친구 네로' 아이템 옵션을 선택하기.";
                break;
            case 3:
                clear_text.text = "달성 조건 : 일반모드에서 '복실복실한 친구 몽몽' 아이템 옵션을 선택하기.";
                break;
            case 4:
                clear_text.text = "달성 조건 : 일반모드에서 '작은친구 네로', '복실복실한 친구 몽몽' 아이템을 동시에 보유하세요.";
                break;
            case 5:
                clear_text.text = "달성 조건 : 일반모드에서 챕터 1의 보스를 처치하세요.";
                break;
            case 6:
                clear_text.text = "달성 조건 : 일반모드에서 챕터 2의 보스를 처치하세요.";
                break;
            case 7:
                clear_text.text = "달성 조건 : 일반모드에서 챕터 3의 보스를 처치하세요.";
                break;
            case 8:
                clear_text.text = "달성 조건  일반모드에서: 챕터 4의 보스를 처치하세요.";
                break;
            case 9:
                clear_text.text = "달성 조건 : 일반모드에서 '라이트닝 샷' 옵션으로 한번에 적 3명을 공격하세요.";
                break;
            case 10:
                clear_text.text = "달성 조건 : 일반모드에서 '보호의 장막' 옵션으로 공격을 1회 무효화 하세요.";
                break;
            case 11:
                clear_text.text = "달성 조건 : 일반모드에서 공격속도를 최대수치로 달성하세요.";
                break;
            case 12:
                clear_text.text = "달성 조건 : 일반모드에서 캐릭터의 크기를 최대치로 달성하세요.";
                break;
            case 13:
                clear_text.text = "달성 조건 : 일반모드에서 캐릭터의 크기를 최소치로 달성하세요.";
                break;
            case 14:
                clear_text.text = "달성 조건 : 일반모드에서 '점착탄' 혹은 '마법부여 : 폭발마법'으로 생성된 폭발로\n한번에 적 3명을 공격하세요.";
                break;
            case 15:
                clear_text.text = "달성 조건 : 일반모드에서 남은 체력이 1이여야 합니다.";
                break;
            case 16:
                clear_text.text = "달성 조건 : 일반모드에서 기절 상태이상에 걸리세요.";
                break;
            case 17:
                clear_text.text = "달성 조건 : 일반모드에서 게임중 등장하는 방랑상인과 거래를 1회를 성사시키세요.";
                break;
            case 18:
                clear_text.text = "달성 조건 : 게임중 처음으로 코인을 주워보세요.";
                break;
            case 19:
                clear_text.text = "달성 조건 : 영구스텟 상점에서 아무 스텟이나 구매하세요.";
                break;
            case 20:
                clear_text.text = "달성 조건 : 일반모드의 한 스테이지에서 '보호의 장막' 효과를 10번 이상 발동시키세요.";
                break;
            case 21:
                clear_text.text = "달성 조건 : 일반모드에서 '더블 샷' 과 '트리플 샷' 아이템을 동시에 보유하세요.";
                break;
            case 22:
                clear_text.text = "달성 조건 : 특정 커맨드를 입력하세요.";
                break;
            case 23:
                clear_text.text = "달성 조건 : 일반모드에서 '마력질주' 옵션을 사용하세요.";
                break;
            case 24:
                clear_text.text = "달성 조건 : 일반모드에서 '붉은 전기의 위습' 과 '푸른 전기의 위습' 아이템을 동시에 보유하세요.";
                break;
            case 25:
                clear_text.text = "달성 조건 : 일반모드에서 액티브 아이템 '바베큐 파티!' 의 사용 효과로 요리된 바베큐를 먹어야합니다.";
                break;
            case 26:
                clear_text.text = "달성 조건 : 일반모드에서 일반공격 횟수를 총 10회 이상을 달성하세요.";
                break;
            case 27:
                clear_text.text = "달성 조건 : 일반모드에서 일반공격 횟수를 총 100회 이상을 달성하세요.";
                break;
            case 28:
                clear_text.text = "달성 조건 : 일반모드에서 일반공격 횟수를 총 1000회 이상을 달성하세요.";
                break;
            case 29:
                clear_text.text = "달성 조건 : 생존모드를 처음으로 플레이 하세요.";
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
