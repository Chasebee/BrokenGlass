using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Fade : MonoBehaviour
{
    public Image panel;
    public float time = 0f;
    public float f_time = 1f;

    public void Start()
    {
        StartCoroutine(Fade_Flow());
    }
    public void Skip()
    {
        Loading_Scene.LoadScene("Title");
        GameManager.instance.BGM_Play(GameManager.instance.bgm[0]);
    }

    public void Fades() 
    {
        StartCoroutine(Fade_Flow());
    }

    IEnumerator Fade_Flow() 
    {
        Color alpha = panel.color;
        while (alpha.a >0f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            panel.color = alpha;
            yield return null;
        }

        yield return new WaitForSeconds(6f);
        time = 0f;

        while (alpha.a < 1f) 
        {
        time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            panel.color = alpha;
            yield return null;
        }

        Loading_Scene.LoadScene("Title");
        GameManager.instance.BGM_Play(GameManager.instance.bgm[0]);
        yield return null;
    }
    
}
