using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public Material[] bg_Material;
    private MeshRenderer render;
    private float offset;
    
    void Start()
    {
        // 2스테이지는 사용 안할 예졍 (피라미드 내부라 굳이 만들 이유가 없다
        render= GetComponent<MeshRenderer>();
        switch (GameManager.instance.area_num)
        {
            case 1:
                render.material = bg_Material[0];
                break;
            case 2:
                render.material = bg_Material[1];
                break;
            case 3:
                render.material = bg_Material[2];
                break;
            case 4:
                render.material = bg_Material[3];
                break;
        }
        // 챕터2 보스전 배경
        if (GameManager.instance.area_num == 2 && GameManager.instance.stage == 10) 
        {

            render.material = bg_Material[4];
        }
    }

    void Update()
    {
        if(GameManager.instance.area_num <= 2)
        offset += Time.deltaTime * speed;
        render.material.mainTextureOffset = new Vector2(offset, 0);        
    }
}
