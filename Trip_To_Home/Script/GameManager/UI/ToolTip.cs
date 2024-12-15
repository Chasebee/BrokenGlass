using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    float halfwidth, halfheight;
    RectTransform rt;
    Camera camera;

    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        halfwidth = GetComponentInParent<CanvasScaler>().referenceResolution.x * 0.5f;
        halfheight = GetComponentInParent<CanvasScaler>().referenceResolution.y * 0.5f;
        rt = GetComponent<RectTransform>();
    }
    void Update()
    {
        Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y, 0);

        if (rt.anchoredPosition.x + rt.sizeDelta.x > halfwidth)
        {
            rt.pivot = new Vector2(1f, 0);

            if (rt.anchoredPosition.y + rt.sizeDelta.y > halfheight)
            {
                rt.pivot = new Vector2(1f, -1);
            }
        }
        else
        {
            rt.pivot = new Vector2(0f, 0);
            if (rt.anchoredPosition.y + rt.sizeDelta.y > halfheight)
            {
                rt.pivot = new Vector2(0f, 0);
            }
        }
    }
}
