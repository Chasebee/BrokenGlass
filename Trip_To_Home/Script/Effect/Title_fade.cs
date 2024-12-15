using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Title_fade : MonoBehaviour
{
    public Image panel;
    public float time = 0f;
    public float f_time = 1f;

    void Start()
    {
        StartCoroutine(Fade_Out());
    }

    // Update is called once per frame
    void Update()
    {
    }
    public IEnumerator Fade_Out() 
    {
        Color alpha = panel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / f_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            panel.color = alpha;
            yield return null;
        }
    }
}
