using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    GameObject stm;
    Stage_Manager st;

    public GameObject effect_img;
    public string map_name;
    public int dir;
    public float[] char_start;

    // 포탈 효과 처리 관련
    public void Effect(string name)
    {
        GameManager.instance.portal_bool = true;
        GameManager.instance.chr_start[0] = char_start[0];
        GameManager.instance.chr_start[1] = char_start[1];
        effect_img.SetActive(true);
        effect_img.GetComponent<Animator>().SetInteger("num", dir);
        effect_img.GetComponent<Animator>().SetBool("confirm", true);
        effect_img.GetComponent<Portal>().map_name = name;
    }

    public void Next_Map() 
    {
        stm = GameObject.FindWithTag("Manager");
        st = stm.GetComponent<Stage_Manager>();

        GameManager.instance.day_color[0] = st.Object_Day.color.r;
        GameManager.instance.day_color[1] = st.Object_Day.color.g;
        GameManager.instance.day_color[2] = st.Object_Day.color.b;
        GameManager.instance.day_color[3] = st.Object_Day.color.a;

        Loading_Scene.LoadScene(map_name);
    }
}
