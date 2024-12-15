using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public Image Achievements_img;
    public TextMeshProUGUI Achievements_text;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("Destroy_Object", 5.6f);
    }
    public void Destroy_Object() 
    {
        GameManager.instance.achievement_bool = false;
        Destroy(gameObject);
    }
}
