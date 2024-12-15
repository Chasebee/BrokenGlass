using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public GameObject player;
    public Transform pos;
    public float move_speed;
    public float alpha_Speed;
    TextMeshPro text;
    Color alpha;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pos = player.GetComponent<Transform>();
        gameObject.transform.localPosition = pos.localPosition;
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        Invoke("Text_Destory", 3f);
        
    }
    
    void Update()
    {
        transform.Translate(new Vector3(0, move_speed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alpha_Speed);
        text.color = alpha;
    }
    private void Text_Destory()
    {
        Destroy(gameObject);
    }
}
